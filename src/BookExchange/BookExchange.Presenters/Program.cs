using BookExchange.Infrastructure;
using BookExchange.Application;
using System.Reflection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);



PostgreSqlConnectionOptions? options = builder
    .Configuration
    .GetSection(nameof(PostgreSqlConnectionOptions))
    .Get<PostgreSqlConnectionOptions>();

if (options == null)
{
    throw new ApplicationException("Конфигурация базы данных PostgreSQL не задана.");
}

Console.WriteLine(options.HostName);
Console.WriteLine(options.DatabaseName);
Console.WriteLine(options.UserName);
Console.WriteLine(options.Password);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BookExchange API",
        Version = "v1",
        Description = "API для обмена книгами."
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();