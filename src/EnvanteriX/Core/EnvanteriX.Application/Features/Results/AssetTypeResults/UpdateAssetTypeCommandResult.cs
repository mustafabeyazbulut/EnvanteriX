namespace EnvanteriX.Application.Features.Commands.AssetTypeCommands
{
    public class UpdateAssetTypeCommandResult
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public UpdateAssetTypeCommandResult(int id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }
    }
}
