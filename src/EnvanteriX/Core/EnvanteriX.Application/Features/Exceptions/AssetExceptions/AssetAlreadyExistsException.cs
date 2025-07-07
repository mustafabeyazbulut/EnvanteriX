using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetExceptions
{
    public class AssetAlreadyExistsException : BaseException
    {
        public AssetAlreadyExistsException(string vendorName)
            : base($"'{vendorName}' adlı varlık zaten mevcut.")
        {
        }
    }
}
