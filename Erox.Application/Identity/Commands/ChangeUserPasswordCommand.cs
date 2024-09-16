using Erox.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Commands
{
    public class ChangeUserPasswordCommand: IRequest<OperationResult<bool>>
    {
        public Guid IdentityId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
