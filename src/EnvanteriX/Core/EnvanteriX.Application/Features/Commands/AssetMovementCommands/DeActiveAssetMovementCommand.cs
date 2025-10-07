using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetMovementCommands
{
    public class DeActiveAssetMovementCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
