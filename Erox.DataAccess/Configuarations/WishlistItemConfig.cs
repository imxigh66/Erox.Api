using Erox.Domain.Aggregates.WishlistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations
{
    public class WishlistItemConfig : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
           builder  .HasOne(wi => wi.Product)
                    .WithMany(m => m.WishlistItems)
                    .HasForeignKey(wi => wi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
