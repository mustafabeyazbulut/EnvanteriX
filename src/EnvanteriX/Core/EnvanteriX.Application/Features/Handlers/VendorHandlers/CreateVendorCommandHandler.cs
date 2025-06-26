using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.VendorCommands;
using EnvanteriX.Application.Features.Results.VendorResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.VendorHandlers
{
    public class CreateVendorCommandHandler : BaseHandler, IRequestHandler<CreateVendorCommand, CreateVendorCommandResult>
    {
        public CreateVendorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<CreateVendorCommandResult> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = new Vendor
            {
                VendorName = request.VendorName,
                ContactPerson = request.ContactPerson,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            await _unitOfWork.GetWriteRepository<Vendor>().AddAsync(vendor);
            await _unitOfWork.SaveAsync();

            return new CreateVendorCommandResult(vendor);
        }
    }
}
