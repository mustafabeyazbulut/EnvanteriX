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
    public class UpdateLocationCommandHandler : BaseHandler, IRequestHandler<UpdateLocationCommand, Unit>
    {
        private readonly LocationRules _locationRules;
        public UpdateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LocationRules locationRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _locationRules = locationRules;
        }

        public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id );
            await _locationRules.LocationShouldExist(location);

            if (!string.Equals(location.Building, request.Building, StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(location.Floor, request.Floor, StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(location.Room, request.Room, StringComparison.OrdinalIgnoreCase))
            { // Değerlerden en az biri farklı ise kontrol edicez yeni haliyle başka kayıt var mı diye
                bool locationExists = await _unitOfWork.GetReadRepository<Location>()
                                        .AnyAsync(l => l.Building.ToUpper() == request.Building.ToUpper() &&
                                                  l.Floor.ToUpper() == request.Floor.ToUpper() &&
                                                  l.Room.ToUpper() == request.Room.ToUpper());
                await _locationRules.LocationAlreadyExists(locationExists, $"Bina: {request.Building}, Kat: {request.Floor}, Oda: {request.Room}");
            }
            location.Building = request.Building;
            location.Floor = request.Floor;
            location.Room = request.Room;
            location.Description = request.Description;
            await _unitOfWork.GetWriteRepository<Location>().UpdateAsync(location);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
