using Erox.Domain.Aggregates.UsersProfile;
using Erox.Domain.Aggregates.UsersProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations
{
    internal class UserProfileCnfig : IEntityTypeConfiguration<UserProfileEntity>
    {
        public void Configure(EntityTypeBuilder<UserProfileEntity> builder)
        {
            builder.HasKey(up => up.UserProfileId);
            builder.OwnsOne(up => up.Basicinfo);
        }
    }
}
