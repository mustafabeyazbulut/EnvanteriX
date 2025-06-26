using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetMovementQueries;
using EnvanteriX.Application.Features.Results.AssetMovementResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetMovementHandlers
{
    public class GetAssetMovementsByAssetIdQueryHandler : BaseHandler, IRequestHandler<GetAssetMovementsByAssetIdQuery, List<GetAssetMovementsByAssetIdQueryResult>>
    {
        public GetAssetMovementsByAssetIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAssetMovementsByAssetIdQueryResult>> Handle(GetAssetMovementsByAssetIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<AssetMovement>();
            var list = await repo.GetAllAsync(x => x.AssetId == request.AssetId && !x.IsDeleted);

            return _mapper.Map<List<GetAssetMovementsByAssetIdQueryResult>>(list);
        }
    }
}
