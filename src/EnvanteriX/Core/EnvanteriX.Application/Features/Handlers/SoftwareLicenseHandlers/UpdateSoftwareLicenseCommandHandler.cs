using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.SoftwareLicenseCommands;
using EnvanteriX.Application.Features.Results.SoftwareLicenseResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;


namespace EnvanteriX.Application.Features.Handlers.SoftwareLicenseHandlers
{
    public class UpdateSoftwareLicenseCommandHandler : BaseHandler, IRequestHandler<UpdateSoftwareLicenseCommand, UpdateSoftwareLicenseCommandResult>
    {
        public UpdateSoftwareLicenseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<UpdateSoftwareLicenseCommandResult> Handle(UpdateSoftwareLicenseCommand request, CancellationToken cancellationToken)
        {
            var license = await _unitOfWork.GetReadRepository<SoftwareLicense>()
                .GetAsync(x => x.Id == request.SoftwareLicenseId && !x.IsDeleted);

            if (license == null)
                throw new NotFoundException("Software license not found.");

            license.AssetId = request.AssetId;
            license.LicenseKey = request.LicenseKey;
            license.SoftwareName = request.SoftwareName;
            license.Version = request.Version;
            license.VendorId = request.VendorId;
            license.PurchaseDate = request.PurchaseDate;
            license.ExpiryDate = request.ExpiryDate;
            license.AssignedUserId = request.AssignedUserId;

          await  _unitOfWork.GetWriteRepository<SoftwareLicense>().UpdateAsync(license);
            await _unitOfWork.SaveAsync();

            return new UpdateSoftwareLicenseCommandResult(license);
        }
    }
}
