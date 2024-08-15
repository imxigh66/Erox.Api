using Erox.Application.Identity.Commands;
using Erox.Application.Models;
using Erox.Application.UserProfile;
using Erox.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Handlers
{
    public class RemoveAccountHandler : IRequestHandler<RemoveAccount, OperationResult<bool>>
    {
        private readonly DataContext _ctx;
        public RemoveAccountHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<OperationResult<bool>> Handle(RemoveAccount request, CancellationToken cancellationToken)
        {
            var result=new OperationResult<bool>();

            try
            {
                var identityUser = await _ctx.Users.FirstOrDefaultAsync(iu => iu.Id == request.IdentityUserId.ToString(),cancellationToken);
                if(identityUser == null)
                {
                    result.AddError(Enums.ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessage.NonExistentIdentityUser);
                    return result;
                }

                var userProfile =await _ctx.UserProfiles.FirstOrDefaultAsync(up=>up.IdentityId==request.IdentityUserId.ToString(),cancellationToken);
                if(userProfile == null) 
                {
                    result.AddError(Enums.ErrorCode.NotFound,UserProfileErrorMessage.UserProfileNotFound);
                    return result;
                }

                if(identityUser.Id!=request.RequestorGuid.ToString())
                {
                    result.AddError(Enums.ErrorCode.UnauthorizedAccountRemoval, IdentityErrorMessage.UnauthorizedAcoountRemoval);
                    return result;  
                }

                _ctx.UserProfiles.Remove(userProfile);
                _ctx.Users.Remove(identityUser);
                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = true; 

                    
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }
            return result;
        }
    }
}
