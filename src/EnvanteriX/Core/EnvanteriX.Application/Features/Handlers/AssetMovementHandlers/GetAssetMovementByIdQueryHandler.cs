using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetMovementQueries;
using EnvanteriX.Application.Features.Results.AssetMovementResults;
using EnvanteriX.Application.Features.Rules.AssetMovementRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.AssetMovementHandlers
{
    public class GetAssetMovementByIdQueryHandler : BaseHandler, IRequestHandler<GetAssetMovementByIdQuery, GetAssetMovementByIdQueryResult>
    {
        private readonly AssetMovementRules _assetMovementRules;
        public GetAssetMovementByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetMovementRules assetMovementRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetMovementRules = assetMovementRules;
        }

        public async Task<GetAssetMovementByIdQueryResult> Handle(GetAssetMovementByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<AssetMovement>().GetAsync(
                predicate: x => x.Id == request.Id,
                include: x => x.Include(y => y.Asset).ThenInclude(z => z.Model).ThenInclude(z => z.Brand)
                              .Include(y => y.FromUser)
                              .Include(y => y.ToUser)
                              .Include(y => y.FromLocation)
                              .Include(y => y.ToLocation)
               );
            await _assetMovementRules.AssetMovementShouldExist(model);
            var map = _mapper.Map<GetAssetMovementByIdQueryResult, AssetMovement>(model, config: cfg =>
            {
                cfg.CreateMap<AssetMovement, GetAssetMovementByIdQueryResult>()
                   .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => $"Marka: {src.Asset.Model.Brand.BrandName}, " +
                    $"Model: {src.Asset.Model.ModelName}, SeriNo: {src.Asset.SerialNumber}, Key: {src.Asset.AssetTag}"))
                   .ForMember(dest => dest.FromUserFullName, opt => opt.MapFrom(src => src.FromUser.FullName))
                   .ForMember(dest => dest.ToUserFullName, opt => opt.MapFrom(src => src.ToUser.FullName))
                   .ForMember(dest => dest.FromLocationName, opt => opt.MapFrom(src => $"Bina: {src.FromLocation.Building}, Kat: {src.FromLocation.Floor}, Oda: {src.FromLocation.Room}"))
                   .ForMember(dest => dest.ToLocationName, opt => opt.MapFrom(src => $"Bina: {src.ToLocation.Building}, Kat: {src.ToLocation.Floor}, Oda: {src.ToLocation.Room}"));
            });
            return map;
        }
    }
}
