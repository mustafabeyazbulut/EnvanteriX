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
    public class UpdatePortal365CommandHandler : BaseHandler, IRequestHandler<UpdatePortal365Command, Unit>
    {
        private readonly Portal365Rules _portal365Rules;
        public UpdatePortal365CommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, Portal365Rules portal365Rules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _portal365Rules = portal365Rules;
        }

        public async Task<Unit> Handle(UpdatePortal365Command request, CancellationToken cancellationToken)
        {
            var modelExits = await _unitOfWork.GetReadRepository<Portal365>().GetAsync(x => x.Id == request.Id);
            await _portal365Rules.Portal365ShouldExist(modelExits);

            if (!string.Equals(modelExits.ClientId, request.ClientId, StringComparison.OrdinalIgnoreCase))
            { // Değerlerden en az biri farklı ise kontrol edicez yeni haliyle başka kayıt var mı diye
                bool modelExists = await _unitOfWork.GetReadRepository<Portal365>()
                                        .AnyAsync(l => l.ClientId.ToUpper() == request.ClientId.ToUpper() );
                await _portal365Rules.Portal365AlreadyExists(modelExists, $"{request.ClientId}");
            }

            modelExits.ClientId = request.ClientId;
            modelExits.ClientSecret = request.ClientSecret;
            modelExits.TenantId = request.TenantId;
            modelExits.SenderEmail = request.SenderEmail;
            await _unitOfWork.GetWriteRepository<Portal365>().UpdateAsync(modelExits);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
