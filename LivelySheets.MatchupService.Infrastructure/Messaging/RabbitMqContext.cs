using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LivelySheets.MatchupService.Infrastructure.Messaging;

public class RabbitMqContext : IAsyncDisposable
{
    private readonly ConnectionFactory _factory;

    public IConnection? Connection { get; private set; }
    public IChannel? Channel { get; private set; }
    private bool isSetup;

    public RabbitMqContext()
    {
        _factory = new ConnectionFactory { HostName = "localhost" };
    }

    public async Task SetupAsync(CancellationToken cancellationToken = default)
    {
        if (isSetup)
            return;

        Connection = await _factory.CreateConnectionAsync(cancellationToken);
        Channel = await Connection!.CreateChannelAsync(cancellationToken: cancellationToken);
        isSetup = true;
    }

    public async Task<(AsyncEventingBasicConsumer Consumer, string QueueName)> SetupTopicConsumer(string exchange, string routingKey, CancellationToken cancellationToken = default)
    {
        if (!isSetup)
            throw new InvalidOperationException($"Cannot Invoke this method without a prior setup. {nameof(isSetup)}: {isSetup}");

        await Channel!.ExchangeDeclareAsync(exchange: exchange, type: ExchangeType.Topic, cancellationToken: cancellationToken);
        QueueDeclareOk queueDeclareResult = await Channel!.QueueDeclareAsync(cancellationToken: cancellationToken);
        await Channel!.QueueBindAsync(
            queue: queueDeclareResult.QueueName,
            exchange: exchange,
            routingKey: routingKey,
            cancellationToken: cancellationToken);

        return (new AsyncEventingBasicConsumer(Channel), queueDeclareResult.QueueName);
    }

    public async ValueTask DisposeAsync()
    {
        if (Connection is not null && Connection.IsOpen)
            await Connection.CloseAsync();

        isSetup = false;
        Connection = null;
        Channel = null;
    }
}
