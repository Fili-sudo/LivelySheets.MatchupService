using LivelySheets.MatchupService.Infrastructure.Messaging;
using LivelySheets.MatchupService.Infrastructure.Messaging.Constants;

namespace LivelySheets.MatchupService.API.Consumers;

public class FindBattleMessageConsumer
{
    private readonly RabbitMqContextFactory rabbitMqContextFactory;

    public FindBattleMessageConsumer(RabbitMqContextFactory rabbitMqContextFactory)
    {
        this.rabbitMqContextFactory = rabbitMqContextFactory;
    }

    public async Task StartConsumingAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using var rabbitMqContext = await rabbitMqContextFactory.GenerateContext(stoppingToken);
            var (consumer, queueName) = await rabbitMqContext.SetupTopicConsumer(exchange: "topic_logs", routingKey: TopicRoutingKey.FindBattleRoutingKey, stoppingToken);


            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
