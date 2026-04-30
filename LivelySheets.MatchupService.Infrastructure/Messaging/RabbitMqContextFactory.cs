namespace LivelySheets.MatchupService.Infrastructure.Messaging;

public class RabbitMqContextFactory
{
    public async Task<RabbitMqContext> GenerateContext(CancellationToken cancellationToken = default)
    {
        var context = new RabbitMqContext();
        await context.SetupAsync(cancellationToken);
        return context;
    }
}
