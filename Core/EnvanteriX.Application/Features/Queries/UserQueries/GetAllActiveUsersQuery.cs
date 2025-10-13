using EnvanteriX.Application.Features.Results.UserResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.UserQueries
{
    public class GetAllActiveUsersQuery:IRequest<List<GetAllActiveUsersQueryResult>>
    {
    }
}
