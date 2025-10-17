using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.DepartmentCommands;
using EnvanteriX.Application.Features.Rules.DepartmentRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.DepartmentHandlers
{
    public class UpdateDepartmentCommandHandler : BaseHandler, IRequestHandler<UpdateDepartmentCommand, Unit>
    {
        private readonly DepartmentRules _departmentRules;
        public UpdateDepartmentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, DepartmentRules departmentRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _departmentRules = departmentRules;
        }

        public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.GetReadRepository<Domain.Entities.Department>().GetAsync(x => x.Id == request.Id );
            await _departmentRules.DepartmentShouldExist(department);

            if (!string.Equals(department.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            { // Değerlerden en az biri farklı ise kontrol edicez yeni haliyle başka kayıt var mı diye
                bool departmentExists = await _unitOfWork.GetReadRepository<Domain.Entities.Department>()
                                        .AnyAsync(d => d.Name.ToUpper() == request.Name.ToUpper() );
                await _departmentRules.DepartmentAlreadyExists(departmentExists, $"{request.Name}");
            }
            department.Name = request.Name;
            department.Description = request.Description;
            department.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<Domain.Entities.Department>().UpdateAsync(department);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
