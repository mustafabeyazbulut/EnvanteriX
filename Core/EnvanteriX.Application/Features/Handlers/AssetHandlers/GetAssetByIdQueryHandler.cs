using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetQueries;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Features.Rules.AssetRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class GetAssetByIdQueryHandler : BaseHandler, IRequestHandler<GetAssetByIdQuery, GetAssetByIdQueryResult>
    {
        private readonly AssetRules _assetRules;
        public GetAssetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetRules assetRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetRules = assetRules;
        }
        public async Task<GetAssetByIdQueryResult> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            var asset = await _unitOfWork.GetReadRepository<Asset>().GetAsync(
                predicate: x => x.Id == request.AssetId,
                include: x => x.Include(c => c.AssetType).Include(b => b.Model).ThenInclude(b => b.Brand).Include(b => b.Vendor).Include(b => b.Location).Include(b => b.AssignedUser));
            await _assetRules.AssetShouldExist(asset);

            var map = _mapper.Map<GetAssetByIdQueryResult, Asset>(asset, config: cfg =>
            {
                cfg.CreateMap<Asset, GetAssetByIdQueryResult>()
                   .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.AssetType.TypeName))
                   .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.ModelName))
                   .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Model.Brand.BrandName))
                   .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor.VendorName))
                   .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => $"Bina: {src.Location.Building}, Kat: {src.Location.Floor}, Oda: {src.Location.Room}"))
                   .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser.FullName))
                   .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Model.Brand.Id));
            });
            return map;
        }
    }
}
