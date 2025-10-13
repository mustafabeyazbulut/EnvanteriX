using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries
{
    public class GetAllMaintenanceRecordByAssetIdQuery :IRequest<List<GetAllMaintenanceRecordByAssetIdQueryResult>>
    {
        public int Id { get; set; }

        public GetAllMaintenanceRecordByAssetIdQuery(int id)
        {
            Id = id;
        }
    }
}
