using MediatR;

namespace EnvanteriX.Application.Features.Commands.ModelCommands
{
    public class ActiveModelCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
