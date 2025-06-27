using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.BrandCommands;
using EnvanteriX.Application.Features.Results.BrandResults;
using EnvanteriX.Application.Features.Rules.BrandRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.BrandHandlers
{
    public class CreateBrandCommandHandler : BaseHandler, IRequestHandler<CreateBrandCommand, CreateBrandCommandResult>
    {
        private readonly BrandRules _brandRules;
        public CreateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor accessor, BrandRules brandRules)
            : base(mapper, unitOfWork, accessor)
        {
            _brandRules = brandRules;
        }

        public async Task<CreateBrandCommandResult> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await _unitOfWork.GetReadRepository<Brand>()
                .GetAsync(b => b.BrandName.ToLower() == request.BrandName.ToLower());
            await _brandRules.BrandAlreadyExists(existingBrand);
            var brand = _mapper.Map<Brand, CreateBrandCommand>(request);
            await _unitOfWork.GetWriteRepository<Brand>().AddAsync(brand);
            await _unitOfWork.SaveAsync();
            var map = _mapper.Map<CreateBrandCommandResult, Brand>(brand);
            return map;
        }
    }
}
