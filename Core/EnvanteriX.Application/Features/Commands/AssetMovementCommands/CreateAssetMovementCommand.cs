using EnvanteriX.Application.Features.Results.AssetMovementResults;
using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetMovementCommands
{
    public class CreateAssetMovementCommand : IRequest<CreateAssetMovementCommandResult>
    {
        public int AssetId { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }
        public string Note { get; set; }
    }
}
