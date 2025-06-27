using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.ModelExceptions
{
    public class ModelShouldExistException : BaseException
    {
        public ModelShouldExistException()
          : base("Belirtilen Model sistemde bulunmamaktadır.") { }
    }
}
