using Erox.Domain.Aggregates.CardAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations
{
    internal class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasMany(w => w.Items)
                .WithOne(wi => wi.Card)
                .HasForeignKey(wi => wi.CardId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
