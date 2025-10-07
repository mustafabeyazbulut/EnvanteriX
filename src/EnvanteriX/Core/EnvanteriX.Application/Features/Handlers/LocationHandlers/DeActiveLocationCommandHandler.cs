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
    public class DeActiveLocationCommandHandler : BaseHandler, IRequestHandler<DeActiveLocationCommand, Unit>
    {
        private readonly LocationRules _locationRules;
        public DeActiveLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LocationRules locationRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _locationRules = locationRules;
        }
        public async Task<Unit> Handle(DeActiveLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id);
            await _locationRules.LocationShouldExist(location);
            location.IsDeleted = false;
            await _unitOfWork.GetWriteRepository<Location>().UpdateAsync(location);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
