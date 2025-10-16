using MediatR;

namespace EnvanteriX.Application.Features.Commands.Portal365Commands
{
    public class CreatePortal365Command:IRequest<Unit>
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SenderEmail { get; set; }
    }
}
