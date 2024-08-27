using Erox.Domain.Aggregates.CardAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Erox.DataAccess.Configuarations
{
    internal class CardItemsConfig : IEntityTypeConfiguration<CardItem>
    {
        public void Configure(EntityTypeBuilder<CardItem> builder)
        {
            builder .HasOne(wi => wi.Product)
                    .WithMany(m => m.CardItems)
                    .HasForeignKey(wi => wi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder .HasOne(s=>s.Size)
                    .WithMany()
                    .HasForeignKey(w=>w.SizeId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
