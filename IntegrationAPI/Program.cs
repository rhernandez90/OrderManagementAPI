using DotNetEnv;
using IntegrationAPI.Application.Consumer;
using IntegrationAPI.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var docker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")?.Equals("true") ?? false;

if (!docker)
{
    Env.Load();
}

if (docker)
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(5114);
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
