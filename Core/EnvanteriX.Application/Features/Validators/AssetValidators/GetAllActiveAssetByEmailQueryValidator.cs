using EnvanteriX.Application.Features.Queries.AssetQueries;
using FluentValidation;

namespace EnvanteriX.Application.Features.Validators.AssetValidators
{
    public class GetAllActiveAssetByEmailQueryValidator : AbstractValidator<GetAllActiveAssetByEmailQuery>
    {
        public GetAllActiveAssetByEmailQueryValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .MaximumLength(60)
              .EmailAddress()
              .MinimumLength(8)
              .WithName("E-posta Adresi");
        }
    }
}
