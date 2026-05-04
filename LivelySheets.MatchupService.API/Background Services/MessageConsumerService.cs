
using LivelySheets.MatchupService.API.Constants;
using LivelySheets.MatchupService.API.Consumers;
using LivelySheets.MatchupService.Application.Generics;
using LivelySheets.MatchupService.Domain.Entities.Messages;
using System.Collections.Concurrent;
using System.Net;

namespace LivelySheets.MatchupService.API.Background_Services;

public class MessageConsumerService : BackgroundService
{
    private readonly ILogger<MessageConsumerService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly FindBattleMessageConsumer _consumer;
    private readonly ConcurrentQueue<OutboxMessage> internalMessageQueue;

    public MessageConsumerService(ILogger<MessageConsumerService> logger,
        IHttpClientFactory httpClientFactory,
        FindBattleMessageConsumer consumer)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _consumer = consumer;
        internalMessageQueue = new();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation(LoggingMessages.BackgroundServiceUnhandledErrorMessage, nameof(MessageConsumerService), DateTimeOffset.Now);
            Task consumeTask = _consumer.StartConsumingAsync(internalMessageQueue, stoppingToken);
            Task periodicTask = ProcessMessagesAsync(internalMessageQueue, periodInSeconds: 5, cancellationToken: stoppingToken);

            await Task.WhenAll(periodicTask, consumeTask);
        }
        catch (Exception ex)
        {
            _logger.LogError(LoggingMessages.BackgroundServiceUnhandledErrorMessage, nameof(MessageConsumerService), DateTimeOffset.Now);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

    }

    async Task ProcessMessagesAsync(ConcurrentQueue<OutboxMessage> internalMessageQueue, int periodInSeconds = 5, CancellationToken cancellationToken = default)
    {
        var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(periodInSeconds));
        while (await periodicTimer.WaitForNextTickAsync(cancellationToken))
        {
            _logger.LogInformation(LoggingMessages.PeriodicTaskTicked, DateTimeOffset.Now);
            var concurrentTaskCount = internalMessageQueue.Count > 3 ? 3 : internalMessageQueue.Count;
            await Task.WhenAll(Enumerable.Range(0, concurrentTaskCount)
                .Select(async t =>
                    {
                        if (internalMessageQueue.TryDequeue(out var outboxMessage))
                            return await CallbackActionAsync(outboxMessage, cancellationToken);

                        return Result<HttpStatusCode>.Failure([LoggingMessages.DequeueErrorMessage]);
                    }));
        }

        await Task.Delay(Timeout.Infinite, cancellationToken);
    }

    async Task<Result<HttpStatusCode>> CallbackActionAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(LoggingMessages.StartProcessMessage, outboxMessage.Id, DateTimeOffset.Now);
        try
        {
            HttpClient httpClient = _httpClientFactory.CreateClient(NamedHttpClients.CatalogServiceNamedHttpClient ?? "");
            var response = await httpClient.DeleteAsync($"{NamedHttpClients.CatalogServiceEndpoints.DeleteOutboxMessageEndpoint}/{outboxMessage.Id}", cancellationToken);
            _logger.LogInformation(LoggingMessages.FinishProcessMessage, outboxMessage.Id, DateTimeOffset.Now);

            if (response.IsSuccessStatusCode)
                return response.StatusCode;

            return new List<string>() { response.ReasonPhrase! };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return ex;
        }
    }
}
