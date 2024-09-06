using TechreoChallenge.Api.Data.Enums;
using TechreoChallenge.Api.Data.Models;

public interface ISavingsAccountRepository
{
    Task<SavingsAccount> AddSavingsAccountAsync(SavingsAccount savingsAccount);
    Task<SavingsAccount> GetSavingsAccountByIdAsync(string savingsAccountId);
    Task<IEnumerable<SavingsAccount>> GetSavingsAccountsByCustomerIdAsync(string customerId);
    Task<SavingsAccount> UpdateSavingsAccountBalance(string savingsAccountId, decimal amount, TransactionType transactionType);
}