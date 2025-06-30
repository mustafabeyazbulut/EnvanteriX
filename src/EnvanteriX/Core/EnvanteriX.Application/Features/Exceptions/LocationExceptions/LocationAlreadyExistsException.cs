using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.LocationExceptions
{

    public class LocationAlreadyExistsException : BaseException
    {
        public LocationAlreadyExistsException(string location)
            : base($"'{location}' adlı lokasyon zaten mevcut.")
        {
        }
    }
}
