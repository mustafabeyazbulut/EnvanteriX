using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.DepartmentExceptions
{
    public class DepartmentShouldNotHaveAnyAssetException : BaseException
    {
        public DepartmentShouldNotHaveAnyAssetException(string name)
            : base($"'{name}' adlı departmana ait kayıtlı varlıklar var. Önce varlıkların departmanlarını değiştirmelisiniz.")
        {
        }
    }
}
