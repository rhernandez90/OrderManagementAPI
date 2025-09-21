using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Infrastructure.Percistence;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string dbUser = Environment.GetEnvironmentVariable("DB_USER");
string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer($"Server={Environment.GetEnvironmentVariable("DB_HOST")};Database={Environment.GetEnvironmentVariable("DB_NAME")};User Id={dbUser};Password={dbPassword};"));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {

        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1");
        options.OAuthClientId(builder.Configuration["AzureAd:ClientId"]);
        options.OAuthScopeSeparator(" ");
        options.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
