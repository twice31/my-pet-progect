using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BookExchange.Infrastructure.Data;
using BookExchange.Application.Contracts;
using BookExchange.Infrastructure.Repositories;

namespace BookExchange.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new PostgresConnectionOptions();
            configuration.GetSection("PostgresConnectionOptions").Bind(options);

            services.AddSingleton(Microsoft.Extensions.Options.Options.Create(options));

            services.AddDbContext<ApplicationDbContext>();

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}