using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.UserExceptions
{
    public class UserHasAssetMovementsException : BaseException
    {
        public UserHasAssetMovementsException()
           : base("Bu kullanıcıya ait demirbaş hareketleri bulunduğu için silinemez.")
        {
        }
    }
}
