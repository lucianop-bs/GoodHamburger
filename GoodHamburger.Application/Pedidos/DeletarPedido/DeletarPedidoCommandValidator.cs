using FluentValidation;
using GoodHamburger.Domain.Errors;

namespace GoodHamburger.Application.Pedidos.DeletarPedido
{
    public class DeletarPedidoCommandValidator : AbstractValidator<DeletarPedidoCommand>
    {
        public DeletarPedidoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                    .WithMessage(PedidoError.IdInvalido.Message);
        }
    }
}