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
    public class CreateAssetCommandHandler : BaseHandler, IRequestHandler<CreateAssetCommand, CreateAssetCommandResult>
    {
        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateAssetCommandResult> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = new Asset
            {
                AssetTag = request.AssetTag,
                SerialNumber = request.SerialNumber,
                AssetTypeId = request.AssetTypeId,
                ModelId = request.ModelId,
                VendorId = request.VendorId,
                PurchaseDate = request.PurchaseDate,
                WarrantyEndDate = request.WarrantyEndDate,
                Cost = request.Cost,
                LocationId = request.LocationId,
                AssignedUserId = request.AssignedUserId,
                Description = request.Description,
                Status = request.Status,
            };
            await _unitOfWork.GetWriteRepository<Asset>().AddAsync(asset);
            await _unitOfWork.SaveAsync();
            return new CreateAssetCommandResult(asset);
        }
    }
}
