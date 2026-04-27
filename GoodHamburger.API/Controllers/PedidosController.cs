using GoodHamburger.Application.Pedidos.AtualizarPedido;
using GoodHamburger.Application.Pedidos.BuscarPedidoPorId;
using GoodHamburger.Application.Pedidos.CriarPedido;
using GoodHamburger.Application.Pedidos.DeletarPedido;
using GoodHamburger.Application.Pedidos.ObterPedidos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObterPedidos()
    {
        var query = new ObterPedidosQuery();

        var resultado = await mediator.Send(query);

        return Ok(resultado);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPedidoPorId([FromRoute] Guid id)
    {
        var query = new BuscarPedidoPorIdQuery(id);

        var resultado = await mediator.Send(query);

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.IsFailure)
            return Ok(resultado);

        return CreatedAtAction(
                    nameof(ObterPedidoPorId),
                    new { id = resultado.Value?.Id },
                    resultado);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> AtualizarPedido([FromBody] List<int> produtos, [FromRoute] Guid id)
    {
        var resultado = await mediator.Send(new AtualizarPedidoCommand(id, produtos));

        return Ok(resultado);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletarPedido([FromRoute] Guid id)
    {
        var command = new DeletarPedidoCommand(id);
        var resultado = await mediator.Send(command);

        return Ok(resultado);
    }
}