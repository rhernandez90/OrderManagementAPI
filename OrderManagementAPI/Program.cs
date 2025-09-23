using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Aplication.Middleware;
using OrderManagementAPI.Aplication.Services;
using OrderManagementAPI.Aplication.Services.MessageBus;
using OrderManagementAPI.Aplication.Services.Products;
using OrderManagementAPI.Infrastructure.Percistence;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);


var docker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")?.Equals("true") ?? false;

if (!docker)
{
    Env.Load();
}

if (docker)
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(5000); 
    });
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Variables de entorno
string dbUser = Environment.GetEnvironmentVariable("DB_USER");
string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
string dbHost = Environment.GetEnvironmentVariable("DB_HOST");
string dbName = Environment.GetEnvironmentVariable("DB_NAME");


Console.WriteLine($"[DB Config] Host: {dbHost}, User: {dbUser}, Database: {dbName}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        $"Server={dbHost};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;"));

// Add services to the container
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();



await WaitForSqlServerAsync(dbHost, 1433);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 
}

app.UseMiddleware<ErrorHandlerMiddleware>();

// Swagger solo en desarrollo
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

app.UseAuthorization();

app.MapControllers();

app.Run();



static async Task WaitForSqlServerAsync(string host, int port, int retries = 10, int delayMs = 5000)
{
    for (int i = 0; i < retries; i++)
    {
        try
        {
            using var client = new TcpClient();
            await client.ConnectAsync(host, port);
            Console.WriteLine("SQL Server está listo!");
            return;
        }
        catch
        {
            Console.WriteLine($"Esperando SQL Server... intento {i + 1}/{retries}");
            await Task.Delay(delayMs);
        }
    }
    throw new Exception("No se pudo conectar a SQL Server después de varios intentos.");
}
