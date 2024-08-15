using Erox.Application.Models;
using Erox.Domain.Aggregates.UsersProfiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.UserProfile.Queries
{
    public class GetAllUserProfilesbyId:IRequest<OperationResult<UserProfiles>>
    {
        public Guid UserProfileId {  get; set; }
    }
}
