using AutoMapper;
using Erox.Api.Contracts.identity;
using Erox.Api.Contracts.userprofile.requests;
using Erox.Application.Identity.Commands;
using Erox.Application.Identity.Dtos;
using Erox.Application.UserProfile.Commands;

namespace Erox.Api.MappingProfiles
{
    public class IdentityMapping:Profile
    {
        public IdentityMapping()
        {
            CreateMap<UserProfileCreateUpdate, UpdateUserInfoBasic>();
            CreateMap<UserRegistration, RegisterIdentity>();
            CreateMap<Login,LoginCommand>();
            CreateMap<Application.Identity.Dtos.IdentityUserProfile, Contracts.identity.IdentityUserProfile>();
        }


    }
}
