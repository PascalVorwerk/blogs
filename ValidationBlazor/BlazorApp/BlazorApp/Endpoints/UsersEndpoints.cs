using BlazorApp.Filters;
using Business.Dtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/users")
            .WithTags("Users");

        group.MapGet("/", GetUsersAsync);
        group.MapGet("/{userId:guid}", GetUserByIdAsync);
        group.MapGet("/email/{email:required}", GetUserByEmail);
        group.MapPost("/", CreateUserAsync)
            .AddEndpointFilter<ValidationFilter<CreateUserRequest>>();
        
        return group;
    }
    
    private static async Task<IResult> GetUsersAsync([FromServices] IUserService userService)
    {
        var users = await userService.GetUsersAsync();
        return TypedResults.Ok(users);
    }
    
    private static async Task<IResult> GetUserByIdAsync([FromRoute] Guid userId, [FromServices] IUserService userService)
    {
        var user = await userService.GetUserAsync(userId);
        return TypedResults.Ok(user);
    }
    
    private static Ok<GetUserResponse> GetUserByEmail([FromRoute] string email, [FromServices] IUserService userService)
    {
        var user = userService.GetUserByEmail(email);
        return TypedResults.Ok(user);
    }
    
    private static async Task<Ok<Guid>> CreateUserAsync([FromBody] CreateUserRequest request, [FromServices] IUserService userService)
    {
        var userId = await userService.CreateUserAsync(request);
        return TypedResults.Ok(userId);
    }
}