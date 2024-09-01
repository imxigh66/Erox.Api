using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Posts;
using Erox.Application.Products.Command;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.CommandHandler
{
	public class DeleteProductHandler : IRequestHandler<DeleteProduct, OperationResult<Product>>
	{
		private readonly DataContext _ctx;
		public DeleteProductHandler(DataContext ctx)
		{
			_ctx = ctx;
		}
		public async Task<OperationResult<Product>> Handle(DeleteProduct request, CancellationToken cancellationToken)
		{
			var result = new OperationResult<Product>();
			try
			{
				var includedProducts = _ctx.Products.Include(i => i.Sizes)
													.Include(i => i.CardItems)
													.Include(i => i.OrderItems)
													.Include(i => i.WishlistItems)
													.Include(i => i.Reviews)
													.Include(i=>i.Category)
													.Include(i=>i.ProductNameTranslations)
													.Include(i=>i.ProductDescriptionTranslations)
													;

				var product = await includedProducts.FirstOrDefaultAsync(p => p.ProductId == request.ProductId);
				if (product is null)
				{

					result.AddError(ErrorCode.NotFound, string.Format(ProductsErrorMessage.ProductNotFound, request.ProductId));
					return result;
				}


				_ctx.Products.Remove(product);
				await _ctx.SaveChangesAsync(cancellationToken);
				result.PayLoad = product;
			}
			catch (Exception e)
			{
				result.AddUnknownError(e.Message);
			}
			return result;
		}
	}
}
