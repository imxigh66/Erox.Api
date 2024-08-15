namespace Erox.Api.Contracts.userprofile.responses
{
    public record UserProfileResponse
    {
        public Guid UserProfileId { get;  set; }
        public BasicInformation Basicinfo { get;  set; }
        

        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
    }
}
