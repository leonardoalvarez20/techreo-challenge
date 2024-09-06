using System;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class CustomerDTORequest : CustomerDTO
{
    [JsonPropertyName("Password")]
    public required string Password { get; set; }
}
