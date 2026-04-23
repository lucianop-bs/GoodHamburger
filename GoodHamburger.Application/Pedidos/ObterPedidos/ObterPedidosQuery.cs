using GoodHamburger.Application.Utils.Responses;
using MediatR;

namespace GoodHamburger.Application.Pedidos.ObterPedidos
{
    public record class ObterPedidosQuery : IRequest<List<PedidoResponse>>;
}