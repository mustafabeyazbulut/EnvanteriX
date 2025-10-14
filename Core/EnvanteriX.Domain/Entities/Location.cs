using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    // +
    public class Location : EntityBase, IEntityBase
    {
        public string Building { get; set; }
        public string? Description { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}
