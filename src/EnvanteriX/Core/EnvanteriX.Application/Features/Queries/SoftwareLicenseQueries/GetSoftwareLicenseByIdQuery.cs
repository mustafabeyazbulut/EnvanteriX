using MediatR;
using EnvanteriX.Application.Features.Results.SoftwareLicenseResults;

namespace EnvanteriX.Application.Features.Queries.SoftwareLicenseQueries
{
    public class GetSoftwareLicenseByIdQuery : IRequest<GetSoftwareLicenseByIdQueryResult>
    {
        public int SoftwareLicenseId { get; set; }

        public GetSoftwareLicenseByIdQuery(int id)
        {
            SoftwareLicenseId = id;
        }
    }
}
