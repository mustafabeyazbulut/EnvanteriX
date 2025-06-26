using MediatR;

namespace EnvanteriX.Application.Features.Commands.ModelCommands
{
    public class DeleteModelCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public DeleteModelCommand(int id)
        {
            Id = id;
        }
    }
}
