using CWB.CommonUtils.Common;
using CWB.CompanySettings.CompanySettingsUtils;
using CWB.CompanySettings.Services.Designations;
using CWB.CompanySettings.ViewModels.Designations;
using CWB.CompanySettings.ViewModelValidators.Designations;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Controllers
{
    [ApiController]
    [Authorize]
    public class DesignationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IDesignationService _designationService;

        public DesignationController(ILoggerManager logger, IDesignationService designationService)
        {
            _logger = logger;
            _designationService = designationService;
        }


        /// <summary>
        /// Get Designations by tenant Id..
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Designation.GetDesignations)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<DesignationListVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult GetDesignations(long tenantId)
        {
            var designations = _designationService.GetDesignations(tenantId);
            return Ok(designations);
        }


        /// <summary>
        /// Add/edit Designation
        /// </summary>
        /// <param name="documentTypeVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Designation.PostDesignation)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DesignationVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostDesignation([FromBody] DesignationVM designationVM)
        {
            var validator = new DesignationVMValidator();
            var validationResult = await validator.ValidateAsync(designationVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            //check if duplicate
            var designationExist = _designationService.CheckDesignationExisit(new CheckDesignationVM
            {
                DesignationId = designationVM.DesignationId,
                Name = designationVM.Name,
                TenantId = designationVM.TenantId
            });
            if (designationExist)
            {
                ModelState.AddModelError("Name", $"Document Type: {designationVM.Name} Already Exist");
                return BadRequest(ModelState);
            }
            var result = await _designationService.Designation(designationVM);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.Designation.DelDesignation)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult DelDesignation(long designationId)
        {
            var docTypes = _designationService.DelDesignation(designationId);
            return Ok(docTypes);
        }


    }
}
