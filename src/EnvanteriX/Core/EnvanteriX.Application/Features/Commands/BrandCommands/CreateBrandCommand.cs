using MediatR;
using EnvanteriX.Application.Features.Results.BrandResults;

namespace EnvanteriX.Application.Features.Commands.BrandCommands
{
    public class CreateBrandCommand : IRequest<CreateBrandCommandResult>
    {
        public string BrandName { get; set; }
    }
}
