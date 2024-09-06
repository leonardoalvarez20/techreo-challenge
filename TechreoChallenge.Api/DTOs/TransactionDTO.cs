using System;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class TransactionDTO
{
    [JsonPropertyName("Amount")]
    public required decimal Amount { get; set; }    
    [JsonPropertyName("Description")]
    public required string Description { get; set; }
}
