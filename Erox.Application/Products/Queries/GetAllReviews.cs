using Erox.Application.Base;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;

namespace Erox.Application.Products.Queries
{
	public class GetAllReviews: QueryBase<ProductReview>
    {
		public class GetAllReviewsHandler : QueryBaseHandler<GetAllReviews>
		{			
			public GetAllReviewsHandler(DataContext ctx) : base(ctx)
			{
			}
		}
	}
}
