using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetTypeCommands
{
    public class CreateAssetTypeCommand : IRequest<CreateAssetTypeCommandResult>
    {
        public string TypeName { get; set; }

        public CreateAssetTypeCommand(string typeName)
        {
            TypeName = typeName;
        }
    }
}
