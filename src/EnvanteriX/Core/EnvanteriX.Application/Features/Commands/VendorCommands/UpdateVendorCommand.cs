using MediatR;

namespace EnvanteriX.Application.Features.Commands.VendorCommands
{
    public class UpdateVendorCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
