using AutoMapper;
using MongoDB.Bson;
using System;
using TechreoChallenge.Api.Data.Enums;
using TechreoChallenge.Api.Data.Models;
using TechreoChallenge.Api.Data.Repositories;
using TechreoChallenge.Api.DTOs;
using TechreoChallenge.Api.Exceptions;

namespace TechreoChallenge.Api.Services;

public class SavingsAccountService : ISavingsAccountService
{
    private readonly IMapper _mapper;
    private readonly ISavingsAccountRepository _savingsAccountRepository;
    private readonly ITransactionRepository _transactionRepository;
    public SavingsAccountService(IMapper mapper, ISavingsAccountRepository savingsAccountRepository, ITransactionRepository transactionRepository)
    {
        _mapper = mapper;
        _savingsAccountRepository = savingsAccountRepository;
        _transactionRepository = transactionRepository;
    }
    public async Task<SavingsAccountDTOResponse> CreateSavingsAccountAsync(string customerId, SavingsAccountDTORequest savingsAccountDTORequest)
    {
        SavingsAccount savingsAccount = GenerateSavingsAccount(customerId, savingsAccountDTORequest);
        var account = await _savingsAccountRepository.AddSavingsAccountAsync(savingsAccount);
        return _mapper.Map<SavingsAccountDTOResponse>(account);
    }

    public async Task<TransactionDTOResponse> DepositAsync(string customerId, string accountId, TransactionDTORequest transactionDTORequest)
    {
        var transaction = await CreateTransactionAsync(accountId, transactionDTORequest, TransactionType.Deposit);
        var account = await _savingsAccountRepository.UpdateSavingsAccountBalance(accountId, transaction.Amount, TransactionType.Deposit);
        return GetTransactionDTOResponse(transaction, account);
    }

    public async Task<TransactionDTOResponse> WithdrawAsync(string customerId, string accountId, TransactionDTORequest transactionDTORequest)
    {
        var transaction = await CreateTransactionAsync(accountId, transactionDTORequest, TransactionType.Withdrawal);
        var account = await _savingsAccountRepository.UpdateSavingsAccountBalance(accountId, transaction.Amount, TransactionType.Withdrawal);
        return GetTransactionDTOResponse(transaction, account);
    }

    public async Task<Transaction> CreateTransactionAsync(string accountId, TransactionDTORequest transactionDTORequest, TransactionType transactionType)
    {
        var account = await _savingsAccountRepository.GetSavingsAccountByIdAsync(accountId);
        if (account == null) throw new NotFoundException($"The account {accountId} doesn't exist.");
        if (transactionType == TransactionType.Withdrawal && (account.Balance == 0 || (account.Balance - transactionDTORequest.Amount) < 0))
        {
            throw new BadRequestException($"The account {accountId} doesn't have enough funds.");
        }
        return await _transactionRepository.AddTransactionAsync(new Transaction
        {
            Id = ObjectId.GenerateNewId().ToString(),
            AccountId = accountId,
            Amount = transactionDTORequest.Amount,
            Date = DateTime.UtcNow,
            TransactionType = transactionType,
            Description = transactionDTORequest.Description
        });
    }

    private SavingsAccount GenerateSavingsAccount(string customerId, SavingsAccountDTORequest savingsAccountDTORequest)
    {
        DateTime dateTimeNow = DateTime.UtcNow;
        return new SavingsAccount()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            CustomerId = customerId,
            Alias = savingsAccountDTORequest.Alias,
            AccountNumber = GenerateRandomAccount(),
            CLABE = GenerateRandomClabe(),
            Balance = savingsAccountDTORequest.InitialBalance,
            CreatedAt = dateTimeNow,
            UpdatedAt = dateTimeNow
        };
    }
    private string GenerateRandomAccount() => new Random().Next(10000000, 99999999).ToString();

    private string GenerateRandomClabe() => string.Concat(Enumerable.Range(0, 18)
            .Select(_ => new Random().Next(0, 10).ToString()));

    private TransactionDTOResponse GetTransactionDTOResponse(Transaction transaction, SavingsAccount savingsAccount) =>
    new TransactionDTOResponse()
    {
        Id = transaction.Id,
        Description = transaction.Description,
        Amount = transaction.Amount,
        SavingsAccountId = savingsAccount.Id,
        SavingsAccountNewBalance = savingsAccount.Balance,
        TransactionType = transaction.TransactionType
    };

    public async Task<IEnumerable<SavingsAccountDTOResponse>> GetSavingsAccountsByCustomerIdAsync(string customerId)
    {
        List<SavingsAccount> accounts = (List<SavingsAccount>)await _savingsAccountRepository.GetSavingsAccountsByCustomerIdAsync(customerId);
        return _mapper.Map<List<SavingsAccountDTOResponse>>(accounts);
    }
}
