using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetMovementCommands;
using EnvanteriX.Application.Features.Results.AssetMovementResults;
using EnvanteriX.Application.Features.Rules.AssetMovementRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetMovementHandlers
{
    public class CreateAssetMovementCommandHandler : BaseHandler, IRequestHandler<CreateAssetMovementCommand, CreateAssetMovementCommandResult>
    {
        private readonly AssetMovementRules _assetMovementRules;
        public CreateAssetMovementCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetMovementRules assetMovementRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetMovementRules = assetMovementRules;
        }

        public async Task<CreateAssetMovementCommandResult> Handle(CreateAssetMovementCommand request, CancellationToken cancellationToken)
        {
            var model= _mapper.Map<AssetMovement, CreateAssetMovementCommand>(request);
            await _unitOfWork.GetWriteRepository<AssetMovement>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CreateAssetMovementCommandResult, AssetMovement>(model);
        }
    }
}
