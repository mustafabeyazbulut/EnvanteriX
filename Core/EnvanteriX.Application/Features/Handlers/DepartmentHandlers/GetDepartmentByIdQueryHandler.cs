using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.DepartmentQueries;
using EnvanteriX.Application.Features.Results.DepartmentResults;
using EnvanteriX.Application.Features.Rules.DepartmentRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.DepartmentHandlers
{
    public class GetDepartmentByIdQueryHandler : BaseHandler, IRequestHandler<GetDepartmentByIdQuery, GetDepartmentByIdQueryResult>
    {
        private readonly DepartmentRules _departmentRules;
        public GetDepartmentByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, DepartmentRules departmentRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _departmentRules = departmentRules;
        }

        public async Task<GetDepartmentByIdQueryResult> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department =await _unitOfWork.GetReadRepository<Department>().GetAsync(d => d.Id == request.Id);
            await _departmentRules.DepartmentShouldExist(department);
            return _mapper.Map<GetDepartmentByIdQueryResult, Department>(department);
        }
    }
}
