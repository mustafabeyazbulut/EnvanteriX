using EnvanteriX.Application.Features.Results.AssetResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.AssetQueries
{
    public class GetAllActiveAssetByEmailQuery: IRequest<List<GetAllActiveAssetByEmailQueryResult>>
    {
        public string Email { get; set; }
        public GetAllActiveAssetByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
