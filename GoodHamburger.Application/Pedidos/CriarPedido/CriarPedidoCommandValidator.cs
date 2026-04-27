using FluentValidation;
using GoodHamburger.Domain.Errors;

namespace GoodHamburger.Application.Pedidos.CriarPedido
{
    public class CriarPedidoCommandValidator : AbstractValidator<CriarPedidoCommand>
    {
        public CriarPedidoCommandValidator()
        {
            RuleFor(x => x.ProdutosId)
                .NotEmpty()
                    .WithMessage(PedidoError.PedidoEmBranco.Message)
                .NotNull()
                    .WithMessage(PedidoError.PedidoNull.Message)
                .Must(ids => ids != null && ids.Distinct().Count() == ids.Count)
                    .WithMessage(PedidoError.PedidoComItemDuplicado.Message);

            RuleForEach(x => x.ProdutosId)
                .GreaterThan(0)
                    .WithMessage(ProdutoError.ValoresNegativos.Message);

            RuleFor(x => x.ProdutosId.Count)
            .LessThanOrEqualTo(3)
                .When(x => x.ProdutosId != null)
                .WithMessage(PedidoError.MaximoDePedidos.Message);
        }
    }
}