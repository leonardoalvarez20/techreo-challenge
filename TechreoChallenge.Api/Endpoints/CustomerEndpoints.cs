using System;
using TechreoChallenge.Services;
using TechreoChallenge.Api.DTOs;
using TechreoChallenge.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TechreoChallenge.Api.Extensions;

namespace TechreoChallenge.Endpoints;


public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customers").WithTags("Customers");
        group.MapGet("/{id}", [Authorize] (string id) => GetCustomerById)
            .WithOpenApi(operation => operation.AddJwtBearerSecurityToOperation())
            .Produces<CustomerDTOResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        group.MapGet("", [Authorize] () => GetAllCustomers)
            .WithOpenApi(operation => operation.AddJwtBearerSecurityToOperation())
            .Produces<IEnumerable<CustomerDTOResponse>>(StatusCodes.Status200OK);
        group.MapPost("", CreateCustomer)
            .AllowAnonymous()
            .Produces<CustomerDTOResponse>(StatusCodes.Status200OK);
        group.MapPut("/{id}", [Authorize] (string id, CustomerDTORequest customerDTORequest) => UpdateCustomer)
            .WithOpenApi(operation => operation.AddJwtBearerSecurityToOperation())
            .Produces<bool>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        group.MapDelete("/{id}", [Authorize] (string id) => DeleteCustomer)
            .WithOpenApi(operation => operation.AddJwtBearerSecurityToOperation())
            .Produces<bool>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetCustomerById(string id, [FromServices] ICustomerService customerService)
    {
        var customer = await customerService.GetCustomerByIdAsync(id);
        return customer is not null ? Results.Ok(customer) : Results.NotFound();
    }

    private static async Task<IResult> GetAllCustomers(int skip, int limit, [FromServices] ICustomerService customerService)
    {
        var customers = await customerService.GetCustomersAsync(skip, limit);
        return customers is not null ? Results.Ok(customers) : Results.NotFound();
    }

    private static async Task<IResult> CreateCustomer([FromBody] CustomerDTORequest customerDTORequest, [FromServices] ICustomerService customerService)
    {
        var customerId = await customerService.CreateCustomerAsync(customerDTORequest);
        return Results.Created($"/customers/{customerId}", customerId);
    }

    private static async Task<IResult> UpdateCustomer(string id, [FromBody] CustomerDTORequest customerDTORequest, [FromServices] ICustomerService customerService)
    {
        var success = await customerService.UpdateCustomerAsync(id, customerDTORequest);
        return success ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> DeleteCustomer(string id, [FromServices] ICustomerService customerService)
    {
        var success = await customerService.DeleteCustomerAsync(id);
        return success ? Results.NoContent() : Results.NotFound();
    }
}
