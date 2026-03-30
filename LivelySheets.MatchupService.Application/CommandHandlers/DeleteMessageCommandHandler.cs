using LivelySheets.MatchupService.Application.Commands;
using LivelySheets.MatchupService.Application.Interfaces;
using LivelySheets.MatchupService.Domain.Entities.Messages;
using MediatR;

namespace LivelySheets.MatchupService.Application.CommandHandlers;

internal class DeleteMessageCommandHandler(IGenericRepository<InboxMessage> inboxMessageRepository)
    : IRequestHandler<DeleteMessageCommand>
{
    public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        await inboxMessageRepository.DeleteAsync(request.MessageId, cancellationToken);
    }
}
