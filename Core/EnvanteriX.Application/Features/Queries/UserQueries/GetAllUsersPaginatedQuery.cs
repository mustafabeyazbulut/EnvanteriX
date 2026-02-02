using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Results.UserResults;
using MediatR;

namespace EnvanteriX.Application.Features.Queries.UserQueries
{
    public class GetAllUsersPaginatedQuery : IRequest<PaginatedList<GetAllUsersQueryResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? Role { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
