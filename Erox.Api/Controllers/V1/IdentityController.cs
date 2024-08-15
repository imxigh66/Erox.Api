using AutoMapper;
using Erox.Api.Contracts.identity;
using Erox.Api.Extentions;
using Erox.Api.Filters;
using Erox.Application.Identity.Commands;
using Erox.Application.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class IdentityController:BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public IdentityController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpPost]
        [Route(ApiRoutes.Identity.Registration)]
        [ValidateModel]
        public async Task<IActionResult> Register(UserRegistration registration, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterIdentity>(registration);
            var result=await _mediator.Send(command,  cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(_mapper.Map<Contracts.identity.IdentityUserProfile>(result.PayLoad));
            }

        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult> Login(Login login,CancellationToken cancellationToken)
        {
            var command= _mapper.Map<LoginCommand>(login);
            var result= await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);
            return Ok(_mapper.Map<Contracts.identity.IdentityUserProfile>(result.PayLoad));
        }


        [HttpDelete]
        [Route(ApiRoutes.Identity.IdentityById)]
        [ValidateGuid("identityUserId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAccount(string identityUserId,CancellationToken cancellationToken)
        {
            var identityUserGuid=Guid.Parse(identityUserId);
            var requestorGuid = HttpContext.GetIdentityIdClaimValue();
            var command = new RemoveAccount
            {
                IdentityUserId = identityUserGuid,
                RequestorGuid = requestorGuid
            };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return NoContent();
        }


        [HttpGet]
        [Route(ApiRoutes.Identity.CurrentUser)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CurrentUser(CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var claimsPrincipal = HttpContext.User;
            var query = new GetCurrentUser
            {
                userProfileId = userProfileId,
                ClaimsPrincipal = claimsPrincipal
            };
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);
             
            
            return Ok(_mapper.Map<IdentityUserProfile>(result.PayLoad));
        }


    }
}
