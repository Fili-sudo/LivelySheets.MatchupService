using LivelySheets.MatchupService.Domain.Entities.Messages;
using LivelySheets.MatchupService.Infrastructure.Messaging;
using LivelySheets.MatchupService.Infrastructure.Messaging.Constants;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace LivelySheets.MatchupService.API.Consumers;

public class FindBattleMessageConsumer
{
    private readonly RabbitMqContextFactory rabbitMqContextFactory;

    public FindBattleMessageConsumer(RabbitMqContextFactory rabbitMqContextFactory)
    {
        this.rabbitMqContextFactory = rabbitMqContextFactory;
    }

    public async Task StartConsumingAsync(ConcurrentQueue<OutboxMessage> internalMessageQueue, CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var rabbitMqContext = await rabbitMqContextFactory.GenerateContext(stoppingToken);
            var (consumer, queueName) = await rabbitMqContext.SetupTopicConsumer(exchange: "topic_logs", routingKey: TopicRoutingKey.FindBattleRoutingKey, stoppingToken);

            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                var outboxMessage = JsonSerializer.Deserialize<OutboxMessage>(message);
                internalMessageQueue.Enqueue(outboxMessage);
                Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
                return Task.CompletedTask;
            };

            await rabbitMqContext.Channel!.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
