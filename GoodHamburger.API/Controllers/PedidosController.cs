using GoodHamburger.Application.Produtos.ObterProdutos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : Controller
    {
        private readonly IMediator _mediator;

       public PedidosController(IMediator mediator) { 
            _mediator = mediator;
       }

        [HttpGet]
        public async Task<IActionResult> ObterLista()
        {
            var query = new ObterProdutosQuery();

            var resultado = await _mediator.Send(query);

            return Ok(resultado);
        }
    }
}
