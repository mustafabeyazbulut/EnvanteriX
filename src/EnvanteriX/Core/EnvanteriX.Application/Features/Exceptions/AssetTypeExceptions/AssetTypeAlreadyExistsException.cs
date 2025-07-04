using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetTypeExceptions
{
    public class AssetTypeAlreadyExistsException : BaseException
    {
        public AssetTypeAlreadyExistsException(string location)
            : base($"'{location}' adlı varlık tipi zaten mevcut.")
        {
        }
    }
}
