using Erox.Application.Models;
using Erox.Application.UserProfile.Commands;
using Erox.DataAccess;
using Erox.Domain.Aggregates.UsersProfile;
using Erox.Domain.Aggregates.UsersProfiles;
using Erox.Domain.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.UserProfile.CommandHandlers
{
    internal class UpdateUserInfoBasicHandler : IRequestHandler<UpdateUserInfoBasic,OperationResult<UserProfiles>>
    {
        private readonly DataContext _ctx;
        public UpdateUserInfoBasicHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfiles>> Handle(UpdateUserInfoBasic request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfiles>();

            try
            {
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId,cancellationToken);
                if (userProfile == null) 
                {
                    result.AddError(Enums.ErrorCode.NotFound, string.Format(UserProfileErrorMessage.UserProfileNotFound, request.UserProfileId));
                    return result;
                }
                var basicInfo = BasicInfo.CreateBasicInfo(request.Firstname, request.Lastname, request.Phone, request.CurrentCity, request.DateOfBirth, request.EmailAddress);
                userProfile.UpdateBasicInfo(basicInfo);
                _ctx.UserProfiles.Update(userProfile);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = userProfile;
                return result;
            }

            catch (UserProfileNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e =>
                {
                    result.AddError(Enums.ErrorCode.ValidationError, e);
                });
                return result;
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }
           
            return result;
        }
    }
}
