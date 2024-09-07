using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Admin.Query
{
    public class TotalSalesQuery:IRequest<TotalSalesDto>
    {
        public bool GetTodayCount { get; set; }
        public bool GetThisWeekCount { get; set; }
        public bool GetThisMonthCount { get; set; }
        public bool GetTotalCount { get; set; }
    }
}
