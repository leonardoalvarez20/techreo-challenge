using System;
using TechreoChallenge.Api.Data.Enums;
using TechreoChallenge.Api.Data.Models;
using TechreoChallenge.Api.DTOs;

namespace TechreoChallenge.Api.Services;

public interface ISavingsAccountService
{
    Task<SavingsAccountDTOResponse> CreateSavingsAccountAsync(string customerId, SavingsAccountDTORequest savingsAccountDTORequest);
    Task<IEnumerable<SavingsAccountDTOResponse>> GetSavingsAccountsByCustomerIdAsync(string customerId);
    Task<TransactionDTOResponse> DepositAsync(string customerId, string accountId, TransactionDTORequest transactionDTORequest);
    Task<TransactionDTOResponse> WithdrawAsync(string customerId, string accountId, TransactionDTORequest transactionDTORequest);
    Task<Transaction> CreateTransactionAsync(string accountId, TransactionDTORequest transactionDTORequest, TransactionType transactionType);
}
