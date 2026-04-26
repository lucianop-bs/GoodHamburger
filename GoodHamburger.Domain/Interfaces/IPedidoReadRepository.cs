using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IPedidoReadRepository
    {
        Task<List<Pedido>> ObterPedidosAsync();

        Task<Pedido?> ObterPedidoPorIdAsync(Guid id);
    }
}