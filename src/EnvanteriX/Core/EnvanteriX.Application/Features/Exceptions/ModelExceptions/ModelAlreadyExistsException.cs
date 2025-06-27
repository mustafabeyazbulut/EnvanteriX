using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.ModelExceptions
{
     public class ModelAlreadyExistsException : BaseException
    {
        public ModelAlreadyExistsException(string brandName)
           : base($"'{brandName}' adlı model zaten mevcut.")
        {
        }
    }
}
