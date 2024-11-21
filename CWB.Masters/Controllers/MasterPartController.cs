using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class MasterPartController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRawMaterialDetailService _rawMaterialDetailService;
        private readonly IBoughtOutFinishDetailService _boughtOutFinishDetailService;
        private readonly IManufacturedPartNoDetailService _manufacturedPartNoDetailService;
        private readonly IMasterPartService _masterPartService;


        public MasterPartController(ILoggerManager logger
            , IRawMaterialDetailService rawMaterialDetailService
            , IManufacturedPartNoDetailService manufacturedPartNoDetailService
            , IBoughtOutFinishDetailService boughtOutFinishDetailService
            , IMasterPartService masterPartService)
        {
            _logger = logger;
            _rawMaterialDetailService = rawMaterialDetailService;
            _manufacturedPartNoDetailService = manufacturedPartNoDetailService;
            _boughtOutFinishDetailService = boughtOutFinishDetailService;
            _masterPartService = masterPartService;
        }

        [HttpGet]
        [Route(ApiRoutes.MasterParts.CheckPartNo)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckPartNo(string partNo)
        {
            bool exists = false;
            int partId = _masterPartService.CheckPartNo(partNo);
            if (partId > 0)
            {
                exists = _manufacturedPartNoDetailService.CheckPartNo(partId);
                if (!exists)
                {
                    exists = _rawMaterialDetailService.CheckPartNo(partId);
                    if (!exists)
                    {
                        exists = _boughtOutFinishDetailService.CheckPartNo(partId);
                    }
                }
            }
            return Ok(exists);
        }

        [HttpGet]
        [Route(ApiRoutes.Masters.GetStatuses)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<PartStatusVM>))]
        public IActionResult GetStatuses()
        {
            var companyTypes = _masterPartService.GetStatuses();
            return Ok(companyTypes);
        }


        [HttpGet]
        [Route(ApiRoutes.Masters.GetAllItemMasterDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ItemMasterDocListVM))]
        public async Task<IActionResult> GetDocumentType(long tenantId)
        {
            var result = await _masterPartService.GetAllItemMasterDocList(tenantId);
            return Ok(result);
        }

        [HttpPost]
        [Route(ApiRoutes.Masters.PostItemMasterDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ItemMasterDocListVM))]
        public async Task<IActionResult> PostItemMasterDocList([FromBody] ItemMasterDocListVM documentType)
        {
            var result = await _masterPartService.PostItemMasterDocList(documentType);
            return Ok(result);
        }


        [HttpGet]
        [Route(ApiRoutes.Masters.GetAllItemMasterContent)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ItemMasterContentVM))]
        public async Task<IActionResult> GetAllItemMasterContent()
        {
            var result = await _masterPartService.GetAllItemMasterContent();
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.Masters.DeleteItemMasterDoc)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DeleteItemMasterDoc(long itemMasterDocListId, long tenantId)
        {
            var result = await _masterPartService.DeleteItemMasterDoc(itemMasterDocListId, tenantId);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.Masters.CheckPartNoInDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckPartNoInDocList(long documentTypeId, long contentId, long tenantId)
        {
            bool exists = false;
            exists = await _masterPartService.CheckDocumentTypeInItemMaster(documentTypeId,contentId, tenantId);
            return Ok(exists);
        }

    }

}
