using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.Masters.Domain;
using CWB.Masters.MastersUtils;
using CWB.Masters.Services.ItemMaster;
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidators.ItemMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class BoughtOutFinishDetailController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IBoughtOutFinishDetailService _boughtOutFinishDetailService;
        private readonly IMasterPartService _masterPartService;

        public BoughtOutFinishDetailController(ILoggerManager logger, 
            IBoughtOutFinishDetailService boughtOutFinishDetailService,
            IMasterPartService masterPartService)
        {
            _logger = logger;
            _boughtOutFinishDetailService = boughtOutFinishDetailService;
            _masterPartService = masterPartService;
        }


        [HttpGet]
        [Route(ApiRoutes.ManufacturedPartNoDetail.GetBOFPart)]
        [Produces(AppContentTypes.ContentType, Type = typeof(BoughtOutFinishDetailVM))]
        public async Task<IActionResult> GetBOFPart(int partId, long tenantId)
        {
            BoughtOutFinishDetailVM manufP = await _masterPartService.GetBOFPart(partId, tenantId);
            return Ok(manufP);
        }

        /// <summary>
        /// Get BoughtOutFinishDetail List by Tenant Id
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.BoughtOutFinishDetail.GetBoughtOutFinishDetailList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<BoughtOutFinishDetailVM>))]
        public IActionResult GetBoughtOutFinishDetailList(long tenantId)
        {
            var boughtoutfinishdetails = _boughtOutFinishDetailService.GetBoughtOutFinishDetailsByTenant(tenantId);
            return Ok(boughtoutfinishdetails);
        }

        

        /// <summary>
        /// Add/Edit BoughtOutFinishDetail
        /// </summary>
        /// <param name="BoughtOutFinishDetailVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.BoughtOutFinishDetail.PostBoughtOutFinishDetail)]
        [Produces(AppContentTypes.ContentType, Type = typeof(BoughtOutFinishDetailVM))]
        public async Task<IActionResult> PostBoughtOutFinishDetail([FromBody] BoughtOutFinishDetailVM boughtOutFinishDetailVM)
        {
            var validator = new BoughtOutFinishDetailVMValidator();
            var validationResult = await validator.ValidateAsync(boughtOutFinishDetailVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var result = await _boughtOutFinishDetailService.BoughtOutFinishDetail(boughtOutFinishDetailVM);
            return Ok(result);
        }
    }
}
