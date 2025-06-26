using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AuthExceptions
{
    public class EmailAddressShouldBeValidException : BaseException
    {
        public EmailAddressShouldBeValidException() : base("Böyle bir email adresi bulunmamaktadır.") { }
    }
}
