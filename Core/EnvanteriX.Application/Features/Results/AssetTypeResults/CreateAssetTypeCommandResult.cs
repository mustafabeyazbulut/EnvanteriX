namespace EnvanteriX.Application.Features.Commands.AssetTypeCommands
{
    public class CreateAssetTypeCommandResult
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public CreateAssetTypeCommandResult(int id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }
    }
}
