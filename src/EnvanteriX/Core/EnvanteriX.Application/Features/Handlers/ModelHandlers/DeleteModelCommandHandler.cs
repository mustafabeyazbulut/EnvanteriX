using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.ModelCommands;
using EnvanteriX.Application.Features.Rules.ModelRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.ModelHandlers
{
    public class DeleteModelCommandHandler : BaseHandler, IRequestHandler<DeleteModelCommand, Unit>
    {
        private readonly ModelRules _modelRules;
        public DeleteModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ModelRules modelRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _modelRules = modelRules;
        }

        public async Task<Unit> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Model>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            await _modelRules.ModelShouldExist(model);
            var hasAnyAsset = await _unitOfWork.GetReadRepository<Asset>().AnyAsync(a => a.ModelId == model.Id && !a.IsDeleted); //asset içinde kullanılmış mı diye kontrol ediyoruz.
            await _modelRules.ModelShouldNotHaveAnyModel(model, hasAnyAsset);
            model.IsDeleted = true;
            await _unitOfWork.GetWriteRepository<Model>().UpdateAsync(model);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
