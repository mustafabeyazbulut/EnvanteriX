using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.LocationCommands;
using EnvanteriX.Application.Features.Results.LocationResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.LocationHandlers
{
    public class CreateLocationCommandHandler : BaseHandler, IRequestHandler<CreateLocationCommand, CreateLocationCommandResult>
    {
        public CreateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<CreateLocationCommandResult> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<Location>(request);
            await _unitOfWork.GetWriteRepository<Location>().AddAsync(location);
            await _unitOfWork.SaveAsync();

            return new CreateLocationCommandResult
            {
                Id = location.Id,
                Building = location.Building
            };
        }
    }
}
