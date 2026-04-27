using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.AtualizarPedido
{
    public record AtualizarPedidoCommand(
        Guid IdPedido,
        List<int> IdProdutos)
        : IRequest<Result>;
}