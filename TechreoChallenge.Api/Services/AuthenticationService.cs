using AutoMapper;
using System;
using TechreoChallenge.Api.DTOs;
using TechreoChallenge.Data.Repositories;
using TechreoChallenge.Api.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TechreoChallenge.Api.Settings;

namespace TechreoChallenge.Api.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;
    private readonly ICustomerRepository _customerRepository;

    public AuthenticationService(IMapper mapper, JwtSettings jwtSettings, ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _jwtSettings = jwtSettings;
        _customerRepository = customerRepository;
    }

    public async Task<LoginDTOResponse> Login(LoginDTORequest loginDTORequest)
    {
        var user = await _customerRepository.GetByEmailAsync(loginDTORequest.Email);
        if (user == null || !PasswordHasher.VerifyPassword(loginDTORequest.Password, user.Password))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var token_written = tokenHandler.WriteToken(token);
        LoginDTOResponse loginResponseDTO = _mapper.Map<LoginDTOResponse>(user);
        loginResponseDTO.Token = token_written;
        return loginResponseDTO;
    }
}
