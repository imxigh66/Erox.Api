using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Exeptions
{
    public class PostNotValidException : DomainModelInvalidException
    {
        internal PostNotValidException() { }
        internal PostNotValidException(string message) : base(message) { }
        internal PostNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
