using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.DepartmentCommands;
using EnvanteriX.Application.Features.Rules.DepartmentRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.DepartmentHandlers
{
    public class DeleteDepartmentCommandHandler : BaseHandler, IRequestHandler<DeleteDepartmentCommand, Unit>
    {
        private readonly DepartmentRules _departmentRules;
        public DeleteDepartmentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, DepartmentRules departmentRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _departmentRules = departmentRules;
        }

        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.GetReadRepository<Domain.Entities.Department>().GetAsync(x => x.Id == request.Id );
            await _departmentRules.DepartmentShouldExist(department);

            var hasAnyDepartment = await _unitOfWork.GetReadRepository<Domain.Entities.Asset>().AnyAsync(x => x.AssignedDepartmentId == request.Id && !x.IsDeleted);
            await _departmentRules.DepartmentShouldNotHaveAnyAsset(hasAnyDepartment, $"{department.Name}");
            await _unitOfWork.GetWriteRepository<Domain.Entities.Department>().HardDeleteAsync(department);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
