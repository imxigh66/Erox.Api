using Erox.Application.Models;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;

namespace Erox.Application.Products.Command
{
    public class AddProductReview:IRequest<OperationResult<ProductReview>>
    {
        public Guid ProductId { get; set; }
        public string Text { get; set; }
      
        public string Rating { get; set; }
        public bool IsApproved { get; set; }
      
    }
}