using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;

namespace EnvanteriX.Application.Features.Handlers.AssetTypeHandlers
{
    public class UpdateAssetTypeCommandHandler : IRequestHandler<UpdateAssetTypeCommand, UpdateAssetTypeCommandResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAssetTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateAssetTypeCommandResult> Handle(UpdateAssetTypeCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetWriteRepository<AssetType>();

            var entity = await _unitOfWork.GetReadRepository<AssetType>()
                                         .GetAsync(x => x.Id == request.Id && !x.IsDeleted);

            if (entity == null)
            {
                throw new KeyNotFoundException($"AssetType with ID {request.Id} not found.");
            }

            entity.TypeName = request.TypeName;

            await repo.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return new UpdateAssetTypeCommandResult(entity.Id, entity.TypeName);
        }
    }
}
