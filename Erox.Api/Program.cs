using CwkSocial.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));



var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.RegisterPipelineComponents(typeof(Program));

app.Run();