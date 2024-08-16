using Erox.Domain.Aggregates.ProductAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Validators.ProductValidators
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
            .NotNull().WithMessage("Product name can't be null")
            .NotEmpty().WithMessage("Product name can't be empty");
        }
    }
}
