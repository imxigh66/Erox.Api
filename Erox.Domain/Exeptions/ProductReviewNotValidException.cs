using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Exeptions
{
    public class ProductReviewNotValidException: DomainModelInvalidException
    {
        internal ProductReviewNotValidException() { }
        internal ProductReviewNotValidException(string message) : base(message) { }
        internal ProductReviewNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
