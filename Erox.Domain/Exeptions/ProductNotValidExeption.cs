using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Exeptions
{
    public class ProductNotValidExeption:DomainModelInvalidException
    {
        internal ProductNotValidExeption() { }
        internal ProductNotValidExeption(string message) : base(message) { }
        internal ProductNotValidExeption(string message, Exception inner) : base(message, inner) { }
    }
}
