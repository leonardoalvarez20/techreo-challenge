using System;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class SavingsAccountDTORequest
{
    [JsonPropertyName("Alias")]
    public required string Alias { get; set; }

    [JsonPropertyName("InitialBalance")]
    public required decimal InitialBalance { get; set; }
}
