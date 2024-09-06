using System;
using TechreoChallenge.Api.Data.Models;

using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using TechreoChallenge.Api.Settings;
using MongoDB.Bson.Serialization;

namespace TechreoChallenge.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _customers;
    private readonly FindOneAndUpdateOptions<Customer, Customer> _defaultFindOneAndUpdateOptions;

    public CustomerRepository(IMongoClient mongoClient, MongoDbSettings mongoDbSettings)
    {
        var database = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
        _customers = database.GetCollection<Customer>("Customers");
        _defaultFindOneAndUpdateOptions = new FindOneAndUpdateOptions<Customer, Customer>
        {
            ReturnDocument = ReturnDocument.After
        };
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        await _customers.InsertOneAsync(customer);
        return customer;
    }

    public async Task<Customer> DeleteAsync(string id)
    {
        var filter = Builders<Customer>.Filter.Eq(customer => customer.Id, id);
        var update = Builders<Customer>.Update
            .Set("IsActive", false)
            .Set("IsDeleted", true);
        return await _customers.FindOneAndUpdateAsync(
            filter: filter,
            update: update,
            options: _defaultFindOneAndUpdateOptions
        );
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(int skip, int limit)
    {
        return await _customers.Find(x => true)
            .SortBy(o => o.Id)
            .Skip((skip - 1) * limit)
            .Limit(limit)
            .ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(string id)
    {
        return await _customers.Find(customer => customer.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Customer> GetByEmailAsync(string email)
    {
        return await _customers.Find(customer => customer.Email == email).FirstOrDefaultAsync();
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        var filter = Builders<Customer>.Filter.Eq(customer => customer.Id, customer.Id);
        var update = Builders<Customer>.Update
            .Set("FirstName", customer.FirstName)
            .Set("LastName", customer.LastName)
            .Set("Email", customer.Email)
            .Set("PhoneNumber", customer.PhoneNumber)
            .Set("RFC", customer.RFC)
            .Set("BirthDate", customer.BirthDate);
        return await _customers.FindOneAndUpdateAsync(
            filter: filter,
            update: update,
            options: _defaultFindOneAndUpdateOptions
        );
    }
}
