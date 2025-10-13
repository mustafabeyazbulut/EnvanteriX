using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.BrandQueries;
using EnvanteriX.Application.Features.Results.BrandResults;
using EnvanteriX.Application.Features.Rules.BrandRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.BrandHandlers
{
    public class GetBrandByIdQueryHandler : BaseHandler, IRequestHandler<GetBrandByIdQuery, GetBrandByIdQueryResult>
    {
        private readonly BrandRules _brandRules;
        public GetBrandByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, BrandRules brandRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _brandRules = brandRules;
        }
        public async Task<GetBrandByIdQueryResult> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.GetReadRepository<Brand>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            await _brandRules.BrandShouldExist(brand);
            return _mapper.Map<GetBrandByIdQueryResult,Brand>(brand);
        }
    }
}
