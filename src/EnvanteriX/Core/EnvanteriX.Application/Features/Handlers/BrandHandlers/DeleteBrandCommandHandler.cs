using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.BrandCommands;
using EnvanteriX.Application.Features.Rules.BrandRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.BrandHandlers
{
    public class DeleteBrandCommandHandler : BaseHandler, IRequestHandler<DeleteBrandCommand, Unit>
    {
        private readonly BrandRules _brandRules;
        public DeleteBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, BrandRules brandRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _brandRules = brandRules;
        }

        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.GetReadRepository<Brand>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            await _brandRules.BrandShouldExist(brand);
            brand.IsDeleted = true;
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
