using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.DepartmentQueries;
using EnvanteriX.Application.Features.Results.DepartmentResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.DepartmentHandlers
{
    public class GetAllActiveDepartmentsQueryHandler : BaseHandler, IRequestHandler<GetAllActiveDepartmentsQuery, List<GetAllActiveDepartmentsQueryResult>>
    {
        public GetAllActiveDepartmentsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllActiveDepartmentsQueryResult>> Handle(GetAllActiveDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments =await  _unitOfWork.GetReadRepository<Domain.Entities.Department>().GetAllAsync(x => x.IsDeleted == false);
            return _mapper.Map<GetAllActiveDepartmentsQueryResult, Domain.Entities.Department>(departments).ToList();
        }
    }
}
