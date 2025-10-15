using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries
{
    public class GetLastOpenMaintenanceRecordByAssetIdQuery: IRequest<GetLastOpenMaintenanceRecordByAssetIdQueryResult>
    {
        public int Id { get; set; }

        public GetLastOpenMaintenanceRecordByAssetIdQuery(int id)
        {
            Id = id;
        }
    }
}
