using System;

namespace TechreoChallenge.Api.Exceptions;

public class InternalServerError : Exception
{
    public InternalServerError(string message) : base(message) { }
}
