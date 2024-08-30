namespace Erox.Api.Contracts.identity
{
    public class IdentityUserProfile
    {
        public Guid UserProfileId { get; set; }
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CurrentCity { get; set; }
        public string Token { get; set; }
    }
}
