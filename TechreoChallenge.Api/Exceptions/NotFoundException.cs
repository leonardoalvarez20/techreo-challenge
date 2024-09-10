using System;

namespace TechreoChallenge.Api.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
