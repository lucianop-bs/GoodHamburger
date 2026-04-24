using GoodHamburger.Application.Utils.Mappers;
using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.ObterPedidos
{
    public class ObterPedidosQueryHandler : IRequestHandler<ObterPedidosQuery, Result<List<PedidoResponse>>>
    {
        private readonly IPedidoReadRepository _pedidoRepository;

        public ObterPedidosQueryHandler(IPedidoReadRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Result<List<PedidoResponse>>> Handle(ObterPedidosQuery request, CancellationToken cancellationToken)
        {
            var pedidos = await _pedidoRepository.ObterPedidosAsync();

            var response = pedidos.ToPedidosResponse();

            return Result<List<PedidoResponse>>.Success(response);
        }
    }
}