using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.SoftwareLicenseCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.SoftwareLicenseHandlers
{
    public class DeleteSoftwareLicenseCommandHandler : BaseHandler, IRequestHandler<DeleteSoftwareLicenseCommand, Unit>
    {
        public DeleteSoftwareLicenseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteSoftwareLicenseCommand request, CancellationToken cancellationToken)
        {
            var license = await _unitOfWork.GetReadRepository<SoftwareLicense>()
                .GetAsync(x => x.Id == request.SoftwareLicenseId && !x.IsDeleted);

            if (license == null)
                throw new KeyNotFoundException("Software license not found or already deleted.");

            license.IsDeleted = true;
           await _unitOfWork.GetWriteRepository<SoftwareLicense>().UpdateAsync(license);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
