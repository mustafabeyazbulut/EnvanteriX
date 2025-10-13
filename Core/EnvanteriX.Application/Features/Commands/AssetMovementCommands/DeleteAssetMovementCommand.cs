using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetMovementCommands
{
    public class DeleteAssetMovementCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteAssetMovementCommand(int id)
        {
            Id = id;
        }
    }
}
