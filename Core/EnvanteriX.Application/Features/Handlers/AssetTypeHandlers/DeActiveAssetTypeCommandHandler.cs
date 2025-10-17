using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using EnvanteriX.Application.Features.Rules.AssetTypeRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetTypeHandlers
{
    public class DeActiveAssetTypeCommandHandler : BaseHandler, IRequestHandler<DeActiveAssetTypeCommand, Unit>
    {
        private readonly AssetTypeRules _assetTypeRules;
        public DeActiveAssetTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetTypeRules assetTypeRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetTypeRules = assetTypeRules;
        }
        public async Task<Unit> Handle(DeActiveAssetTypeCommand request, CancellationToken cancellationToken)
        {
            var assetType = await _unitOfWork.GetReadRepository<AssetType>()
                                              .GetAsync(x => x.Id == request.Id);
            await _assetTypeRules.AssetTypeShouldExist(assetType);
            assetType.IsDeleted = true;
            assetType.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<AssetType>().UpdateAsync(assetType);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
