using Erox.Domain.Aggregates.Translations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations.Translations
{
    public class ProductNameTranslationConfig: BaseTranslationConfig<ProductNameTranslation>
    {
        public override void Configure(EntityTypeBuilder<ProductNameTranslation> builder)
        {
            base.Configure(builder);

            builder.HasOne(i => i.Product)
                .WithMany(i=>i.ProductNameTranslations)
                .HasForeignKey(t=>t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ProductNameTranslations");

        }
    }
}
