using EnvanteriX.Application.Features.Commands.AssetCommands;
using EnvanteriX.Application.Features.Queries.AssetQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAssetsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAssetByIdQuery (id));
            return Ok(result);
        }

        [HttpGet("type/{assetTypeId}")]
        public async Task<IActionResult> GetByType(int assetTypeId)
        {
            var result = await _mediator.Send(new GetAssetsByTypeQuery(assetTypeId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssetCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.AssetId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAssetCommand command)
        {
            if (id != command.AssetId)
                return BadRequest("ID uyuşmuyor.");

            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteAssetCommand { AssetId = id });
            return NoContent();
        }
    }
}
