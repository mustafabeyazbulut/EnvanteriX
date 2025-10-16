using MediatR;

namespace EnvanteriX.Application.Features.Commands.Portal365Commands
{
    public class UpdatePortal365Command:IRequest<Unit>
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SenderEmail { get; set; }
    }
}
