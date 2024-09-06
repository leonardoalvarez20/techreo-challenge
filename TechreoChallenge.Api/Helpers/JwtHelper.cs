using System;
using System.Security.Claims;

namespace TechreoChallenge.Api.Helpers;

public static class JwtHelper
{
    public static (string? userId, string? email) GetUserInfo(this HttpContext httpContext)
    {
        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = httpContext.User.FindFirstValue(ClaimTypes.Name);
        return (userId, email);
    }
}
