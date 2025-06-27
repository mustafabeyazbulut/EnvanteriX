using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.ModelQueries;
using EnvanteriX.Application.Features.Results.ModelResults;
using EnvanteriX.Application.Features.Rules.ModelRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.ModelHandlers
{
    public class GetModelByIdQueryHandler : BaseHandler, IRequestHandler<GetModelByIdQuery, GetModelByIdQueryResult>
    {
        private readonly ModelRules _modelRules;
        public GetModelByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ModelRules modelRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _modelRules = modelRules;
        }

        public async Task<GetModelByIdQueryResult> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Model>()
                .GetAsync(
                predicate: x => x.Id == request.Id && !x.IsDeleted,
                include: x => x.Include(m => m.Brand)
                );
            await _modelRules.ModelShouldExist(model);
            var map = _mapper.Map<GetModelByIdQueryResult, Model>(model, config: cfg =>
            {
                cfg.CreateMap<Model, GetModelByIdQueryResult>()
                   .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName));
            });

            return map;
        }
    }
}
