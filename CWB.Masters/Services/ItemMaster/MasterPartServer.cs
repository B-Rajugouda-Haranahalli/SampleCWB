using AutoMapper;
using CWB.Logging;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Company;
using CWB.Masters.Repositories.ItemMaster;
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.MastersUtils.ItemMaster;
using CWB.CommonUtils.Common;

namespace CWB.Masters.Services.ItemMaster
{
    public class MasterPartService : IMasterPartService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRawMaterialDetailService _rawMaterialDetailService;
        private readonly IBoughtOutFinishDetailService _boughtOutFinishDetailService;
        private readonly IManufacturedPartNoDetailService _manufacturedPartNoDetailService;
        private readonly IMasterPartRepository _masterPartRepository;
        private readonly IItemMasterDocListRepository _itemMasterDocListRepository;
        private readonly IItemMasterContentRepository _itemMasterContentRepository;

        public MasterPartService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork
            , IRawMaterialDetailService rawMaterialDetailService
            , IManufacturedPartNoDetailService manufacturedPartNoDetailService
            , IBoughtOutFinishDetailService boughtOutFinishDetailService,IMasterPartRepository masterPartRepository
            , IItemMasterDocListRepository itemMasterDocListRepository, IItemMasterContentRepository itemMasterContentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _rawMaterialDetailService = rawMaterialDetailService;
            _manufacturedPartNoDetailService = manufacturedPartNoDetailService;
            _boughtOutFinishDetailService = boughtOutFinishDetailService;
            _masterPartRepository = masterPartRepository;
            _itemMasterDocListRepository = itemMasterDocListRepository;
            _itemMasterContentRepository = itemMasterContentRepository;
        }

        public IEnumerable<ItemMasterPartVM> GetMasterPartView()
        {
            
            var partPurchases = _rawMaterialDetailService.GetPartPurchases(1);
            var rawMaterialDetails = _rawMaterialDetailService.GetRawMaterialDetailsByTenant(1);
            var bofs = _boughtOutFinishDetailService.GetBoughtOutFinishDetailsByTenant(1);
            var manufPdetails = _manufacturedPartNoDetailService.GetAllManufacturedPartNoDetailsByTypeTenant(1);
            return new List<ItemMasterPartVM>();
        }


        public IEnumerable<MasterPartVM> GetAllMasterParts()
        {
            var marsterParts = _masterPartRepository.GetRangeAsync(m => m.TenantId > -1);
            return _mapper.Map<IEnumerable<MasterPartVM>>(marsterParts);
        }

        //public MasterPartVM GetMasterPartByID(int partid)
        //{
        //    var masterpart = _masterPartRepository.GetRangeAsync(m => m.Id == partid);
        //    return _mapper.Map<MasterPartVM>(masterpart)
        //}

        public IEnumerable<MasterPartVM> GetAllMasterPartsWithIds(List<int> ids)
        {
            var marsterParts = _masterPartRepository.GetRangeAsync(m => ids.Contains((int)m.Id)).ToList();
            return _mapper.Map<IEnumerable<MasterPartVM>>(marsterParts);
        }
        


        public async Task<ManufacturedPartNoDetailVM> MasterPart(ManufacturedPartNoDetailVM manufacturedPartNoDetailVM)
        {
            var masterPart = _mapper.Map<Domain.ItemMaster.MasterPart>(manufacturedPartNoDetailVM);
            if (masterPart.Id == 0)
            {
                await _masterPartRepository.AddAsync(masterPart);
            }
            else
            {
                masterPart = await _masterPartRepository.UpdateAsync(masterPart.Id, masterPart);
            }
            await _unitOfWork.CommitAsync();
            manufacturedPartNoDetailVM.PartId = (int)masterPart.Id;
            return manufacturedPartNoDetailVM;
        }

        public int CheckPartNo(string partNo)
        {
            var rms =  _masterPartRepository.GetRangeAsync(c => c.PartNo.Equals(partNo));
            if (!rms.Any())
            {
                return 0;
            }
            MasterPart part = _mapper.Map<Domain.ItemMaster.MasterPart>(rms.First());
            return (int)part.Id;
        }

        public IEnumerable<PartStatusVM> GetStatuses()
        {
            var partStatuses = Enum.GetValues(typeof(PartStatus))
                         .Cast<PartStatus>()
                         .Select(t => new PartStatusVM { Status = t.GetEnumDescription(), StatusValue = t.ToString() });
            return partStatuses;
        }

        public async Task<MasterPartVM> GetMasterPart(int partId)
        {
            var rms = await _masterPartRepository.SingleOrDefaultAsync(m=>m.Id == partId);
            if (rms == null)
            {
                return new MasterPartVM { MasterPartId = -1 };
            }
            return _mapper.Map<MasterPartVM>(rms);
        }

        public async Task<ManufacturedPartNoDetailVM> GetManufPart(int partId,long tenantId)
        {
            
            ManufacturedPartNoDetailVM manufObj =  await _manufacturedPartNoDetailService.GetManuPart(partId, tenantId);
            if(manufObj.ManufacturedPartNoDetailId != -1)
            {
                MasterPartVM mpVm =  await GetMasterPart(partId);
                if(mpVm.MasterPartId != -1)
                {
                    manufObj.PartNo = mpVm.PartNo;
                    manufObj.PartDescription = mpVm.PartDescription;
                    manufObj.Status = mpVm.Status.ToString();
                    manufObj.StatusChangeReason = mpVm.StatusChangeReason;
                    manufObj.RevDate = mpVm.RevDate;
                    manufObj.RevNo = mpVm.RevNo;
                    manufObj.MasterPartType = mpVm.MasterPartType.ToString();

                }
            }
            return manufObj;
        }
        public async Task<RawMaterialDetailVM> GetRMPart(int partId, long tenantId)
        {

            RawMaterialDetailVM rmObj = await _rawMaterialDetailService.GetRMPart(partId, tenantId);
            if (rmObj.RawMaterialDetailId != -1)
            {
                MasterPartVM mpVm = await GetMasterPart(partId);
                if (mpVm.MasterPartId != -1)
                {
                    rmObj.PartNo = mpVm.PartNo;
                    rmObj.PartDescription = mpVm.PartDescription;
                    rmObj.Status = mpVm.Status.ToString();
                    rmObj.StatusChangeReason = mpVm.StatusChangeReason;
                    rmObj.RevDate = mpVm.RevDate;
                    rmObj.RevNo = mpVm.RevNo;
                    rmObj.MasterPartType = mpVm.MasterPartType.ToString();

                }
            }
            return rmObj;
        }

        public async Task<BoughtOutFinishDetailVM> GetBOFPart(int partId, long tenantId)
        {
            BoughtOutFinishDetailVM rmObj = await _boughtOutFinishDetailService.GetPart(partId,tenantId);
            if (rmObj.BoughtOutFinishDetailId != -1)
            {
                MasterPartVM mpVm = await GetMasterPart(partId);
                if (mpVm.MasterPartId != -1)
                {
                    rmObj.PartNo = mpVm.PartNo;
                    rmObj.PartDescription = mpVm.PartDescription;
                    rmObj.Status = mpVm.Status.ToString();
                    rmObj.StatusChangeReason = mpVm.StatusChangeReason;
                    rmObj.RevDate = mpVm.RevDate;
                    rmObj.RevNo = mpVm.RevNo;
                    rmObj.MasterPartType = mpVm.MasterPartType.ToString();

                }
            }
            return rmObj;

        }

        public async Task<IEnumerable<ItemMasterDocListVM>> GetAllItemMasterDocList(long tenantId)
        {
            var allDocuType = _itemMasterDocListRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<ItemMasterDocListVM>>(allDocuType);
        }

        public async Task<ItemMasterDocListVM> PostItemMasterDocList(ItemMasterDocListVM itemMasterDocList)
        {
            var itemMaster = _mapper.Map<ItemMasterDocList>(itemMasterDocList);
            if (itemMaster.Id == 0)
            {
                try
                {
                    await _itemMasterDocListRepository.AddAsync(itemMaster);
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                var itemMasterDoc = await _itemMasterDocListRepository.SingleOrDefaultAsync(x => x.Id == itemMaster.Id);
                if (itemMasterDoc == null)
                {
                    return itemMasterDocList;
                }
                itemMaster = await _itemMasterDocListRepository.UpdateAsync(itemMasterDoc.Id, itemMaster);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            itemMasterDocList.DocumentTypeId = itemMaster.Id;
            return itemMasterDocList;
        }


        public async Task<IEnumerable<ItemMasterContentVM>> GetAllItemMasterContent()
        {
            var itemMasterContents =await _itemMasterContentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemMasterContentVM>>(itemMasterContents);
        }
        public async Task<bool> DeleteItemMasterDoc(long itemMasterDocListId, long tenantId)
        {
            var co = await _itemMasterDocListRepository.SingleOrDefaultAsync(m => m.Id == itemMasterDocListId && m.TenantId == tenantId);
            if (co != null)
            {
                try
                {
                    _itemMasterDocListRepository.Remove(co);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                catch (Exception ex) { }
            }
            return false;
        }
        public async Task<bool> CheckDocumentTypeInItemMaster(long documentTypeId, long contentId, long tenantId)
        {
            try
            {
                var docLists = await _itemMasterDocListRepository.SingleOrDefaultAsync(c => c.ContentId == contentId && c.DocumentTypeId == documentTypeId && c.TenantId == tenantId);
                if (docLists == null)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

            }
            return true;
        }
    }
}