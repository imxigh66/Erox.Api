using Erox.Domain.Aggregates.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations
{
    internal class ProductSizeConfig : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.HasKey(iur => iur.SizeId);

            builder.HasIndex(i => new { i.ProductId, i.Size }).IsUnique();

            builder.HasOne(p => p.Product)
              .WithMany(p => p.Sizes)
              .HasForeignKey(pr => pr.ProductId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
