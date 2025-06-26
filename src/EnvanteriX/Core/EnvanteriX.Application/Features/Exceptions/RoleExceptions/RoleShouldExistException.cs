using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.RoleExceptions
{
    public class RoleShouldExistException : BaseException
    {
        public RoleShouldExistException()
            : base("Belirtilen Rol sistemde bulunmamaktadır.") { }
    }
}
