using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.LocationQueries;
using EnvanteriX.Application.Features.Results.LocationResults;
using EnvanteriX.Application.Features.Rules.LocationRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.LocationHandlers
{
    public class GetLocationByIdQueryHandler : BaseHandler, IRequestHandler<GetLocationByIdQuery, GetLocationByIdQueryResult>
    {
        private readonly LocationRules _locationRules;
        public GetLocationByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LocationRules locationRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _locationRules = locationRules;
        }

        public async Task<GetLocationByIdQueryResult> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id);
            await _locationRules.LocationShouldExist(location);
            return _mapper.Map<GetLocationByIdQueryResult, Location>(location);
        }
    }
}
