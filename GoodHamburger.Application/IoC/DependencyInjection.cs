using FluentValidation;
using GoodHamburger.Application.Produtos.ObterProdutos;
using GoodHamburger.Application.Utils.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ObterProdutosQueryHandler).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(ObterProdutosQueryHandler).Assembly);

            return services;
        }
    }
}