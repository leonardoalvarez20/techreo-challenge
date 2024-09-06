using System;
using TechreoChallenge.Data.Repositories;
using TechreoChallenge.Api.Data.Models;
using TechreoChallenge.Api.DTOs;
using TechreoChallenge.Api.Helpers;
using AutoMapper;

namespace TechreoChallenge.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDTOResponse> CreateCustomerAsync(CustomerDTORequest customerDTORequest)
    {
        DateTime dateTimeNow = DateTime.UtcNow;
        Customer customer = _mapper.Map<Customer>(customerDTORequest);
        customer.IsActive = true;
        customer.Password = PasswordHasher.HashPassword(customer.Password);
        customer.CreatedAt = dateTimeNow;
        customer.UpdatedAt = dateTimeNow;
        return _mapper.Map<CustomerDTOResponse>(await _customerRepository.AddAsync(customer));
    }

    public async Task<bool> DeleteCustomerAsync(string id)
    {
        var result = await _customerRepository.DeleteAsync(id);
        return result.IsDeleted;
    }

    public async Task<CustomerDTOResponse> GetCustomerByIdAsync(string id)
    {
        return _mapper.Map<CustomerDTOResponse>(await _customerRepository.GetByIdAsync(id));
    }

    public async Task<IEnumerable<CustomerDTOResponse>> GetCustomersAsync(int skip, int limit)
    {
        List<Customer> customers = (List<Customer>)await _customerRepository.GetAllAsync(skip, limit);
        return _mapper.Map<List<CustomerDTOResponse>>(customers);
    }

    public async Task<bool> UpdateCustomerAsync(string id, CustomerDTORequest customerDTORequest)
    {
        Customer customer = _mapper.Map<Customer>(customerDTORequest);
        customer.Id = id;
        customer.UpdatedAt = DateTime.UtcNow;

        var result = await _customerRepository.UpdateAsync(customer);
        return result != null;
    }
}
