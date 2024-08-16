using Erox.Application.Models;
using Erox.Domain.Aggregates.UsersProfiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.UserProfile.Commands
{
    public class UpdateUserInfoBasic:IRequest<OperationResult<UserProfileEntity>>
    {
        public Guid UserProfileId { get; set; } 
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string CurrentCity { get; private set; }
    }
}
