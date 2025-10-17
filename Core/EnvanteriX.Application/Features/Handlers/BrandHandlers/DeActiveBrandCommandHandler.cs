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
    public class DeActiveBrandCommandHandler:BaseHandler,IRequestHandler<DeActiveBrandCommand, Unit>
    {
        private readonly BrandRules _brandRules;

        public DeActiveBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, BrandRules brandRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _brandRules = brandRules;
        }

        public async Task<Unit> Handle(DeActiveBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.GetReadRepository<Brand>()
                            .GetAsync(
                                predicate: x => x.Id == request.Id,
                                include: x => x.Include(b => b.Models)
                            );
            await _brandRules.BrandShouldExist(brand);
            brand.IsDeleted = true;
            brand.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<Brand>().UpdateAsync(brand);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
