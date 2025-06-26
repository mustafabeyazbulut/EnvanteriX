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
    public class GetLocationByIdQueryHandler : BaseHandler, IRequestHandler<GetLocationByIdQuery, GetLocationByIdQueryResult>
    {
        public GetLocationByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<GetLocationByIdQueryResult> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with ID {request.Id} not found.");
            }

            return _mapper.Map<GetLocationByIdQueryResult>(location);
        }
    }
}
