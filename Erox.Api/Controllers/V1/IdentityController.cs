using AutoMapper;
using Erox.Api.Contracts.identity;
using Erox.Api.Contracts.userprofile.requests;
using Erox.Api.Extentions;
using Erox.Api.Filters;
using Erox.Application.Identity.Commands;
using Erox.Application.Identity.Queries;
using Erox.Application.UserProfile.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class IdentityController:BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
       
        public IdentityController(IMediator mediator, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userManager = userManager;
            
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

            var identityUserProfile = result.PayLoad;

            

            Response.Cookies.Append("AuthToken", identityUserProfile.Token);

            return Ok(_mapper.Map<Contracts.identity.IdentityUserProfile>(identityUserProfile));

            //return Ok(_mapper.Map<Contracts.identity.IdentityUserProfile>(result.PayLoad));
        }


        [HttpDelete]
        [Route("DeleteAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="AppUser")]
        public async Task<IActionResult> DeleteAccount(string? identityUserId, CancellationToken cancellationToken)
        {
            var identityUserGuid = identityUserId == null
                                    ? HttpContext.GetIdentityIdClaimValue()
                                    : Guid.Parse(identityUserId);

            var command = new RemoveAccount
            {
                IdentityUserId = identityUserGuid,
            };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            return NoContent();
        }


        [HttpGet]
        [Route(ApiRoutes.Identity.CurrentUser)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "AppUser")]
        public async Task<IActionResult> CurrentUser(CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var claimsPrincipal = HttpContext.User;
            //foreach (var claim in claimsPrincipal)
            //{
            //    Console.WriteLine($"{claim.Type}: {claim.Value}");
            //}
            var query = new GetCurrentUser
            {
                UserProfileId = userProfileId,
                ClaimsPrincipal = claimsPrincipal
            };
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);
             
            
            return Ok(_mapper.Map<IdentityUserProfile>(result.PayLoad));
        }

        [HttpGet]
        [Route("GetRoleByUser")]

        public async Task<IActionResult> GetRoleByUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new { roles });
        }


        [HttpPatch]
        [Route("UpdateUserProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "AppUser")]
        public async Task<IActionResult> UpdateUserProfile(UserProfileCreateUpdate updateProfile, CancellationToken cancellationToken)
        {
            if (updateProfile == null)
            {
                return BadRequest("Profile data is missing.");
            }

            var userId = HttpContext.User.FindFirst("UserProfileId")?.Value;
            var command = _mapper.Map<UpdateUserInfoBasic>(updateProfile);
            command.UserProfileId = Guid.Parse(userId);
            var response = await _mediator.Send(command, cancellationToken);
            Console.WriteLine($"Email received: {updateProfile.EmailAddress}");

            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "AppUser")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest, CancellationToken cancellationToken)
        {
            if (changePasswordRequest == null)
            {
                return BadRequest("Password change data is missing.");
            }

            // Получаем IdentityId из токена
            var identityId = HttpContext.GetIdentityIdClaimValue().ToString(); // Убедитесь, что этот метод возвращает IdentityId из токена
            if (string.IsNullOrEmpty(identityId))
            {
                return Unauthorized("Identity ID not found in token.");
            }

            // Маппим запрос в команду для смены пароля
            var command = new ChangeUserPasswordCommand
            {
                IdentityId = Guid.Parse(identityId), // Используем IdentityId
                CurrentPassword = changePasswordRequest.CurrentPassword,
                NewPassword = changePasswordRequest.NewPassword,
                ConfirmNewPassword = changePasswordRequest.ConfirmNewPassword
            };

            // Отправляем команду через MediatR
            var response = await _mediator.Send(command, cancellationToken);

            // Проверяем результат выполнения команды
            if (response.IsError)
            {
                return HandleErrorResponse(response.Errors);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            // Удаление куки 'AuthToken'
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies.Delete("AuthToken", new CookieOptions
                {
                    Path = "/",
                    HttpOnly = true,
                });
            }

            return Ok(new { message = "Logged out successfully" });
        }
    }
}
