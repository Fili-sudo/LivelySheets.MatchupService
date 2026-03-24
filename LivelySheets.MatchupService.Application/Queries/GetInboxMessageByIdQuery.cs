using LivelySheets.MatchupService.Application.Dtos;
using MediatR;

namespace LivelySheets.MatchupService.Application.Queries;

public class GetInboxMessageByIdQuery : IRequest<InboxMessageDto?>
{
    public Guid MessageId { get; set; }
}
