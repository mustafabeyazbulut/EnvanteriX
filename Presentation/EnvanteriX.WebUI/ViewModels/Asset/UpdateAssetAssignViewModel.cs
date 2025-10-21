using EnvanteriX.WebUI.Enums;

namespace EnvanteriX.WebUI.ViewModels.Asset
{
    public class UpdateAssetAssignViewModel
    {
        public int Id { get; set; }
        public int? AssignedUserId { get; set; }
        public int? AssignedDepartmentId { get; set; }
        public StatusEnum? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
