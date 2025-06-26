using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetQueries;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class GetAssetsByTypeQueryHandler : BaseHandler, IRequestHandler<GetAssetsByTypeQuery, List<GetAssetsByTypeQueryResult>>
    {
        public GetAssetsByTypeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAssetsByTypeQueryResult>> Handle(GetAssetsByTypeQuery request, CancellationToken cancellationToken)
        {
            // İlgili AssetTypeId'ye göre filtrelenmiş asset'leri getir
            var assets = await _unitOfWork.GetReadRepository<Asset>()
                .GetAllAsync(x => x.AssetTypeId == request.AssetTypeId && !x.IsDeleted);

            // Entity listesini DTO listesine map et
            var result = _mapper.Map<List<GetAssetsByTypeQueryResult>>(assets);

            return result;
        }

    }
}
