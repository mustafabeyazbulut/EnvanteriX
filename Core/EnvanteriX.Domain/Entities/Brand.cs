using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    // +
    public class Brand : EntityBase, IEntityBase
    {
        public string BrandName { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
