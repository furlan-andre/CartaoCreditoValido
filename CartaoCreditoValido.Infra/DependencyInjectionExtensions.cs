using CartaoCreditoValido.Domain.CartoesCredito.Repositorios;
using CartaoCreditoValido.Domain.CartoesCredito.Eventos;
using CartaoCreditoValido.Infra.Data;
using CartaoCreditoValido.Infra.Messaging;
using CartaoCreditoValido.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

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

        public static async Task InitializeDatabaseAsync(
            this IServiceProvider serviceProvider,
            CancellationToken cancellationToken = default)
        {
            const int maxAttempts = 15;
            var delay = TimeSpan.FromSeconds(3);

            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger("DatabaseStartup");
            var context = scope.ServiceProvider.GetRequiredService<CartaoCreditoContext>();

            for (var attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    await context.Database.MigrateAsync(cancellationToken);
                    logger.LogInformation("Banco de dados criado/atualizado com sucesso.");
                    return;
                }
                catch (Exception ex)
                {
                    logger.LogWarning(
                        "Não foi possível criar/migrar o banco de dados (tentativa {Attempt}/{Max}): {Message}",
                        attempt, maxAttempts, ex.Message);
                }

                if (attempt < maxAttempts)
                {
                    logger.LogInformation("Aguardando SQL Server... nova tentativa em {Seconds}s.", delay.TotalSeconds);
                    await Task.Delay(delay, cancellationToken);
                }
            }

            throw new InvalidOperationException(
                "Não foi possível criar/migrar o banco de dados após múltiplas tentativas.");
        }

        public static async Task InitializeRabbitMqAsync(
            this IServiceProvider serviceProvider,
            CancellationToken cancellationToken = default)
        {
            const int maxAttempts = 15;
            var delay = TimeSpan.FromSeconds(3);

            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger("RabbitMqStartup");
            var options = scope.ServiceProvider.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

            for (var attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    var factory = new ConnectionFactory
                    {
                        HostName = options.HostName,
                        Port = options.Port,
                        UserName = options.UserName,
                        Password = options.Password,
                        RequestedConnectionTimeout = TimeSpan.FromSeconds(5)
                    };

                    using var connection = factory.CreateConnection();
                    logger.LogInformation("RabbitMQ disponível e pronto para conexão.");
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogWarning("RabbitMQ ainda não disponível (tentativa {Attempt}/{Max}): {Message}",
                        attempt, maxAttempts, ex.Message);

                    if (attempt == maxAttempts)
                        throw new InvalidOperationException("Não foi possível conectar ao RabbitMQ após múltiplas tentativas.");
                }

                if (attempt < maxAttempts)
                {
                    logger.LogInformation("Aguardando RabbitMQ... nova tentativa em {Seconds}s.", delay.TotalSeconds);
                    await Task.Delay(delay, cancellationToken);
                }
            }

            var topologyInitializer = scope.ServiceProvider.GetRequiredService<RabbitMqTopologyInitializer>();
            await topologyInitializer.InitializeAsync(cancellationToken);
            logger.LogInformation("Topologia do RabbitMQ inicializada com sucesso.");
        }
    }
}
