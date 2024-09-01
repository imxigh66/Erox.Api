using Erox.Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.Translations
{
    public class CategoryTranslation:BaseTranslation
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
