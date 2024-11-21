using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.Masters.Domain;
using CWB.Masters.MastersUtils;
using CWB.Masters.Services.Company;
using CWB.Masters.Services.ItemMaster;
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidators.ItemMaster;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class ManufacturedPartNoDetailController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IManufacturedPartNoDetailService _manufacturedPartNoDetailService;
        private readonly IMasterPartService _masterPartService;

        public ManufacturedPartNoDetailController(ILoggerManager logger, IManufacturedPartNoDetailService manufacturedPartNoDetailService
            ,IMasterPartService masterPartService)
        {
            _logger = logger;
            _manufacturedPartNoDetailService = manufacturedPartNoDetailService;
            _masterPartService = masterPartService; 
        }

        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetManufPart)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ManufacturedPartNoDetailVM))]
        public async Task<IActionResult> GetManufPart(int partId, long tenantId)
        {
            ManufacturedPartNoDetailVM manufP = await _masterPartService.GetManufPart(partId, tenantId);
            return Ok(manufP);   
        }

        /// <summary>
        /// Get All ManufacturedPartNoDetail by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetManufacturedPartNoDetailList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ManufacturedPartNoDetailVM>))]
        public IActionResult GetManufacturedPartNoDetailList(long ManufPartType, string companyName, long tenantID)
        {
            List<MasterPartVM> masterParts = _masterPartService.GetAllMasterParts().ToList();
            List<ManufacturedPartNoDetailVM> manufList = _manufacturedPartNoDetailService.GetManufacturedPartNoDetailsByTypeTenant(ManufPartType, companyName, tenantID).ToList();

            var query = from manuf in manufList
                        join mp in masterParts on manuf.PartId equals mp.MasterPartId into mpjoin
                        from scojoin in mpjoin.DefaultIfEmpty()
                        select new ManufacturedPartNoDetailVM
                        {
                            ManufacturedPartType = manuf.ManufacturedPartType,
                            PartId = scojoin.MasterPartId,
                            CompanyId = manuf.CompanyId,
                            FinishedWeight = manuf.FinishedWeight,
                            UOMId = manuf.UOMId,
                            ManufacturedPartNoDetailId = manuf.ManufacturedPartNoDetailId,
                            PartNo = scojoin.PartNo,
                            PartDescription = scojoin.PartDescription,
                            RevNo = scojoin.RevNo,
                            RevDate = scojoin.RevDate,
                            Status = Convert.ToString(scojoin.Status),
                            StatusChangeReason = scojoin.StatusChangeReason,
                            MasterPartType = Convert.ToString(scojoin.MasterPartType)
                        };
            manufList = query.ToList();
            return Ok(manufList);
        }

        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetAllManufacturedPartNoDetailList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ManufacturedPartNoDetailVM>))]
        public IActionResult GetAllManufacturedPartNoDetailList(long ManufPartType, string companyName, long tenantID)
        {
            var manufacturedpartnodetails = _manufacturedPartNoDetailService.GetAllManufacturedPartNoDetailsByTypeTenant(tenantID);
            return Ok(manufacturedpartnodetails);
        }

        /*[HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.HelloWorld)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ManufacturedPartNoDetailListVM>))]
        public IActionResult HelloWorld()
        {
            //var manufacturedpartnodetails = _manufacturedPartNoDetailService.GetManufacturedPartNoDetailsByTypeTenant(manPartTypeId, tenantId);
            return Ok("Hello World");
        }*/

        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetUOMs)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<UOMVM>))]
        public IActionResult GetUOMs(long Id)
        {
            var companies = _manufacturedPartNoDetailService.GetUOMsByTenantId(Id);
            Console.Write(companies.ToString());
            return Ok(companies);
        }

        [HttpPost]
        [Route(ApiRoutes.ManufacturedPartNoDetail.AddUOM)]
        [Produces(AppContentTypes.ContentType, Type = typeof(UOMVM))]
        public async Task<IActionResult> AddUOM([FromBody] UOMVM uomvm)
        {
            var result = await _manufacturedPartNoDetailService.UOM(uomvm);
            return Ok(result);
        }
        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.CheckUOM)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckUOM(string uomName)
        {
            bool exists = false;
            exists = await _manufacturedPartNoDetailService.CheckUOM(uomName);
            return Ok(exists);
        }





        //return Ok("Hello World");

        /// <summary>
        /// Add/Edit ManufacturedPartNoDetail
        /// </summary>
        /// <param name="ManufacturedPartNoDetailVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.ManufacturedPartNoDetail.PostManufacturedPartNoDetail)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ManufacturedPartNoDetailVM))]
        public async Task<IActionResult> PostManufacturedPartNoDetail([FromBody] ManufacturedPartNoDetailVM manufacturedPartNoDetailVM)
        {
            var validator = new ManufacturedPartNoDetailVMValidator();
            var validationResult = await validator.ValidateAsync(manufacturedPartNoDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var result = await _manufacturedPartNoDetailService.ManufacturedPartNoDetail(manufacturedPartNoDetailVM);
            return Ok(result);
        }
        [HttpPost]
        [Route(ApiRoutes.ManufacturedPartNoDetail.PostMPMakeFrom)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MPMakeFromVM))]
        public async Task<IActionResult> PostMPMakeFrom([FromBody] MPMakeFromVM manufacturedPartNoDetailVM)
        {
           /** var validator = new ManufacturedPartNoDetailVMValidator();
            var validationResult = await validator.ValidateAsync(manufacturedPartNoDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/
            var result = await _manufacturedPartNoDetailService.MPMakeFrom(manufacturedPartNoDetailVM);
            return Ok(result);
        }
        [HttpPost]
        [Route(ApiRoutes.ManufacturedPartNoDetail.PreferredMPMakeFrom)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MPMakeFromVM))]
        public async Task<IActionResult> PreferredMPMakeFrom([FromBody] MPMakeFromVM manufacturedPartNoDetailVM)
        {
            /** var validator = new ManufacturedPartNoDetailVMValidator();
             var validationResult = await validator.ValidateAsync(manufacturedPartNoDetailVM);
             if (!validationResult.IsValid)
                 return BadRequest(validationResult.Errors);*/
            var result = await _manufacturedPartNoDetailService.PreferredInputMatl(manufacturedPartNoDetailVM);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetPartStatus)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<PartStatusChangeLogVM>))]
        public async Task<IActionResult> GetPartStatus(long tenantId)
        {
            var companyTypes = await _manufacturedPartNoDetailService.GetPartStatusChangelog(tenantId);
            return Ok(companyTypes);
        }
        /// <summary>
        /// Get All Companies by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetMPMakeFromList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<MPMakeFromVM>))]
        public IActionResult GetMPMakeFromList(string partId)
        {
            var mpmakefromlist = _manufacturedPartNoDetailService.GetMPMakeFromList(partId, -1);
            return Ok(mpmakefromlist);
        }
        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetMPMakeFrom)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MPMakeFromVM))]
        public async Task<IActionResult> GetMPMakeFrom(long Id)
        {
            var mpmakefromlist = await _manufacturedPartNoDetailService.GetMPMakeFrom(Id);
            return Ok(mpmakefromlist);
        }

        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetMPBOM)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MPBOMVM))]
        public async Task<IActionResult> GetMPBOM(long Id)
        {
            var mpmakefromlist = await _manufacturedPartNoDetailService.GetMPBOM(Id);
            return Ok(mpmakefromlist);
        }

        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetMPBOMList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<MPBOMVM>))]
        public IActionResult GetMPBOMList(string partId)
        {
            var mpmakefromlist = _manufacturedPartNoDetailService.GetMPBOMList(partId,-1);
            return Ok(mpmakefromlist);
        }

        [HttpPost]
        [Route(ApiRoutes.ManufacturedPartNoDetail.PostMPBOM)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MPBOMVM))]
        public async Task<IActionResult> PostMPBOM([FromBody] MPBOMVM manufacturedPartNoDetailVM)
        {
            /*var validator = new ManufacturedPartNoDetailVMValidator();
            var validationResult = await validator.ValidateAsync(manufacturedPartNoDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/
            var result = await _manufacturedPartNoDetailService.MPBOM(manufacturedPartNoDetailVM);
            return Ok(result);
        }

        [HttpPost]
        [Route(ApiRoutes.ManufacturedPartNoDetail.RemMakeFrom)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MPMakeFromVM))]
        public async Task<IActionResult> RemMakeFrom([FromBody] MPMakeFromVM rawMaterialDetailVM)
        {
           /** var validator = new PartPurchaseDetailVMValidator();
            var validationResult = await validator.ValidateAsync(rawMaterialDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/
            var result = await _manufacturedPartNoDetailService.RemMakeFrom(rawMaterialDetailVM);
            return Ok(result);
        }


        [HttpPost]
        [Route(ApiRoutes.ManufacturedPartNoDetail.RemBOM)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MPBOMVM))]
        public async Task<IActionResult> RemBOM([FromBody] MPBOMVM rawMaterialDetailVM)
        {
            /**var validator = new PartPurchaseDetailVMValidator();
            var validationResult = await validator.ValidateAsync(rawMaterialDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);*/
            var result = await _manufacturedPartNoDetailService.RemBOM(rawMaterialDetailVM);
            return Ok(result);
        }

    }
}
