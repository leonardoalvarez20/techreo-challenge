using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TechreoChallenge.Api.Helpers;
using TechreoChallenge.Api.Services;

namespace TechreoChallenge.Api.Endpoints;

public static class TransactionsEndpoints
{
    public static void MapTransactionsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/transactions/{accountId}", [Authorize] async (HttpContext httpContext, string accountId, [FromServices] ITransactionService transactionService) =>
            {
                var (userId, email) = httpContext.GetUserInfo();
                if (userId == null || email == null)
                {
                    return Results.Unauthorized();
                }
                return await GetTransactions(accountId, transactionService);
            })
            .WithTags("Transactions");
    }

    private static async Task<IResult> GetTransactions(string accountId, ITransactionService transactionService)
    {
        var transactions = await transactionService.GetTransactionsByAccountIdAsync(accountId);
        return transactions != null ? Results.Ok(transactions) : Results.NoContent();
    }
}