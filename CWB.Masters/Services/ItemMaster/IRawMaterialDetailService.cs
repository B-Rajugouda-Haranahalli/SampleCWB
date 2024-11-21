using CWB.Masters.ViewModels.ItemMaster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Services.ItemMaster
{
    public interface IRawMaterialDetailService
    {
        Task<RawMaterialDetailVM> RawMaterialDetail(RawMaterialDetailVM rawMaterialDetailVM);
        IEnumerable<RawMaterialDetailVM> GetRawMaterialDetailsByTenant(long tenantID);
        Task<IEnumerable<RawMaterialDetailVM>> GetOwnRMS(long tenantId);
        Task<IEnumerable<RawMaterialDetailVM>> GetSupplierRMS(long supplierId);

        IEnumerable<PartPurchaseDetailsVM> GetParchasesByMasterPartNo(string partNo);
        IEnumerable<PartPurchaseDetailsVM> GetPartPurchases(long tenantId);
        IEnumerable<PartPurchaseDetailsVM> GetPartPurchasesForPartNo(int partId, long tenantId);
        Task<PartPurchaseDetailsVM> PartPurchaseDetail(PartPurchaseDetailsVM partPurchaseDetailVM);
        Task<PartPurchaseDetailsVM> PreferredSupplier(PartPurchaseDetailsVM partPurchaseDetailVM);
        Task<PartPurchaseDetailsVM> RemPartPurchaseDetail(PartPurchaseDetailsVM partPurchaseDetailVM);

        Task<PartPurchaseDetailsVM> GetPartPurchase(int partPurchaseId,long tenantId);

        IEnumerable<RawMaterialTypeVM> GetRMTypes(long tenantID);
        IEnumerable<RawMaterialSepcVM> GetRMSpecs(long tenantID);
        IEnumerable<RawMaterialStandardVM> GetRMStandards(long tenantID);
        IEnumerable<BaseRawMaterialVM> GetBaseRMs(long tenantID);

        Task<BaseRawMaterialVM> BaseRM(BaseRawMaterialVM baseRMVm);
        Task<bool> CheckBaseRm(string rmName);
        Task<bool> CheckRmType(string rmName);
        Task<bool> CheckRmSpec(string rmName);
        Task<bool> CheckRmStandard(string rmName);
        Task<RawMaterialTypeVM> RMType(RawMaterialTypeVM rMTypeVm);
        Task<RawMaterialSepcVM> RMSpec(RawMaterialSepcVM rMSpecVm);
        Task<RawMaterialStandardVM> RMStandard(RawMaterialStandardVM rMStandardVm);
        bool CheckPartNo(long partId);
        Task<RawMaterialDetailVM> GetRMPart(int partId, long tenantId);


    }
}
