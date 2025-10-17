using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.Portal365Queries;
using EnvanteriX.Application.Features.Rules.Portal365Rules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.PasswordGenerator;
using EnvanteriX.Application.Interfaces.Portal365Interfaces;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnvanteriX.Application.Features.Handlers.Portal365Handlers
{
    public class SyncPortal365UsersQueryHandler : BaseHandler, IRequestHandler<SyncPortal365UsersQuery, Unit>
    {
        private readonly Portal365Rules _portal365Rules;
        private readonly IPortal365Service _portal365Service;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordGenerator _passwordGenerator;

        public SyncPortal365UsersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IPortal365Service portal365Service, Portal365Rules portal365Rules, UserManager<User> userManager, IPasswordGenerator passwordGenerator) : base(mapper, unitOfWork, httpContextAccessor)
        {
            _portal365Service = portal365Service;
            _portal365Rules = portal365Rules;
            _userManager = userManager;
            _passwordGenerator = passwordGenerator;
        }

        public async Task<Unit> Handle(SyncPortal365UsersQuery request, CancellationToken cancellationToken)
        {
            var portal365Config = await _unitOfWork.GetReadRepository<Portal365>().GetAsync(
                  predicate:x=>x.IsDeleted==false,
                  orderBy:q=>q.OrderByDescending(x=>x.CreatedDate)
                );
            await _portal365Rules.Portal365ShouldExist(portal365Config);
            var portal365Users = await _portal365Service.GetAllUsersAsync(portal365Config);
            foreach (var user in portal365Users)
            {
                string email = user.Mail;
                string displayName = user.DisplayName;
                string phoneNumber = user.PhoneNumber;
                var exists = await _userManager.Users.Where(u =>  u.Email == email).FirstOrDefaultAsync();
                if (exists != null)
                {
                    continue;
                }
                var newUser = new User
                {
                    UserName = email.Split("@")[0],
                    Email = email,
                    NormalizedEmail=email,
                    NormalizedUserName = email.Split("@")[0],
                    FullName = displayName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumber= phoneNumber
                };
               var result = await _userManager.CreateAsync(newUser, _passwordGenerator.Generate(8));
                if (result.Succeeded) // kullanıcı oluşturulduysa
                {
                    await _userManager.AddToRoleAsync(newUser,"user");
                }

            }

            return Unit.Value;
        }

        //public class PortalUser
        //{
        //    public string Id { get; set; }
        //    public string DisplayName { get; set; }
        //    public string Mail { get; set; }
        //    public string UserPrincipalName { get; set; }
        //    public string Department { get; set; }
        //    public string CompanyName { get; set; }
        //    public string PhoneNumber { get; set; }
        //    public string OU { get; set; }
        //    public DateTime CreatedDateTime { get; set; }
        //}
    }
}
