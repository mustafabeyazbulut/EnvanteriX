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
    public class UpdateAssetTypeCommandHandler : BaseHandler, IRequestHandler<UpdateAssetTypeCommand, Unit>
    {
        private readonly AssetTypeRules _assetTypeRules;
        public UpdateAssetTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetTypeRules assetTypeRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetTypeRules = assetTypeRules;
        }

        public async Task<Unit> Handle(UpdateAssetTypeCommand request, CancellationToken cancellationToken)
        {
           var assetType=await _unitOfWork.GetReadRepository<AssetType>().GetAsync(x => x.Id == request.Id);
            await _assetTypeRules.AssetTypeShouldExist(assetType);

            if (!string.Equals(assetType.TypeName, request.TypeName, StringComparison.OrdinalIgnoreCase))
            { // Değer farklı ise kontrol edicez yeni haliyle başka kayıt var mı diye
                bool assetTypeExists = await _unitOfWork.GetReadRepository<AssetType>()
                                        .AnyAsync(at => at.TypeName.ToUpper() == request.TypeName.ToUpper());
                await _assetTypeRules.AssetTypeAlreadyExists(assetTypeExists, $"Varlık Türü: {request.TypeName}");
            }
            assetType.TypeName = request.TypeName;
            await _unitOfWork.GetWriteRepository<AssetType>().UpdateAsync(assetType);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
