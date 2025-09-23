using IntegrationAPI.Application.Consumer;
using IntegrationAPI.Application.Services;
using DotNetEnv;
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
        options.ListenAnyIP(5121);
    });
}


// Add services to the container.
builder.Services.AddHostedService<OrderCreatedConsumer>();

builder.Services.AddHttpClient<ITrelloService, TrelloService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
