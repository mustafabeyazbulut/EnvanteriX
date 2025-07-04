using EnvanteriX.Application.Bases;
using EnvanteriX.Application.Features.Exceptions.VendorExceptions;
using EnvanteriX.Domain.Entities;

namespace EnvanteriX.Application.Features.Rules.VendorRules
{
    public class VendorRules : BaseRules
    {
        public Task VendorShouldExist(Vendor? model)
        {
            if (model is null) throw new VendorNotFoundException();
            return Task.CompletedTask;
        }
        public Task VendorAlreadyExists(Vendor? Vendor)
        {
            if (Vendor is not null) throw new VendorAlreadyExistsException(Vendor.VendorName);
            return Task.CompletedTask;
        }
        public Task VendorAlreadyExists(bool VendorExists, string name)
        {
            if (VendorExists) throw new VendorAlreadyExistsException(name);
            return Task.CompletedTask;
        }
        public Task VendorShouldNotHaveAnyAsset(bool hasAnyAsset, string name)
        {
            if (hasAnyAsset)
                throw new VendorShouldNotHaveAnyAssetException(name);
            return Task.CompletedTask;
        }
        public Task VendorShouldNotHaveAnySoftwareLicense(bool hasAnyLicance, string name)
        {
            if (hasAnyLicance)
                throw new VendorShouldNotHaveAnySoftwareLicenseException(name);
            return Task.CompletedTask;
        }
        public Task VendorShouldNotHaveAnyMaintenanceRecord(bool hasAnyMaintenanceRecord, string name)
        {
            if (hasAnyMaintenanceRecord)
                throw new VendorShouldNotHaveAnyMaintenanceRecordException(name);
            return Task.CompletedTask;
        }
    }
}
