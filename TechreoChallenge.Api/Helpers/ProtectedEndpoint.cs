using System;
using Microsoft.OpenApi.Models;
namespace TechreoChallenge.Api.Helpers;

public static class ProtectedEndpoint
{
    public static OpenApiOperation ProtectEndpoint(OpenApiOperation operation)
    {
        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            }
        };
        return operation;
    }
}
