using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetTypeCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.AssetTypeHandlers
{
    public class CreateAssetTypeCommandHandler : BaseHandler, IRequestHandler<CreateAssetTypeCommand, CreateAssetTypeCommandResult>
    {
        public CreateAssetTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateAssetTypeCommandResult> Handle(CreateAssetTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new AssetType
            {
                TypeName = request.TypeName
            };

            await _unitOfWork.GetWriteRepository<AssetType>().AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return new CreateAssetTypeCommandResult(entity.Id, entity.TypeName);
        }
    }
}
