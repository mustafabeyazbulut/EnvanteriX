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
    public class GetAllPortal365sQueryHandler : BaseHandler, IRequestHandler<GetAllPortal365sQuery, List<GetAllPortal365sQueryResult>>
    {
        public GetAllPortal365sQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public Task<List<GetAllPortal365sQueryResult>> Handle(GetAllPortal365sQuery request, CancellationToken cancellationToken)
        {
            var portal365s = _unitOfWork.GetReadRepository<Portal365>().GetAllAsync();
            return Task.FromResult(_mapper.Map<GetAllPortal365sQueryResult, Portal365>(portal365s.Result).ToList());
        }
    }
}
