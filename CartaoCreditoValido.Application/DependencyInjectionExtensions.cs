using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using CartaoCreditoValido.Application.Commons.Behaviors;
using CartaoCreditoValido.Application.Services;
using CartaoCreditoValido.Domain.CartaoCredito.Validadores;
using CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;
using CartaoCreditoValido.Domain.CartoesCredito.Services.Validadores;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CartaoCreditoValido.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICartaoCreditoService, CartaoCreditoService>();
            services.AddScoped<IValidadorNumeroCartao, ValidadorNumeroCartao>();
            services.AddScoped<IRegraBandeira, RegraAmex>();
            services.AddScoped<IRegraBandeira, RegraDiscover>();
            services.AddScoped<IRegraBandeira, RegraVisa>();
            services.AddScoped<IRegraBandeira, RegraMasterCard>();
          
            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CriarCartaoCreditoCommand>();
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            
            services.AddValidatorsFromAssemblyContaining<CriarCartaoCreditoCommandValidator>();
            
            return services;
        }
    }
}
