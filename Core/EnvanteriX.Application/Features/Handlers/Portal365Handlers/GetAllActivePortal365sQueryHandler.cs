using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.Portal365Queries;
using EnvanteriX.Application.Features.Results.Portal365Results;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.Portal365Handlers
{
    public class GetAllActivePortal365sQueryHandler : BaseHandler, IRequestHandler<GetAllActivePortal365sQuery, List<GetAllActivePortal365sQueryResult>>
    {
        public GetAllActivePortal365sQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllActivePortal365sQueryResult>> Handle(GetAllActivePortal365sQuery request, CancellationToken cancellationToken)
        {
            var portal365s =await _unitOfWork.GetReadRepository<Portal365>().GetAllAsync(p => !p.IsDeleted);
            return _mapper.Map<GetAllActivePortal365sQueryResult, Portal365>(portal365s).ToList();
        }
    }
}
