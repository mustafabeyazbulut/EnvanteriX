using MediatR;

namespace EnvanteriX.Application.Features.Commands.LocationCommands
{
    public class DeleteLocationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
