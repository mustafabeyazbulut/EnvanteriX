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
    public class GetAssetByIdQueryHandler : BaseHandler, IRequestHandler<GetAssetByIdQuery, GetAssetByIdQueryResult>
    {
        public GetAssetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<GetAssetByIdQueryResult> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            // Repository üzerinden varlık (asset) sorgusu
            var asset = await _unitOfWork.GetReadRepository<Asset>().GetAsync(x => x.Id == request.AssetId && !x.IsDeleted);

            // Eğer varlık bulunamazsa hata fırlat
            if (asset == null)
                throw new Exception("Varlık bulunamadı.");

            // Entity'den DTO'ya mapleme
            var result = _mapper.Map<GetAssetByIdQueryResult>(asset);

            return result;
        }

    }
}
