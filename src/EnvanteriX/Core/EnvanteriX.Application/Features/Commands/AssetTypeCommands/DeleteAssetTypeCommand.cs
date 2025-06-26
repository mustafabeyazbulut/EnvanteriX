using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetTypeCommands
{
    public class DeleteAssetTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteAssetTypeCommand(int id)
        {
            Id = id;
        }
    }
}
