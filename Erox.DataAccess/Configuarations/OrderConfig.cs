using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder .HasMany(w => w.Items)
                    .WithOne(wi => wi.Order)
                    .HasForeignKey(wi => wi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder .HasOne(v=>v.User)
                    .WithMany()
                    .HasForeignKey(vi=>vi.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasCheckConstraint("Orders_Status_RestrictValues",
            $"{nameof(Order.Status)} IN ('{string.Join("', '", Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>())}')");

            builder.HasCheckConstraint("Orders_PaymentMethods_RestrictValues",
            $"{nameof(Order.PaymenentMethod)} IN ('{string.Join("', '", Enum.GetValues(typeof(PaymentMethodEnum)).Cast<PaymentMethodEnum>())}')");

            builder.HasCheckConstraint("Orders_ShippingMethods_RestrictValues",
            $"{nameof(Order.ShippingMethod)} IN ('{string.Join("', '", Enum.GetValues(typeof(ShippingMethodEnum)).Cast<ShippingMethodEnum>())}')");
        }
    
    }
}
