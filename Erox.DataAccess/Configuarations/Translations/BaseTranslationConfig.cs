using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
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
    public abstract class BaseTranslationConfig<T> : IEntityTypeConfiguration<T>
        where T : BaseTranslation
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(iur => iur.Id);

            builder.HasIndex(i => i.Language);

        }
    }
}
