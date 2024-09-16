using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Identity
{
    public class IdentityErrorMessage
    {
        public const string NonExistentIdentityUser = "Unable to find a user with the specified username";
        public const string IncorrectPassword = "The provided password is incorrect";
        public const string IdentityUserAlreadyExists = "Provided email addres already exists.Cannot register new user";
        public const string UnauthorizedAcoountRemoval = "Cannot remove account as you are not its owner";
        public const string NotConfirmPassword = "New password and confirmation do not match";
    }
}
