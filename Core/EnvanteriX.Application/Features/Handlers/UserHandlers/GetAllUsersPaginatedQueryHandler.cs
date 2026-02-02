using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.UserQueries;
using EnvanteriX.Application.Features.Results.UserResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.UserHandlers
{
    public class GetAllUsersPaginatedQueryHandler : IRequestHandler<GetAllUsersPaginatedQuery, PaginatedList<GetAllUsersQueryResult>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetAllUsersPaginatedQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetAllUsersQueryResult>> Handle(GetAllUsersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users.AsQueryable();

            // IsDeleted filtresi
            if (request.IsDeleted.HasValue)
            {
                query = query.Where(x => x.IsDeleted == request.IsDeleted.Value);
            }

            // SearchTerm filtresi
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.FullName.ToLower().Contains(searchTerm) ||
                    x.UserName.ToLower().Contains(searchTerm) ||
                    x.Email.ToLower().Contains(searchTerm));
            }

            // DB filtrelerine uyan tüm kullanıcıları çek
            var allFilteredUsers = await query
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            // Her kullanıcı için rol bilgisini al
            var userResults = new List<GetAllUsersQueryResult>();
            foreach (var user in allFilteredUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var roleName = roles.FirstOrDefault();

                var mappedUser = _mapper.Map<GetAllUsersQueryResult, User>(user);
                mappedUser.Role = roleName;

                userResults.Add(mappedUser);
            }

            // Role filtresi - rol bilgisi alındıktan sonra uygula
            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                userResults = userResults
                    .Where(x => x.Role != null && x.Role.Equals(request.Role, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Toplam kayıt sayısı (tüm filtreler uygulandıktan sonra)
            var totalCount = userResults.Count;

            // Sayfalama
            var pagedResults = userResults
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new PaginatedList<GetAllUsersQueryResult>(
                pagedResults,
                totalCount,
                request.PageNumber,
                request.PageSize);
        }
    }
}
