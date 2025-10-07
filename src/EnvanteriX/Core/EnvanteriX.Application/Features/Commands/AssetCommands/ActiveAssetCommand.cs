using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetCommands
{
    public class ActiveAssetCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
