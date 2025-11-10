using EnvanteriX.Application.Features.Commands.BrandCommands;
using EnvanteriX.Application.Features.Queries.BrandQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]/[action]")]
[ApiController]
[Authorize(Roles = "admin")]

public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;
    public BrandController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _mediator.Send(new GetAllBrandsQuery()));

    [HttpGet]
    public async Task<IActionResult> GetAllActive() =>
    Ok(await _mediator.Send(new GetAllActiveBrandsQuery()));
    

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteBrandCommand(id));
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Active(int id)
    {
        await _mediator.Send(new ActiveBrandCommand { Id = id });
        return StatusCode(StatusCodes.Status200OK);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeActive(int id)
    {
        await _mediator.Send(new DeActiveBrandCommand { Id = id });
        return StatusCode(StatusCodes.Status200OK);
    }
}
