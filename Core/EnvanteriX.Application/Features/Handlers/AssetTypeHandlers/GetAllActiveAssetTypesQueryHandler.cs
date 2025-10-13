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
    public class GetAllActiveAssetTypesQueryHandler : BaseHandler, IRequestHandler<GetAllActiveAssetTypesQuery, List<GetAllActiveAssetTypesQueryResult>>
    {
        public GetAllActiveAssetTypesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllActiveAssetTypesQueryResult>> Handle(GetAllActiveAssetTypesQuery request, CancellationToken cancellationToken)
        {
            var assetTypes = await _unitOfWork.GetReadRepository<AssetType>().GetAllAsync(x=>x.IsDeleted==false);
            return _mapper.Map<GetAllActiveAssetTypesQueryResult, AssetType>(assetTypes).ToList();
        }
    }
}
