namespace LivelySheets.MatchupService.Application.Interfaces;

public interface ICatalogServiceClient
{
    Task<HttpResponseMessage> DeleteOutboxMessageAsync(Guid messageId);
}
