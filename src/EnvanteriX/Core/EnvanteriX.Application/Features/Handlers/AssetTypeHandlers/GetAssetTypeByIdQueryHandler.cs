using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetTypeQueries;
using EnvanteriX.Application.Features.Results.AssetTypeResults;
using EnvanteriX.Application.Features.Rules.AssetTypeRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetTypeHandlers
{
    public class GetAssetTypeByIdQueryHandler : BaseHandler, IRequestHandler<GetAssetTypeByIdQuery, GetAssetTypeByIdQueryResult>
    {
        private readonly AssetTypeRules _assetTypeRules;
        public GetAssetTypeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetTypeRules assetTypeRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetTypeRules = assetTypeRules;
        }

        public async Task<GetAssetTypeByIdQueryResult> Handle(GetAssetTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var assetType = await _unitOfWork.GetReadRepository<AssetType>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            await _assetTypeRules.AssetTypeShouldExist(assetType);
            return _mapper.Map<GetAssetTypeByIdQueryResult, AssetType>(assetType);
        }
    }
}
