using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.ModelQueries;
using EnvanteriX.Application.Features.Results.ModelResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace EnvanteriX.Application.Features.Handlers.ModelHandlers
{
    public class GetModelByIdQueryHandler : BaseHandler, IRequestHandler<GetModelByIdQuery, GetModelByIdQueryResult>
    {
        public GetModelByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        { }

        public async Task<GetModelByIdQueryResult> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Model>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (model == null) 
                throw new NotFoundException($"Model with ID {request.Id} not found.");

            return new GetModelByIdQueryResult
            {
                Id = model.Id,
                ModelName = model.ModelName,
                BrandId = model.BrandId,
                BrandName = model.Brand?.BrandName
            };
        }
    }
}
