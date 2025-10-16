using EnvanteriX.Domain.Common;

namespace EnvanteriX.Domain.Entities
{
    public class Portal365 : EntityBase, IEntityBase
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SenderEmail { get; set; }
    }
}
