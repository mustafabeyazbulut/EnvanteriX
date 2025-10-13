using MediatR;

namespace EnvanteriX.Application.Features.Commands.BrandCommands
{
    public class DeActiveBrandCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
