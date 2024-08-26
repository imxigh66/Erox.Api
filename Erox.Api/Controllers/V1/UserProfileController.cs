using AutoMapper;
using Erox.Api.Contracts.common;
using Erox.Api.Contracts.userprofile.requests;
using Erox.Api.Contracts.userprofile.responses;
using Erox.Api.Filters;
using Erox.Application.Enums;
using Erox.Application.UserProfile.Commands;
using Erox.Application.UserProfile.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfileController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public UserProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAllProfiles(CancellationToken cancellationToken)
        {
            

            var query = new GetAllUserProfiles();   
            var response=await _mediator.Send(query, cancellationToken);
            var profiles=_mapper.Map<List<UserProfileResponse>>(response.PayLoad);
            return Ok(profiles);
        }


       


        [Route(ApiRoutes.UserProfile.IdRoute)]
        [HttpGet]
        [ValidateGuid("id")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "AppUser")]
        public async Task<IActionResult> GetUserProfileById(string id, CancellationToken cancellationToken)
        {
            var quary = new GetAllUserProfilesbyId { UserProfileId = Guid.Parse(id) };
            var response=await _mediator.Send(quary, cancellationToken);
                
            if(response.IsError)
                return HandleErrorResponse(response.Errors);

            var userProfile=_mapper.Map<UserProfileResponse>(response.PayLoad);
            return Ok(userProfile);
        }

        [HttpPatch]
        [Route(ApiRoutes.UserProfile.IdRoute)]
        [ValidateModel]
        [ValidateGuid("id")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "AppUser")]
        public async Task<IActionResult> UpdateUserProfile(string id,UserProfileCreateUpdate updateProfile, CancellationToken cancellationToken)
        {
            var command =_mapper.Map<UpdateUserInfoBasic>(updateProfile);
            command.UserProfileId=Guid.Parse(id);
            var response=await _mediator.Send(command, cancellationToken);
            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }

     
    }
}
