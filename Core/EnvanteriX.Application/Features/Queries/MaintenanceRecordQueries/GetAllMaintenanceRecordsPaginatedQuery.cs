using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries
{
    /// <summary>
    /// Sayfalama ve filtreleme destekli bakım kayıtları listesi sorgusu
    /// </summary>
    public class GetAllMaintenanceRecordsPaginatedQuery : IRequest<PaginatedList<GetAllMaintenanceRecordsQueryResult>>
    {
        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Filtreleme
        public string? SearchTerm { get; set; }
        public int? AssetId { get; set; }
        public int? VendorId { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public bool? IsCompleted { get; set; } // EndDate null mu değil mi
        public bool? IsDeleted { get; set; }
    }
}
