using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
        .HasOne(wi => wi.Product)
        .WithMany()
        .HasForeignKey(wi => wi.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.Size).WithMany().HasForeignKey(w => w.SizeId).OnDelete(DeleteBehavior.Restrict);


            builder.ToTable("OrderItems");
        }
    }
}
