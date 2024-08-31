using Erox.Domain.Aggregates.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

			builder .HasMany(m => m.Sizes)
					.WithOne(o => o.Product)
					.HasForeignKey(fk => fk.ProductId)
					.OnDelete(DeleteBehavior.Cascade);
			
			builder .HasMany(m => m.Reviews)
					.WithOne(o => o.Product)
					.HasForeignKey(fk => fk.Productid)
					.OnDelete(DeleteBehavior.Cascade);

			builder .HasMany(m => m.OrderItems)
					.WithOne(o => o.Product)
					.HasForeignKey(fk => fk.ProductId)
					.OnDelete(DeleteBehavior.Cascade);

			builder .HasMany(m => m.CardItems)
					.WithOne(o => o.Product)
					.HasForeignKey(fk => fk.ProductId)
					.OnDelete(DeleteBehavior.Cascade);

			builder .HasMany(m => m.WishlistItems)
					.WithOne(o => o.Product)
					.HasForeignKey(fk => fk.ProductId)
					.OnDelete(DeleteBehavior.Cascade);

           
        }
	}
}
