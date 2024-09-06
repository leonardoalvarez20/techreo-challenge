using System;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class LoginDTOResponse : LoginDTO
{
    [JsonPropertyName("Id")]
    public required string Id { get; set; }
    [JsonPropertyName("FullName")]
    public required string FullName { get; set; }
    public required decimal Balance { get; set; }
    [JsonPropertyName("Token")]
    public required string Token { get; set; }
}
