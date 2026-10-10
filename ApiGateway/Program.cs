using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Shared.JwtConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddSharedJwtAuth(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseAuthentication();
app.UseAuthorization();


await app.UseOcelot();

app.Run();
