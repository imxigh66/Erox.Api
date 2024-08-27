using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.userprofile.requests
{
    public class UserProfileCreateUpdate
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string Phone { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; }
    }
}
