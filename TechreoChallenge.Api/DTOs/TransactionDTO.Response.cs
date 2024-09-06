using System;
using System.Text.Json.Serialization;
using TechreoChallenge.Api.Data.Enums;

namespace TechreoChallenge.Api.DTOs;

public class TransactionDTOResponse : TransactionDTO
{
    [JsonPropertyName("Id")]
    public required string Id { get; set; }
    [JsonPropertyName("SavingsAccountId")]
    public required string SavingsAccountId { get; set; }
    [JsonPropertyName("SavingsAccountNewBalance")]
    public required decimal SavingsAccountNewBalance { get; set; }
    [JsonIgnore]
    public required TransactionType TransactionType { get; set; }

    [JsonPropertyName("TransactionType")]
    public string TransactionTypeString => TransactionType.ToString();
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; }
}
