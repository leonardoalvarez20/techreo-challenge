using Microsoft.AspNetCore.Mvc;
using System;
using TechreoChallenge.Api.Services;
using TechreoChallenge.Api.DTOs;

namespace TechreoChallenge.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", Login).WithTags("Login");
    }

    private static async Task<IResult> Login(LoginDTORequest loginDTORequest, [FromServices] IAuthenticationService authenticationService)
    {
        var login = await authenticationService.Login(loginDTORequest);
        return login != null ? Results.Ok(login) : Results.Unauthorized();
    }
}
