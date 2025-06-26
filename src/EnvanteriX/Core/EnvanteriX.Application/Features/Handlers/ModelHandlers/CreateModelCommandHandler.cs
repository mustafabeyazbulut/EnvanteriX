using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.ModelCommands;
using EnvanteriX.Application.Features.Results.ModelResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.ModelHandlers
{
    public class CreateModelCommandHandler : BaseHandler, IRequestHandler<CreateModelCommand, CreateModelCommandResult>
    {
        public CreateModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateModelCommandResult> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            var model = new Model
            {
                ModelName = request.ModelName,
                BrandId = request.BrandId
            };

            await _unitOfWork.GetWriteRepository<Model>().AddAsync(model);
            await _unitOfWork.SaveAsync();

            return new CreateModelCommandResult(model.Id, model.ModelName, model.BrandId);
        }
    }
}
