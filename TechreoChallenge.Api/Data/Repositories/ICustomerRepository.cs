using System;
using TechreoChallenge.Api.Data.Models;

namespace TechreoChallenge.Data.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync(int skip, int limit);
    Task<Customer> GetByIdAsync(string id);
    Task<Customer> GetByEmailAsync(string email);
    Task<Customer> AddAsync(Customer customer);
  
    Task<Customer> UpdateAsync(Customer customer);
    Task<Customer> DeleteAsync(string id);
}
