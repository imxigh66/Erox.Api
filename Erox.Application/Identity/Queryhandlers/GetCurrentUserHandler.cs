using AutoMapper;
using Erox.Application.Identity.Dtos;
using Erox.Application.Identity.Queries;
using Erox.Application.Models;
using Erox.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace Erox.Application.Identity.Queryhandlers
{
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUser, OperationResult<IdentityUserProfile>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private OperationResult<IdentityUserProfile> _result = new();
        public GetCurrentUserHandler(DataContext ctx,UserManager<IdentityUser> usermanager,IMapper mapper)
        {
                _ctx = ctx;
            _userManager = usermanager;
            _mapper = mapper;
        }

        public async  Task<OperationResult<IdentityUserProfile>> Handle(GetCurrentUser request, CancellationToken cancellationToken)
        {
            var identity = await _userManager.GetUserAsync(request.ClaimsPrincipal);
            if(identity==null)
            {
                _result.AddError(Enums.ErrorCode.NotFound, IdentityErrorMessage.NonExistentIdentityUser);
                return _result;
            }
            var profile = await _ctx.UserProfiles.FirstOrDefaultAsync(up=>up.UserProfileId==request.userProfileId,cancellationToken);
            _result.PayLoad = _mapper.Map<IdentityUserProfile>(profile);
            _result.PayLoad.UserName = identity.UserName;
           
            return _result;
        }
    }
}
