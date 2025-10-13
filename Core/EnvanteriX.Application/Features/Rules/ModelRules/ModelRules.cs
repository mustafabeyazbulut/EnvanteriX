using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.ModelExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.ModelRules
{
    public class ModelRules : BaseRules
    {
        public Task ModelShouldExist(Model? model)
        {
            if (model is null)
                throw new ModelShouldExistException();
            return Task.CompletedTask;
        }
        public Task ModelAlreadyExists(Model? model)
        {
            if (model is not null) throw new ModelAlreadyExistsException(model.ModelName);
            return Task.CompletedTask;
        }
        public async Task ModelShouldNotHaveAnyModel(Model? model,bool hasAnyAsset)
        {
            if (model is null) throw new ModelShouldExistException();

            if (hasAnyAsset)
                throw new ModelShouldNotHaveAnyModelException(model.ModelName);
        }
    }
}
