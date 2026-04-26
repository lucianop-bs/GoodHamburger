using GoodHamburger.Application.Utils.Mappers;
using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Errors;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.BuscarPedidoPorId
{
    public class BuscarPedidoPorIdQueryHandler : IRequestHandler<BuscarPedidoPorIdQuery, Result<PedidoResponse>>
    {
        private readonly IPedidoReadRepository _pedidoRepository;

        public BuscarPedidoPorIdQueryHandler(IPedidoReadRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Result<PedidoResponse>> Handle(BuscarPedidoPorIdQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorIdAsync(request.Id);

            if (pedido is null)
                return Result<PedidoResponse>.Failure(PedidoError.PedidoNaoEncontrado);

            return Result<PedidoResponse>.Success(pedido.ToPedidoResponse());
        }
    }
}