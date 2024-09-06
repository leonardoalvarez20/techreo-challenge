using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechreoChallenge.Api.Data.Models;

public class SavingsAccount
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public required string CustomerId { get; set; }
    [StringLength(11)]
    public required string Alias { get; set; }
    public required string AccountNumber { get; set; }
    [StringLength(18)]
    public required string CLABE { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Balance { get; set; }
    [BsonElement]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; }
    [BsonElement]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime UpdatedAt { get; set; }

}
