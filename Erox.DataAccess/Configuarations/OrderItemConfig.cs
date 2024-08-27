using Erox.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Erox.DataAccess.Configuarations
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder .HasOne(wi => wi.Product)
                    .WithMany(m => m.OrderItems)
                    .HasForeignKey(wi => wi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder .HasOne(s => s.Size)
                    .WithMany()
                    .HasForeignKey(w => w.SizeId)
                    .OnDelete(DeleteBehavior.Restrict);


            builder.ToTable("OrderItems");
        }
    }
}
