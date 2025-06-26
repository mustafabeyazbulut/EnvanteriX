using MediatR;
using EnvanteriX.Application.Features.Results.SoftwareLicenseResults;
using System.Collections.Generic;

namespace EnvanteriX.Application.Features.Queries.SoftwareLicenseQueries
{
    public class GetAllSoftwareLicensesQuery : IRequest<List<GetAllSoftwareLicensesQueryResult>>
    {
    }
}
