using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetQueries;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Features.Rules.AssetRules;
using EnvanteriX.Application.Features.Rules.UserRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class GetAllActiveAssetByEmailQueryHandler : BaseHandler, IRequestHandler<GetAllActiveAssetByEmailQuery, List<GetAllActiveAssetByEmailQueryResult>>
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRules _userRules;
        public GetAllActiveAssetByEmailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,  UserRules userRules, UserManager<User> userManager) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _userRules = userRules;
            _userManager = userManager;
        }

        public async Task<List<GetAllActiveAssetByEmailQueryResult>> Handle(GetAllActiveAssetByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            await _userRules.UserShouldExist(user);
            var assets = await _unitOfWork.GetReadRepository<Asset>().GetAllAsync(
                predicate: x => x.IsDeleted == false && x.AssignedUserId == user.Id,
               include: x => x.Include(c => c.AssetType)
               .Include(b => b.Model).ThenInclude(b => b.Brand)
               .Include(b => b.Vendor)
               .Include(b => b.Location)
               );
            var map = _mapper.Map<GetAllActiveAssetByEmailQueryResult, Asset>(assets, config: cfg =>
            {
                cfg.CreateMap<Asset, GetAllActiveAssetByEmailQueryResult>()
                   .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.AssetType.TypeName))
                   .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.ModelName))
                   .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Model.Brand.BrandName))
                   .ForMember(dest => dest.VendorName, opt => opt.MapFrom(src => src.Vendor.VendorName))
                   .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => $"{src.Location.Building}"))
                   .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Model.Brand.Id));
            });
            return map.ToList();
        }
    }
}
