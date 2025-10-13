using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetTypeCommands
{
    public class UpdateAssetTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public UpdateAssetTypeCommand(int id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }
    }
}
