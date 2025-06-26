using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.MaintenanceRecordQueries;
using EnvanteriX.Application.Features.Results.MaintenanceRecordResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.MaintenanceRecordHandlers
{
    public class GetAllMaintenanceRecordsQueryHandler : BaseHandler, IRequestHandler<GetAllMaintenanceRecordsQuery, List<GetAllMaintenanceRecordsQueryResult>>
    {
        public GetAllMaintenanceRecordsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<List<GetAllMaintenanceRecordsQueryResult>> Handle(GetAllMaintenanceRecordsQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetReadRepository<MaintenanceRecord>();
            var entities = await repository.GetAllAsync(x => !x.IsDeleted);

            var result = _mapper.Map<List<GetAllMaintenanceRecordsQueryResult>>(entities);
            return result;
        }
    }
}
