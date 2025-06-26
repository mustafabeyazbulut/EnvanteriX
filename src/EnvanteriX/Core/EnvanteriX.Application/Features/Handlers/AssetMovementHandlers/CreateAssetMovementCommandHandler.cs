using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetMovementCommands;
using EnvanteriX.Application.Features.Results.AssetMovementResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetMovementHandlers
{
    public class CreateAssetMovementCommandHandler : BaseHandler, IRequestHandler<CreateAssetMovementCommand, CreateAssetMovementCommandResult>
    {
        public CreateAssetMovementCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateAssetMovementCommandResult> Handle(CreateAssetMovementCommand request, CancellationToken cancellationToken)
        {
            var entity = new AssetMovement
            {
                AssetId = request.AssetId,
                FromUserId = request.FromUserId,
                ToUserId = request.ToUserId,
                FromLocationId = request.FromLocationId,
                ToLocationId = request.ToLocationId,
                Note = request.Note,
                
            };

            await _unitOfWork.GetWriteRepository<AssetMovement>().AddAsync(entity);
            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<CreateAssetMovementCommandResult>(entity);
            return result;
        }
    }
}
