using Erox.DataAccess.Configuarations;
using Erox.DataAccess.Configuarations.Translations;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.UsersProfiles;
using Erox.Domain.Aggregates.WishlistAggregate;
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

        public DbSet<UserProfileEntity> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PostCommentConfig());
            builder.ApplyConfiguration(new UserProfileCnfig());
            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());
            builder.ApplyConfiguration(new ProductReviewConfig());
            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new WishlistConfig());
            builder.ApplyConfiguration(new WishlistItemConfig());
            builder.ApplyConfiguration(new ProductSizeConfig());
            builder.ApplyConfiguration(new CardConfig());
            builder.ApplyConfiguration(new CardItemsConfig());
            builder.ApplyConfiguration(new OrderItemConfig());
            builder.ApplyConfiguration(new OrderConfig());
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new ProductNameTranslationConfig());
            base.OnModelCreating(builder);
        }
    }
}
