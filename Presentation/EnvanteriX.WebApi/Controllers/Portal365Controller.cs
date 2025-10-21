using EnvanteriX.Application.Features.Commands.Portal365Commands;
using EnvanteriX.Application.Features.Queries.Portal365Queries;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class Portal365Controller : ControllerBase
    {
        private readonly IMediator _mediator;
        public Portal365Controller(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
          Ok(await _mediator.Send(new GetAllPortal365sQuery()));

        [HttpGet]
        public async Task<IActionResult> GetAllActive() =>
      Ok(await _mediator.Send(new GetAllActivePortal365sQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPortal365ByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePortal365Command command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePortal365Command command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePortal365Command(id));
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Active(int id)
        {
            await _mediator.Send(new ActivePortal365Command { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            await _mediator.Send(new DeActivePortal365Command { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TestConnection(int id)
        {
            var token = await _mediator.Send(new TestPortal365ConnectionQuery { Id = id });
            return Ok(token);
        }

        [HttpGet]
        public async Task<IActionResult> SyncUsers()
        {
            var result = await _mediator.Send(new SyncPortal365UsersQuery());
            return Ok(result);
        }
    }
}
