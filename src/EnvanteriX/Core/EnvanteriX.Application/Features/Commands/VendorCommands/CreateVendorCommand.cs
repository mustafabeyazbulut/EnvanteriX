using MediatR;
using EnvanteriX.Application.Features.Results.VendorResults;

namespace EnvanteriX.Application.Features.Commands.VendorCommands
{
    public class CreateVendorCommand : IRequest<CreateVendorCommandResult>
    {
        public string VendorName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
