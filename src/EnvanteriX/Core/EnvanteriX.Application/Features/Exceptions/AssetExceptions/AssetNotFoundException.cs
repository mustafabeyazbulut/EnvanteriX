using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetExceptions
{
    public class AssetNotFoundException : BaseException
    {
        public AssetNotFoundException()
            : base("Varlık bulunamadı.")
        {
        }
        public AssetNotFoundException(string message) : base(message)
        {
        }
    }
}
