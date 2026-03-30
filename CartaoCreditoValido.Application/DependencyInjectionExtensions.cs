using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using CartaoCreditoValido.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CartaoCreditoValido.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
    
            services.AddScoped<ICartaoCreditoService, CartaoCreditoService>();
            
            return services;
        }
    }
}
