using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.VendorCommands;
using EnvanteriX.Application.Features.Rules.VendorRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.VendorHandlers
{
    public class UpdateVendorCommandHandler : BaseHandler, IRequestHandler<UpdateVendorCommand, Unit>
    {
        private readonly VendorRules _vendorRules;
        public UpdateVendorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, VendorRules vendorRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _vendorRules = vendorRules;
        }
        public async Task<Unit> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.GetReadRepository<Vendor>().GetAsync(x => x.Id == request.Id);
            await _vendorRules.VendorShouldExist(vendor);

            if (!string.Equals(vendor.VendorName, request.VendorName, StringComparison.OrdinalIgnoreCase))
            { // Eğer vendor adı değiştiyse yeni adıyla başka kayıt var mı diye kontrol edicez
                bool vendorExists = await _unitOfWork.GetReadRepository<Vendor>()
                                        .AnyAsync(v => v.VendorName.ToUpper() == request.VendorName.ToUpper());
                await _vendorRules.VendorAlreadyExists(vendorExists, request.VendorName);
            }
            vendor.VendorName = request.VendorName;
            vendor.ContactPerson = request.ContactPerson;
            vendor.PhoneNumber = request.PhoneNumber;
            vendor.Email = request.Email;
            await _unitOfWork.GetWriteRepository<Vendor>().UpdateAsync(vendor);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
