using GoodHamburger.Application.Utils.Mappers;
using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Errors;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.CriarPedido
{
    public class CriarPedidoCommandHandler : IRequestHandler<CriarPedidoCommand, Result<CriarPedidoResponse>>
    {
        private readonly IProdutoReadRepository _produtoRepository;
        private readonly IPedidoWriteRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarPedidoCommandHandler(
            IProdutoReadRepository produtoRepository,
            IPedidoWriteRepository pedidoRepository,
            IUnitOfWork unitOfWork)
        {
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CriarPedidoResponse>> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.ObterProdutosPorIdsAsync(request.ProdutosId);

            if (produtos is null || produtos.Count == 0 || produtos.Count != request.ProdutosId.Count)
                return Result<CriarPedidoResponse>.Failure(ProdutoError.ProdutosNaoEncontrados);

            var produtoDuplicado = produtos.GroupBy(p => p.Categoria).Any(g => g.Count() > 1);

            if (produtoDuplicado)
            {
                return Result<CriarPedidoResponse>.Failure(PedidoError.PedidoComItemDuplicado);
            }

            var pedido = new Pedido();

            foreach (var produto in produtos)
                pedido.AdicionarItem(produto);

            await _pedidoRepository.AdicionarPedidoAsync(pedido);

            var commitResult = await _unitOfWork.CommitAsync();

            if (!commitResult)
                return Result<CriarPedidoResponse>.Failure(BancoError.TransacaoFalhou);

            var response = pedido.ToCriarPedidoResponse();

            return Result<CriarPedidoResponse>.Success(response);
        }
    }
}