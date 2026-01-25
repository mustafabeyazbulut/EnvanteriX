namespace EnvanteriX.Application.Bases
{
    /// <summary>
    /// Sayfalanmış liste için genel yanıt sarmalayıcı sınıf
    /// </summary>
    /// <typeparam name="T">Liste öğe tipi</typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// Mevcut sayfadaki öğeler
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// Mevcut sayfa numarası (1-indexed)
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Sayfa başına öğe sayısı
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
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Sonraki sayfa var mı?
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedList()
        {
            Items = new List<T>();
        }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
