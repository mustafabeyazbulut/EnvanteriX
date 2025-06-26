using MediatR;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;

namespace EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries
{
    public class GetAllMaintenanceRecordsQuery : IRequest<List<GetAllMaintenanceRecordsQueryResult>> { }
}
