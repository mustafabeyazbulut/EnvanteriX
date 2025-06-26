using MediatR;

namespace EnvanteriX.Application.Features.Commands.VendorCommands
{
    public class DeleteVendorCommand : IRequest<Unit>
    {
        public int VendorId { get; set; }

        public DeleteVendorCommand(int id)
        {
            VendorId = id;
        }
    }
}
