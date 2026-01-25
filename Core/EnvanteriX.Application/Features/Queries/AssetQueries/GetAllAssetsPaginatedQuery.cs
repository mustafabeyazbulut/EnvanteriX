using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Results.AssetResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetQueries
{
    /// <summary>
    /// Sayfalama ve filtreleme destekli varlık listesi sorgusu
    /// </summary>
    public class GetAllAssetsPaginatedQuery : IRequest<PaginatedList<GetAllAssetsQueryResult>>
    {
        /// <summary>
        /// Sayfa numarası (1-indexed)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Sayfa başına kayıt sayısı
        /// </summary>
        public int PageSize { get; set; } = 10;

        // Filtreleme parametreleri

        /// <summary>
        /// Genel arama terimi (AssetTag ve SerialNumber'da arar)
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Varlık etiketi filtresi
        /// </summary>
        public string? AssetTag { get; set; }

        /// <summary>
        /// Seri numarası filtresi
        /// </summary>
        public string? SerialNumber { get; set; }

        /// <summary>
        /// Varlık tipi ID filtresi
        /// </summary>
        public int? AssetTypeId { get; set; }

        /// <summary>
        /// Marka ID filtresi
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// Model ID filtresi
        /// </summary>
        public int? ModelId { get; set; }

        /// <summary>
        /// Lokasyon ID filtresi
        /// </summary>
        public int? LocationId { get; set; }

        /// <summary>
        /// Tedarikçi ID filtresi
        /// </summary>
        public int? VendorId { get; set; }

        /// <summary>
        /// Atanan kullanıcı ID filtresi
        /// </summary>
        public int? AssignedUserId { get; set; }

        /// <summary>
        /// Atanan departman ID filtresi
        /// </summary>
        public int? AssignedDepartmentId { get; set; }

        /// <summary>
        /// Statü filtresi
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Kiralık mı filtresi
        /// </summary>
        public bool? IsRented { get; set; }

        /// <summary>
        /// Silinmiş mi filtresi
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}
