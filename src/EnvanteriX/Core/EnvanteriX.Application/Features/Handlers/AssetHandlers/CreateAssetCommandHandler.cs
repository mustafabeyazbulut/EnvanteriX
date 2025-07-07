using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Features.Rules.AssetRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class CreateAssetCommandHandler : BaseHandler, IRequestHandler<CreateAssetCommand, CreateAssetCommandResult>
    {
        private readonly AssetRules _assetRules;
        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetRules assetRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetRules = assetRules;
        }
        public async Task<CreateAssetCommandResult> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var assetTag = request.AssetTag?.Trim();
            var serialNumber = request.SerialNumber?.Trim();

            bool assetExists = await _unitOfWork.GetReadRepository<Asset>()
                .AnyAsync(a => a.AssetTag == assetTag || a.SerialNumber == serialNumber);
            await _assetRules.AssetAlreadyExists(assetExists, $"AssetTag: {assetTag}, SerialNumber: {serialNumber}");

            var asset = _mapper.Map<Asset, CreateAssetCommand>(request);
            await _unitOfWork.GetWriteRepository<Asset>().AddAsync(asset);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<CreateAssetCommandResult, Asset>(asset);
        }
    }
}
