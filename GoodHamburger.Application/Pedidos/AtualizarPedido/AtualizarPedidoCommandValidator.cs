using FluentValidation;
using GoodHamburger.Domain.Errors;

namespace GoodHamburger.Application.Pedidos.AtualizarPedido
{
    public class AtualizarPedidoCommandValidator : AbstractValidator<AtualizarPedidoCommand>
    {
        public AtualizarPedidoCommandValidator()
        {
            RuleFor(x => x.IdPedido)
                .NotEmpty()
                    .WithMessage(PedidoError.IdInvalido.Message);

            RuleFor(x => x.IdProdutos)
                .NotNull()
                    .WithMessage(PedidoError.PedidoNull.Message)
                .NotEmpty()
                    .WithMessage(PedidoError.PedidoEmBranco.Message);

            When(x => x.IdProdutos != null, () =>
            {
                RuleFor(x => x.IdProdutos.Count)
                    .LessThanOrEqualTo(3)
                        .WithMessage(PedidoError.MaximoDePedidos.Message);

                RuleForEach(x => x.IdProdutos)
                    .GreaterThan(0)
                        .WithMessage(ProdutoError.ValoresNegativos.Message);
            });
        }
    }
}