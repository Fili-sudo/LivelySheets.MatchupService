namespace LivelySheets.MatchupService.Domain.Entities.Messages
{
    public class InboxMessage : Entity
    {
        public Guid OutboxMessageId { get; set; }
        public string Body { get; set; }
        public DateTimeOffset ReceivedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public int RetryCount { get; set; }
    }
}
