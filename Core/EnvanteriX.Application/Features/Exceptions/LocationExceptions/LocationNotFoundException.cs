using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.LocationExceptions
{
    public class LocationNotFoundException : BaseException
    {
        public LocationNotFoundException() : base("Lokasyon bulunamadı.") { }
    }
}
