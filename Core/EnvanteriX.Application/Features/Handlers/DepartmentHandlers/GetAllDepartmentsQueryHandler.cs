using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.DepartmentQueries;
using EnvanteriX.Application.Features.Results.DepartmentResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.DepartmentHandlers
{
    public class GetAllDepartmentsQueryHandler : BaseHandler, IRequestHandler<GetAllDepartmentsQuery, List<GetAllDepartmentsQueryResult>>
    {
        public GetAllDepartmentsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllDepartmentsQueryResult>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _unitOfWork.GetReadRepository<Domain.Entities.Department>().GetAllAsync();
            return _mapper.Map<GetAllDepartmentsQueryResult, Domain.Entities.Department>(departments).ToList();
        }
    }
}
