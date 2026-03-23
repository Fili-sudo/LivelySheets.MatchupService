namespace LivelySheets.MatchupService.API.Contracts.Requests;

public class PostCreateInboxMessageDto
{
    public Guid OutboxMessageId { get; set; }
    public string Body { get; set; }
    public int RetryCount { get; set; }
}
