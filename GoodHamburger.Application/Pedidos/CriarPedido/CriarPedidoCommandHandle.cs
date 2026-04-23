using GoodHamburger.Application.Utils.Mappers;
using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Pedidos.CriarPedido
{
    public class CriarPedidoCommandHandle : IRequestHandler<CriarPedidoCommand, PedidoResponse>
    {
        private readonly IProdutoReadRepository _produtoRepository;
        private readonly IPedidoWriteRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarPedidoCommandHandle(
            IProdutoReadRepository produtoRepository,
            IPedidoWriteRepository pedidoRepository,
            IUnitOfWork unitOfWork)
        {
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PedidoResponse> Handle(CriarPedidoCommand request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.ObterProdutosPorIdsAsync(request.ProdutosId);
            
            if (produtos.GroupBy(p => p.Categoria).Any(g => g.Count() > 1))
            {
                throw new Exception("Não é permitido adicionar mais de um produto da mesma categoria.");
            }

            var pedido = new Pedido();

            foreach (var produto in produtos)
                pedido.AdicionarItem(produto);

            var temSanduiche = pedido.Itens.Any(i => i.Categoria.ToString() == "Sanduiche");

            var temBebida = pedido.Itens.Any(i => i.Categoria.ToString() == "Bebida");

            var temAcompanhamento = pedido.Itens.Any(i => i.Categoria.ToString() == "Acompanhamento");

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

            await _pedidoRepository.AdicionarPedidoAsync(pedido);

            await _unitOfWork.CommitAsync();

            return pedido.ToPedidoResponse();
        }
    }
}