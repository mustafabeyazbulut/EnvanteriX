using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Commands.AssetCommands;
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
    public class UpdateAssetCommandHandler : BaseHandler, IRequestHandler<UpdateAssetCommand, Unit>
    {
        private readonly AssetRules _assetRules;
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly IPortal365Service _portal365Service;
        private readonly Portal365Rules _portal365Rules;
        private readonly UserManager<User> _userManager;
        public UpdateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AssetRules assetRules, IEmailTemplateProvider emailTemplateProvider, IPortal365Service portal365Service, UserManager<User> userManager, Portal365Rules portal365Rules)
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _assetRules = assetRules;
            _emailTemplateProvider = emailTemplateProvider;
            _portal365Service = portal365Service;
            _userManager = userManager;
            _portal365Rules = portal365Rules;
        }

        public async Task<Unit> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await _unitOfWork.GetReadRepository<Asset>()
                                          .GetAsync(
                                                     predicate: x => x.Id == request.Id
                                                     );
            await _assetRules.AssetShouldExist(asset);

            var previousUserId = asset.AssignedUserId;

            // AssetTag veya SerialNumber değişiklik kontrolü
            if (!string.Equals(request.AssetTag, asset.AssetTag, StringComparison.OrdinalIgnoreCase)
                || !string.Equals(request.SerialNumber, asset.SerialNumber, StringComparison.OrdinalIgnoreCase))
            {
                bool assetExists = await _unitOfWork.GetReadRepository<Asset>()
                    .AnyAsync(x => x.Id != asset.Id &&
                                   (x.AssetTag.ToUpper() == request.AssetTag.ToUpper() ||
                                    x.SerialNumber.ToUpper() == request.SerialNumber.ToUpper()));

                await _assetRules.AssetAlreadyExists(assetExists,
                    $"Varlık Etiketi: {request.AssetTag}, SerialNumber: {request.SerialNumber}");
            }

            // Asset alanlarını güncelle
            asset.AssetTag = request.AssetTag;
            asset.SerialNumber = request.SerialNumber;
            asset.AssetTypeId = request.AssetTypeId;
            asset.ModelId = request.ModelId;
            asset.VendorId = request.VendorId;
            asset.RentalStartDate = request.RentalStartDate;
            asset.IsRented = request.IsRented;
            asset.LocationId = request.LocationId;
            asset.AssignedUserId = request.AssignedUserId;
            asset.AssignedDepartmentId = request.AssignedDepartmentId;
            asset.Description = request.Description;
            asset.Status = request.Status;
            asset.LastModifiedByEmail = _userEmail;

            await _unitOfWork.GetWriteRepository<Asset>().UpdateAsync(asset);
            await _unitOfWork.SaveAsync();

            asset = await _unitOfWork.GetReadRepository<Asset>()
                                          .GetAsync(
                                                     predicate: x => x.Id == request.Id,
                                                     include: x => x.Include(c => c.AssetType)
                                                    .Include(b => b.Model).ThenInclude(b => b.Brand)
                                                    .Include(b => b.Vendor)
                                                    .Include(b => b.Location)
                                                    .Include(c => c.AssignedDepartment));

            // Kullanıcı değişmişse mail gönder
            if (previousUserId != asset.AssignedUserId)
            {
                var portal365Config = await _unitOfWork.GetReadRepository<Portal365>()
                    .GetAsync(predicate: x => !x.IsDeleted,
                              orderBy: q => q.OrderByDescending(x => x.CreatedDate));

                await _portal365Rules.Portal365ShouldExist(portal365Config);

                // Önceki kullanıcıya mail (kullanıcı kaldırıldıysa)
                if (previousUserId.HasValue)
                {
                    var previousUser = await _userManager.Users
                        .Where(u => !u.IsDeleted && u.Id == previousUserId.Value)
                        .FirstOrDefaultAsync();

                    if (previousUser != null)
                    {
                        var emailTemplate = await _emailTemplateProvider
                            .GetTemplateAsync("AssetAssignmentNotification");

                        if (!string.IsNullOrEmpty(emailTemplate))
                        {
                            // Placeholder’ları doldur
                            emailTemplate = emailTemplate.Replace("{{UserName}}", previousUser.FullName)
                                                         .Replace("{{AssetTag}}", asset.AssetTag)
                                                         .Replace("{{SerialNumber}}", asset.SerialNumber)
                                                         .Replace("{{BrandName}}", asset.Model?.Brand?.BrandName ?? "")
                                                         .Replace("{{ModelName}}", asset.Model?.ModelName ?? "")
                                                         .Replace("{{VendorName}}", asset.Vendor?.VendorName ?? "")
                                                         .Replace("{{LocationName}}", asset.Location?.Building ?? "")
                                                         .Replace("{{Url}}", "https://envanter.aundeteknik.com")
                                                         .Replace("{{MessageBlock}}", "Size atanmış olan varlık kaldırıldı");

                            await _portal365Service.SendEmailAsync(portal365Config, previousUser.Email,
                                "Varlık Ataması Kaldırıldı", emailTemplate);
                        }
                    }
                }

                // Yeni kullanıcıya mail (yeni atandıysa)
                if (asset.AssignedUserId.HasValue)
                {
                    var newUser = await _userManager.Users
                        .Where(u => !u.IsDeleted && u.Id == asset.AssignedUserId.Value)
                        .FirstOrDefaultAsync();

                    if (newUser != null)
                    {
                        var emailTemplate = await _emailTemplateProvider
                            .GetTemplateAsync("AssetAssignmentNotification");

                        if (!string.IsNullOrEmpty(emailTemplate))
                        {
                            // Placeholder’ları doldur
                            emailTemplate = emailTemplate.Replace("{{UserName}}", newUser.FullName)
                                                         .Replace("{{AssetTag}}", asset.AssetTag)
                                                         .Replace("{{SerialNumber}}", asset.SerialNumber)
                                                         .Replace("{{BrandName}}", asset.Model?.Brand?.BrandName ?? "")
                                                         .Replace("{{ModelName}}", asset.Model?.ModelName ?? "")
                                                         .Replace("{{VendorName}}", asset.Vendor?.VendorName ?? "")
                                                         .Replace("{{LocationName}}", asset.Location?.Building ?? "")
                                                         .Replace("{{Url}}", "https://envanter.aundeteknik.com")
                                                         .Replace("{{MessageBlock}}", "Size yeni bir varlık atandı");

                            await _portal365Service.SendEmailAsync(portal365Config, newUser.Email,
                                "Yeni Varlık Ataması", emailTemplate);
                        }
                    }
                }
            }

            return Unit.Value;
        }

    }
}
