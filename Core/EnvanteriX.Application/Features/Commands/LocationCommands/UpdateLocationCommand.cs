using MediatR;

namespace EnvanteriX.Application.Features.Commands.LocationCommands
{
    public class UpdateLocationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
    }
}
