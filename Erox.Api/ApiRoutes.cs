namespace Erox.Api
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public class UserProfile
        {
            public const string IdRoute = "{id}";
        }

       

        public static class Identity
        {
            public const string Login = "login";
            public const string Registration = "registration";
            public const string IdentityById = "{identityUserId}";
            public const string CurrentUser = "currentuser";
            
        }

        public static class Product
        {
            public const string getById = "{id}";
            public const string ProductReview = "{productId}/reviews";
            public const string ReviewById = "{productId}/reviews/{reviewId}";
            public const string ProductSize = "{productId}/sizes";
        }
        public static class Wishlist
        {
            public const string getById = "{id}";
        }

        public static class Order
        {
            public const string getByOrder = "{id}";
        }
    }
}
