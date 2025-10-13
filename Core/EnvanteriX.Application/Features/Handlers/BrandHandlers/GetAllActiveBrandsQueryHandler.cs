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
    public class GetAllActiveBrandsQueryHandler : BaseHandler, IRequestHandler<GetAllActiveBrandsQuery, List<GetAllActiveBrandsQueryResult>>
    {
        public GetAllActiveBrandsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllActiveBrandsQueryResult>> Handle(GetAllActiveBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.GetReadRepository<Brand>().GetAllAsync(x=>x.IsDeleted==false);
            var map = _mapper.Map<GetAllActiveBrandsQueryResult, Brand>(brands);
            return map.ToList();
        }
    }
}
