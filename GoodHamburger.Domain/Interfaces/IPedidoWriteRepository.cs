using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IPedidoWriteRepository
    {
        Task AdicionarPedidoAsync(Pedido pedido);
    }
}