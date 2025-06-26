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
    public class UpdateModelCommandHandler : BaseHandler, IRequestHandler<UpdateModelCommand, UpdateModelCommandResult>
    {
        public UpdateModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        { }

        public async Task<UpdateModelCommandResult> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Model>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (model == null) return null;

            model.ModelName = request.ModelName;
            model.BrandId = request.BrandId;

          await  _unitOfWork.GetWriteRepository<Model>().UpdateAsync(model);
            await _unitOfWork.SaveAsync();

            return new UpdateModelCommandResult(model.Id, model.ModelName, model.BrandId);
        }
    }
}
