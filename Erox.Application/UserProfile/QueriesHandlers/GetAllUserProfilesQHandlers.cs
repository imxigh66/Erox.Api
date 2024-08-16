using Erox.Application.Models;
using Erox.Application.UserProfile.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.UsersProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.UserProfile.QueriesHandlers
{
    internal class GetAllUserProfilesQHandlers : IRequestHandler<GetAllUserProfiles, OperationResult<IEnumerable<UserProfileEntity>>>
    {
        private readonly DataContext _ctx;
        public GetAllUserProfilesQHandlers(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<IEnumerable<UserProfileEntity>>> Handle(GetAllUserProfiles request, CancellationToken cancellationToken)
        {
            var result=new OperationResult<IEnumerable<UserProfileEntity>>();
            result.PayLoad= await _ctx.UserProfiles.ToListAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}
