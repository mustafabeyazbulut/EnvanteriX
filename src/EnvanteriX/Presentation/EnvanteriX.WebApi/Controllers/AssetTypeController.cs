using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using EnvanteriX.Application.Features.Queries.AssetTypeQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/AssetType
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAssetTypesQuery());
            return Ok(result);
        }

        // GET: api/AssetType/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAssetTypeByIdQuery(id));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/AssetType
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssetTypeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/AssetType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAssetTypeCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID uyuşmuyor.");

            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE: api/AssetType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             await _mediator.Send(new DeleteAssetTypeCommand(id));
            return NoContent() ;
        }
    }
}
