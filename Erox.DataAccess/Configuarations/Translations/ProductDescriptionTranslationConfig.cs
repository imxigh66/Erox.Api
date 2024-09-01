using Erox.Domain.Aggregates.Translations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations.Translations
{
    public class ProductDescriptionTranslationConfig: BaseTranslationConfig<ProductDescriptionTranslation>
    {
        public override void Configure(EntityTypeBuilder<ProductDescriptionTranslation> builder)
        {
            base.Configure(builder);

            builder.HasOne(i => i.Product)
                .WithMany(i => i.ProductDescriptionTranslations)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ProductDescriptionTranslations");

        }
    }
}
