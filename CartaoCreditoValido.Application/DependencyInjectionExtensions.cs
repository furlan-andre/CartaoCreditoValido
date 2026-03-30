using CartaoCreditoValido.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CartaoCreditoValido.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
    
            services.AddScoped<ICartaoCreditoService, CartaoCreditoService>();
            // services.AddScoped<ICommandHandler<CriarCartaoCreditoCommand>, CriarCartaoCreditoCommandHandler>();

            return services;
        }
    }
}
