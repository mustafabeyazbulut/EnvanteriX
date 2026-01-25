namespace EnvanteriX.WebUI.ViewModels.Asset
{
    /// <summary>
    /// Sayfalanmış varlık listesi için view model
    /// </summary>
    public class PaginatedAssetViewModel
    {
        /// <summary>
        /// Mevcut sayfadaki varlıklar
        /// </summary>
        public List<AssetViewModel> Items { get; set; } = new List<AssetViewModel>();

        /// <summary>
        /// Mevcut sayfa numarası
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Toplam sayfa sayısı
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Toplam kayıt sayısı
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Önceki sayfa var mı?
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Sonraki sayfa var mı?
        /// </summary>
        public bool HasNextPage { get; set; }

        public PaginatedAssetViewModel()
        {
            Items = new List<AssetViewModel>();
        }

        // UI Dropdowns
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AssetTypes { get; set; } = new();
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Brands { get; set; } = new();
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Models { get; set; } = new();
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Locations { get; set; } = new();
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Vendors { get; set; } = new();
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Users { get; set; } = new();
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Departments { get; set; } = new();
    }
}
