using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Features.Rules.AssetRules;
using EnvanteriX.Application.Features.Rules.Portal365Rules;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.Email;
using EnvanteriX.Application.Interfaces.Portal365Interfaces;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class CreateAssetCommandHandler : BaseHandler, IRequestHandler<CreateAssetCommand, CreateAssetCommandResult>
    {
        private readonly AssetRules _assetRules;
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly IPortal365Service _portal365Service;
        private readonly Portal365Rules _portal365Rules;
        private readonly UserManager<User> _userManager;

        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetRules assetRules, IEmailTemplateProvider emailTemplateProvider, IPortal365Service portal365Service, Portal365Rules portal365Rules, UserManager<User> userManager)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetRules = assetRules;
            _emailTemplateProvider = emailTemplateProvider;
            _portal365Service = portal365Service;
            _portal365Rules = portal365Rules;
            _userManager = userManager;
        }
        public async Task<CreateAssetCommandResult> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var assetTag = request.AssetTag?.Trim();
            var serialNumber = request.SerialNumber?.Trim();

            bool assetExists = await _unitOfWork.GetReadRepository<Asset>()
                .AnyAsync(a => a.SerialNumber == serialNumber);
            await _assetRules.AssetAlreadyExists(assetExists, $"SerialNumber: {serialNumber}");

            var asset = _mapper.Map<Asset, CreateAssetCommand>(request);
            if (asset.AssignedUserId!=null && asset.AssignedUserId>0)
            {
                asset.Status = Domain.Enums.StatusEnum.Kullanimda;

            }
            if (asset.AssignedDepartmentId != null && asset.AssignedDepartmentId > 0)
            {
                asset.Status = Domain.Enums.StatusEnum.Kullanimda;
            }
            asset.LastModifiedByEmail = _userEmail;
            await _unitOfWork.GetWriteRepository<Asset>().AddAsync(asset);
            await _unitOfWork.SaveAsync();
            if (asset.AssignedUserId != null && asset.AssignedUserId > 0)
            {
                asset.Status = Domain.Enums.StatusEnum.Kullanimda;
                var portal365Config = await _unitOfWork.GetReadRepository<Portal365>()
                   .GetAsync(predicate: x => !x.IsDeleted,
                             orderBy: q => q.OrderByDescending(x => x.CreatedDate));

                await _portal365Rules.Portal365ShouldExist(portal365Config);

                var newUser = await _userManager.Users
                       .Where(u => !u.IsDeleted && u.Id == asset.AssignedUserId.Value).FirstOrDefaultAsync();

                if (newUser != null)
                {
                    var asset2 = await _unitOfWork.GetReadRepository<Asset>()
                                         .GetAsync(
                                                    predicate: x => x.Id == asset.Id,
                                                    include: x => x.Include(c => c.AssetType)
                                                   .Include(b => b.Model).ThenInclude(b => b.Brand)
                                                   .Include(b => b.Vendor)
                                                   .Include(b => b.Location)
                                                   .Include(c => c.AssignedDepartment));
                    if (asset2!=null)
                    {
                        var emailTemplate = await _emailTemplateProvider
                        .GetTemplateAsync("AssetAssignmentNotification");

                        if (!string.IsNullOrEmpty(emailTemplate))
                        {
                            // Placeholder’ları doldur
                            emailTemplate = emailTemplate.Replace("{{UserName}}", newUser.FullName)
                                                         .Replace("{{AssetTag}}", asset2.AssetTag)
                                                         .Replace("{{SerialNumber}}", asset2.SerialNumber)
                                                         .Replace("{{BrandName}}", asset2.Model?.Brand?.BrandName ?? "")
                                                         .Replace("{{ModelName}}", asset2.Model?.ModelName ?? "")
                                                         .Replace("{{VendorName}}", asset2.Vendor?.VendorName ?? "")
                                                         .Replace("{{LocationName}}", asset2.Location?.Building ?? "")
                                                         .Replace("{{Url}}", "https://itenvanter.aundeteknik.com/")
                                                         .Replace("{{MessageBlock}}", "Size yeni bir varlık atandı");

                            await _portal365Service.SendEmailAsync(portal365Config, newUser.Email,
                                "Yeni Varlık Ataması", emailTemplate);
                        }
                    }
                    
                }
            }

            return _mapper.Map<CreateAssetCommandResult, Asset>(asset);
        }
    }
}
