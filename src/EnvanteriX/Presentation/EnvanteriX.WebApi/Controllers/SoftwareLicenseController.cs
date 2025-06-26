using EnvanteriX.Application.Features.Commands.SoftwareLicenseCommands;
using EnvanteriX.Application.Features.Queries.SoftwareLicenseQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareLicenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SoftwareLicenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSoftwareLicensesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSoftwareLicenseByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSoftwareLicenseCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.SoftwareLicenseId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSoftwareLicenseCommand command)
        {
            if (id != command.SoftwareLicenseId)
                return BadRequest("ID uyuşmuyor.");

            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteSoftwareLicenseCommand(id));
            return NoContent();
        }
    }
}
