using Erox.Application.Identity.Dtos;
using Erox.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Queries
{
    public class GetCurrentUser:IRequest<OperationResult<IdentityUserProfile>>
    {
        public Guid userProfileId {  get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
