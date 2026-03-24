using LivelySheets.MatchupService.Application.Dtos;
using LivelySheets.MatchupService.Application.Interfaces;
using LivelySheets.MatchupService.Application.Queries;
using LivelySheets.MatchupService.Domain.Entities.Messages;
using MediatR;

namespace LivelySheets.MatchupService.Application.QueryHandlers;

public class GetInboxMessageByIdQueryHandler(IGenericRepository<InboxMessage> inboxMessageRepository)
    : IRequestHandler<GetInboxMessageByIdQuery, InboxMessageDto?>
{
    public async Task<InboxMessageDto?> Handle(GetInboxMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await inboxMessageRepository.GetByIdAsync(request.MessageId, cancellationToken);
        if (result is null)
            return null;

        return result;
    }
}
