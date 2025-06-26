using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.BrandCommands;
using EnvanteriX.Application.Features.Results.BrandResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.BrandHandlers
{
    public class CreateBrandCommandHandler : BaseHandler, IRequestHandler<CreateBrandCommand, CreateBrandCommandResult>
    {
        public CreateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
            : base(mapper, unitOfWork, accessor) { }

        public async Task<CreateBrandCommandResult> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = new Brand { BrandName = request.BrandName };
            await _unitOfWork.GetWriteRepository<Brand>().AddAsync(brand);
            await _unitOfWork.SaveAsync();
            return new CreateBrandCommandResult { Id = brand.Id, BrandName = brand.BrandName };
        }
    }
}
