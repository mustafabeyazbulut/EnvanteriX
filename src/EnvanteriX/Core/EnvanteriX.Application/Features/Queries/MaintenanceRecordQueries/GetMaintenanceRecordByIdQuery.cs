using MediatR;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;

namespace EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries
{
    public class GetMaintenanceRecordByIdQuery : IRequest<GetMaintenanceRecordByIdQueryResult>
    {
        public int Id { get; set; }

        public GetMaintenanceRecordByIdQuery(int id)
        {
            Id = id;
        }
    }
}
