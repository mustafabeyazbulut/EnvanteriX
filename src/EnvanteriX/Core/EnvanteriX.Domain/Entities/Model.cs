using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    public class Model : EntityBase, IEntityBase
    {
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}
