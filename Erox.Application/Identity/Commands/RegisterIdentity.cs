using Erox.Application.Identity.Dtos;
using Erox.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity.Commands
{
    public class RegisterIdentity:IRequest<OperationResult<IdentityUserProfile>>
    {
        
        public string Username { get; set; }
      
        public string Password { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

  
        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }
        public string CurrentCity { get; set; }
    }
}
