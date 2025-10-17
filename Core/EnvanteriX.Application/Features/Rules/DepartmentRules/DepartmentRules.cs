using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.DepartmentExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.DepartmentRules
{
    public class DepartmentRules : BaseRules
    {
        public Task DepartmentShouldExist(Department? model)
        {
            if (model is null) throw new DepartmentNotFoundException();
            return Task.CompletedTask;
        }
        public Task DepartmentAlreadyExists(Department? model)
        {
            if (model is not null) throw new DepartmentAlreadyExistsException(model.Name);
            return Task.CompletedTask;
        }
        public Task DepartmentAlreadyExists(bool DepartmentExists, string name)
        {
            if (DepartmentExists) throw new DepartmentAlreadyExistsException(name);
            return Task.CompletedTask;
        }
        public Task DepartmentShouldNotHaveAnyAsset(bool hasAnyDepartment, string name)
        {
            if (hasAnyDepartment)
                throw new DepartmentShouldNotHaveAnyAssetException(name);
            return Task.CompletedTask;
        }
    }
}
