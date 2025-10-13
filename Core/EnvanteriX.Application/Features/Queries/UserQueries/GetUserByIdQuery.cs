using MediatR;
using EnvanteriX.Application.Features.Results.UserResults;

namespace EnvanteriX.Application.Features.Queries.UserQueries
{
    public class GetUserByIdQuery : IRequest<GetUserByIdQueryResult>
    {
        public int UserId { get; set; }

        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
