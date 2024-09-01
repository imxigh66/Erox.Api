using Erox.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.Command
{
    public class ProductTranslationCreateCommand
    {
        public string Title { get; set; }
        public LanguageCodeEnum LanguageCode { get; set; }
    }
}
