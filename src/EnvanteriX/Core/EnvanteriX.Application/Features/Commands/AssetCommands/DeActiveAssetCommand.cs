using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetCommands
{
    public class DeActiveAssetCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
