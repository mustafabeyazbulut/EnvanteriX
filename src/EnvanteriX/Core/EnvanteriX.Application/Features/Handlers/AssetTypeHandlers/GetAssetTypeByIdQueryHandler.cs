using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetTypeQueries;
using EnvanteriX.Application.Features.Results.AssetTypeResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace EnvanteriX.Application.Features.Handlers.AssetTypeHandlers
{
    public class GetAssetTypeByIdQueryHandler : BaseHandler, IRequestHandler<GetAssetTypeByIdQuery, GetAssetTypeByIdQueryResult>
    {
        public GetAssetTypeByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<GetAssetTypeByIdQueryResult> Handle(GetAssetTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var assetType = await _unitOfWork.GetReadRepository<AssetType>()
                                             .GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (assetType == null)
                throw new NotFoundException($"AssetType with ID {request.Id} not found.");

            return _mapper.Map<GetAssetTypeByIdQueryResult>(assetType);
        }
    }
}
