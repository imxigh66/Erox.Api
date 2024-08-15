using AutoMapper;
using Erox.Application.Identity.Dtos;
using Erox.Domain.Aggregates.UsersProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Mapping
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<UserProfiles,IdentityUserProfile>()
                .ForMember(dest => dest.Phone, opt
            => opt.MapFrom(src => src.Basicinfo.Phone))
            .ForMember(dest => dest.CurrentCity, opt
            => opt.MapFrom(src => src.Basicinfo.CurrentCity))
            .ForMember(dest => dest.EmailAddress, opt
            => opt.MapFrom(src => src.Basicinfo.EmailAddress))
            .ForMember(dest => dest.Firstname, opt
            => opt.MapFrom(src => src.Basicinfo.Firstname))
            .ForMember(dest => dest.Lastname, opt
            => opt.MapFrom(src => src.Basicinfo.Lastname))
            .ForMember(dest => dest.DateOfBirth, opt
            => opt.MapFrom(src => src.Basicinfo.DateOfBirth));
        }
    }
}
