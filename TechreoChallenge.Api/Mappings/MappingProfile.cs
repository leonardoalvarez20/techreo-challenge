using System;
using AutoMapper;

using TechreoChallenge.Api.Data.Models;
using TechreoChallenge.Api.DTOs;

namespace TechreoChallenge.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map from CustomerDTORequest to Customer
        CreateMap<CustomerDTORequest, Customer>();

        // Map from Customer to CustomerDTOResponse
        CreateMap<Customer, CustomerDTOResponse>();

        // Map from Customer to LoginDTOResponse
        CreateMap<Customer, LoginDTOResponse>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
           .ForMember(dest => dest.Token, opt => opt.MapFrom(src => ""));

        // Map from SavingsAccount to SavingsAccountDTOResponse
        CreateMap<SavingsAccount, SavingsAccountDTOResponse>();
        CreateMap<Transaction, TransactionDTOResponse>();
    }

}
