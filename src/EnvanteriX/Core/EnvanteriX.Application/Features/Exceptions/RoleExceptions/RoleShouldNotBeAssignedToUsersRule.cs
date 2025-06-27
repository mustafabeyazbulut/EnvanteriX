using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Exceptions.RoleExceptions
{
    public class RoleAssignedToUsersException : Exception
    {
        public RoleAssignedToUsersException(string roleName)
            : base($"'{roleName}' rolü bir veya daha fazla kullanıcıya atanmış, bu nedenle silinemez.")
        {
        }
    }
}
