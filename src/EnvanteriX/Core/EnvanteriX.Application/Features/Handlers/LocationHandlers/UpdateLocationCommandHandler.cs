using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.LocationCommands;
using EnvanteriX.Application.Features.Results.LocationResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace EnvanteriX.Application.Features.Handlers.LocationHandlers
{
    public class UpdateLocationCommandHandler : BaseHandler, IRequestHandler<UpdateLocationCommand, UpdateLocationCommandResult>
    {
        public UpdateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapper, unitOfWork, httpContextAccessor) { }

        public async Task<UpdateLocationCommandResult> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _unitOfWork.GetReadRepository<Location>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (location == null)
                throw new NotFoundException("Location not found");

            location.Building = request.Building;
            location.Floor = request.Floor;
            location.Room = request.Room;
            location.Description = request.Description;

            await _unitOfWork.SaveAsync();

            return new UpdateLocationCommandResult
            {
                Id = location.Id,
                Building = location.Building
            };
        }
    }
}
