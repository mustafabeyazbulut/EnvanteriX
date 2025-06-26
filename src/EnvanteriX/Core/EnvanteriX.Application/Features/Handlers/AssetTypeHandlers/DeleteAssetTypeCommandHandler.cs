using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetTypeHandlers
{
    public class DeleteAssetTypeCommandHandler :BaseHandler, IRequestHandler<DeleteAssetTypeCommand, Unit>
    {
        public DeleteAssetTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }
        public async Task<Unit> Handle(DeleteAssetTypeCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetWriteRepository<AssetType>();

            var entity = await _unitOfWork.GetReadRepository<AssetType>()
                                         .GetAsync(x => x.Id == request.Id);

            if (entity == null)
                throw new KeyNotFoundException($"AssetType with Id {request.Id} not found.");

            entity.IsDeleted = true; // Soft delete yapıyorsan

            await repo.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
