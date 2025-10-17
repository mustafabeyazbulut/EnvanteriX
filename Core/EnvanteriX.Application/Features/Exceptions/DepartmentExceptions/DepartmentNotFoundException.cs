using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.DepartmentExceptions
{

    public class DepartmentNotFoundException : BaseException
    {
        public DepartmentNotFoundException() : base("Departman bulunamadı.") { }
    }
}
