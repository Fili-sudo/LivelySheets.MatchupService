using LivelySheets.MatchupService.Application.Commands;
using LivelySheets.MatchupService.Application.Interfaces;
using LivelySheets.MatchupService.Domain.Entities.Messages;
using MediatR;

namespace LivelySheets.MatchupService.Application.CommandHandlers;

public class CreateInboxMessageCommandHandler(IGenericRepository<InboxMessage> inboxMessageRepository) 
    : IRequestHandler<CreateInboxMessageCommand, Guid>
{
    public async Task<Guid> Handle(CreateInboxMessageCommand request, CancellationToken cancellationToken)
    {
        var inboxMessage = new InboxMessage
        {
            OutboxMessageId = request.OutboxMessageId,
            Body = request.Body,
            ReceivedOn = DateTimeOffset.Now,
            RetryCount = request.RetryCount,
        };

        await inboxMessageRepository.AddAsync(inboxMessage, cancellationToken);

        return inboxMessage.Id;
    }
}
