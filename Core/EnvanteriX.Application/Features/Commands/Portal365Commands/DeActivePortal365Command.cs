using MediatR;

namespace EnvanteriX.Application.Features.Commands.Portal365Commands
{
    public class DeActivePortal365Command : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
