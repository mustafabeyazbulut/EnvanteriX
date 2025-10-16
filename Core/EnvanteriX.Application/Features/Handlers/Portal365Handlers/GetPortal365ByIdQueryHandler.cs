using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.Portal365Queries;
using EnvanteriX.Application.Features.Results.Portal365Results;
using EnvanteriX.Application.Features.Rules.Portal365Rules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.Portal365Handlers
{
    public class GetPortal365ByIdQueryHandler : BaseHandler, IRequestHandler<GetPortal365ByIdQuery, GetPortal365ByIdQueryResult>
    {
        private readonly Portal365Rules _portal365Rules;

        public GetPortal365ByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, Portal365Rules portal365Rules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _portal365Rules = portal365Rules;
        }

        public async Task<GetPortal365ByIdQueryResult> Handle(GetPortal365ByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Portal365>().GetAsync(x => x.Id == request.Id);
            await _portal365Rules.Portal365ShouldExist(model);
            return _mapper.Map<GetPortal365ByIdQueryResult, Portal365>(model);
        }
    }
}
