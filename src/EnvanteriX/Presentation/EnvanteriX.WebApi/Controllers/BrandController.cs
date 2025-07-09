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
    public async Task<IActionResult> GetById( int id)
    {
        var result = await _mediator.Send(new GetBrandByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBrandCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateBrandCommand command)
    {
        await _mediator.Send(command);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteBrandCommand request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK);
    }
}
