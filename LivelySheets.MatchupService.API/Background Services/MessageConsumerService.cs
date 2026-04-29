
namespace LivelySheets.MatchupService.API.Background_Services;

public class MessageConsumerService : BackgroundService
{
    private readonly ILogger<MessageConsumerService> _logger;

    public MessageConsumerService(ILogger<MessageConsumerService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"MessageConsumerService started at {DateTimeOffset.Now}");
            using var timer = new Timer(CallbackAction, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }

    void CallbackAction(object? state)
    {
        var currentTme = DateTime.Now;
        _logger.LogInformation($"Message consumed at {currentTme}");
    }
}
