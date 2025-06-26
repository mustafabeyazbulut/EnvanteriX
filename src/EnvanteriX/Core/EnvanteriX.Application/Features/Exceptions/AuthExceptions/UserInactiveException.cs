using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AuthExceptions
{
    public class UserInactiveException : BaseException
    {
        public UserInactiveException() : base("Kullanıcı pasif durumda ve bu işlemi gerçekleştiremez.") { }
    }
}
