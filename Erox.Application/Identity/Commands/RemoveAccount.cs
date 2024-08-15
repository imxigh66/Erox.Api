﻿using Erox.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Commands
{
    public class RemoveAccount:IRequest<OperationResult<bool>>
    {
        public Guid IdentityUserId { get; set; }
        public Guid RequestorGuid { get; set; } 
    }
}
