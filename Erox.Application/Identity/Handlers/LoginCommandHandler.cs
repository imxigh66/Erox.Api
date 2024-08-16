using AutoMapper;
using Erox.Application.Enums;
using Erox.Application.Identity.Commands;
using Erox.Application.Identity.Dtos;
using Erox.Application.Models;
using Erox.Application.Optionss;
using Erox.Application.Services;
using Erox.DataAccess;
using Erox.Domain.Aggregates.UsersProfiles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<IdentityUserProfile>>
    {

        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private OperationResult<IdentityUserProfile> _result = new();
        private readonly IdentityService _identityService;
        public LoginCommandHandler(DataContext ctx,UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper)
        {
                _ctx = ctx;
            _userManager = userManager;
            _identityService=identityService;
            _mapper = mapper;   
        }
        public async Task<OperationResult<IdentityUserProfile>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
          
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request);
                if (_result.IsError) return _result;

                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id);
               _result.PayLoad=_mapper.Map<IdentityUserProfile>(userProfile);
                _result.PayLoad.UserName = identityUser.UserName;
                _result.PayLoad.Token = GetJwtString(identityUser, userProfile);
                return _result;



            }
            catch (Exception ex)
            {

               _result.AddUnknownError(ex.Message);
            }
            return _result;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginCommand request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Username);
            if (identityUser is null)
            {
                _result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessage.NonExistentIdentityUser);
               
               
            }

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
            {
                _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessage.IncorrectPassword);
               
            }
            return identityUser;
        }

        private string GetJwtString(IdentityUser identityUser, UserProfileEntity userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
               {
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                    new Claim("IdentityId", identityUser.Id),
                    new Claim("UserProfileId", userProfile.UserProfileId.ToString()),
               });
            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return  _identityService.WriteToken(token);
        }
    }
}
