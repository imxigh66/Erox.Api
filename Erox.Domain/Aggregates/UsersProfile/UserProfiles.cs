using Erox.Domain.Aggregates.UsersProfile;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.UsersProfiles
{
    public class UserProfiles
    {
        private UserProfiles()
        {
        }

       
        public Guid UserProfileId { get; private set; }
        public BasicInfo Basicinfo { get; private set; }
        public string IdentityId { get; private set; }
       
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }

        public static UserProfiles CreateUserProfile(string IdentityId,BasicInfo basicInfo)
        {
            return new UserProfiles
            {
                IdentityId = IdentityId,
                Basicinfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow

            };
        }

      
        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            Basicinfo = newInfo;
            LastModified = DateTime.UtcNow;

        }
    }


}
