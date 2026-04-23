using GoodHamburger.Application.Utils.Mappers;
using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Pedidos.ObterPedidos
{
    public class ObterPedidosQueryHandler : IRequestHandler<ObterPedidosQuery, List<PedidoResponse>>
    {
        private readonly IPedidoReadRepository _pedidoRepository;

        public ObterPedidosQueryHandler(IPedidoReadRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<PedidoResponse>> Handle(ObterPedidosQuery request, CancellationToken cancellationToken)
        {
            var pedidos = await _pedidoRepository.ObterPedidosAsync();

            return pedidos.ToPedidosResponse();
        }
    }
}