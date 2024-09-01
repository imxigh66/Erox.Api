using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.Translations
{
    public class BaseTranslation
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
    }
}
