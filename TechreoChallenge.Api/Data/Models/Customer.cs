using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace TechreoChallenge.Api.Data.Models;

public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string RFC { get; set; }
    [BsonElement]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime BirthDate { get; set; }
    public required string Password { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    [BsonElement]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; }
    [BsonElement]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime UpdatedAt { get; set; }
}
