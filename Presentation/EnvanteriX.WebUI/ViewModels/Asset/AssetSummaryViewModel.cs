namespace EnvanteriX.WebUI.ViewModels.Asset
{
    public class AssetSummaryViewModel
    {
        // 🔹 1. Genel sayılar
        public int TotalAssets { get; set; }          // Toplam envanter sayısı
        public int InUseCount { get; set; }           // Kullanımda olan
        public int InStockCount { get; set; }         // Stokta bekleyen
        public int InServiceCount { get; set; }       // Serviste olan
        public int RetiredCount { get; set; }         // Kullanım dışı / hurda

        // 🔹 2. Zaman bazlı istatistikler
        public int AddedLast30Days { get; set; }      // Son 30 günde eklenen envanter
        public int ServiceLast30Days { get; set; }    // Son 30 günde servise giden

        // 🔹 3. Dağılımlar (grafik için)
        public Dictionary<string, int> AssetsByCategory { get; set; }   // Kategori bazlı dağılım
        public Dictionary<string, int> AssetsByLocation { get; set; }   // Lokasyon bazlı dağılım

        // 🔹 4. Kullanım bilgileri
        public Dictionary<string, int> AssetsByUser { get; set; }       // Kullanıcıya göre envanter sayısı
        public int MovementCountLast30Days { get; set; }                // Son 30 günde zimmet hareketi

        // 🔹 5. Servis performansı (isteğe bağlı)
        public double AverageServiceDurationDays { get; set; }          // Ortalama servis süresi (gün)
        public List<string> TopServiceCategories { get; set; }          // En çok servise giden kategoriler
    }
}
