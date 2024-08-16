using Erox.DataAccess.Configuarations;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.UsersProfiles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.DataAccess
{
    public class DataContext:IdentityDbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserProfiles> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PostCommentConfig());
            builder.ApplyConfiguration(new PostInteractiononfig());
            builder.ApplyConfiguration(new UserProfileCnfig());
            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());
            builder.ApplyConfiguration(new ProductReviewConfig());
            builder.ApplyConfiguration(new ProductConfig());
            base.OnModelCreating(builder);
        }
    }
}
