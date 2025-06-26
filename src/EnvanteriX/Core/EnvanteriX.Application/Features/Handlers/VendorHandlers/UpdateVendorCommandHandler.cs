using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.VendorCommands;
using EnvanteriX.Application.Features.Results.VendorResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;


namespace EnvanteriX.Application.Features.Handlers.VendorHandlers
{
    public class UpdateVendorCommandHandler : BaseHandler, IRequestHandler<UpdateVendorCommand, UpdateVendorCommandResult>
    {
        public UpdateVendorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<UpdateVendorCommandResult> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.GetReadRepository<Vendor>()
                .GetAsync(x => x.Id == request.VendorId && !x.IsDeleted);

            if (vendor == null) 
                throw new NotFoundException("Vendor not found.");

            vendor.VendorName = request.VendorName;
            vendor.ContactPerson = request.ContactPerson;
            vendor.PhoneNumber = request.PhoneNumber;
            vendor.Email = request.Email;

         await   _unitOfWork.GetWriteRepository<Vendor>().UpdateAsync(vendor);
            await _unitOfWork.SaveAsync();

            return new UpdateVendorCommandResult(vendor);
        }
    }
}
