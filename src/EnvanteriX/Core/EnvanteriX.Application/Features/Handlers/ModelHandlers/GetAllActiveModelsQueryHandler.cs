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
    public class GetAllActiveModelsQueryHandler : BaseHandler, IRequestHandler<GetAllActiveModelsQuery, List<GetAllActiveModelsQueryResult>>
    {
        public GetAllActiveModelsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllActiveModelsQueryResult>> Handle(GetAllActiveModelsQuery request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.GetReadRepository<Model>()
                        .GetAllAsync(
                            predicate: m => !m.IsDeleted,
                            include: x => x.Include(m => m.Brand)
                        );

            var map = _mapper.Map<GetAllActiveModelsQueryResult, Model>(models, config: cfg =>
            {
                cfg.CreateMap<Model, GetAllActiveModelsQueryResult>()
                   .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName));
            });
            return map.ToList();
        }
    }
}
