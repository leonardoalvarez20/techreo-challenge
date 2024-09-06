using System;
using TechreoChallenge.Api.DTOs;

namespace TechreoChallenge.Api.Services;

public interface IAuthenticationService
{
    Task<LoginDTOResponse> Login(LoginDTORequest loginDTORequest);
}
