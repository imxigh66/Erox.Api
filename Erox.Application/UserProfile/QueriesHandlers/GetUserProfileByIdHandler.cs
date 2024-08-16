using Erox.Application.Models;
using Erox.Application.UserProfile.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.UsersProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Erox.Application.UserProfile.QueriesHandlers
{
    internal class GetUserProfileByIdHandler : IRequestHandler<GetAllUserProfilesbyId, OperationResult<UserProfileEntity>>
    {
        private readonly DataContext _ctx;
        public GetUserProfileByIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<UserProfileEntity>> Handle(GetAllUserProfilesbyId request, CancellationToken cancellationToken)
        {
            var result=new OperationResult<UserProfileEntity>();
           var profile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken: cancellationToken);
            if (profile == null)
            {
                result.AddError(Enums.ErrorCode.NotFound, string.Format(UserProfileErrorMessage.UserProfileNotFound, request.UserProfileId));
                return result;
            }
            result.PayLoad = profile;
            return result;  
        }
    }
}
