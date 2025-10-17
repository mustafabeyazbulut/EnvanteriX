using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Interfaces.Portal365Interfaces
{
    public  interface IPortal365Service
    {
        Task<List<dynamic>> GetAllUsersAsync(Portal365 model);
        Task<string> GetAccessTokenAsync(Portal365 model);
    }
}
 