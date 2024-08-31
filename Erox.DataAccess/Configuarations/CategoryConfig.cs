using Erox.Domain.Aggregates.CardAggregate;
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
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(iur => iur.Id);

            builder.HasIndex(i => new { i.Sex });

            builder.HasMany(m => m.Products)
                   .WithOne(o => o.Category)
                   .HasForeignKey(fk => fk.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Categories");
        }
    }
}
