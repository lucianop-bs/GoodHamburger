using GoodHamburger.Application.Produtos.ObterProdutos;
using GoodHamburger.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Application.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => { 
                cfg.RegisterServicesFromAssembly(typeof(ObterProdutosQueryHandler).Assembly);
            });

            return services;
        }
    }
}