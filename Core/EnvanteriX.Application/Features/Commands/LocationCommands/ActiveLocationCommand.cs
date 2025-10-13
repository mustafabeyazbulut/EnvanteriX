using MediatR;

namespace EnvanteriX.Application.Features.Commands.LocationCommands
{
    public class ActiveLocationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
