using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.Portal365Commands;
using EnvanteriX.Application.Features.Rules.Portal365Rules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.Portal365Handlers
{
    public class DeletePortal365CommandHandler : BaseHandler, IRequestHandler<DeletePortal365Command, Unit>
    {
        private readonly Portal365Rules _portal365Rules;
        public DeletePortal365CommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, Portal365Rules portal365Rules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _portal365Rules = portal365Rules;
        }

        public async Task<Unit> Handle(DeletePortal365Command request, CancellationToken cancellationToken)
        {
            var modelExits = await _unitOfWork.GetReadRepository<Portal365>().GetAsync(x => x.Id == request.Id);
            await _portal365Rules.Portal365ShouldExist(modelExits);
            await _unitOfWork.GetWriteRepository<Portal365>().HardDeleteAsync(modelExits);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
