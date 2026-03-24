
using LivelySheets.MatchupService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LivelySheets.MatchupService.API.Endpoints.InboxMessage
{
    public class GetInboxMessage : IEndpoint
    {
        public static readonly string GetInboxMessageEndpoint = nameof(GetInboxMessageEndpoint);
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("messages/{messageId}",
                async ([FromRoute] Guid messageId,
                [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetInboxMessageByIdQuery { MessageId = messageId });
                    return Results.Ok(result);
                }
            ).WithName(GetInboxMessageEndpoint);
        }
    }
}
