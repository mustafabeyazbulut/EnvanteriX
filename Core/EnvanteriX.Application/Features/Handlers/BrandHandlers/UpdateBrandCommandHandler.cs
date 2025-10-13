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
    public class UpdateBrandCommandHandler : BaseHandler, IRequestHandler<UpdateBrandCommand, Unit>
    {
        private readonly BrandRules _brandRules;
        public UpdateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, BrandRules brandRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _brandRules = brandRules;
        }

        public async Task<Unit> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.GetReadRepository<Brand>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            await _brandRules.BrandShouldExist(brand);

            if (!string.Equals(brand.BrandName, request.BrandName, StringComparison.OrdinalIgnoreCase))
            {
                var existingBrandWithSameName = await _unitOfWork.GetReadRepository<Brand>()
                    .GetAsync(x => x.BrandName.ToLower() == request.BrandName.ToLower() && x.Id != request.Id && !x.IsDeleted);

                await _brandRules.BrandAlreadyExists(existingBrandWithSameName);
            }

            brand.BrandName = request.BrandName;
            await _unitOfWork.GetWriteRepository<Brand>().UpdateAsync(brand);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
