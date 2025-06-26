using MediatR;

namespace EnvanteriX.Application.Features.Commands.SoftwareLicenseCommands
{
    public class DeleteSoftwareLicenseCommand : IRequest<Unit>
    {
        public int SoftwareLicenseId { get; set; }

        public DeleteSoftwareLicenseCommand(int id)
        {
            SoftwareLicenseId = id;
        }
    }
}
