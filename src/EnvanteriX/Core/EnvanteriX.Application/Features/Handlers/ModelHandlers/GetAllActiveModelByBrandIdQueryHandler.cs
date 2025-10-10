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
    public class GetAllActiveModelByBrandIdQueryHandler : BaseHandler, IRequestHandler<GetAllActiveModelByBrandIdQuery, List<GetAllActiveModelByBrandIdQueryResult>>
    {
        public GetAllActiveModelByBrandIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<List<GetAllActiveModelByBrandIdQueryResult>> Handle(GetAllActiveModelByBrandIdQuery request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.GetReadRepository<Model>()
                       .GetAllAsync(
                           predicate: m => !m.IsDeleted &&m.BrandId==request.BrandId,
                           include: x => x.Include(m => m.Brand)
                       );

            var map = _mapper.Map<GetAllActiveModelByBrandIdQueryResult, Model>(models, config: cfg =>
            {
               
            });
            return map.ToList();
        }
    }
}
