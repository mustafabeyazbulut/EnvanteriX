using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetMovementExceptions
{
    class AssetMovementAlreadyExistsException : BaseException
    {
        public AssetMovementAlreadyExistsException( )
            : base("Bu varlık hareketi zaten mevcut.") // Localized message
        {
        }
    }
}
