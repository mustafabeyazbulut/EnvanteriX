using EnvanteriX.Application.Bases;

namespace EnvanteriX.Application.Features.Exceptions.DepartmentExceptions
{
    public class DepartmentAlreadyExistsException : BaseException
    {
        public DepartmentAlreadyExistsException(string department)
            : base($"'{department}' adlı departman zaten mevcut.")
        {
        }
    }
}
