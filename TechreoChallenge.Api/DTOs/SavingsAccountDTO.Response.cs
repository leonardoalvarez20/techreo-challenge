using System;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class SavingsAccountDTOResponse : SavingsAccountDTO
{
    [JsonPropertyName("Id")]
    public required string Id { get; set; }
    [JsonPropertyName("Alias")]
    public required string Alias { get; set; }
}
