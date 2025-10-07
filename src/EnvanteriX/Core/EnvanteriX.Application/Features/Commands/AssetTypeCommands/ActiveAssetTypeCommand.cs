using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetTypeCommands
{
    public class ActiveAssetTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
