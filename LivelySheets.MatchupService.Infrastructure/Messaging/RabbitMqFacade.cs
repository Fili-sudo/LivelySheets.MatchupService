using LivelySheets.MatchupService.Application.Interfaces;
using RabbitMQ.Client;

namespace LivelySheets.MatchupService.Infrastructure.Messaging;

public class RabbitMqFacade : IRabbitMqFacade, IAsyncDisposable
{
    private readonly ConnectionFactory _factory;

    private IConnection? _connection;
    private IChannel? _channel;
    private bool isSetup;

    public RabbitMqFacade()
    {
        _factory = new ConnectionFactory { HostName = "localhost" };
    }

    private async Task Setup()
    {
        if (isSetup)
            return;

        _connection = await _factory.CreateConnectionAsync();
        _channel = await _connection!.CreateChannelAsync();
        isSetup = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection is not null && _connection.IsOpen)
            await _connection.CloseAsync();

        isSetup = false;
        _connection = null;
        _channel = null;
    }
}
