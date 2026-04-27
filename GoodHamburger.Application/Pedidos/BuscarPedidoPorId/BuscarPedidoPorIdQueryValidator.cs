using FluentValidation;
using GoodHamburger.Domain.Errors;

namespace GoodHamburger.Application.Pedidos.BuscarPedidoPorId
{
    public class BuscarPedidoPorIdQueryValidator : AbstractValidator<BuscarPedidoPorIdQuery>
    {
        public BuscarPedidoPorIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                    .WithMessage(PedidoError.IdInvalido.Message);
        }
    }
}