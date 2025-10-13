using MediatR;

namespace EnvanteriX.Application.Features.Commands.BrandCommands
{
    public class ActiveBrandCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
