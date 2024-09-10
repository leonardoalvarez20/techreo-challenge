using System;

namespace TechreoChallenge.Api.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
