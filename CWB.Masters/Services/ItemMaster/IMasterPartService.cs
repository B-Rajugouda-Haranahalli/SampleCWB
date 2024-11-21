using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.MastersUtils.ItemMaster;
using CWB.Masters.ViewModels.ItemMaster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Services.ItemMaster
{
    public interface IMasterPartService
    {
        IEnumerable<ItemMasterPartVM> GetMasterPartView();
        int CheckPartNo(string partNo);
        IEnumerable<PartStatusVM> GetStatuses();
        
        IEnumerable<MasterPartVM> GetAllMasterParts();
        IEnumerable<MasterPartVM> GetAllMasterPartsWithIds(List<int> ids);

        Task<MasterPartVM> GetMasterPart(int partId);
        Task<ManufacturedPartNoDetailVM> GetManufPart(int partId, long tenantId);
        Task<RawMaterialDetailVM> GetRMPart(int partId, long tenantId);
        Task<BoughtOutFinishDetailVM> GetBOFPart(int partId, long tenantId);

        Task<ManufacturedPartNoDetailVM> MasterPart(ManufacturedPartNoDetailVM manufacturedPartNoDetailVM);

        Task<IEnumerable<ItemMasterDocListVM>> GetAllItemMasterDocList(long tenantId);
        Task<ItemMasterDocListVM> PostItemMasterDocList(ItemMasterDocListVM itemMasterDocList);


        Task<IEnumerable<ItemMasterContentVM>> GetAllItemMasterContent();
        Task<bool> DeleteItemMasterDoc(long itemMasterDocListId, long tenantId);
        Task<bool> CheckDocumentTypeInItemMaster(long documentTypeId,long contentId, long tenantId);

    }
}
