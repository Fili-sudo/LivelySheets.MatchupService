using LivelySheets.MatchupService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LivelySheets.MatchupService.API.Endpoints.InboxMessage;

public class DeleteMessage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("messages/{messageId}",
                async ([FromRoute] Guid messageId,
                [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new DeleteMessageCommand { MessageId = messageId });
                    return Results.NoContent();
                }
            );
    }
}
