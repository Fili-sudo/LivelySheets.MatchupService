namespace LivelySheets.MatchupService.Domain.Entities.Messages;

public class OutboxMessage : Entity
{
    public string Body { get; set; }
    public DateTimeOffset SentOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public Guid? InboxMessageId { get; set; }
    public int RetryCount { get; set; }
}
