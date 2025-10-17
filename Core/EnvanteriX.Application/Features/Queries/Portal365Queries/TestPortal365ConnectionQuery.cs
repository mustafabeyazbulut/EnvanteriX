using MediatR;

namespace EnvanteriX.Application.Features.Queries.Portal365Queries
{
    public class TestPortal365ConnectionQuery : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
