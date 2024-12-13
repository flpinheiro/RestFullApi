using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestFull.Domain.Core.Entities;
using RestFull.QueryService.Queries;

namespace RestFullApi.WebApi.Controllers;

/// <summary>
/// Product controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get([FromQuery] ProductPaginatedQuery dto, CancellationToken cancellationToken)
    {
        var products = await mediator.Send(dto, cancellationToken);
        return Ok(products);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Product>> Get([FromRoute]Guid id, CancellationToken cancellationToken) 
    {
        var query = new ProductByIdQuery(id);
        var product = await mediator.Send(query, cancellationToken); 
        return Ok(product);
    }
}
