using GoodHamburger.Application.Utils.Responses;
using MediatR;

namespace GoodHamburger.Application.Pedidos.CriarPedido
{
    public record class CriarPedidoCommand(List<int> ProdutosId) : IRequest<PedidoResponse>;
}