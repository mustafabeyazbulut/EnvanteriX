using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.ModelQueries;
using EnvanteriX.Application.Features.Results.ModelResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace EnvanteriX.Application.Features.Handlers.ModelHandlers
{
    public class GetAllModelsQueryHandler : BaseHandler, IRequestHandler<GetAllModelsQuery, List<GetAllModelsQueryResult>>
    {
        public GetAllModelsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        { }

        public async Task<List<GetAllModelsQueryResult>> Handle(GetAllModelsQuery request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.GetReadRepository<Model>()
                .GetAllAsync(x => !x.IsDeleted);

            return models.Select(model => new GetAllModelsQueryResult
            {
                Id = model.Id,
                ModelName = model.ModelName,
                BrandId = model.BrandId,
                BrandName = model.Brand?.BrandName
            }).ToList();
        }
    }
}
