using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.LocationCommands;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.LocationHandlers
{
    public class DeleteLocationCommandHandler : BaseHandler, IRequestHandler<DeleteLocationCommand, Unit>
    {
        public DeleteLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (location == null)
                throw new KeyNotFoundException($"Location with ID {request.Id} not found or already deleted.");

            location.IsDeleted = true;
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
