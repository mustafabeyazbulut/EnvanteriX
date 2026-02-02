using EnvanteriX.Application.Features.Commands.UserCommands;
using EnvanteriX.Application.Features.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetAllActive() =>
                    Ok(await _mediator.Send(new GetAllActiveUsersQuery()));

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetAllUsersPaginatedQuery query) =>
            Ok(await _mediator.Send(query));

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("{Email}")]
        [Authorize(Roles = "user,admin")]

        public async Task<IActionResult> GetByEmail(string Email)
        {
            var result = await _mediator.Send(new GetUserByEmailQuery(Email));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> RemoveRole(RemoveUserRoleCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> AddRole(AddUserRoleCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Active(int id)
        {
            await _mediator.Send(new ActiveUserCommand { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeActive(int id)
        {
            await _mediator.Send(new DeActiveUserCommand { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
