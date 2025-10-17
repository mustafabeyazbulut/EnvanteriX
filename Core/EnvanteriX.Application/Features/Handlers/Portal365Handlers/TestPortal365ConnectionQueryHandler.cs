using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.Portal365Queries;
using EnvanteriX.Application.Features.Rules.Portal365Rules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.Portal365Interfaces;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.Portal365Handlers
{
    public class TestPortal365ConnectionQueryHandler : BaseHandler, IRequestHandler<TestPortal365ConnectionQuery, Unit>
    {
        private readonly Portal365Rules _portal365Rules;
        private readonly IPortal365Service _portal365Service;
        public TestPortal365ConnectionQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IPortal365Service portal365Service, Portal365Rules portal365Rules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _portal365Service = portal365Service;
            _portal365Rules = portal365Rules;
        }

        public async Task<Unit> Handle(TestPortal365ConnectionQuery request, CancellationToken cancellationToken)
        {
            var portal365Config=await _unitOfWork.GetReadRepository<Portal365>().GetAsync(x=>x.Id==request.Id);
            await _portal365Rules.Portal365ShouldExist(portal365Config);
            var accessToken=await _portal365Service.GetAccessTokenAsync(portal365Config);
            await _portal365Rules.AccessTokenMustExist(accessToken);
            return Unit.Value;
        }
    }
}
