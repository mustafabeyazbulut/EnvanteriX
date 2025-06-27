using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.ModelQueries;
using EnvanteriX.Application.Features.Results.ModelResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


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
                        .GetAllAsync(
                            predicate: x => !x.IsDeleted,
                            include: x => x.Include(m => m.Brand)
                        );
            var map = _mapper.Map<GetAllModelsQueryResult, Model>(models, config: cfg =>
            {
                cfg.CreateMap<Model, GetAllModelsQueryResult>()
                   .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName));
            });
            return map.ToList();
        }
    }
}
