using System;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class SavingsAccountDTO
{
    [JsonPropertyName("AccountNumber")]
    public required string AccountNumber { get; set; }
    [JsonPropertyName("CLABE")]
    public required string CLABE { get; set; }
    [JsonPropertyName("Balance")]
    public decimal Balance { get; set; }
}
