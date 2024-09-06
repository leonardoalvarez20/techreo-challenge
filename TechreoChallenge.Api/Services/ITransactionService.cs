using System;
using TechreoChallenge.Api.DTOs;

namespace TechreoChallenge.Api.Services;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDTOResponse>> GetTransactionsByAccountIdAsync(string accountId);
}
