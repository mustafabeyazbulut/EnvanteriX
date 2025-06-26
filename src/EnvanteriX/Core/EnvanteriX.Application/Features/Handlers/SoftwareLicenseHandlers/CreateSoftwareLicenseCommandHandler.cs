using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.SoftwareLicenseCommands;
using EnvanteriX.Application.Features.Results.SoftwareLicenseResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.SoftwareLicenseHandlers
{
    public class CreateSoftwareLicenseCommandHandler : BaseHandler, IRequestHandler<CreateSoftwareLicenseCommand, CreateSoftwareLicenseCommandResult>
    {
        public CreateSoftwareLicenseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateSoftwareLicenseCommandResult> Handle(CreateSoftwareLicenseCommand request, CancellationToken cancellationToken)
        {
            var license = new SoftwareLicense
            {
                AssetId = request.AssetId,
                LicenseKey = request.LicenseKey,
                SoftwareName = request.SoftwareName,
                Version = request.Version,
                VendorId = request.VendorId,
                PurchaseDate = request.PurchaseDate,
                ExpiryDate = request.ExpiryDate,
                AssignedUserId = request.AssignedUserId,
                IsActive = request.IsActive
            };

            await _unitOfWork.GetWriteRepository<SoftwareLicense>().AddAsync(license);
            await _unitOfWork.SaveAsync();

            return new CreateSoftwareLicenseCommandResult(license);
        }
    }
}
