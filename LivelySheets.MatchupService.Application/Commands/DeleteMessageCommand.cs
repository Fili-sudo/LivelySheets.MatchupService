using MediatR;

namespace LivelySheets.MatchupService.Application.Commands;

public class DeleteMessageCommand : IRequest
{
    public Guid MessageId { get; set; }
}
