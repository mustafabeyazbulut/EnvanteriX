using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.VendorCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.VendorHandlers
{
    public class DeleteVendorCommandHandler : BaseHandler, IRequestHandler<DeleteVendorCommand, Unit>
    {
        public DeleteVendorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.GetReadRepository<Vendor>()
                .GetAsync(x => x.Id == request.VendorId && !x.IsDeleted);

            if (vendor == null)
                throw new KeyNotFoundException($"Vendor with ID {request.VendorId} not found or already deleted.");

            vendor.IsDeleted = true;
            await _unitOfWork.GetWriteRepository<Vendor>().UpdateAsync(vendor);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
