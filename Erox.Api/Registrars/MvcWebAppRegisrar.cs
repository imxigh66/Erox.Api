
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Erox.Api.Registrars
{
    public class MvcWebAppRegisrar : IWebApplicationRegistrar
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
                }
            });
            app.UseHttpsRedirection();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy=SameSiteMode.Lax,
                HttpOnly=HttpOnlyPolicy.Always,
                Secure=CookieSecurePolicy.Always
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
