using MediatR;

namespace EnvanteriX.Application.Features.Commands.VendorCommands
{
    public class ActiveVendorCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
