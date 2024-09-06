using System;
using TechreoChallenge.Api.Data.Models;

namespace TechreoChallenge.Api.Data.Repositories;

public interface ITransactionRepository
{
    Task<Transaction> AddTransactionAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(string accountId);
}
