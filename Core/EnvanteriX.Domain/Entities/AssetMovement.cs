using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    // +
    public class AssetMovement : EntityBase, IEntityBase
    {
        public int AssetId { get; set; }
        public Asset Asset { get; set; }

        public int? FromUserId { get; set; }
        public User FromUser { get; set; }

        public int? ToUserId { get; set; }
        public User ToUser { get; set; }

        public int? FromLocationId { get; set; }
        public Location FromLocation { get; set; }

        public int? ToLocationId { get; set; }
        public Location ToLocation { get; set; }
        public string Note { get; set; }
    }

}
