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
    public class GetAssetMovementsByUserIdQueryHandler : BaseHandler, IRequestHandler<GetAssetMovementsByUserIdQuery, List<GetAssetMovementsByUserIdQueryResult>>
    {
        public GetAssetMovementsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }
        public async Task<List<GetAssetMovementsByUserIdQueryResult>> Handle(GetAssetMovementsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<AssetMovement>();
            var list = await repo.GetAllAsync(
                x => (x.FromUserId == request.UserId || x.ToUserId == request.UserId) && !x.IsDeleted);

            return _mapper.Map<List<GetAssetMovementsByUserIdQueryResult>>(list);
        }
    }
}
