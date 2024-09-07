using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Admin.Query
{
    public class GetTopClientsQuery: IRequest<List<TopClientDto>>
    {
        public int TopCount { get; set; }

        public GetTopClientsQuery(int topCount)
        {
            TopCount = topCount;
        }
    }
}
