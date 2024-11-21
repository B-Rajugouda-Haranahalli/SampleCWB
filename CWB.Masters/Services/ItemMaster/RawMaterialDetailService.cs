using AutoMapper;
using CWB.Logging;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Company;
using CWB.Masters.Repositories.ItemMaster;
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using System;
using System.Linq;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using CWB.Masters.Domain;

namespace CWB.Masters.Services.ItemMaster
{
    public class RawMaterialDetailService : IRawMaterialDetailService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRawMaterialDetailRepository _rawMaterialDetailRepository;
        private readonly IPartPurchaseDetailRepository _partPurchaseRepository;
        private readonly IRawMaterialTypeRepository _rawMaterialTypeRepository;
        private readonly IRawMaterialSpecRepository _rawMaterialSpecRepository;
        private readonly IRawMaterialStandardRepository _rawMaterialStandardRepository;
        private readonly IBaseRawMaterialRepository _baseRawMaterialRepository;
        private readonly IMasterPartRepository  _masterPartRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPartStatusChangeLogRepository _partStatusChangeLogRepository;
       



        public RawMaterialDetailService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork,
            IRawMaterialDetailRepository rawMaterialDetailRepository,
            IRawMaterialTypeRepository rawMaterialTypeRepository,
            IRawMaterialStandardRepository rawMaterialStandardRepository, 
            IRawMaterialSpecRepository rawMaterialSpecRepository, 
            IBaseRawMaterialRepository baseRawMaterialRepository,
            IPartPurchaseDetailRepository partPurchaseDetailRepository,
            IMasterPartRepository masterPartRepository,
            ICompanyRepository companyRepository,IPartStatusChangeLogRepository partStatusChangeLogRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _rawMaterialDetailRepository = rawMaterialDetailRepository;
            _rawMaterialTypeRepository = rawMaterialTypeRepository;
            _rawMaterialSpecRepository = rawMaterialSpecRepository;
            _rawMaterialStandardRepository = rawMaterialStandardRepository;
            _baseRawMaterialRepository = baseRawMaterialRepository;
            _partPurchaseRepository = partPurchaseDetailRepository;
            _masterPartRepository = masterPartRepository;
            _companyRepository = companyRepository;
            _partStatusChangeLogRepository = partStatusChangeLogRepository;
        }

        public IEnumerable<RawMaterialDetailVM> GetRawMaterialDetailsByTenant(long tenantID)
        {
            var rawmaterialdetails = _rawMaterialDetailRepository.GetRangeAsync(m => m.TenantId == tenantID);
            return _mapper.Map<IEnumerable<RawMaterialDetailVM>>(rawmaterialdetails);
        }

        

        public IEnumerable<RawMaterialTypeVM> GetRMTypes(long tenantID)
        {
            var rawmaterialdetails = _rawMaterialTypeRepository.GetRangeAsync(m => m.TenantId > -1);
            return _mapper.Map<IEnumerable<RawMaterialTypeVM>>(rawmaterialdetails);
        }

        public IEnumerable<RawMaterialSepcVM> GetRMSpecs(long tenantID)
        {
            var rawmaterialdetails = _rawMaterialSpecRepository.GetRangeAsync(m => m.TenantId > -1);
            return _mapper.Map<IEnumerable<RawMaterialSepcVM>>(rawmaterialdetails);
        }

        public IEnumerable<RawMaterialStandardVM> GetRMStandards(long tenantID)
        {
            var rawmaterialdetails = _rawMaterialStandardRepository.GetRangeAsync(m => m.TenantId > -1);
            return _mapper.Map<IEnumerable<RawMaterialStandardVM>>(rawmaterialdetails);
        }

        public IEnumerable<BaseRawMaterialVM> GetBaseRMs(long tenantID)
        {
            var rawmaterialdetails = _baseRawMaterialRepository.GetRangeAsync(m => m.TenantId > -1);
            return _mapper.Map<IEnumerable<BaseRawMaterialVM>>(rawmaterialdetails);
        }

        private int GetPartId(string partNo)
        {
            var rms = _masterPartRepository.GetRangeAsync(c => c.PartNo.Equals(partNo));
            if (!rms.Any())
            {
                return 0;
            }
            MasterPart part = _mapper.Map<Domain.ItemMaster.MasterPart>(rms.First());
            return (int)part.Id;
        }


        public async Task<RawMaterialDetailVM> RawMaterialDetail(RawMaterialDetailVM rawMaterialDetailVM)
        {
            try {
                var rawmaterialdetail = _mapper.Map<RawMaterialDetail>(rawMaterialDetailVM);
                if(rawmaterialdetail.RawMaterialMadeType == 1)
                {
                    rawmaterialdetail.SupplierId = 1;//self
                }
                var masterPart = _mapper.Map<Domain.ItemMaster.MasterPart>(rawMaterialDetailVM);
                int id = GetPartId(masterPart.PartNo);
                if (id == 0)
                {
                    masterPart.Id = 0;
                    await _masterPartRepository.AddAsync(masterPart);
                    await _unitOfWork.CommitAsync();
                    rawMaterialDetailVM.PartId = (int)masterPart.Id;
                    PartStatusChangeLog partStatus = new PartStatusChangeLog()
                    {
                        MasterPartId = masterPart.Id,
                        Status = masterPart.Status,
                        ChangeReason = masterPart.StatusChangeReason,
                        TenantId = masterPart.TenantId
                    };
                    await _partStatusChangeLogRepository.AddAsync(partStatus);
                }
                else
                {
                    masterPart.Id = id;
                    rawmaterialdetail.PartId = masterPart.Id;
                    if (id == rawmaterialdetail.PartId)
                    {
                        PartStatusChangeLog partStatus = new PartStatusChangeLog()
                        {
                            MasterPartId = masterPart.Id,
                            Status = masterPart.Status,
                            ChangeReason = masterPart.StatusChangeReason,
                            TenantId = masterPart.TenantId
                        };
                        await _partStatusChangeLogRepository.AddAsync(partStatus);
                        masterPart = await _masterPartRepository.UpdateAsync(masterPart.Id, masterPart);
                        await _unitOfWork.CommitAsync();
                        rawMaterialDetailVM.PartId = (int)masterPart.Id;
                    }
                }
                rawmaterialdetail.PartId = rawMaterialDetailVM.PartId;
                if(masterPart.Id>0)
                {
                    rawmaterialdetail.PartId = masterPart.Id;
                }
                if (rawmaterialdetail.Id == 0)
                {
                    try
                    {
                        _rawMaterialDetailRepository.AddRawMaterial(rawmaterialdetail);
                    } catch (Exception ex)
                    {
                        string str = ex.InnerException.Message;
                        string str1 = ex.StackTrace;
                    }
                }
                else
                {
                    rawmaterialdetail = await _rawMaterialDetailRepository.UpdateAsync(rawmaterialdetail.Id, rawmaterialdetail);
                }
                await _unitOfWork.CommitAsync();
                rawMaterialDetailVM.RawMaterialDetailId = rawmaterialdetail.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                throw ex;
            }
            return rawMaterialDetailVM;
        }

        //Add/Edit PartPurchase
        public async Task<PartPurchaseDetailsVM> PartPurchaseDetail(PartPurchaseDetailsVM partPurchaseDetailVM)
        {
           /**
            * var co = await _companyRepository.SingleOrDefaultAsync(m=>m.Id == partPurchaseDetailVM.PSupplierId);
            CompaniesVM companiesVM = null;
            if (co != null)
            {
                partPurchaseDetailVM.PSupplier = co.Name;
            }*/

            var partPurchaseDetail = _mapper.Map<Domain.ItemMaster.PartPurchaseDetails>(partPurchaseDetailVM);
            if (partPurchaseDetail.Id == 0)
            {
                try
                {
                    _partPurchaseRepository.AddPartPurchase(partPurchaseDetail);
                }
                catch (Exception ex)
                {
                    string str = ex.InnerException.Message;
                    string str1 = ex.StackTrace;
                }
            }
            else
            {
                partPurchaseDetail = await _partPurchaseRepository.UpdateAsync(partPurchaseDetail.Id, partPurchaseDetail);
            }
            await _unitOfWork.CommitAsync();
            partPurchaseDetailVM.PartPurchaseId = partPurchaseDetail.Id;
            return partPurchaseDetailVM;
        }

        public async Task<PartPurchaseDetailsVM> PreferredSupplier(PartPurchaseDetailsVM partPurchaseDetailVM)
        {
            var findmk =await _partPurchaseRepository.SingleOrDefaultAsync(f => f.Id == partPurchaseDetailVM.PartPurchaseId && f.TenantId ==partPurchaseDetailVM.TenantId);
            var partPurchase = _mapper.Map<PartPurchaseDetails>(findmk);
            if (partPurchase.Id != 0)
            {
                List<PartPurchaseDetailsVM> lst = GetPartPurchasesForPartNo((int)partPurchase.PartId,(int)partPurchase.TenantId).ToList();
                foreach (PartPurchaseDetailsVM rv in lst)
                {
                    var lRouting = _mapper.Map<PartPurchaseDetails>(rv);
                    lRouting.PreferredSupplier = 0;
                    await _partPurchaseRepository.UpdateAsync(lRouting.Id, lRouting);
                }
                partPurchase.PreferredSupplier = 1;
               partPurchase = await _partPurchaseRepository.UpdateAsync(partPurchase.Id, partPurchase);
            }
            await _unitOfWork.CommitAsync();
            partPurchaseDetailVM.PartPurchaseId = (int)partPurchase.Id;
            partPurchaseDetailVM.PPartId = (int)partPurchase.PartId;
            return partPurchaseDetailVM;
        }

        public IEnumerable<PartPurchaseDetailsVM> GetParchasesByMasterPartNo(string partNo)
        {
            var partPurchaseDetails = _partPurchaseRepository.GetRangeAsync(m => m.PartId.Equals(partNo));
            return _mapper.Map<IEnumerable<PartPurchaseDetailsVM>>(partPurchaseDetails);
        }

        public IEnumerable<PartPurchaseDetailsVM> GetPartPurchases(long tenantId)
        {
            try
            {
                var partPurchaseDetails = _partPurchaseRepository.GetRangeAsync(m => m.TenantId == tenantId);
                return _mapper.Map<IEnumerable<PartPurchaseDetailsVM>>(partPurchaseDetails);
            }catch(Exception ex)
            {
                string mst = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return new List<PartPurchaseDetailsVM>();
        }

        public IEnumerable<PartPurchaseDetailsVM> GetPartPurchasesForPartNo(int partId, long tenantId)
        {
            var partPurchaseDetails = _partPurchaseRepository.GetRangeAsync(m => m.PartId == (partId) && m.TenantId == tenantId);
            return _mapper.Map<IEnumerable<PartPurchaseDetailsVM>>(partPurchaseDetails);
        }

        public async Task<PartPurchaseDetailsVM> GetPartPurchase(int partPurchaseId, long tenantId)
        {
            var partPurchaseDetails = await _partPurchaseRepository.SingleOrDefaultAsync(m => m.Id == partPurchaseId && m.TenantId == tenantId);
            return _mapper.Map<PartPurchaseDetailsVM>(partPurchaseDetails);
        }

        public async Task<PartPurchaseDetailsVM> RemPartPurchaseDetail(PartPurchaseDetailsVM partPurchaseDetailVM)
        {
            var partPurchaseDetail = _mapper.Map<Domain.ItemMaster.PartPurchaseDetails>(partPurchaseDetailVM);
            if (partPurchaseDetail.Id > 0)
            {
                var dbc_partPurchaseDetail = await _partPurchaseRepository.SingleOrDefaultAsync(m => m.Id == partPurchaseDetail.Id);
                if (dbc_partPurchaseDetail != null)
                {
                    if (dbc_partPurchaseDetail.Id > 0)
                    {
                        _partPurchaseRepository.Remove(dbc_partPurchaseDetail);
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
            return partPurchaseDetailVM;
        }


        /*public bool CheckIfDivisionExisit(CheckDivisionVM checkDivisionVM)
        {
            var department = _divisionRepository.GetRangeAsync(d => d.Name == checkDivisionVM.DivisionName &&
            d.TenantId == checkDivisionVM.TenantId && d.CompanyId == checkDivisionVM.CompanyId);
            if (!department.Any())
            {
                return false;
            }
            return (department.First().Id != checkDivisionVM.DivisionId);
        }*/
        public async Task<IEnumerable<RawMaterialDetailVM>> GetOwnRMS(long tenantId)
        {
            var co = await _companyRepository.SingleOrDefaultAsync(m => m.Name.ToLower().Equals("self"));

            //if(co!=null)
            //{
                var rawmaterialdetails = _rawMaterialDetailRepository.GetRangeAsync(m => m.TenantId == tenantId); //m.SupplierId == (co.Id) &&
                return _mapper.Map<IEnumerable<RawMaterialDetailVM>>(rawmaterialdetails);
            //}
            //return new List<RawMaterialDetailVM>();
        }

        public async Task<IEnumerable<RawMaterialDetailVM>> GetSupplierRMS(long supplierId)
        {
            var co = await _companyRepository.SingleOrDefaultAsync(m => m.Name.ToLower().Equals("self"));

            try
            {
                //if (co != null)
                //{
                    var rawmaterialdetails = _rawMaterialDetailRepository.GetRangeAsync(m => m.TenantId > -1 );  //&& (m.SupplierId != co.Id)
                return _mapper.Map<IEnumerable<RawMaterialDetailVM>>(rawmaterialdetails);
                //}

            } catch(Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.Source;
            }

            return new List<RawMaterialDetailVM>();
            
        }

      

        public async Task<BaseRawMaterialVM> BaseRM(BaseRawMaterialVM baseRMVm)
        {
            var baseRM = _mapper.Map<Domain.ItemMaster.BaseRawMaterial>(baseRMVm);
            if (baseRM.Id == 0)
            {
                try { 
                   _baseRawMaterialRepository.AddBaseRM(baseRM);
                }
                    catch (Exception ex)
                    {
                    string str = ex.InnerException.Message;
                    string str1 = ex.StackTrace;
                }
            }
            else
            {
                baseRM = await _baseRawMaterialRepository.UpdateAsync(baseRM.Id, baseRM);
                try
                {
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            baseRMVm.BaseRawMaterialId = baseRM.Id;
            return baseRMVm;
        }
        public async Task<bool> CheckBaseRm(string rmName)
        {
            try
            {
                var documentTypes = await _baseRawMaterialRepository.SingleOrDefaultAsync(c => c.Name == (rmName));
                if (documentTypes != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }
        public async Task<RawMaterialTypeVM> RMType(RawMaterialTypeVM rMTypeVm)
        {
            var rmType = _mapper.Map<Domain.ItemMaster.RawMaterialType>(rMTypeVm);
            if (rmType.Id == 0)
            {
                try { 
                    _rawMaterialTypeRepository.AddRmType(rmType);
                }
                catch (Exception ex)
                {
                    string str = ex.InnerException.Message;
                    string str1 = ex.StackTrace;
                }
            }
            else
            {
                rmType = await _rawMaterialTypeRepository.UpdateAsync(rmType.Id, rmType);
            }
            rMTypeVm.RawMaterialTypeId = rmType.Id;
            return rMTypeVm;
        }
        public async Task<bool> CheckRmType(string rmName)
        {
            try
            {
                var documentTypes = await _rawMaterialTypeRepository.SingleOrDefaultAsync(c => c.Name == (rmName));
                if (documentTypes != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        public async Task<RawMaterialSepcVM> RMSpec(RawMaterialSepcVM rMSpecVm)
        {

            var rmSpec = _mapper.Map<Domain.ItemMaster.RawMaterialSpec>(rMSpecVm);
            if (rmSpec.Id == 0)
            {
                try
                {
                    _rawMaterialSpecRepository.AddRMSpec(rmSpec);
                }
                catch (Exception ex)
                {
                    string str = ex.InnerException.Message;
                    string str1 = ex.StackTrace;
                }
            }
            else
            {
                rmSpec = await _rawMaterialSpecRepository.UpdateAsync(rmSpec.Id, rmSpec);
                try
                {
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            rMSpecVm.MaterialSpecId = rmSpec.Id;
            return rMSpecVm;
        }
        public async Task<bool> CheckRmSpec(string rmName)
        {
            try
            {
                var documentTypes = await _rawMaterialSpecRepository.SingleOrDefaultAsync(c => c.Name == (rmName));
                if (documentTypes != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        public async Task<RawMaterialStandardVM> RMStandard(RawMaterialStandardVM rMStandardVm)
        {
            var standard = _mapper.Map<Domain.ItemMaster.RawMaterialStandard>(rMStandardVm);
            if (standard.Id == 0)
            {
                try { 
                    _rawMaterialStandardRepository.AddRMStandard(standard);
                }
                catch (Exception ex)
                {
                    string str = ex.InnerException.Message;
                    string str1 = ex.StackTrace;
                }
            }
            else
            {
                standard = await _rawMaterialStandardRepository.UpdateAsync(standard.Id, standard);
                try
                {
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            rMStandardVm.Standard = standard.Id;
            return rMStandardVm;
        }
        public async Task<bool> CheckRmStandard(string rmName)
        {
            try
            {
                var documentTypes = await _rawMaterialStandardRepository.SingleOrDefaultAsync(c => c.Name == (rmName));
                if (documentTypes != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        public bool CheckPartNo(long partId)
        {
            var rms = _rawMaterialDetailRepository.GetRangeAsync(c => c.PartId == (partId));
            if (!rms.Any())
            {
                return false;
            }
            return true;
        }


        public async Task<RawMaterialDetailVM> GetRMPart(int partId, long tenantId)
        {
            var part = await _rawMaterialDetailRepository.SingleOrDefaultAsync(m => m.PartId == partId && m.TenantId == tenantId);
            if (part != null)
            {
                return _mapper.Map<RawMaterialDetailVM>(part);
            }
            return new RawMaterialDetailVM { RawMaterialDetailId = -1 };
        }


    }
}
