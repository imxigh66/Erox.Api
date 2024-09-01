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
    public class CategoryTranslationConfig: BaseTranslationConfig<CategoryTranslation>
    {
        public override void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            base.Configure(builder);

            builder.HasOne(i => i.Category)
                .WithMany(i => i.CategoryTranslations)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("CategoryTranslations");

        }
    }
}
