using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.identity
{
    public class Login
    {
        [EmailAddress]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
            
    }
}
