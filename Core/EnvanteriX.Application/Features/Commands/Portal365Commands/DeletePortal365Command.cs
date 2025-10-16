using MediatR;

namespace EnvanteriX.Application.Features.Commands.Portal365Commands
{
    public class DeletePortal365Command:IRequest<Unit>
    {
        public DeletePortal365Command(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
