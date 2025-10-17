using EnvanteriX.Application.Features.Results.DepartmentResults;
using MediatR;

namespace EnvanteriX.Application.Features.Commands.DepartmentCommands
{
    public class CreateDepartmentCommand :IRequest<CreateDepartmentCommandResult>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
