using GoodHamburger.Application.Pedidos.CriarPedido;
using GoodHamburger.Application.Pedidos.ObterPedidos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : Controller
    {
        private readonly IMediator _mediator;

        public PedidosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObterPedidos()
        {
            var query = new ObterPedidosQuery();

            var resultado = await _mediator.Send(query);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoCommand command)
        {
            var resultado = await _mediator.Send(command);

            return Ok(resultado);
        }
    }
}