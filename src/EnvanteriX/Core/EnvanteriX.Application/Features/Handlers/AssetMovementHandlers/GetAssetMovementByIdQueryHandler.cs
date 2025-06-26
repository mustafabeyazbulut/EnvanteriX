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
    public class GetAssetMovementByIdQueryHandler : BaseHandler, IRequestHandler<GetAssetMovementByIdQuery, GetAssetMovementByIdQueryResult>
    {
        public GetAssetMovementByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<GetAssetMovementByIdQueryResult> Handle(GetAssetMovementByIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<AssetMovement>();
            var entity = await repo.GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            return _mapper.Map<GetAssetMovementByIdQueryResult>(entity);
        }
    }
}
