using CartaoCreditoValido.Domain.CartoesCredito.Repositorios;
using CartaoCreditoValido.Domain.CartoesCredito.Eventos;
using CartaoCreditoValido.Infra.Data;
using CartaoCreditoValido.Infra.Messaging;
using CartaoCreditoValido.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddSingleton<RabbitMqTopologyManager>();
            services.AddSingleton<RabbitMqTopologyInitializer>();

            return services;
        }

        public static async Task EnsureDatabaseUpdatedAsync(
            this IServiceProvider serviceProvider,
            CancellationToken cancellationToken = default)
        {
            using var scope = serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;
            var logger = scopedProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger("DatabaseStartup");
            var context = scopedProvider.GetRequiredService<CartaoCreditoContext>();

            var canConnect = await context.Database.CanConnectAsync(cancellationToken);

            if (!canConnect)
            {
                logger.LogInformation("Banco indisponível ou inexistente. Aplicando migrations para criação/atualização...");
                await context.Database.MigrateAsync(cancellationToken);
                return;
            }

            var pendingMigrations = await context.Database.GetPendingMigrationsAsync(cancellationToken);
            if (pendingMigrations.Any())
            {
                logger.LogInformation("Foram encontradas migrations pendentes. Aplicando migrations...");
                await context.Database.MigrateAsync(cancellationToken);
                return;
            }

            logger.LogInformation("Banco de dados já está atualizado com as migrations.");
        }
    }
}
