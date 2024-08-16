using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products
{
    public class ProductsErrorMessage
    {
        public const string ProductNotFound = "No product found with ID {0}";
        public const string AddReviewNotAuthorized = "\"Cannot add revie from product as you are not autorisation\";";
    }
}
