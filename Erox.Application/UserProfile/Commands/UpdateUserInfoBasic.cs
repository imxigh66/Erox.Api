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
        public string Firstname { get;  set; }
        public string Lastname { get;  set; }
        public string EmailAddress { get;  set; }
        public string Phone { get;  set; }
        public DateTime DateOfBirth { get;  set; }
        public string CurrentCity { get;  set; }
    }
}
