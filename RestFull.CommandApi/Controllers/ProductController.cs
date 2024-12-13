using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestFull.Domain.Core.Commands;

namespace RestFull.CommandApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddProductCommand([FromBody, FromForm] AddProductCommand command, CancellationToken cancellationToken)
    {
        var id = await mediator.Send(command, cancellationToken);
        return Created("Product Added with success", id);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody, FromForm]UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPatch("{id:Guid}/{rate:int}")]

    public async Task<IActionResult> RateProduct([FromRoute] Guid id, [FromRoute] int rate, CancellationToken cancellationToken)
    {
        var command = new RateProductCommand(id, rate);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
