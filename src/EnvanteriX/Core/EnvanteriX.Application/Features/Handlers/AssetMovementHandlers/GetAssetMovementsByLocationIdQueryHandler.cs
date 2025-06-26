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
    public class GetAssetMovementsByLocationIdQueryHandler : BaseHandler, IRequestHandler<GetAssetMovementsByLocationIdQuery, List<GetAssetMovementsByLocationIdQueryResult>>
    {
        public GetAssetMovementsByLocationIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAssetMovementsByLocationIdQueryResult>> Handle(GetAssetMovementsByLocationIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<AssetMovement>();
            var list = await repo.GetAllAsync(
                x => (x.FromLocationId == request.LocationId || x.ToLocationId == request.LocationId) && !x.IsDeleted);

            return _mapper.Map<List<GetAssetMovementsByLocationIdQueryResult>>(list);
        }
    }
}
