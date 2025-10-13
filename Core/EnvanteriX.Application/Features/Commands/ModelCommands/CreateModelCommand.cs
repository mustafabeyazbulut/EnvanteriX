using EnvanteriX.Application.Features.Results.ModelResults;
using MediatR;

namespace EnvanteriX.Application.Features.Commands.ModelCommands
{
    public class CreateModelCommand : IRequest<CreateModelCommandResult>
    {
        public string ModelName { get; set; }
        public int BrandId { get; set; }
    }
}
