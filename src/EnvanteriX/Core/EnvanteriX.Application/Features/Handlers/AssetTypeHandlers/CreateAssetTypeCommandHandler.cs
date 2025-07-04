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
    public class CreateAssetTypeCommandHandler : BaseHandler, IRequestHandler<CreateAssetTypeCommand, CreateAssetTypeCommandResult>
    {
        private readonly AssetTypeRules _assetTypeRules;
        public CreateAssetTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetTypeRules assetTypeRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetTypeRules = assetTypeRules;
        }

        public async Task<CreateAssetTypeCommandResult> Handle(CreateAssetTypeCommand request, CancellationToken cancellationToken)
        {
            bool assetTypeExists = await _unitOfWork.GetReadRepository<AssetType>()
                                         .AnyAsync(at => at.TypeName.ToUpper() == request.TypeName.ToUpper());
            await _assetTypeRules.AssetTypeAlreadyExists(assetTypeExists, request.TypeName);
            var assetType=_mapper.Map<AssetType, CreateAssetTypeCommand>(request);

            await _unitOfWork.GetWriteRepository<AssetType>().AddAsync(assetType);
            await _unitOfWork.SaveAsync();
            var result = _mapper.Map<CreateAssetTypeCommandResult, AssetType>(assetType);
            return result;
        }
    }
}
