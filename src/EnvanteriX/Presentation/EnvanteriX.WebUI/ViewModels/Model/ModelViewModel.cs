namespace EnvanteriX.WebUI.ViewModels.Model
{
    public class ModelViewModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
