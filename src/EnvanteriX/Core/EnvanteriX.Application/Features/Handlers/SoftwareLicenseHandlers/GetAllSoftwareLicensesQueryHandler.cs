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
    public class GetAllSoftwareLicensesQueryHandler : BaseHandler, IRequestHandler<GetAllSoftwareLicensesQuery, List<GetAllSoftwareLicensesQueryResult>>
    {
        public GetAllSoftwareLicensesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllSoftwareLicensesQueryResult>> Handle(GetAllSoftwareLicensesQuery request, CancellationToken cancellationToken)
        {
            var licenses = await _unitOfWork.GetReadRepository<SoftwareLicense>()
                .GetAllAsync(x => !x.IsDeleted);

            var result = _mapper.Map<List<GetAllSoftwareLicensesQueryResult>>(licenses);
            return result;
        }
    }
}
