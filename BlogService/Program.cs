using BlogService.Database.Contexts;
using BlogService.Mappers;
using BlogService.Repos.Contracts;
using BlogService.Repos.Implementations;
using BlogService.Services.Contracts;
using BlogService.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Shared.JwtConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IBlogService, BlogsService>();
builder.Services.AddScoped<IBlogRepo, BlogRepo>();
builder.Services.AddAutoMapper(cfg => { },typeof(BlogProfile));



builder.Services.AddDbContext<BlogDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSharedJwtAuth(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
