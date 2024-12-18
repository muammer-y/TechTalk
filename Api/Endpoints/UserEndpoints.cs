using Api.Endpoints.Abstract;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Api.Endpoints;

public class UserEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var userGroup = builder.MapGroup("v1/users")
            .WithTags("Users");

        userGroup.MapGet("/", async (IMediator mediator) =>
        {
            var response = await mediator.Send(new GetUsersQuery());

            return response;
        })
        .WithMetadata(new AuthorizeAttribute())
        .RequireAuthorization();

        userGroup.MapPost("/", async (IMediator mediator, CreateUserCommand request) =>
        {
            var response = await mediator.Send(request);
            if (response.IsFailure)
            {
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        });

        userGroup.MapPatch("/{id:int}", async (IMediator mediator, int id, UpdateUserCommand request) =>
        {
            var response = await mediator.Send(request with { Id = id});
            if (!response.IsFailure)
            {
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        });

        userGroup.MapPost("/login", async (IMediator mediator, LoginCommand request) =>
        {
            var response = await mediator.Send(request);
            if (response.IsFailure)
            {
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        });

        userGroup.MapDelete("/{id:int}", async (IMediator mediator, int id) =>
        {
            var response = await mediator.Send(new DeleteUserCommand(id));
            if(response.IsFailure)
            {
                return Results.BadRequest(response);
            }

            return Results.Ok(response);
        });
    }
}
