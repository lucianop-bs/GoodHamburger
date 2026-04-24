using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.CriarPedido
{
    public record class CriarPedidoCommand(List<int> ProdutosId) : IRequest<Result<PedidoResponse>>;
}