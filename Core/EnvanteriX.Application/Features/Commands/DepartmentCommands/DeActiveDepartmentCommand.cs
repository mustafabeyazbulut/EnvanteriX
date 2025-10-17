using MediatR;

namespace EnvanteriX.Application.Features.Commands.DepartmentCommands
{
    public class DeActiveDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
