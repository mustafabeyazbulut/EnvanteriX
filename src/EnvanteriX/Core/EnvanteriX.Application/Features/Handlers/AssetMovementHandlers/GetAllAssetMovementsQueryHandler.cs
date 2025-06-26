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
    public class GetAllAssetMovementsQueryHandler : BaseHandler, IRequestHandler<GetAllAssetMovementsQuery, List<GetAllAssetMovementsQueryResult>>
    {
        public GetAllAssetMovementsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllAssetMovementsQueryResult>> Handle(GetAllAssetMovementsQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<AssetMovement>();
            var list = await repo.GetAllAsync(x => !x.IsDeleted);

            return _mapper.Map<List<GetAllAssetMovementsQueryResult>>(list);
        }
    }
}
