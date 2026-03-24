using LivelySheets.MatchupService.Domain.Entities.Messages;

namespace LivelySheets.MatchupService.Application.Dtos;

public class InboxMessageDto
{
    public Guid OutboxMessageId { get; set; }
    public string Body { get; set; }
    public DateTimeOffset ReceivedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int RetryCount { get; set; }

    public static implicit operator InboxMessageDto(InboxMessage inboxMessage) =>
        new()
        {
            OutboxMessageId = inboxMessage.OutboxMessageId,
            Body = inboxMessage.Body,
            ReceivedOn = inboxMessage.ReceivedOn,
            UpdatedOn = inboxMessage.UpdatedOn,
            RetryCount = inboxMessage.RetryCount,
        };
}
