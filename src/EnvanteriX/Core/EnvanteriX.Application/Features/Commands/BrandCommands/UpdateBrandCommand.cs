using MediatR;
using EnvanteriX.Application.Features.Results.BrandResults;

namespace EnvanteriX.Application.Features.Commands.BrandCommands
{
    public class UpdateBrandCommand : IRequest<UpdateBrandCommandResult>
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
    }
}
