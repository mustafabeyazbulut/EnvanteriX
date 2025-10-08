using EnvanteriX.Application.Features.Commands.AssetCommands;
using EnvanteriX.Application.Features.Commands.VendorCommands;
using EnvanteriX.Application.Features.Queries.VendorQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VendorController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllVendorsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetVendorByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVendorCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateVendorCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            await _mediator.Send(new DeleteVendorCommand(id));
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Active(int id)
        {
            await _mediator.Send(new ActiveVendorCommand { Id=id});
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            await _mediator.Send(new DeActiveVendorCommand { Id=id});
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
