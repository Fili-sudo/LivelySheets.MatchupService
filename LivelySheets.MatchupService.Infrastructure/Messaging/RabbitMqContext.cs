using RabbitMQ.Client;

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

        Connection = await _factory.CreateConnectionAsync();
        Channel = await Connection!.CreateChannelAsync();
        isSetup = true;
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
