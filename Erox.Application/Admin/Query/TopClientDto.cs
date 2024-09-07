using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Admin.Query
{
    public class TopClientDto
    {
        public Guid UserId { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSum { get; set; }
    }
}
