using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.LocationCommands;
using EnvanteriX.Application.Features.Results.LocationResults;
using EnvanteriX.Application.Features.Rules.LocationRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.LocationHandlers
{
    public class CreateLocationCommandHandler : BaseHandler, IRequestHandler<CreateLocationCommand, CreateLocationCommandResult>
    {
        private readonly LocationRules _locationRules;
        public CreateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, LocationRules locationRules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _locationRules = locationRules;
        }

        public async Task<CreateLocationCommandResult> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            bool locationExists = await _unitOfWork.GetReadRepository<Location>()
                                        .AnyAsync(l => l.Building.ToUpper() == request.Building.ToUpper());
            await _locationRules.LocationAlreadyExists( locationExists, $"{request.Building}");

            var location = _mapper.Map<Location, CreateLocationCommand>(request);
            location.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<Location>().AddAsync(location);
            await _unitOfWork.SaveAsync();
            var result = _mapper.Map<CreateLocationCommandResult, Location>(location);
            return result;
        }
    }
}
