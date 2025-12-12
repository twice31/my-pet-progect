using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BookExchange.Infrastructure.Data;
using BookExchange.Application.Contracts;
using BookExchange.Infrastructure.Repositories;
using System;
namespace BookExchange.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            PostgreSqlConnectionOptions? options = configuration
                .GetSection(nameof(PostgreSqlConnectionOptions))
                .Get<PostgreSqlConnectionOptions>();

            if (options is null)
            {
                throw new ApplicationException("Конфигурация базы данных PostgreSQL не задана.");
            }

            string connectionString = options.BuildConnectionString();

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
            });

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();

            return services;
        }
    }
}