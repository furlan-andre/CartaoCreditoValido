using CartaoCreditoValido.Domain.CartoesCredito.Repositorios;
using CartaoCreditoValido.Domain.CartoesCredito.Eventos;
using CartaoCreditoValido.Infra.Data;
using CartaoCreditoValido.Infra.Messaging;
using CartaoCreditoValido.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartaoCreditoValido.Infra
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada no appsettings.json");

            services.AddDbContext<CartaoCreditoContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
        
        public static IServiceCollection AddRespositorios(this IServiceCollection services)
        {
            services.AddScoped<ICartaoCreditoRepository, CartaoCreditoRepository>();
            services.AddScoped<ICartaoCreditoEventPublisher, RabbitMqCartaoCreditoEventPublisher>();
            
            return services;
        }

        public static IServiceCollection AddMessageria(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));

            return services;
        }
    }
}
