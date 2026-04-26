using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.DeletarPedido
{
    public record DeletarPedidoCommand(Guid Id) : IRequest<Result>;
}