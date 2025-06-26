using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetCommands;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class UpdateAssetCommandHandler : BaseHandler, IRequestHandler<UpdateAssetCommand, UpdateAssetCommandResult>
    {
        public UpdateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<UpdateAssetCommandResult> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset=await _unitOfWork.GetReadRepository<Asset>().GetAsync(x=>x.Id==request.AssetId);
            if (asset == null) return null;

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
            return new UpdateAssetCommandResult(asset);
        }
    }
}
