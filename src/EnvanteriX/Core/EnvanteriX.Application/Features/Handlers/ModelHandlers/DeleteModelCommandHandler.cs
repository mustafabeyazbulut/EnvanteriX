using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.ModelCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;


namespace EnvanteriX.Application.Features.Handlers.ModelHandlers
{
    public class DeleteModelCommandHandler : BaseHandler, IRequestHandler<DeleteModelCommand, Unit>
    {
        public DeleteModelCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        { }

        public async Task<Unit> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Model>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (model == null) 
                throw new NotFoundException($"Model with ID {request.Id} not found.");

            model.IsDeleted = true;
          await  _unitOfWork.GetWriteRepository<Model>().UpdateAsync(model);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
