using EnvanteriX.Application.Features.Commands.BrandCommands;
using EnvanteriX.Application.Features.Queries.BrandQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;
    public BrandController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _mediator.Send(new GetAllBrandsQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetBrandByIdQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update( UpdateBrandCommand command)
    {
        var result = await _mediator.Send(command);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
         await _mediator.Send(new DeleteBrandCommand (id));
        return NoContent() ;
    }
}
