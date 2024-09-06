using System;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class LoginDTO
{
    [JsonPropertyName("Email")]
    public required string Email { get; set; }
}
