using EnvanteriX.Application.Features.Commands.DepartmentCommands;
using EnvanteriX.Application.Features.Queries.DepartmentQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnvanteriX.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllDepartmentsQuery()));

        [HttpGet]
        public async Task<IActionResult> GetAllActive() =>Ok(await _mediator.Send(new GetAllActiveDepartmentsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetDepartmentByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDepartmentCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteDepartmentCommand(id));
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Active(int id)
        {
            await _mediator.Send(new ActiveDepartmentCommand { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeActive(int id)
        {
            await _mediator.Send(new DeActiveDepartmentCommand { Id = id });
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
