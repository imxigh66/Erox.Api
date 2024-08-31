using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Products.Command;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.CommandHandler
{
    public class AddCategoryHandler : IRequestHandler<AddCategory, OperationResult<Category>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<Category> _result;
        public AddCategoryHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<Category>();
        }
        public async Task<OperationResult<Category>> Handle(AddCategory request, CancellationToken cancellationToken)
        {
            try
            {


                var category = new Category() { Sex=request.Sex.ToString()}; 


               

                _ctx.Category.Update(category);
                await _ctx.SaveChangesAsync(cancellationToken);

                _result.PayLoad = category;

            }
            catch (ProductReviewNotValidException e)
            {


                e.ValidationErrors.ForEach(er =>
                {

                    _result.AddError(ErrorCode.ValidationError, er);
                });
            }

            catch (Exception e)
            {

                _result.AddUnknownError(e.Message);
            }
            return _result;
        }
    }
}
