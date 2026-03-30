using LivelySheets.MatchupService.Application.Interfaces;

namespace LivelySheets.MatchupService.API.HttpClients
{
    public class CatalogServiceClient(HttpClient httpClient) : ICatalogServiceClient
    {
        private readonly string CreateInboxMessageEndpoint = "messages";

        public async Task<HttpResponseMessage> DeleteOutboxMessageAsync(Guid messageId)
        {
            var response = await httpClient.DeleteAsync($"{CreateInboxMessageEndpoint}/{messageId}");
            return response;
        }
    }
}
