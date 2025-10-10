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
    public class GetAllActiveLocationsQueryHandler : BaseHandler, IRequestHandler<GetAllActiveLocationsQuery, List<GetAllActiveLocationsQueryResult>>
    {
        public GetAllActiveLocationsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllActiveLocationsQueryResult>> Handle(GetAllActiveLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _unitOfWork.GetReadRepository<Location>().GetAllAsync(x=>x.IsDeleted==false);
            return _mapper.Map<GetAllActiveLocationsQueryResult, Location>(locations).ToList();
        }
    }
}
