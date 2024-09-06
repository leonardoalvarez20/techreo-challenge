using System;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class CustomerDTOResponse : CustomerDTO
{
    [JsonPropertyName("Id")]
    public required string Id { get; set; }
    [JsonPropertyName("CreatedAt")]
    public DateTime CreatedAt { get; set; }
}
