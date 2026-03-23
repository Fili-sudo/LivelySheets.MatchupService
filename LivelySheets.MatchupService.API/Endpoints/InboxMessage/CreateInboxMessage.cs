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
                [FromServices] IMediator mediator) =>
            {
                return Results.NoContent();
            })
            .WithName(CreateInboxMessageEndpoint);
        }
    }
}
