using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess.Configuarations
{
    internal class ProductReviewConfig: IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.HasKey(iur => iur.ReviewId);

            builder.HasOne<Product>()
              .WithMany(p => p.Reviews)
              .HasForeignKey(pr => pr.Productid)
              .OnDelete(DeleteBehavior.Cascade); 
            
        }
    }
}
