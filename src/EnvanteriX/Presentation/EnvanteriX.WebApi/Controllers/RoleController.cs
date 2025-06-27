using EnvanteriX.Application.Features.Commands.RoleCommands;
using EnvanteriX.Application.Features.Queries.RoleQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery(id));
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create( CreateRoleCommand command)
        {
            var createdRole = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, createdRole);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteRoleCommand request)
        {
            await _mediator.Send(request);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
