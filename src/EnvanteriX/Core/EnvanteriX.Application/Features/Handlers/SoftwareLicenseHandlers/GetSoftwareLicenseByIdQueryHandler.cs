using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.SoftwareLicenseQueries;
using EnvanteriX.Application.Features.Results.SoftwareLicenseResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.SoftwareLicenseHandlers
{
    public class GetSoftwareLicenseByIdQueryHandler : BaseHandler, IRequestHandler<GetSoftwareLicenseByIdQuery, GetSoftwareLicenseByIdQueryResult>
    {
        public GetSoftwareLicenseByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<GetSoftwareLicenseByIdQueryResult> Handle(GetSoftwareLicenseByIdQuery request, CancellationToken cancellationToken)
        {
            var license = await _unitOfWork.GetReadRepository<SoftwareLicense>()
                .GetAsync(x => x.Id == request.SoftwareLicenseId && !x.IsDeleted);

            if (license == null)
                throw new KeyNotFoundException($"Software license with ID {request.SoftwareLicenseId} not found.");

            var result = _mapper.Map<GetSoftwareLicenseByIdQueryResult>(license);
            return result;
        }
    }
}
