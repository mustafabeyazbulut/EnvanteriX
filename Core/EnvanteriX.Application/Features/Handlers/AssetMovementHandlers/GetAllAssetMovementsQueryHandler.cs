using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetMovementQueries;
using EnvanteriX.Application.Features.Results.AssetMovementResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.AssetMovementHandlers
{
    public class GetAllAssetMovementsQueryHandler : BaseHandler, IRequestHandler<GetAllAssetMovementsQuery, List<GetAllAssetMovementsQueryResult>>
    {
        public GetAllAssetMovementsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllAssetMovementsQueryResult>> Handle(GetAllAssetMovementsQuery request, CancellationToken cancellationToken)
        {
            var models=await _unitOfWork.GetReadRepository<AssetMovement>().GetAllAsync(
                include: x => x.Include(y => y.Asset).ThenInclude(z=>z.Model).ThenInclude(z=>z.Brand)
                               .Include(y => y.FromUser)
                               .Include(y => y.ToUser)
                               .Include(y => y.FromLocation)
                               .Include(y => y.ToLocation)
                );

            var map = _mapper.Map<GetAllAssetMovementsQueryResult, AssetMovement>(models, config: cfg =>
            {
                cfg.CreateMap<AssetMovement, GetAllAssetMovementsQueryResult>()
                   .ForMember(dest => dest.AssetName, opt => opt.MapFrom(src => $"Marka: {src.Asset.Model.Brand.BrandName}, " +
                    $"Model: {src.Asset.Model.ModelName}, SeriNo: {src.Asset.SerialNumber}, Key: {src.Asset.AssetTag}"))
                   .ForMember(dest => dest.FromUserFullName, opt => opt.MapFrom(src => src.FromUser.FullName))
                   .ForMember(dest => dest.ToUserFullName, opt => opt.MapFrom(src => src.ToUser.FullName))
                   .ForMember(dest => dest.FromLocationName, opt => opt.MapFrom(src => $"Bina: {src.FromLocation.Building}, Kat: {src.FromLocation.Floor}, Oda: {src.FromLocation.Room}"))
                   .ForMember(dest => dest.ToLocationName, opt => opt.MapFrom(src => $"Bina: {src.ToLocation.Building}, Kat: {src.ToLocation.Floor}, Oda: {src.ToLocation.Room}"));
            });
            return map.ToList();
        }
    }
}
