using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Admin.Query
{
    public enum PeriodType
    {
        Day,
        Week,
        Month,
        AllTime
    }
    public class RevenueByPeriodQuery:IRequest<decimal>
    {
        public PeriodType PeriodType { get; set; }

        public RevenueByPeriodQuery(PeriodType periodType)
        {
            PeriodType = periodType;
        }
    }
}
