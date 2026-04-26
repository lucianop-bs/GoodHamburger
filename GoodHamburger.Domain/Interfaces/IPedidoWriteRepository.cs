using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IPedidoWriteRepository
    {
        Task AdicionarPedidoAsync(Pedido pedido);

        void AtualizarPedido(Pedido pedido);

        void DeletarPedido(Pedido pedido);
    }
}