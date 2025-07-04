using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.VendorExceptions
{
    public class VendorAlreadyExistsException : BaseException
    {
        public VendorAlreadyExistsException(string vendorName)
            : base($"'{vendorName}' adlı vendor zaten mevcut.")
        {
        }
    }
}
