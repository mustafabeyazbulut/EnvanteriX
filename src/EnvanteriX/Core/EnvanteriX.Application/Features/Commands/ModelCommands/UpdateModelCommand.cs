using EnvanteriX.Application.Features.Results.ModelResults;
using MediatR;

namespace EnvanteriX.Application.Features.Commands.ModelCommands
{
    public class UpdateModelCommand : IRequest<UpdateModelCommandResult>
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
    }
}
