using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.AtualizarPedido
{
    public record AtualizarPedidoCommand(Guid idPedido, List<int> idProdutos) : IRequest<Result>;
}