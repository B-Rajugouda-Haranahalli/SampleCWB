using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.MastersUtils;
using CWB.Masters.MastersUtils.ItemMaster;
using CWB.Masters.Services.Company;
using CWB.Masters.Services.ItemMaster;
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidators.ItemMaster;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static CWB.Masters.MastersUtils.ApiRoutes;

namespace CWB.Masters.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class RawMaterialDetailController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRawMaterialDetailService _rawMaterialDetailService;
        private readonly IBoughtOutFinishDetailService _boughtOutFinishDetailService;
        private readonly IManufacturedPartNoDetailService _manufacturedPartNoDetailService;
        private readonly IMasterPartService _masterPartService;
        private readonly ICompanyService _companyService;


        public RawMaterialDetailController(ILoggerManager logger
            , IRawMaterialDetailService rawMaterialDetailService
            , IManufacturedPartNoDetailService manufacturedPartNoDetailService
            , IBoughtOutFinishDetailService boughtOutFinishDetailService
            , IMasterPartService masterPartService
            , ICompanyService companyService)
        {
            _logger = logger;
            _rawMaterialDetailService = rawMaterialDetailService;
            _manufacturedPartNoDetailService = manufacturedPartNoDetailService;
            _boughtOutFinishDetailService = boughtOutFinishDetailService;
            _masterPartService = masterPartService;
            _companyService = companyService;
        }


        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetRMPart)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RawMaterialDetailVM))]
        public async Task<IActionResult> GetRMPart(int partId, long tenantId)
        {
            RawMaterialDetailVM manufP = await _masterPartService.GetRMPart(partId, tenantId);
            return Ok(manufP);
        }


        /**
         * [HttpGet]
         [Route(ApiRoutes.RawMaterialDetail.CheckPartNo)]
         [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
         public async Task<IActionResult> CheckPartNo(string partNo)
         {
             /*bool exists = _manufacturedPartNoDetailService.CheckPartNo(partNo);
             if(!exists)
             {
                 exists = _rawMaterialDetailService.CheckPartNo(partNo);
                 if (!exists)
                 {
                     exists = _boughtOutFinishDetailService.CheckPartNo(partNo);
                 }
             }
             bool exists = _masterPartService.CheckPartNo(partNo);
             return Ok(exists);
         }*/

        /// <summary>
        /// Get All RawMaterialDetail by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetRawMaterialDetailList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<RawMaterialDetailVM>))]
        public IActionResult GetRawMaterialDetailList(long tenantId)
        {
            var rawmaterialdetails = _rawMaterialDetailService.GetRawMaterialDetailsByTenant(tenantId);
            return Ok(rawmaterialdetails);
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.OwnRMS)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<RawMaterialDetailVM>))]
        public async Task<IActionResult> GetOwnRMS(long tenantId)
        {
            var vcos = await _companyService.GetCompaniesByTenant(tenantId);
            List<CompaniesVM> cos = vcos.ToList();

            var ownRms = _rawMaterialDetailService.GetOwnRMS(tenantId);
            List<RawMaterialDetailVM> rawmaterialdetails = ownRms.Result.ToList();
            List<MasterPartVM> mps = _masterPartService.GetAllMasterParts().ToList();
            List<PartPurchaseDetailsVM> ppd = _rawMaterialDetailService.GetPartPurchases(tenantId).ToList();
            List<RawMaterialTypeVM> rmtypes = _rawMaterialDetailService.GetRMTypes(tenantId).ToList();
            List<BaseRawMaterialVM> baserms = _rawMaterialDetailService.GetBaseRMs(tenantId).ToList();
           
            var query1 = from pp in ppd
                         join co in cos on pp.PSupplierId equals co.CompanyId into ppjoin
                         from ppj in ppjoin
                         select new PartPurchaseDetailsVM
                         {
                             PPartId = pp.PPartId,
                             PSupplierId = pp.PSupplierId,
                             PSupplier = ppj.CompanyName,
                             LeadTimeInDays = pp.LeadTimeInDays,
                             MinimumOrderQuantity = pp.MinimumOrderQuantity,
                             ShareOfBusiness = pp.ShareOfBusiness,
                             Price = pp.Price,
                             RMId = pp.RMId,
                             BOFId = pp.BOFId,
                             PAdditionalInfo = pp?.PAdditionalInfo ?? string.Empty,
                         };
            List<PartPurchaseDetailsVM> newppd = query1.ToList();

            /*join pp in newppd on rm.RawMaterialDetailId equals pp.RMId into ppjoin
                       from ppj in ppjoin*/
           var query2 = from rm in rawmaterialdetails
                        join mpj in mps on rm.PartId equals mpj.MasterPartId into rmjoin
                        join rmtype in rmtypes on rm.RawMaterialTypeId equals rmtype.RawMaterialTypeId
                        join basermsn in baserms on rm.BaseRawMaterialId equals basermsn.BaseRawMaterialId
                        from rmj in rmjoin
            select new RawMaterialDetailVM
            {
                PartId = rm.PartId,
                SupplierId = rm.SupplierId,
                RawMaterialMadeType = rm.RawMaterialMadeType,
                RawMaterialMadeSubType = rm.RawMaterialMadeSubType,
                RawMaterialTypeId = rm.RawMaterialTypeId,
                BaseRawMaterialId = rm.BaseRawMaterialId,
                RawMaterialWeight = rm.RawMaterialWeight,
                RawMaterialNotes = rm.RawMaterialNotes,
                Standard = rm.Standard,
                MaterialSpecId = rm.MaterialSpecId,
                PartNo = rmj.PartNo,
                PartDescription = rmj.PartDescription,
                RawMaterialDetailId = rm.RawMaterialDetailId,
                Supplier = string.Empty,
                AdditionalInfo = string.Empty,
                RawMaterialType = rmtype?.Name ?? string.Empty,
                BaseRawMaterial = basermsn?.Name ?? string.Empty

            };
            List<RawMaterialDetailVM> newrms = query2.ToList();
            var query = from rm in newrms
                        join pp in newppd on rm.RawMaterialDetailId equals pp.RMId into rmjoin_new
                        join rmtype in rmtypes on rm.RawMaterialTypeId equals rmtype.RawMaterialTypeId
                        join basermsn in baserms on rm.BaseRawMaterialId equals basermsn.BaseRawMaterialId
                        from rmj in rmjoin_new
                        select new RawMaterialDetailVM
                        {
                            PartId = rm.PartId,
                            SupplierId = rm.SupplierId,
                            RawMaterialMadeType = rm.RawMaterialMadeType,
                            RawMaterialMadeSubType = rm.RawMaterialMadeSubType,
                            RawMaterialTypeId = rm.RawMaterialTypeId,
                            BaseRawMaterialId = rm.BaseRawMaterialId,
                            RawMaterialWeight = rm.RawMaterialWeight,
                            RawMaterialNotes = rm.RawMaterialNotes,
                            Standard = rm.Standard,
                            MaterialSpecId = rm.MaterialSpecId,
                            PartNo = rm.PartNo,
                            PartDescription = rm.PartDescription,
                            RawMaterialDetailId = rm.RawMaterialDetailId,
                            Supplier = rmj?.PSupplier??string.Empty,
                            AdditionalInfo = rmj?.PAdditionalInfo??string.Empty,
                            RawMaterialType = rmtype?.Name ?? string.Empty,
                            BaseRawMaterial = basermsn?.Name ?? string.Empty
                        };
            var query0 = from rm in newrms
                        join pp in newppd on rm.RawMaterialDetailId equals pp.BOFId into rmjoin_new
                         join rmtype in rmtypes on rm.RawMaterialTypeId equals rmtype.RawMaterialTypeId
                         join basermsn in baserms on rm.BaseRawMaterialId equals basermsn.BaseRawMaterialId
                         from rmj in rmjoin_new
                        select new RawMaterialDetailVM
                        {
                            PartId = rm.PartId,
                            SupplierId = rm.SupplierId,
                            RawMaterialMadeType = rm.RawMaterialMadeType,
                            RawMaterialMadeSubType = rm.RawMaterialMadeSubType,
                            RawMaterialTypeId = rm.RawMaterialTypeId,
                            BaseRawMaterialId = rm.BaseRawMaterialId,
                            RawMaterialWeight = rm.RawMaterialWeight,
                            RawMaterialNotes = rm.RawMaterialNotes,
                            Standard = rm.Standard,
                            MaterialSpecId = rm.MaterialSpecId,
                            PartNo = rm.PartNo,
                            PartDescription = rm.PartDescription,
                            RawMaterialDetailId = rm.RawMaterialDetailId,
                            Supplier = rmj?.PSupplier ?? string.Empty,
                            AdditionalInfo = rmj?.PAdditionalInfo ?? string.Empty,
                            RawMaterialType = rmtype?.Name ?? string.Empty,
                            BaseRawMaterial = basermsn?.Name ?? string.Empty
                        };
            List<RawMaterialDetailVM> temp = query.ToList();
            List<RawMaterialDetailVM> temp1 = query0.ToList();
            temp.AddRange(temp1);
            return Ok(temp);
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.SupplierRMS)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<RawMaterialDetailVM>))]
        public IActionResult SupplierRMS(long supplierId, long tenantId)
        {
            List<MasterPartVM> mps = _masterPartService.GetAllMasterParts().ToList();
            List<RawMaterialDetailVM> rawmaterialdetails = _rawMaterialDetailService.GetSupplierRMS(supplierId).Result.ToList();
            List<RawMaterialTypeVM> rmtypes = _rawMaterialDetailService.GetRMTypes(tenantId).ToList();
            List<BaseRawMaterialVM> baserms = _rawMaterialDetailService.GetBaseRMs(tenantId).ToList();
            var query = from rm in rawmaterialdetails
                        join mp in mps on rm.PartId equals mp.MasterPartId into mpjoin
                        join rmtype in rmtypes on rm.RawMaterialTypeId equals rmtype.RawMaterialTypeId
                        join basermsn in baserms on rm.BaseRawMaterialId equals basermsn.BaseRawMaterialId
                        from scojoin in mpjoin
                        select new RawMaterialDetailVM
                        {
                            PartId = rm.PartId,
                            SupplierId = rm.SupplierId,
                            RawMaterialMadeType = rm.RawMaterialMadeType,
                            RawMaterialMadeSubType = rm.RawMaterialMadeSubType,
                            RawMaterialTypeId = rm.RawMaterialTypeId,
                            BaseRawMaterialId = rm.BaseRawMaterialId,
                            RawMaterialWeight = rm.RawMaterialWeight,
                            RawMaterialNotes = rm.RawMaterialNotes,
                            Standard = rm.Standard,
                            MaterialSpecId = rm.MaterialSpecId,
                            PartNo = scojoin.PartNo,
                            PartDescription = scojoin.PartDescription,
                            RawMaterialDetailId = rm.RawMaterialDetailId,
                            RawMaterialType = rmtype?.Name ?? string.Empty,
                            BaseRawMaterial = basermsn?.Name ?? string.Empty
                        };
            return Ok(query.ToList());
        }

        /// <summary>
        /// Add/Edit company
        /// </summary>
        /// <param name="RawMaterialDetailVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.RawMaterialDetail.PostRawMaterialDetail)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RawMaterialDetailVM))]
        public async Task<IActionResult> PostRawMaterialDetail([FromBody] RawMaterialDetailVM rawMaterialDetailVM)
        {
            var validator = new RawMaterialDetailVMValidator();
            var validationResult = await validator.ValidateAsync(rawMaterialDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var result = await _rawMaterialDetailService.RawMaterialDetail(rawMaterialDetailVM);
            return Ok(result);
        }

        /// <summary>
        /// Get All RawMaterialType by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetRMTypes)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<RawMaterialTypeVM>))]
        public IActionResult GetRMTypes()
        {
            var rawmaterialdetails = _rawMaterialDetailService.GetRMTypes(-1);
            return Ok(rawmaterialdetails);
        }

        /// <summary>
        /// Get All RawMaterialType by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetRMSpecs)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<RawMaterialSepcVM>))]
        public IActionResult GetRMSpecs()
        {
            var rawmaterialdetails = _rawMaterialDetailService.GetRMSpecs(-1);
            return Ok(rawmaterialdetails);
        }


        /// <summary>
        /// Get All RawMaterialType by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetRMStandards)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<RawMaterialStandardVM>))]
        public IActionResult GetRMStandaGetRMStandardsrds()
        {
            var rawmaterialdetails = _rawMaterialDetailService.GetRMStandards(-1);
            return Ok(rawmaterialdetails);
        }


        /// <summary>
        /// Get All RawMaterialType by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetBaseRMs)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<BaseRawMaterialVM>))]
        public IActionResult GetBaseRMs()
        {
            var rawmaterialdetails = _rawMaterialDetailService.GetBaseRMs(-1);
            return Ok(rawmaterialdetails);
        }


        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetPartPurchases)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<PartPurchaseDetailsVM>))]
        public IActionResult GetPartPurchases(long tenantId)
        {
            var rawmaterialdetails = _rawMaterialDetailService.GetPartPurchases(tenantId);
            return Ok(rawmaterialdetails);
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetPartPurchase)]
        [Produces(AppContentTypes.ContentType, Type = typeof(PartPurchaseDetailsVM))]
        public async Task<IActionResult> GetPartPurchase(int partPurchaseId, long tenantId)
        {
            var result = await _rawMaterialDetailService.GetPartPurchase(partPurchaseId, tenantId);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetPartPurchasesForPartId)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<PartPurchaseDetailsVM>))]
        public IActionResult GetPartPurchasesForPartId(int partId,long tenantId)
        {
            var rawmaterialdetails = _rawMaterialDetailService.GetPartPurchasesForPartNo(partId, tenantId);
            return Ok(rawmaterialdetails);
        }

        /// <summary>
        /// Add/Edit company
        /// </summary>
        /// <param name="RawMaterialDetailVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.RawMaterialDetail.PostPartPurchaseDetail)]
        [Produces(AppContentTypes.ContentType, Type = typeof(PartPurchaseDetailsVM))]
        public async Task<IActionResult> PostPartPurchaseDetail([FromBody] PartPurchaseDetailsVM rawMaterialDetailVM)
        {
            var validator = new PartPurchaseDetailVMValidator();
            var validationResult = await validator.ValidateAsync(rawMaterialDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var result = await _rawMaterialDetailService.PartPurchaseDetail(rawMaterialDetailVM);
            return Ok(result);
        }


        [HttpPost]
        [Route(ApiRoutes.RawMaterialDetail.PostPartPreferredSupplier)]
        [Produces(AppContentTypes.ContentType, Type = typeof(PartPurchaseDetailsVM))]
        public async Task<IActionResult> PreferredSupplier([FromBody] PartPurchaseDetailsVM rawMaterialDetailVM)
        {
            //var validator = new PartPurchaseDetailVMValidator();
            //var validationResult = await validator.ValidateAsync(rawMaterialDetailVM);
            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors);
            var result = await _rawMaterialDetailService.PreferredSupplier(rawMaterialDetailVM);
            return Ok(result);
        }

        [HttpPost]
        [Route(ApiRoutes.RawMaterialDetail.RemPartPurchaseDetail)]
        [Produces(AppContentTypes.ContentType, Type = typeof(PartPurchaseDetailsVM))]
        public async Task<IActionResult> RemPartPurchaseDetail([FromBody] PartPurchaseDetailsVM rawMaterialDetailVM)
        {
            var validator = new PartPurchaseDetailVMValidator();
            var validationResult = await validator.ValidateAsync(rawMaterialDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var result = await _rawMaterialDetailService.RemPartPurchaseDetail(rawMaterialDetailVM);
            return Ok(result);
        }




        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.CheckBaseRm)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckBaseRm(string rmName)
        {
            bool exists = false;
            exists = await _rawMaterialDetailService.CheckBaseRm(rmName);
            return Ok(exists);
        }

        [HttpPost]
        [Route(ApiRoutes.RawMaterialDetail.BaseRM)]
        [Produces(AppContentTypes.ContentType, Type = typeof(BaseRawMaterialVM))]
        public async Task<IActionResult> BaseRM([FromBody] BaseRawMaterialVM baseRMVm)
        {
            var result = await _rawMaterialDetailService.BaseRM(baseRMVm);
            return Ok(result);
        }

        [HttpPost]
        [Route(ApiRoutes.RawMaterialDetail.RMType)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RawMaterialTypeVM))]
        public async Task<IActionResult> RMType(RawMaterialTypeVM rMTypeVm)
        {
            var result = await _rawMaterialDetailService.RMType(rMTypeVm);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.CheckRMType)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckRmType(string rmTypeName)
        {
            bool exists = false;
            exists = await _rawMaterialDetailService.CheckRmType(rmTypeName);
            return Ok(exists);
        }
        [HttpPost]
        [Route(ApiRoutes.RawMaterialDetail.RMSpec)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RawMaterialSepcVM))]
        public async Task<IActionResult> RMSpec(RawMaterialSepcVM rMSpecVm)
        {
            var result = await _rawMaterialDetailService.RMSpec(rMSpecVm);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.CheckRMSpec)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckRMSpec(string rmSpecName)
        {
            bool exists = false;
            exists = await _rawMaterialDetailService.CheckRmSpec(rmSpecName);
            return Ok(exists);
        }
        [HttpPost]
        [Route(ApiRoutes.RawMaterialDetail.RMStandard)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RawMaterialStandardVM))]
        public async Task<IActionResult> RMStandard(RawMaterialStandardVM rMStandardVm)
        {
            var result = await _rawMaterialDetailService.RMStandard(rMStandardVm);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.CheckRMStandard)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckRMStandard(string rmStName)
        {
            bool exists = false;
            exists = await _rawMaterialDetailService.CheckRmStandard(rmStName);
            return Ok(exists);
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetPartsUOMs)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ItemMasterPartVM>))]
        public async Task<IEnumerable<PartUOMVM>> GetPartsUOMs(long TenantId)
        {
            List<PartUOMVM> list = new List<PartUOMVM>();
            List<ManufacturedPartNoDetailVM> manufList = _manufacturedPartNoDetailService.GetAllManufacturedPartNoDetailsByTypeTenant(TenantId).ToList();
            List<UOMVM> uoms =  _manufacturedPartNoDetailService.GetUOMsByTenantId(TenantId).ToList();
            try
            {
                List<PartUOMVM> tempList = new List<PartUOMVM>();
                var query = from manuf in manufList
                            join mp in uoms on manuf.UOMId equals mp.UOMId
                            select new PartUOMVM
                            {
                                PartId = manuf.PartId,
                                UOMName = mp.Name
                            };
                list = query.ToList();
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.Source;
            }
            var bofs = _boughtOutFinishDetailService.GetBoughtOutFinishDetailsByTenant(TenantId);

            try
            {
                var query1 =
                         from bof in bofs
                             //join pp in partPurchases on bof.BoughtOutFinishDetailId equals pp.BOFId
                         join mp in uoms on bof.UOMId equals mp.UOMId into tempjoin
                         from scojoin in tempjoin.DefaultIfEmpty()
                         select new PartUOMVM
                         {
                             PartId = bof.PartId,
                             UOMName = scojoin.Name
                         };
                List<PartUOMVM> list1 = query1.ToList();
                list.AddRange(list1);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.Source;
            }
            var rms = _rawMaterialDetailService.GetRawMaterialDetailsByTenant(TenantId);
            try
            {

                // join pp in partPurchases on rm.RawMaterialDetailId equals pp.RMId
                var query2 =
                 from rm in rms
                 join mp in uoms on rm.UOMId equals mp.UOMId into tempjoin
                 from scojoin in tempjoin.DefaultIfEmpty()
                 select new PartUOMVM
                 {
                     PartId = (int)rm.PartId,
                     UOMName = scojoin.Name
                     

                 };
                List<PartUOMVM> list2 = query2.ToList();
                list.AddRange(list2);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.Source;
            }

            return list;
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetMasterPartById)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MasterPartVM))]
        public async Task<MasterPartVM> GetMasterPartById(int partid)
        {
            var masterpart = await _masterPartService.GetMasterPart(partid);
            return masterpart;
        }


        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetMasterParts)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ItemMasterPartVM>))]
        public async Task<IEnumerable<ItemMasterPartVM>> GetMasterPartView(long TenantId)
        {
            List<ItemMasterPartVM> list = new List<ItemMasterPartVM>();
            var vcos = await _companyService.GetCompaniesByTenant(TenantId);
            List<CompaniesVM> cos = vcos.ToList();
            List<MasterPartVM> masterParts = _masterPartService.GetAllMasterParts().ToList();
            List<ManufacturedPartNoDetailVM> manufList = _manufacturedPartNoDetailService.GetAllManufacturedPartNoDetailsByTypeTenant(TenantId).ToList();
            try
            {
                List<ItemMasterPartVM> tempList = new List<ItemMasterPartVM>();
                var query = from manuf in manufList
                            join mp in masterParts on manuf.PartId equals mp.MasterPartId
                            select new ItemMasterPartVM
                            {
                                PartId = manuf.PartId,
                                MasterPartType = manuf.ManufacturedPartType == 1 ? "ManufacturedPart" : "Assembly",
                                CompanyId = manuf.CompanyId,
                                Company = "",
                                PartNo = mp.PartNo,
                                Description = mp?.PartDescription ?? string.Empty,
                                BoughtOutFinishMadeType = -1,
                                RawMaterialMadeSubType = -1,
                                RawMaterialTypeId = -1,
                                BaseRawMaterialId = -1,
                                BOFSupplierPartNo = "",
                                RMSupplier = "",
                                Supplier = "",
                                SupplierPartNo = "",
                                Status=mp.Status,
                                Notes = mp?.PartDescription ?? string.Empty,
                                Type = 0,
                                TenantId = manuf.TenantId
                            };
                tempList = query.ToList();
                var newquery = (from manuf in tempList
                                join co in cos on manuf.CompanyId equals co.CompanyId
                                select new ItemMasterPartVM
                                {
                                    PartId = manuf.PartId,
                                    MasterPartType = manuf.MasterPartType,
                                    CompanyId = manuf.CompanyId,
                                    Company = co.CompanyName,
                                    PartNo = manuf.PartNo,
                                    Description = manuf.Description,
                                    BoughtOutFinishMadeType = -1,
                                    RawMaterialMadeSubType = -1,
                                    RawMaterialTypeId = -1,
                                    BaseRawMaterialId = -1,
                                    BOFSupplierPartNo = "",
                                    RMSupplier = "",
                                    Supplier = "",
                                    SupplierPartNo = "",
                                    Status = manuf.Status,
                                    Notes = manuf.Description,
                                    Type = 0,
                                    TenantId = manuf.TenantId
                                })
                .GroupBy(x => x.PartId)
                .Select(g => g.First()); // Select the first unique entry in each group
                list = newquery.ToList();

            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.Source;
            }
            var bofs = _boughtOutFinishDetailService.GetBoughtOutFinishDetailsByTenant(TenantId);
            var partPurchases = _rawMaterialDetailService.GetPartPurchases(TenantId);

            try
            {
                var query1 =
                         from bof in bofs
                             //join pp in partPurchases on bof.BoughtOutFinishDetailId equals pp.BOFId
                         join mp in masterParts on bof.PartId equals mp.MasterPartId into tempjoin
                         from scojoin in tempjoin.DefaultIfEmpty()
                         select new ItemMasterPartVM
                         {
                             PartId = bof.PartId,
                             MasterPartType = "BOF",
                             Company = "",
                             PartNo = scojoin.PartNo,
                             Description = scojoin?.PartDescription ?? string.Empty,
                             BoughtOutFinishMadeType = -1,
                             RawMaterialMadeSubType = -1,
                             RawMaterialTypeId = -1,
                             BaseRawMaterialId = -1,
                             BOFSupplierPartNo = "",
                             RMSupplier = "",
                             Supplier = "",
                             SupplierPartNo = "",
                             Status = scojoin.Status,
                             Notes = scojoin?.PartDescription ?? string.Empty,
                             Type = 0,
                             BOFId = (int)bof.BoughtOutFinishDetailId,
                             TenantId = bof.TenantId
                         };
                List<ItemMasterPartVM> list1 = query1.ToList();
                foreach (ItemMasterPartVM imp in list1)
                {
                    foreach (PartPurchaseDetailsVM pp in partPurchases)
                    {
                        if (imp.BOFId == pp.BOFId)
                        {
                            imp.Company = pp.PSupplier;
                        }
                    }
                }
                list.AddRange(list1);
            }
            catch(Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.Source;
            }
            var rms = _rawMaterialDetailService.GetRawMaterialDetailsByTenant(TenantId);
            try
            {

                // join pp in partPurchases on rm.RawMaterialDetailId equals pp.RMId
                var query2 =
                 from rm in rms
                 join mp in masterParts on rm.PartId equals mp.MasterPartId into tempjoin
                 from scojoin in tempjoin.DefaultIfEmpty()
                 select new ItemMasterPartVM
                 {
                     PartId = rm.PartId,
                     MasterPartType = "RawMaterial",
                     Company = "",
                     PartNo = scojoin.PartNo,
                     Description = scojoin?.PartDescription ?? string.Empty,
                     BoughtOutFinishMadeType = -1,
                     RawMaterialMadeSubType = -1,
                     RawMaterialTypeId = -1,
                     BaseRawMaterialId = -1,
                     BOFSupplierPartNo = "",
                     RMSupplier = "",
                     Supplier = "",
                     SupplierPartNo = "",
                     Status=scojoin.Status,
                     Notes = scojoin?.PartDescription ?? string.Empty,
                     Type = 0,
                     RMId = (int)rm.RawMaterialDetailId,
                     TenantId = rm.TenantId
                     
                 };
                List<ItemMasterPartVM> list2 = query2.ToList();
                foreach (ItemMasterPartVM imp in list2)
                {
                    foreach (PartPurchaseDetailsVM pp in partPurchases)
                    {
                        if(imp.RMId == pp.RMId)
                        {
                            imp.Company = pp.PSupplier;
                        }
                    }
                }
                list.AddRange(list2);
            }
            catch(Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.Source;
            }
            
            return list;
        }

        [HttpGet]
        [Route(ApiRoutes.RawMaterialDetail.GetSelectParts)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<SelectPartVM>))]
        public async Task<IEnumerable<SelectPartVM>> GetSelectParts(long tenantId)
        {
            var vcos = await _companyService.GetCompaniesByTenant(tenantId);
            List<CompaniesVM> cos = vcos.ToList();
            List<MasterPartVM> masterParts = _masterPartService.GetAllMasterParts().ToList();
            List<ManufacturedPartNoDetailVM> manufList = _manufacturedPartNoDetailService.GetAllManufacturedPartNoDetailsByTypeTenant(tenantId).ToList();
            var query = from manuf in manufList
                        join mp in masterParts on manuf.PartId equals mp.MasterPartId into mpjoin
                        from smpjoin in mpjoin.DefaultIfEmpty()
                        join co in cos on manuf.CompanyId equals co.CompanyId into cojoin
                        from scojoin in cojoin.DefaultIfEmpty()
                        select new SelectPartVM
                        {
                            PartId = manuf.PartId,
                            MasterPartType = manuf.ManufacturedPartType == 1 ? "Child" : "Assembly",
                            BoughtOutFinishMadeType = "",
                            Company = scojoin?.CompanyName??string.Empty,
                            PartNo = smpjoin.PartNo,
                            Description = smpjoin?.PartDescription ?? string.Empty,
                        };
            List<SelectPartVM> list = query.ToList();
            try
            {
                var bofs = _boughtOutFinishDetailService.GetBoughtOutFinishDetailsByTenant(tenantId);
                var query1 =
                    from bof in bofs
                    join mp in masterParts on bof.PartId equals mp.MasterPartId into bofjoin
                    from subpp in bofjoin.DefaultIfEmpty()
                    select new SelectPartVM
                    {
                        PartId = bof.PartId,
                        MasterPartType = "BOF",
                        BoughtOutFinishMadeType = (bof.BoughtOutFinishMadeType == 1) ? "Standard" :(((bof.BoughtOutFinishMadeType == 2) ? "Catalog" : "Made To Print")),
                        Company = string.Empty,
                        PartNo = subpp.PartNo,
                        Description = subpp?.PartDescription ?? string.Empty,
                        
                    };
                List<SelectPartVM> list1 = query1.ToList();
                list.AddRange(list1);
            }
            catch (Exception ex)
            {
                string str = ex.InnerException.Message;
            }

            return list;
        }
      
    }
   


}
