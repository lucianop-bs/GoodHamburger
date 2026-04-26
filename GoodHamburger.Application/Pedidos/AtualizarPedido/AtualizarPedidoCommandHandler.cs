using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Errors;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Pedidos.AtualizarPedido
{
    public class AtualizarPedidoCommandHandler : IRequestHandler<AtualizarPedidoCommand, Result>
    {
        private readonly IPedidoReadRepository _pedidoReadRepository;
        private readonly IPedidoWriteRepository _pedidoWriteRepository;
        private readonly IProdutoReadRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarPedidoCommandHandler(
            IPedidoReadRepository pedidoReadRepository,
            IPedidoWriteRepository pedidoWriteRepository,
            IProdutoReadRepository produtoRepository,
            IUnitOfWork unitOfWork)
        {
            _pedidoReadRepository = pedidoReadRepository;
            _pedidoWriteRepository = pedidoWriteRepository;
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AtualizarPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoReadRepository.ObterPedidoPorIdAsync(request.idPedido);

            if (pedido is null)
                return Result.Failure(PedidoError.PedidoNaoEncontrado);

            var produtos = await _produtoRepository.ObterProdutosPorIdsAsync(request.idProdutos);

            if (produtos is null || produtos.Count == 0)
                return Result.Failure(ProdutoError.ProdutoNaoEncontrado);

            var produtoDuplicado = produtos.GroupBy(p => p.Categoria).Any(g => g.Count() > 1);

            if (produtoDuplicado)
            {
                return Result.Failure(PedidoError.PedidoComItemDuplicado);
            }

            pedido.AtualizarPedido(produtos);

            var temSanduiche = pedido.Itens.Any(i => i.Categoria == Categoria.Sanduiche);
            var temBebida = pedido.Itens.Any(i => i.Categoria == Categoria.Bebida);
            var temAcompanhamento = pedido.Itens.Any(i => i.Categoria == Categoria.Acompanhamento);

            if (temSanduiche && temBebida && temAcompanhamento)
            {
                pedido.AplicarDesconto(20);
            }
            else if (temSanduiche && temBebida)
            {
                pedido.AplicarDesconto(15);
            }
            else if (temAcompanhamento && temSanduiche)
            {
                pedido.AplicarDesconto(10);
            }
            else
            {
                pedido.AplicarDesconto(0);
            }

            _pedidoWriteRepository.AtualizarPedido(pedido);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}