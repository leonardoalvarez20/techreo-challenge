using MongoDB.Driver;
using System;
using TechreoChallenge.Api.Data.Models;


namespace TechreoChallenge.Api.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly IMongoCollection<Transaction> _transactions;

    public TransactionRepository(IMongoDatabase database)
    {
        _transactions = database.GetCollection<Transaction>("Transactions");
    }

    public async Task<Transaction> AddTransactionAsync(Transaction transaction)
    {
        await _transactions.InsertOneAsync(transaction);
        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(string accountId)
    {
        var filter = Builders<Transaction>.Filter.Eq(t => t.AccountId, accountId);
        return await _transactions.Find(filter).ToListAsync();
    }
}
