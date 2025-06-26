using MediatR;
using EnvanteriX.Application.Features.Results.LocationResults;

namespace EnvanteriX.Application.Features.Commands.LocationCommands
{
    public class UpdateLocationCommand : IRequest<UpdateLocationCommandResult>
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
    }
}
