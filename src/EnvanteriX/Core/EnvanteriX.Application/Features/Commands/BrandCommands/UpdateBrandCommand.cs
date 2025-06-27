using MediatR;

namespace EnvanteriX.Application.Features.Commands.BrandCommands
{
    public class UpdateBrandCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
    }
}
