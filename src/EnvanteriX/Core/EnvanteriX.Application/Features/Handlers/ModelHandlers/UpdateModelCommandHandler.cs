using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.ModelCommands;
using EnvanteriX.Application.Features.Results.ModelResults;
using EnvanteriX.Application.Features.Rules.BrandRules;
using EnvanteriX.Application.Features.Rules.ModelRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.ModelHandlers
{
    public class UpdateModelCommandHandler : BaseHandler, IRequestHandler<UpdateModelCommand, Unit>
    {
        private readonly ModelRules _modelRules;
        private readonly BrandRules _brandRules;
        public UpdateModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ModelRules modelRules, BrandRules brandRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _modelRules = modelRules;
            _brandRules = brandRules;
        }

        public async Task<Unit> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Model>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            await _modelRules.ModelShouldExist(model);

            var brand = await _unitOfWork.GetReadRepository<Brand>().GetAsync(x => x.Id == request.BrandId && !x.IsDeleted);
            await _brandRules.BrandShouldExist(brand);

            if (!string.Equals(model.ModelName, request.ModelName, StringComparison.OrdinalIgnoreCase))
            {
                var existingBrandWithSameName = await _unitOfWork.GetReadRepository<Model>()
                    .GetAsync(x => x.ModelName.ToLower() == request.ModelName.ToLower() && x.Id != request.Id && !x.IsDeleted);

                await _modelRules.ModelAlreadyExists(existingBrandWithSameName);
            }

            model.ModelName = request.ModelName;
            model.BrandId = request.BrandId;

            await _unitOfWork.GetWriteRepository<Model>().UpdateAsync(model);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
