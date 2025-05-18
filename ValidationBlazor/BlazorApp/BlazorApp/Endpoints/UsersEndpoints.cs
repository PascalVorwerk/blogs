using Business.Dtos;
using Business.Services.Interfaces;
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
        group.MapGet("/email/{email:required}", GetUserByEmailAsync);
        group.MapPost("/", CreateUserAsync);
        
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
    
    private static async Task<IResult> GetUserByEmailAsync([FromRoute] string email, [FromServices] IUserService userService)
    {
        var user = await userService.GetUserByEmailAsync(email);
        return TypedResults.Ok(user);
    }
    
    private static async Task<IResult> CreateUserAsync([FromBody] CreateUserRequest request, [FromServices] IUserService userService)
    {
        var userId = await userService.CreateUserAsync(request);
        return TypedResults.CreatedAtRoute($"/api/users/{userId}", userId);
    }
}