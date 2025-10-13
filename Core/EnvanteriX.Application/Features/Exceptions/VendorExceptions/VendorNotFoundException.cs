using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.VendorExceptions
{
    public class VendorNotFoundException :BaseException
    {
        public VendorNotFoundException() : base("Vendor bulunamadı.")
        {
        }
        public VendorNotFoundException(string message) : base(message)
        {
        }
    }
}
