using Erox.Domain.Aggregates.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations
{
    internal class PostInteractiononfig : IEntityTypeConfiguration<PostInterection>
    {
        public void Configure(EntityTypeBuilder<PostInterection> builder)
        {
            builder.HasKey(pi => pi.InterectionId);
        }
    }
}
