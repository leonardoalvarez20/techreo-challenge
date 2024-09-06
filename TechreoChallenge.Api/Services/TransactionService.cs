using AutoMapper;
using System;
using TechreoChallenge.Api.Data.Models;
using TechreoChallenge.Api.Data.Repositories;
using TechreoChallenge.Api.DTOs;

namespace TechreoChallenge.Api.Services;

public class TransactionService : ITransactionService
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
    {
        _mapper = mapper;
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<TransactionDTOResponse>> GetTransactionsByAccountIdAsync(string accountId)
    {
        List<Transaction> transactions = (List<Transaction>)await _transactionRepository.GetTransactionsByAccountIdAsync(accountId);
        return _mapper.Map<List<TransactionDTOResponse>>(transactions);
    }
}
