namespace Erox.Api
{
    public class ApiRoutes
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public class UserProfile
        {
            public const string IdRoute = "{id}";
        }

        public class Post
        {
            public const string getById = "{id}";
            public const string PostComments = "{postId}/comments";
            public const string CommentById = "{postId}/comments/{commentId}";
           
            public const string InterectionById = "{postId}/interections/{interectionId}";
            public const string PostInteractions = "{postid}/interactions";
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
        }
        public static class Wishlist
        {
            public const string getById = "{id}";
        }
    }
}
