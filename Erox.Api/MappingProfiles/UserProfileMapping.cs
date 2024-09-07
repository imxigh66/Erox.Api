using AutoMapper;

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
            
            
            CreateMap<UserProfileEntity,UserProfileResponse>();

            //CreateMap<OperationResult<IEnumerable<UserProfiles>>, List<UserProfileResponse>>()
            //.ConvertUsing((src, dest, context) => context.Mapper.Map<List<UserProfileResponse>>(src.Errors.ToList()));
            //CreateMap<OperationResult<UserProfiles>, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
            


        }
    }
}
