
using Erox.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Erox.Api.Registrars
{
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(cs));
            builder.Services.AddIdentityCore<IdentityUser>(Options =>
            {
                Options.Password.RequireDigit = false;
                Options.Password.RequiredLength = 5;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireNonAlphanumeric = false;

            })
                .AddEntityFrameworkStores<DataContext>();
        }

        
    }
}
