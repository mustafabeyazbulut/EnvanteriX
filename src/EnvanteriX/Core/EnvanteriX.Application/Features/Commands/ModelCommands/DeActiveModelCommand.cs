using MediatR;

namespace EnvanteriX.Application.Features.Commands.ModelCommands
{
    public class DeActiveModelCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
