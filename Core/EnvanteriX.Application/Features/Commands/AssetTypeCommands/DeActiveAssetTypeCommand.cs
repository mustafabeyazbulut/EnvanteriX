using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetTypeCommands
{
    public class DeActiveAssetTypeCommand: IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
