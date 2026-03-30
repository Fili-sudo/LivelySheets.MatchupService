using LivelySheets.MatchupService.Application.Interfaces;

namespace LivelySheets.MatchupService.API.HttpClients
{
    public class CatalogServiceClient(HttpClient httpClient) : ICatalogServiceClient
    {
        private readonly string DeleteOutboxMessageEndpoint = "messages/purge-message";

        public async Task<HttpResponseMessage> DeleteOutboxMessageAsync(Guid messageId)
        {
            var response = await httpClient.DeleteAsync($"{DeleteOutboxMessageEndpoint}/{messageId}");
            return response;
        }
    }
}
