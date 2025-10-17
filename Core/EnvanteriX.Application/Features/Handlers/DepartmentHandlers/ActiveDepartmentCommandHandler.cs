using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.DepartmentCommands;
using EnvanteriX.Application.Features.Rules.DepartmentRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.DepartmentHandlers
{
    public class ActiveDepartmentCommandHandler : BaseHandler, IRequestHandler<ActiveDepartmentCommand, Unit>
    {
        private readonly DepartmentRules _departmentRules;
        public ActiveDepartmentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, DepartmentRules departmentRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _departmentRules = departmentRules;
        }

        public async Task<Unit> Handle(ActiveDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department =await  _unitOfWork.GetReadRepository<Domain.Entities.Department>().GetAsync(x => x.Id == request.Id);
            await _departmentRules.DepartmentShouldExist(department);
            department.IsDeleted = false;
            department.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<Domain.Entities.Department>().UpdateAsync(department);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
