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

        public MaintenanceRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/MaintenanceRecord
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _mediator.Send(new GetAllMaintenanceRecordsQuery());
            return Ok(records);
        }

        // GET: api/MaintenanceRecord/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _mediator.Send(new GetMaintenanceRecordByIdQuery(id));
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        // POST: api/MaintenanceRecord
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMaintenanceRecordCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/MaintenanceRecord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMaintenanceRecordCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID uyuşmuyor.");

            var updatedRecord = await _mediator.Send(command);
            if (updatedRecord == null)
                return NotFound();

            return Ok(updatedRecord);
        }

        // DELETE: api/MaintenanceRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteMaintenanceRecordCommand(id));
           
            return NoContent();
        }
    }
}
