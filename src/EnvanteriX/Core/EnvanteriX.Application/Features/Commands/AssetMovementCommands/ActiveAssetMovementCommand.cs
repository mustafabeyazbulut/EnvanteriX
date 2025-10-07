using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetMovementCommands
{
    public class ActiveAssetMovementCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
