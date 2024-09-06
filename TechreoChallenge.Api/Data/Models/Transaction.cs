using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TechreoChallenge.Api.Data.Enums;

namespace TechreoChallenge.Api.Data.Models;

public class Transaction
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }
    public required string AccountId { get; set; }
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Amount { get; set; }
    [BsonElement]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Date { get; set; }
    [BsonRepresentation(BsonType.String)]
    public TransactionType TransactionType { get; set; }
    public string Description { get; set; }
}
