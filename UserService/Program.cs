using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsersService.Database;
using UsersService.Models;
using UsersService.Repos.Contracts;
using UsersService.Repos.Implementations;
using UsersService.Services.Contracts;
using UsersService.Services.Implementations;
using MassTransit;
using Shared.JwtConfig;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<UserDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSharedJwtAuth(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UsersService.Consumers.UserCreatedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("user-created-queue", e =>
        {
            e.ConfigureConsumer<UsersService.Consumers.UserCreatedEventConsumer>(context);
        });
    });
});



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
