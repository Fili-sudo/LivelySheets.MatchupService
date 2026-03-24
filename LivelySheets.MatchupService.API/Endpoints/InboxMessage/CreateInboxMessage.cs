using LivelySheets.MatchupService.API.Contracts.Requests;
using LivelySheets.MatchupService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LivelySheets.MatchupService.API.Endpoints.InboxMessage
{
    public class CreateInboxMessage : IEndpoint
    {
        internal static readonly string CreateInboxMessageEndpoint = nameof(CreateInboxMessageEndpoint);
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("messages/create-message", async (HttpContext context,
                LinkGenerator linkGenerator,
                [FromBody] PostCreateInboxMessageDto body,
                [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send((CreateInboxMessageCommand)body);
                var messageLink = linkGenerator.GetUriByName(
                        context, GetInboxMessage.GetInboxMessageEndpoint,
                        new { messageId = result }
                    );

                return Results.Created(messageLink, result);
            })
            .WithName(CreateInboxMessageEndpoint);
        }
    }
}
