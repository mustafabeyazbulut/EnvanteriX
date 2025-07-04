using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    // +
    public class AssetType : EntityBase, IEntityBase
    {
        public string TypeName { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
