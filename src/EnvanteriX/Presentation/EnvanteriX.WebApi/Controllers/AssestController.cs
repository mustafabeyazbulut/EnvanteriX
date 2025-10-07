using EnvanteriX.Application.Features.Commands.AssetCommands;
using EnvanteriX.Application.Features.Queries.AssetQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AssetController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllAssetsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAssetByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAssetCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAssetCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteAssetCommand request)
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Active(int id)
        {
            await _mediator.Send(new ActiveAssetCommand { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            await _mediator.Send(new DeActiveAssetCommand { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}