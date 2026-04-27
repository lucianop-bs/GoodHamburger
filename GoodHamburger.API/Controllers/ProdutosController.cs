using GoodHamburger.Application.Produtos.ObterProdutos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObterLista()
    {
        var query = new ObterProdutosQuery();
        var resultado = await mediator.Send(query);
        return Ok(resultado);
    }
}