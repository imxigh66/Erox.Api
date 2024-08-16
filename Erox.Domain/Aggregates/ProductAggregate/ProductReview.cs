using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Exeptions;
using Erox.Domain.Validators.PostValidators;
using Erox.Domain.Validators.ProductValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.ProductAggregate
{
    public class ProductReview
    {
       
        public Guid ReviewId { get; private set; }
        public Guid Productid { get; private set; }
        public string Text { get; private set; }
        public Guid UserProfieId { get; private set; }
        public string Rating { get; private set; }
        public bool IsApproved { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }


        public static ProductReview CreateProductReview(Guid productid, string text,string rating,bool isApproved)
        {
            var validator = new ProductReviewValidator();
            var objectToValidate = new ProductReview
            {
                Productid = productid,
                Text = text,
                Rating = rating,
                IsApproved = isApproved,
              
                CreatedDate = DateTime.Now,
                LastModified = DateTime.Now,
            };
            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exeption = new ProductReviewNotValidException("Review is not valid");
            validationResult.Errors.ForEach(vr => exeption.ValidationErrors.Add(vr.ErrorMessage));
            throw exeption;
        }

    }
}
