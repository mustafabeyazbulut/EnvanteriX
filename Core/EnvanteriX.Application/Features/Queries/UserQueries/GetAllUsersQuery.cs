using MediatR;
using EnvanteriX.Application.Features.Results.UserResults;

namespace EnvanteriX.Application.Features.Queries.UserQueries
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersQueryResult>>
    {
    }
}
