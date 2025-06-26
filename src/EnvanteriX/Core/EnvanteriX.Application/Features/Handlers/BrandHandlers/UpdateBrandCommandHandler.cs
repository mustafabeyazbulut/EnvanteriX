using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.BrandCommands;
using EnvanteriX.Application.Features.Results.BrandResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace EnvanteriX.Application.Features.Handlers.BrandHandlers
{
    public class UpdateBrandCommandHandler : BaseHandler, IRequestHandler<UpdateBrandCommand, UpdateBrandCommandResult>
    {
        public UpdateBrandCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<UpdateBrandCommandResult> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.GetReadRepository<Brand>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (brand == null)
                throw new NotFoundException($"Brand with ID {request.Id} not found.");

            brand.BrandName = request.BrandName;
            await _unitOfWork.SaveAsync();

            return new UpdateBrandCommandResult { Id = brand.Id, BrandName = brand.BrandName };
        }
    }
}
