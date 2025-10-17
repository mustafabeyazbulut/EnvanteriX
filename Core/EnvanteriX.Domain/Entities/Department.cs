using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    public class Department : EntityBase, IEntityBase
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}
