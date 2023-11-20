using Application.Models.Requests.Account;
using Application.Models.Responses.Account;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationModel, User>()
            // .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login))
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => s.Password));

        CreateMap<User, AuthorizedModel>()
            .ForMember(d => d.Token, opt => opt.Ignore());
    }
}