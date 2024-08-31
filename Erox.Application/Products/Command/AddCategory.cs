using Erox.Application.Models;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Enumerations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.Command
{
    public class AddCategory:IRequest<OperationResult<Category>>
    {
        public SexEnum Sex { get; set; }
    }
}
