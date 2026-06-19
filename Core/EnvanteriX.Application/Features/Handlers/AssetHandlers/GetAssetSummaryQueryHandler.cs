using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Queries.AssetQueries;
using EnvanteriX.Application.Features.Results.AssetResults;
using EnvanteriX.Application.Interfaces.AutoMapper;
using EnvanteriX.Application.Interfaces.UnitOfWorks;
using EnvanteriX.Domain.Entities;
using EnvanteriX.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnvanteriX.Application.Features.Handlers.AssetHandlers
{
    public class GetAssetSummaryQueryHandler
        : BaseHandler, IRequestHandler<GetAssetSummaryQuery, GetAssetSummaryQueryResult>
    {
        public GetAssetSummaryQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapper, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<GetAssetSummaryQueryResult> Handle(GetAssetSummaryQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadRepository<Asset>();

            var assets = await repo.GetAllAsync(
                include: q => q
                    .Include(a => a.AssetType)
                    .Include(a => a.Location)
                    .Include(a => a.AssignedUser)
                    .Include(a => a.AssetMovements)
                    .Include(a => a.MaintenanceRecords)
            );

            // 🧮 Durum bazlı sayılar
            var total = assets.Count;
            var inUse = assets.Count(a => a.Status == StatusEnum.Kullanimda);
            var inStock = assets.Count(a => a.Status == StatusEnum.Stokta);
            var inService = assets.Count(a => a.Status == StatusEnum.Tamirde);
            var retired = assets.Count(a => a.Status == StatusEnum.KullanimDisi);
            var passive = assets.Count(a => a.Status == StatusEnum.Pasif);

            var now = DateTime.UtcNow;
            var last30 = now.AddDays(-30);

            var addedLast30Days = assets.Count(a => a.CreatedDate >= last30);
            var serviceLast30Days = assets.Count(a =>
                a.MaintenanceRecords.Any(m => m.StartDate >= last30)
            );

            // 🔹 Kategoriye göre dağılım
            var byCategory = assets
                .GroupBy(a => a.AssetType.TypeName)
                .ToDictionary(g => g.Key, g => g.Count());

            // 🔹 Lokasyona göre dağılım
            var byLocation = assets
                .GroupBy(a => a.Location.Building)
                .ToDictionary(g => g.Key, g => g.Count());

            // 🔹 Kullanıcıya göre dağılım (zimmetli makineler)
            var byUser = assets
                .Where(a => a.AssignedUser != null)
                .GroupBy(a => a.AssignedUser.FullName)
                .ToDictionary(g => g.Key, g => g.Count());

            // 🔹 Son 30 günde zimmet hareketi olan makineler
            var movementCountLast30Days = assets .Count(a => a.AssetMovements.Any(m => m.CreatedDate >= last30));

            // 🔹 Ortalama servis süresi
            var allServiceDurations = assets
                .SelectMany(a => a.MaintenanceRecords)
                .Where(m => m.EndDate != null)
                .Select(m => (m.EndDate!.Value - m.StartDate).TotalDays)
                .ToList();

            double avgServiceDays = allServiceDurations.Any()
                ? allServiceDurations.Average()
                : 0;

            // 🔹 En çok servise giden ilk 3 kategori
            var topServiceCategories = assets
                .Where(a => a.MaintenanceRecords.Any())
                .GroupBy(a => a.AssetType.TypeName)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            // 🔸 Sonuç
            var result = new GetAssetSummaryQueryResult
            {
                TotalAssets = total,
                InUseCount = inUse,
                InStockCount = inStock,
                InServiceCount = inService,
                RetiredCount = retired,
                PassiveCount = passive,
                AddedLast30Days = addedLast30Days,
                ServiceLast30Days = serviceLast30Days,
                AssetsByCategory = byCategory,
                AssetsByLocation = byLocation,
                AssetsByUser = byUser,
                MovementCountLast30Days = movementCountLast30Days,
                AverageServiceDurationDays = Math.Round(avgServiceDays, 1),
                TopServiceCategories = topServiceCategories
            };

            return result;
        }
    }
}
