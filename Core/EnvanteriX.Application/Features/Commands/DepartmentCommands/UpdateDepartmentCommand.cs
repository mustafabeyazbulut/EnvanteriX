using MediatR;

namespace EnvanteriX.Application.Features.Commands.DepartmentCommands
{
    public class UpdateDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
