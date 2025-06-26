using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetTypeQueries;
using EnvanteriX.Application.Features.Results.AssetTypeResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.AssetTypeHandlers
{
    public class GetAllAssetTypesQueryHandler :BaseHandler, IRequestHandler<GetAllAssetTypesQuery, List<GetAllAssetTypesQueryResult>>
    {
        public GetAllAssetTypesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllAssetTypesQueryResult>> Handle(GetAllAssetTypesQuery request, CancellationToken cancellationToken)
        {
            var assetTypes = await _unitOfWork.GetReadRepository<AssetType>()
                                              .GetAllAsync(x => !x.IsDeleted);

            var result = _mapper.Map<List<GetAllAssetTypesQueryResult>>(assetTypes);
            return result;
        }
    }
}
