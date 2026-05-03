namespace LivelySheets.MatchupService.API.Constants;

public static class NamedHttpClients
{
    public static readonly string CatalogServiceNamedHttpClient = nameof(CatalogServiceNamedHttpClient);

    public static class CatalogServiceEndpoints
    {
        public static readonly string DeleteOutboxMessageEndpoint = "messages/purge-message";
    }
}
