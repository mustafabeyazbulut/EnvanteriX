using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetMovementExceptions
{
    class AssetMovementNotFoundException : BaseException
    {
        public AssetMovementNotFoundException()
            : base("Varlık hareketi bulunamadı.")
        {
        }
        public AssetMovementNotFoundException(string message) : base(message)
        {
        }
    }
}
