using System.Collections.Generic;
using MediatR;
using EnvanteriX.Application.Features.Results.ModelResults;

namespace EnvanteriX.Application.Features.Queries.ModelQueries
{
    public class GetAllModelsQuery : IRequest<List<GetAllModelsQueryResult>>
    {
    }
}
