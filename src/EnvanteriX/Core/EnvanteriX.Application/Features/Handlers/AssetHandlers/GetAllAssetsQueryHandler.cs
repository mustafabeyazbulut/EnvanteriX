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
    public class GetAllAssetsQueryHandler : BaseHandler, IRequestHandler<GetAllAssetsQuery, List<GetAllAssetsQueryResult>>
    {
        public GetAllAssetsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllAssetsQueryResult>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
        {
            // Veritabanından silinmemiş tüm asset'leri getir
            var assets = await _unitOfWork.GetReadRepository<Asset>()
                .GetAllAsync(x => !x.IsDeleted);

            // Entity listesini DTO'ya map et
            var result = _mapper.Map<List<GetAllAssetsQueryResult>>(assets);

            return result;
        }

    }
}
