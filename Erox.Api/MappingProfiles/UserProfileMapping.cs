using AutoMapper;
using Erox.Api.Contracts.posts.responses;
using Erox.Api.Contracts.userprofile.requests;
using Erox.Api.Contracts.userprofile.responses;
using Erox.Application.Models;
using Erox.Application.UserProfile.Commands;
using Erox.Domain.Aggregates.UsersProfile;
using Erox.Domain.Aggregates.UsersProfiles;

namespace Erox.Api.MappingProfiles
{
    public class UserProfileMapping:Profile
    {
        public UserProfileMapping()
        {
            
            CreateMap<UserProfileCreateUpdate, UpdateUserInfoBasic>();
            CreateMap<UserProfileEntity,UserProfileResponse>();

            //CreateMap<OperationResult<IEnumerable<UserProfiles>>, List<UserProfileResponse>>()
            //.ConvertUsing((src, dest, context) => context.Mapper.Map<List<UserProfileResponse>>(src.Errors.ToList()));
            //CreateMap<OperationResult<UserProfiles>, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
            CreateMap<UserProfileEntity, InteractionUser>()
                .ForMember(dest => dest.FullName,opt
                =>opt.MapFrom(src
                =>src.Basicinfo.Firstname+ " " + src.Basicinfo.Lastname))
                .ForMember(dest => dest.City, opt
                => opt.MapFrom(src => src.Basicinfo.CurrentCity));


        }
    }
}
