using Microsoft.AspNetCore.Mvc;
using System;
using TechreoChallenge.Api.Services;
using TechreoChallenge.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using TechreoChallenge.Api.Helpers;

namespace TechreoChallenge.Api.Endpoints;

public static class SavingsAccounts
{
    public static void MapSavingsAccountsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/savings-account", [Authorize] async (HttpContext httpContext, [FromBody] SavingsAccountDTORequest savingsAccountDTORequest, [FromServices] ISavingsAccountService savingsAccountService) =>
        {
            return await HandleSavingsAccountOperation(httpContext, savingsAccountService, userId => AddSavingsAccountAsync(userId, savingsAccountDTORequest, savingsAccountService));
        })
        .WithTags("Savings Accounts")
        .Produces<SavingsAccountDTOResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        app.MapGet("/savings-account", [Authorize] async (HttpContext httpContext, [FromServices] ISavingsAccountService savingsAccountService) =>
        {
            return await HandleSavingsAccountOperation(httpContext, savingsAccountService, userId => GetSavingsAccountsByCustomerIdAsync(userId, savingsAccountService));
        })
        .WithTags("Savings Accounts")
        .Produces<SavingsAccountDTOResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);

        app.MapPost("/savings-account/{accountId}/deposit", [Authorize] async (HttpContext httpContext, string accountId, [FromBody] TransactionDTORequest transactionDTORequest, [FromServices] ISavingsAccountService savingsAccountService) =>
        {
            return await HandleSavingsAccountOperation(httpContext, savingsAccountService, userId => DepositSavingsAccountAsync(userId, accountId, transactionDTORequest, savingsAccountService));
        })
        .WithTags("Savings Accounts")
        .Produces<SavingsAccountDTOResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        app.MapPost("/savings-account/{accountId}/withdraw", [Authorize] async (HttpContext httpContext, string accountId, [FromBody] TransactionDTORequest transactionDTORequest, [FromServices] ISavingsAccountService savingsAccountService) =>
        {
            return await HandleSavingsAccountOperation(httpContext, savingsAccountService, userId => WithdrawSavingsAccountAsync(userId, accountId, transactionDTORequest, savingsAccountService));
        })
        .WithTags("Savings Accounts")
        .Produces<SavingsAccountDTOResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> AddSavingsAccountAsync(string customerId, SavingsAccountDTORequest savingsAccountDTORequest, ISavingsAccountService savingsAccountService)
    {
        var savingsAccount = await savingsAccountService.CreateSavingsAccountAsync(customerId, savingsAccountDTORequest);
        return savingsAccount != null ? Results.Ok(savingsAccount) : Results.NotFound();
    }

    private static async Task<IResult> GetSavingsAccountsByCustomerIdAsync(string customerId, ISavingsAccountService savingsAccountService)
    {
        var savingsAccounts = await savingsAccountService.GetSavingsAccountsByCustomerIdAsync(customerId);
        return savingsAccounts != null ? Results.Ok(savingsAccounts) : Results.NoContent();
    }

    private static async Task<IResult> DepositSavingsAccountAsync(string customerId, string accountId, TransactionDTORequest transactionDTORequest, ISavingsAccountService savingsAccountService)
    {
        var savingsAccount = await savingsAccountService.DepositAsync(customerId, accountId, transactionDTORequest);
        return savingsAccount != null ? Results.Ok(savingsAccount) : Results.NotFound();
    }

    private static async Task<IResult> WithdrawSavingsAccountAsync(string customerId, string accountId, TransactionDTORequest transactionDTORequest, ISavingsAccountService savingsAccountService)
    {
        var savingsAccount = await savingsAccountService.WithdrawAsync(customerId, accountId, transactionDTORequest);
        return savingsAccount != null ? Results.Ok(savingsAccount) : Results.NotFound();
    }

    private static async Task<IResult> HandleSavingsAccountOperation(
    HttpContext httpContext,
    ISavingsAccountService savingsAccountService,
    Func<string, Task<IResult>> operationMethod)
    {
        var (userId, email) = httpContext.GetUserInfo();
        if (userId == null || email == null)
        {
            return Results.Unauthorized();
        }

        return await operationMethod(userId);
    }
}
