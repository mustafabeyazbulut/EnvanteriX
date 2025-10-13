using EnvanteriX.Application.Features.Results.UserResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.UserQueries
{
    
    public class GetUserByEmailQuery : IRequest<GetUserByEmailQueryResult>
    {
        public string Email { get; set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
