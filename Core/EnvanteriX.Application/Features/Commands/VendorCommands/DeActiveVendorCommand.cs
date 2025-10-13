using MediatR;

namespace EnvanteriX.Application.Features.Commands.VendorCommands
{
    public class DeActiveVendorCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
