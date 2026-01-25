using EnvanteriX.Application.Features.Commands.MaintenanceRecordCommands;
using EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class MaintenanceRecordController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MaintenanceRecordController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllMaintenanceRecordsQuery()));

        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetAllMaintenanceRecordsPaginatedQuery query) =>
            Ok(await _mediator.Send(query));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetMaintenanceRecordByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllByAssetId(int id)
        {
            var result = await _mediator.Send(new GetAllMaintenanceRecordByAssetIdQuery(id));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLastOpenMaintenanceRecordByAssetId(int id)
        {
            var result = await _mediator.Send(new GetLastOpenMaintenanceRecordByAssetIdQuery(id));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            await _mediator.Send(new DeleteMaintenanceRecordCommand(id));
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Active(int id)
        {
            await _mediator.Send(new ActiveMaintenanceRecordCommand { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            await _mediator.Send(new DeActiveMaintenanceRecordCommand { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
