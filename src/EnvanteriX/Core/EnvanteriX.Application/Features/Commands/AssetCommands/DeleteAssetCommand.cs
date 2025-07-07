using MediatR;

namespace EnvanteriX.Application.Features.Commands.AssetCommands
{
    public class DeleteAssetCommand : IRequest<Unit>
    {
       public  int Id;
    }
}
