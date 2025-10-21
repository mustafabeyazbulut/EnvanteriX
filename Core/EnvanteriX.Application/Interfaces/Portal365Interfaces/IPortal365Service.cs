using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Interfaces.Portal365Interfaces
{
    public  interface IPortal365Service
    {
        Task<List<dynamic>> GetAllUsersAsync(Portal365 model);
        Task<string> GetAccessTokenAsync(Portal365 model);
        Task SendEmailAsync(Portal365 model,string toMail,string subject, string emailBody);
        Task<bool> LoginAsync( string username, string password);
    }
}
 