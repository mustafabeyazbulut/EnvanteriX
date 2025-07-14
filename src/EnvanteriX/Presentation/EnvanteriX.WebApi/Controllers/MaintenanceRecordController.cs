using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRecordController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MaintenanceRecordController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllMaintenanceRecordsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetMaintenanceRecordByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaintenanceRecordCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMaintenanceRecordCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteMaintenanceRecordCommand request)
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
