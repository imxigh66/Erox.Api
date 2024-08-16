using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Validators.ProductValidators
{
    public class ProductReviewValidator: AbstractValidator<ProductReview>
    {
        public ProductReviewValidator()
        {
            RuleFor(pc => pc.Text)
           .NotNull().WithMessage("Comment text should not be null")
           .NotEmpty().WithMessage("Comment text should not be empty")
           .MaximumLength(1000)
           .MinimumLength(1);
        }
    }
}
