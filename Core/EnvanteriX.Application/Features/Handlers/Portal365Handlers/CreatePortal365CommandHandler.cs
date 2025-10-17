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
    public class CreatePortal365CommandHandler : BaseHandler, IRequestHandler<CreatePortal365Command, Unit>
    {
        private readonly Portal365Rules _portal365Rules;
        public CreatePortal365CommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, Portal365Rules portal365Rules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _portal365Rules = portal365Rules;
        }

        public async Task<Unit> Handle(CreatePortal365Command request, CancellationToken cancellationToken)
        {
            var modelExits = await _unitOfWork.GetReadRepository<Portal365>().GetAsync(x => x.ClientId == request.ClientId);
            await _portal365Rules.Portal365AlreadyExists(modelExits);

            var model = _mapper.Map<Portal365, CreatePortal365Command>(request);
            model.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<Portal365>().AddAsync(model);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
