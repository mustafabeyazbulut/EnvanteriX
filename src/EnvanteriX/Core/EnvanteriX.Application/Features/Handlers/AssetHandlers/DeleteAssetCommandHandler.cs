using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class DeleteAssetCommandHandler : BaseHandler, IRequestHandler<DeleteAssetCommand, Unit>
    {
        public DeleteAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await _unitOfWork.GetReadRepository<Asset>().GetAsync(x => x.Id == request.Id);
            if (asset == null) return Unit.Value;
            await _unitOfWork.GetWriteRepository<Asset>().HardDeleteAsync(asset);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
