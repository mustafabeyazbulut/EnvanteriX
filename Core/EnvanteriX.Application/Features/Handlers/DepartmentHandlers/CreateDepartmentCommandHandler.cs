using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.DepartmentCommands;
using EnvanteriX.Application.Features.Results.DepartmentResults;
using EnvanteriX.Application.Features.Rules.DepartmentRules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnvanteriX.Application.Features.Handlers.DepartmentHandlers
{
    public class CreateDepartmentCommandHandler : BaseHandler, IRequestHandler<CreateDepartmentCommand, CreateDepartmentCommandResult>
    {
        private readonly DepartmentRules _departmentRules;
        public CreateDepartmentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, DepartmentRules departmentRules) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _departmentRules = departmentRules;
        }

        public async Task<CreateDepartmentCommandResult> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            bool departmentExists = await _unitOfWork.GetReadRepository<Domain.Entities.Department>()
                                        .AnyAsync(d => d.Name.ToUpper() == request.Name.ToUpper());
            await _departmentRules.DepartmentAlreadyExists(departmentExists, $"{request.Name}");

            var department = _mapper.Map<Department, CreateDepartmentCommand>(request);
            department.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<Department>().AddAsync(department);
            await _unitOfWork.SaveAsync();
            var result = _mapper.Map<CreateDepartmentCommandResult, Department>(department);
            return result;
        }
    }
}
