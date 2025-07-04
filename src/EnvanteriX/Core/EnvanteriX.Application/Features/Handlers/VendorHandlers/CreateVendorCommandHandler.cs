using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.VendorCommands;
using EnvanteriX.Application.Features.Results.VendorResults;
using EnvanteriX.Application.Features.Rules.VendorRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.VendorHandlers
{
    public class CreateVendorCommandHandler : BaseHandler, IRequestHandler<CreateVendorCommand, CreateVendorCommandResult>
    {
        private readonly VendorRules _vendorRules;
        public CreateVendorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, VendorRules vendorRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _vendorRules = vendorRules;
        }

        public async Task<CreateVendorCommandResult> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            bool vendorExists = await _unitOfWork.GetReadRepository<Vendor>()
                .AnyAsync(v => v.VendorName.ToUpper() == request.VendorName.ToUpper());
            await _vendorRules.VendorAlreadyExists(vendorExists, request.VendorName);
            var vendor= _mapper.Map<Vendor, CreateVendorCommand>(request);

            await _unitOfWork.GetWriteRepository<Vendor>().AddAsync(vendor);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CreateVendorCommandResult, Vendor>(vendor);
        }
    }
}
