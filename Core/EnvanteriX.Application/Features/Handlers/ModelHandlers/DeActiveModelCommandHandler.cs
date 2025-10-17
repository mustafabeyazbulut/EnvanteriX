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
    public class DeActiveModelCommandHandler : BaseHandler, IRequestHandler<DeActiveModelCommand, Unit>
    {
        private readonly ModelRules _modelRules;
        public DeActiveModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ModelRules modelRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _modelRules = modelRules;
        }

        public async Task<Unit> Handle(DeActiveModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Model>().GetAsync(x => x.Id == request.Id);
            await _modelRules.ModelShouldExist(model);
            model.IsDeleted = true;
            model.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<Model>().UpdateAsync(model);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
