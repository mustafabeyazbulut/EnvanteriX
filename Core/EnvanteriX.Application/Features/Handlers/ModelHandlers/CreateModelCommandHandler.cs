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
    public class CreateModelCommandHandler : BaseHandler, IRequestHandler<CreateModelCommand, CreateModelCommandResult>
    {
        private readonly ModelRules _modelRules;
        private readonly BrandRules _brandRules;
        public CreateModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ModelRules modelRules, BrandRules brandRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _modelRules = modelRules;
            _brandRules = brandRules;
        }

        public async Task<CreateModelCommandResult> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            var existingModel = await _unitOfWork.GetReadRepository<Model>()
                                .GetAsync(b => b.ModelName.ToLower() == request.ModelName.ToLower()
                                            && b.BrandId == request.BrandId); //aynı brand altında aynı isme sahip model olmaması için kontrol
            await _modelRules.ModelAlreadyExists(existingModel);

            var brand = await _unitOfWork.GetReadRepository<Brand>().GetAsync(x => x.Id == request.BrandId && !x.IsDeleted);
            await _brandRules.BrandShouldExist(brand);

            var model =_mapper.Map<Model, CreateModelCommand>(request);
            await _unitOfWork.GetWriteRepository<Model>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            var map = _mapper.Map<CreateModelCommandResult, Model>(model);
            return map;
        }
    }
}
