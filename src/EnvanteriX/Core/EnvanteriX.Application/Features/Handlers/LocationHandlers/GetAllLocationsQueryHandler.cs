using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.LocationQueries;
using EnvanteriX.Application.Features.Results.LocationResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.LocationHandlers
{
    public class GetAllLocationsQueryHandler : BaseHandler, IRequestHandler<GetAllLocationsQuery, List<GetAllLocationsQueryResult>>
    {
        public GetAllLocationsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<List<GetAllLocationsQueryResult>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _unitOfWork.GetReadRepository<Location>().GetAllAsync();
            return _mapper.Map<GetAllLocationsQueryResult, Location>(locations).ToList();
        }
    }
}
