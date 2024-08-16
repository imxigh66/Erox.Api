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
    public class ProductConfig: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(e => e.Price)
                .HasPrecision(18, 2);

            builder.Property(e => e.DiscountPrice)
                .HasPrecision(18, 2);

            // Добавьте здесь другие конфигурации для сущности Product
        }
    }
}
