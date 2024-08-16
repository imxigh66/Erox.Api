using Erox.Application.Models;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.Command
{
    public class DeleteReview:IRequest<OperationResult<ProductReview>>
    {
        public Guid UserProfileId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ReviewId { get; set; }
    }
}
