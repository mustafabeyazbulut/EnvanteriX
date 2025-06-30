using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.BrandQueries;
using EnvanteriX.Application.Features.Results.BrandResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.BrandHandlers
{
    public class GetAllBrandsQueryHandler : BaseHandler, IRequestHandler<GetAllBrandsQuery, List<GetAllBrandsQueryResult>>
    {
        public GetAllBrandsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllBrandsQueryResult>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.GetReadRepository<Brand>().GetAllAsync();
            var map = _mapper.Map<GetAllBrandsQueryResult, Brand>(brands);
            return map.ToList();
        }
    }
}
