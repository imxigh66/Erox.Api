using Erox.Domain.Aggregates.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.ProductAggregate
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Sex { get; set; }
        public IEnumerable<Product> Products { get; set;}
        public IEnumerable<CategoryTranslation> CategoryTranslations { get; set;}
    }
}
