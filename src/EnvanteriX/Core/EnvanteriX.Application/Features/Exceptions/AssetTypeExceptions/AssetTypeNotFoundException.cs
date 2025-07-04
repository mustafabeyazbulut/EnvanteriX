using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.AssetTypeExceptions
{
    public class AssetTypeNotFoundException : BaseException
    {
        public AssetTypeNotFoundException() : base("Varlık Tipi bulunamadı.") { }
    }
}
