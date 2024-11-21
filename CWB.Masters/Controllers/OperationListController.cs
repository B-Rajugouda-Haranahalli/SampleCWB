using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.Masters.MastersUtils;
using CWB.Masters.OperationList.ViewModelValidators;
using CWB.Masters.Services.OperationList;
using CWB.Masters.ViewModels.OperationList;
using CWB.Masters.ViewModelValidators.OperationList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class OperationListController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IOperationListService _operationListService;

        public OperationListController(ILoggerManager logger, IOperationListService operationListService)
        {
            _logger = logger;
            _operationListService = operationListService;
        }

        /// <summary>
        /// Get Operation List by Tenant Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.OperationList.GetOperationList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<OperationListVM>))]
        public IActionResult GetOperationList(long Id)
        {
            var companies = _operationListService.GetOperationsByTenant(Id);
            return Ok(companies);
        }


        /// <summary>
        /// Add or Edit Operation List
        /// </summary>
        /// <param name="operationVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.OperationList.PostOperation)]
        [Produces(AppContentTypes.ContentType, Type = typeof(OperationListVM))]
        public async Task<IActionResult> PostOperation([FromBody] OperationListVM operationVM)
        {
            var validator = new OperationVMValidator();
            var validationResult = await validator.ValidateAsync(operationVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var result = await _operationListService.Operation(operationVM);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.OperationList.GetOperation)]
        [Produces(AppContentTypes.ContentType, Type = typeof(OperationListVM))]
        public IActionResult GetOperation(long Id, long tenantId)
        {
            var result = _operationListService.Operation(Id, tenantId);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        /// <summary>
        /// Check if Operation Exist
        /// </summary>
        /// <param name="checkOperationVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.OperationList.IsOperationExist)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckOperation([FromBody] CheckOperationVM checkOperationVM)
        {
            var validator = new CheckOperationVMValidator();
            var validationResult = await validator.ValidateAsync(checkOperationVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _operationListService.CheckIfOperationExisit(checkOperationVM);
            return Ok(result);
        }

        /// <summary>
        /// Get list of document types by operationlist id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.OperationList.GetOperationalDocumentTypes)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<OperationalDocumentListVM>))]
        public IActionResult GetOperationalDocumentTypes(long Id, long tenantId)
        {
            var operationalDocTypes = _operationListService.GetOperationDocumentTypes(tenantId, Id);
            return Ok(operationalDocTypes);

        }

        /// <summary>
        /// Add - Edit Operation doctypes
        /// </summary>
        /// <param name="operationalDocumentListVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.OperationList.PostOperationalDocumentTypes)]
        [Produces(AppContentTypes.ContentType, Type = typeof(OperationalDocumentListVM))]
        public async Task<IActionResult> PostOperationalDocumentTypes([FromBody] OperationalDocumentListVM operationalDocumentListVM)
        {
            //var validator = new OperationalDocumentListVMValidator();
            //var validationResult = await validator.ValidateAsync(operationalDocumentListVM);
            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors);
            var result = await _operationListService.OperationDocumentTypes(operationalDocumentListVM);
            return Ok(result);
        }


        [HttpGet]
        [Route(ApiRoutes.OperationList.DeleteOperationDoc)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DeleteOperationDoc(long opDocId, long tenantId)
        {
            var result = await _operationListService.DeleteOperationDoc(opDocId, tenantId);
            return Ok(result);
        }
    }
}
