using MediatR;

namespace EnvanteriX.Application.Features.Commands.VendorCommands
{
    public class DeleteVendorCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteVendorCommand(int id)
        {
            this.Id = id;
        }
    }
}
