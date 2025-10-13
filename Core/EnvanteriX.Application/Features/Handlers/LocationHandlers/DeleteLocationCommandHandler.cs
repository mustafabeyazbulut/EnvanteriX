using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.LocationCommands;
using EnvanteriX.Application.Features.Rules.LocationRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.LocationHandlers
{
    public class DeleteLocationCommandHandler : BaseHandler, IRequestHandler<DeleteLocationCommand, Unit>
    {
        private readonly LocationRules _locationRules;
        public DeleteLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LocationRules locationRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _locationRules = locationRules;
        }
        public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id );
            await _locationRules.LocationShouldExist(location);
            var hasAnyLocation = await _unitOfWork.GetReadRepository<Asset>().AnyAsync(x=>x.LocationId==request.Id && !x.IsDeleted);
            await _locationRules.LocationShouldNotHaveAnyAsset(hasAnyLocation, $"Bina: {location.Building}, Kat: {location.Floor}, Oda: {location.Room}");
            await _unitOfWork.GetWriteRepository<Location>().HardDeleteAsync(location);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
