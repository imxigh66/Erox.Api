using Erox.Application.Models;
using Erox.Application.Products.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.QueryHandler
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, OperationResult<List<Category>>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<List<Category>> _result;
        public GetAllCategoriesHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<List<Category>> ();
        }
        public async Task<OperationResult<List<Category>>> Handle(GetAllCategories request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _ctx.Categories.Include(i=>i.CategoryTranslations).ToListAsync();   
                _result.PayLoad = category;
            }
            catch (Exception e)
            {

                _result.AddUnknownError(e.Message);

            }
            return _result;
        }
    }
}
