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
    public class GetMaintenanceRecordByIdQueryHandler : BaseHandler, IRequestHandler<GetMaintenanceRecordByIdQuery, GetMaintenanceRecordByIdQueryResult>
    {
        public GetMaintenanceRecordByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<GetMaintenanceRecordByIdQueryResult> Handle(GetMaintenanceRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetReadRepository<MaintenanceRecord>();
            var entity = await repository.GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (entity == null) 
                throw new KeyNotFoundException($"Maintenance record with ID {request.Id} not found.");

            var result = _mapper.Map<GetMaintenanceRecordByIdQueryResult>(entity);
            return result;
        }
    }
}
