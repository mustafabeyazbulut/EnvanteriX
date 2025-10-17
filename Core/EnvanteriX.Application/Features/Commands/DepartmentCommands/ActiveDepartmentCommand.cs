using MediatR;

namespace EnvanteriX.Application.Features.Commands.DepartmentCommands
{
    public class ActiveDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
