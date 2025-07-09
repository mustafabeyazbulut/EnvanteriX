using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetQueries;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
            var assets = await _unitOfWork.GetReadRepository<Asset>().GetAllAsync(
                include: x => x.Include(c=>c.AssetType).Include(b => b.Model).ThenInclude(b=>b.Brand).Include(b=>b.Vendor).Include(b=>b.Location).Include(b=>b.AssignedUser)
                );

            var map = _mapper.Map<GetAllAssetsQueryResult, Asset>(assets, config: cfg =>
            {
                cfg.CreateMap<Asset, GetAllAssetsQueryResult>()
                   .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.AssetType.TypeName))
                   .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.ModelName))
                   .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Model.Brand.BrandName))
                   .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor.VendorName))
                   .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => $"Bina: {src.Location.Building}, Kat: {src.Location.Floor}, Oda: {src.Location.Room}"))
                   .ForMember(dest => dest.AssignedUserName, opt => opt.MapFrom(src => src.AssignedUser.FullName))
                   .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Model.Brand.Id));
            });
            return map.ToList();
        }
    }
}
