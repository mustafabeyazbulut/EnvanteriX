using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EnvanteriX.Application.Bases
{
    public class BaseHandler
    {
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly string _userId;
        public readonly string _userEmail;
        public readonly string _userIp;

        public BaseHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._httpContextAccessor = httpContextAccessor;
            _userId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _userEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value ?? httpContextAccessor.HttpContext?.User?.FindFirst("preferred_username")?.Value;
            //_userId = "1";
            //_userEmail = "mbeyazbulut@gmail.com";

            // Kullanıcının IP adresi
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress;

            _userIp = ip != null
                ? (ip.IsIPv4MappedToIPv6 ? ip.MapToIPv4().ToString() : ip.ToString())
                : "Unknown";
        }
    }
}
