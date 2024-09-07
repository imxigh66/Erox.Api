
using Erox.Application.Admin.QueriesHandler;
using Erox.Application.Services;

namespace Erox.Api.Registrars
{
    public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IdentityService>();
            builder.Services.AddScoped<TopSalesHandler>();
        }
    }
}
