using EnvanteriX.Application.Features.Commands.LocationCommands;
using EnvanteriX.Application.Features.Queries.LocationQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LocationController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllLocationsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetLocationByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLocationCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteLocationCommand request)
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
