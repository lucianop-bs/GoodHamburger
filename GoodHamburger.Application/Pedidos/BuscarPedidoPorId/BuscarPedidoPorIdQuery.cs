using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.BuscarPedidoPorId
{
    public record BuscarPedidoPorIdQuery(Guid Id) : IRequest<Result<PedidoResponse>>;
}