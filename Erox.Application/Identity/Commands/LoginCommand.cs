using Erox.Application.Identity.Dtos;
using Erox.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Commands
{
    public class LoginCommand:IRequest<OperationResult<IdentityUserProfile>>
    {
        public string Username { get; set; }
        public string Password { get; set; }    
    }
}
