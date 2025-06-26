using MediatR;

namespace EnvanteriX.Application.Features.Commands.BrandCommands
{
    public class DeleteBrandCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteBrandCommand(int id)
        {
            Id = id;
        }
    }
}
