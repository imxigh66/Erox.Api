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
    internal class ProductImageConfig : IEntityTypeConfiguration<ProductImages>
    {
        public void Configure(EntityTypeBuilder<ProductImages> builder)
        {
            builder.HasKey(iur => iur.Id);

            builder.HasIndex(i => new { i.ProductId ,i.Path}).IsUnique();

            builder.HasOne(p => p.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(pr => pr.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
