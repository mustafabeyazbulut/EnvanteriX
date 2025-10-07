using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.BrandCommands;
using EnvanteriX.Application.Features.Rules.BrandRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.BrandHandlers
{
    public class ActiveBrandCommandHandler : BaseHandler, IRequestHandler<ActiveBrandCommand, Unit>
    {
        private readonly BrandRules _brandRules;

        public ActiveBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, BrandRules brandRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _brandRules = brandRules;
        }

        public async Task<Unit> Handle(ActiveBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.GetReadRepository<Brand>()
                             .GetAsync(
                                 predicate: x => x.Id == request.Id,
                                 include: x => x.Include(b => b.Models)
                             );
            await _brandRules.BrandShouldExist(brand);
            brand.IsDeleted = false;
            await _unitOfWork.GetWriteRepository<Brand>().UpdateAsync(brand);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
