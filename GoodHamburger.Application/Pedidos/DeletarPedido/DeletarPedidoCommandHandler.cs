using GoodHamburger.Domain.Errors;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.DeletarPedido
{
    public class DeletarPedidoCommandHandler : IRequestHandler<DeletarPedidoCommand, Result>
    {
        private readonly IPedidoReadRepository _pedidoRepository;
        private readonly IPedidoWriteRepository _pedidoWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarPedidoCommandHandler(IPedidoReadRepository pedidoRepository, IPedidoWriteRepository pedidoWriteRepository, IUnitOfWork unitOfWork)
        {
            _pedidoRepository = pedidoRepository;
            _pedidoWriteRepository = pedidoWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeletarPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorIdAsync(request.Id);

            if (pedido is null)
                return Result.Failure(PedidoError.PedidoNaoEncontrado);

            _pedidoWriteRepository.DeletarPedido(pedido);
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }
    }
}