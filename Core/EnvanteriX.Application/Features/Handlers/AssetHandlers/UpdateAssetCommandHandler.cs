using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetCommands;
using EnvanteriX.Application.Features.Rules.AssetRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class UpdateAssetCommandHandler : BaseHandler, IRequestHandler<UpdateAssetCommand, Unit>
    {
        private readonly AssetRules _assetRules;
        public UpdateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetRules assetRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetRules = assetRules;
        }

        public async Task<Unit> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset=await _unitOfWork.GetReadRepository<Asset>().GetAsync(x=>x.Id==request.Id);
            await _assetRules.AssetShouldExist(asset);

            if (!string.Equals(request.AssetTag,asset.AssetTag,StringComparison.OrdinalIgnoreCase)
                || !string.Equals(request.SerialNumber, asset.SerialNumber, StringComparison.OrdinalIgnoreCase))
            {
                bool assetExists = await _unitOfWork.GetReadRepository<Asset>()
                                                .AnyAsync(x => x.Id != asset.Id &&
                                                              (x.AssetTag.ToUpper() == request.AssetTag.ToUpper()
                                                            || x.SerialNumber.ToUpper() == request.SerialNumber.ToUpper()));

                await _assetRules.AssetAlreadyExists(assetExists, $"Varlık Etiketi: {request.AssetTag}, SerialNumber: {request.SerialNumber}");
            }
            asset.AssetTag = request.AssetTag;
            asset.SerialNumber = request.SerialNumber;
            asset.AssetTypeId = request.AssetTypeId;
            asset.ModelId = request.ModelId;
            asset.VendorId = request.VendorId;
            asset.PurchaseDate = request.PurchaseDate;
            asset.WarrantyEndDate = request.WarrantyEndDate;
            asset.Cost = request.Cost;
            asset.LocationId = request.LocationId;
            asset.AssignedUserId = request.AssignedUserId;
            asset.Description = request.Description;
            asset.Status = request.Status;

            await _unitOfWork.GetWriteRepository<Asset>().UpdateAsync(asset);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
