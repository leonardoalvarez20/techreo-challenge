using System;
using TechreoChallenge.Api.DTOs;

namespace TechreoChallenge.Services;

public interface ICustomerService
{
    Task<CustomerDTOResponse> CreateCustomerAsync(CustomerDTORequest customerDTORequest);
    Task<CustomerDTOResponse> GetCustomerByIdAsync(string id);
    Task<IEnumerable<CustomerDTOResponse>> GetCustomersAsync(int skip, int limit);
    Task<bool> DeleteCustomerAsync(string id);
    Task<bool> UpdateCustomerAsync(string id, CustomerDTORequest customerDTORequest);
}
