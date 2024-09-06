using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechreoChallenge.Api.DTOs;

public class CustomerDTO
{
    [JsonPropertyName("FirstName")]
    public required string FirstName { get; set; }

    [JsonPropertyName("LastName")]
    public required string LastName { get; set; }

    [JsonPropertyName("Email")]
    [EmailAddress]
    public required string Email { get; set; }

    [JsonPropertyName("PhoneNumber")]
    public required string PhoneNumber { get; set; }

    [JsonPropertyName("RFC")]
    public required string RFC { get; set; }

    [JsonPropertyName("BirthDate")]
    public DateTime BirthDate { get; set; }
}
