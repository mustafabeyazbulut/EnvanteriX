using MediatR;

namespace EnvanteriX.Application.Features.Commands.DepartmentCommands
{
    public class DeleteDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }
    }
}
