using MediatR;

namespace LivelySheets.MatchupService.Application.Commands;

public class CreateInboxMessageCommand : IRequest<Guid>
{
    public Guid OutboxMessageId { get; set; }
    public string Body { get; set; }
    public int RetryCount { get; set; }
}
