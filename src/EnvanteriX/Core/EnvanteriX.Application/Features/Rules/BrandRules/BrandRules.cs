using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.BrandExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.BrandRules
{
    public class BrandRules : BaseRules
    {
        public Task BrandShouldExist(Brand? brand)
        {
            if (brand is null) throw new BrandNotFoundException();
            return Task.CompletedTask;
        }
        public Task BrandAlreadyExists(Brand? brand)
        {
            if (brand is not null) throw new BrandAlreadyExistsException(brand.BrandName);
            return Task.CompletedTask;
        }
    }
}
