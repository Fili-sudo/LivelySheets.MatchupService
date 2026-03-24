using LivelySheets.MatchupService.Application.Commands;

namespace LivelySheets.MatchupService.API.Contracts.Requests;

public class PostCreateInboxMessageDto
{
    public Guid OutboxMessageId { get; set; }
    public string Body { get; set; }
    public int RetryCount { get; set; }

    public static explicit operator CreateInboxMessageCommand(PostCreateInboxMessageDto p) =>
            new()
            {
                OutboxMessageId = p.OutboxMessageId,
                Body = p.Body,
                RetryCount = p.RetryCount,
            };
}
