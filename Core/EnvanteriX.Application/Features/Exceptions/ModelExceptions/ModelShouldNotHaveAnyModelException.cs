using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.ModelExceptions
{
       public class ModelShouldNotHaveAnyModelException : BaseException
    {
        public ModelShouldNotHaveAnyModelException(string Name)
            : base($"'{Name}' adlı markaya ait kayıtlı varlıklar var. Önce varlıkları silmelisiniz.")
        {
        }
    }
}
