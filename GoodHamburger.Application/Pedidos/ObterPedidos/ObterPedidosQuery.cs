using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.ObterPedidos
{
    public record class ObterPedidosQuery : IRequest<Result<List<PedidoResponse>>>;
}