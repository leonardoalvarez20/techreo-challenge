using MongoDB.Driver;
using System;
using TechreoChallenge.Api.Data.Enums;
using TechreoChallenge.Api.Data.Models;

namespace TechreoChallenge.Api.Data.Repositories;

public class SavingsAccountRepository : ISavingsAccountRepository
{
    private readonly IMongoCollection<SavingsAccount> _savingsAccount;
    private readonly FindOneAndUpdateOptions<SavingsAccount, SavingsAccount> _defaultFindOneAndUpdateOptions;


    public SavingsAccountRepository(IMongoDatabase database)
    {
        _savingsAccount = database.GetCollection<SavingsAccount>("SavingsAccount");
        _defaultFindOneAndUpdateOptions = new FindOneAndUpdateOptions<SavingsAccount, SavingsAccount>
        {
            ReturnDocument = ReturnDocument.After
        };
    }

    public async Task<SavingsAccount> AddSavingsAccountAsync(SavingsAccount savingsAccount)
    {
        await _savingsAccount.InsertOneAsync(savingsAccount);
        return savingsAccount;
    }

    public async Task<SavingsAccount> GetSavingsAccountByIdAsync(string savingsAccountId)
    {
        return await _savingsAccount.Find(savingsAccount => savingsAccount.Id == savingsAccountId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SavingsAccount>> GetSavingsAccountsByCustomerIdAsync(string customerId)
    {
        var result = await _savingsAccount.Find(savingsAccount => savingsAccount.CustomerId == customerId).ToListAsync();
        return result;
    }


    public async Task<SavingsAccount> UpdateSavingsAccountBalance(string savingsAccountId, decimal amount, TransactionType transactionType)
    {
        var filter = Builders<SavingsAccount>.Filter.Eq(SavingsAccount => SavingsAccount.Id, savingsAccountId);
        var update = Builders<SavingsAccount>.Update;
        UpdateDefinition<SavingsAccount> updateDefinition;
        updateDefinition = transactionType == TransactionType.Deposit ? update.Inc(a => a.Balance, amount) : update.Inc(a => a.Balance, -amount);
        return await _savingsAccount.FindOneAndUpdateAsync(
            filter,
            updateDefinition,
            options: _defaultFindOneAndUpdateOptions
        );
    }
}
