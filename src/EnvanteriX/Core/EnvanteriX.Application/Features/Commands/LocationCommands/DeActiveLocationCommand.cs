using MediatR;

namespace EnvanteriX.Application.Features.Commands.LocationCommands
{
    public class DeActiveLocationCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
