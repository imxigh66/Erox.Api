using AutoMapper;
using Erox.Api.Contracts.identity;
using Erox.Application.Identity.Commands;
using Erox.Application.Identity.Dtos;

namespace Erox.Api.MappingProfiles
{
    public class IdentityMapping:Profile
    {
        public IdentityMapping()
        {
            CreateMap<UserRegistration, RegisterIdentity>();
            CreateMap<Login,LoginCommand>();
            CreateMap<Application.Identity.Dtos.IdentityUserProfile, Contracts.identity.IdentityUserProfile>();
        }


    }
}
