using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;

using Erox.Domain.Aggregates.Translations;
using Erox.Domain.Aggregates.UsersProfiles;
using Erox.Domain.Aggregates.WishlistAggregate;
using Erox.Domain.Exeptions;

using Erox.Domain.Validators.ProductValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Erox.Domain.Aggregates.ProductAggregate
{
    public class Product
    {
        private readonly List<ProductReview> _reviews = new List<ProductReview>();
        private readonly List<ProductSize> _sizes = new List<ProductSize>();
        public Guid ProductId { get; set; }
      
      
        public decimal Price { get; set; }  
        public decimal DiscountPrice { get;  set; }
        public Guid CategoryId { get;  set; }
        public Category Category { get;  set; }
       
        public IEnumerable<ProductImages> Images {  get; set; }
        public string Season { get;  set; }
        public string  Code { get;  set; }
        public DateTime CreatedDate { get;  set; }
        public DateTime LastModified { get;  set; }
        public IEnumerable<ProductReview> Reviews { get { return _reviews; } }
        public IEnumerable<ProductSize> Sizes {  get { return _sizes; } }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public IEnumerable<CardItem> CardItems { get; set; }
        public IEnumerable<WishlistItem> WishlistItems { get; set; }
        public IEnumerable<ProductNameTranslation> ProductNameTranslations { get; set; }
        public IEnumerable<ProductDescriptionTranslation> ProductDescriptionTranslations { get; set; }




        public static Product CreateProduct(decimal price,decimal discount,Guid categoryId,string season,string code)
        {
            var validator = new ProductValidator();
            var objectToValidate = new Product
            {
               // Name = name,
                //Description = description,  
                Price=price,
                DiscountPrice=discount,
                CategoryId = categoryId,
               
                //Image = image,
                Season=season,
                Code=code,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
            };

            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exception = new PostNotValidException("Product is not valid");
            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;
        }

        public void UpdateProducts(  decimal price, decimal discount, string season, string code)
        {
            //if (string.IsNullOrWhiteSpace(newText))
            //{
            //    var exception = new PostNotValidException("Cannot update post." +
            //                                              "Post text is not valid");
            //    exception.ValidationErrors.Add("The provided text is either null or contains only white space");
            //    throw exception;
            //}
           // Name = name;
            //Description = description;
            Price = price;
            DiscountPrice = discount;

            //Image = image;
            Season = season;
            Code = code;
           


            LastModified = DateTime.UtcNow;
        }

        public void AddProductReview(ProductReview review)
        {
            _reviews.Add(review);
        }
        public void ARemoveReview(ProductReview toRemove)
        {
            _reviews.Remove(toRemove);
        }

        public void AddProductSize(ProductSize size)
        {
            _sizes.Add(size);
        }
    }
}
