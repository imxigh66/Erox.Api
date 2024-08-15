using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Optionss
{
    public class JWTSettings
    {
        public string SigningKey { get; set; }
        public string Issuer {  get; set; }
        public string[] Audiences { get; set; }
    }
}
