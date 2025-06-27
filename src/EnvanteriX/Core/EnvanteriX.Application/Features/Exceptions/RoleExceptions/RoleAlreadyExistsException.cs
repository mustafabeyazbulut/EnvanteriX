using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.RoleExceptions
{
   
    public class RoleAlreadyExistsException : BaseException
    {
        public RoleAlreadyExistsException(string brandName)
           : base($"'{brandName}' adlı Rol zaten mevcut.")
        {
        }
    }
}
