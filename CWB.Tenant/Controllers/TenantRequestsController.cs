using CWB.CommonUtils.Common;
using CWB.Constants.Tenant;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.Tenant.Services.Tenants;
using CWB.Tenant.TenantUtils;
using CWB.Tenant.ViewModels;
using CWB.Tenant.ViewModelValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Tenant.Controllers
{
    [ApiController]
    public class TenantRequestsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ITenantRequestService _tenantRequestService;

        public TenantRequestsController(ILoggerManager logger, ITenantRequestService tenantRequestService)
        {
            _logger = logger;
            _tenantRequestService = tenantRequestService;
        }
        /// <summary>
        /// Request for Tenant Creation
        /// </summary>
        /// <param name="model">tenant request view model</param>
        /// <returns>Ok result on success</returns>
        [HttpPost]
        [Route(ApiRoutes.TenantRequest.Request)]
        public async Task<IActionResult> TenantRequests([FromBody] TenantRequestsVM model)
        {
            //add validations..
            var validator = new TenantRequestsVMValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (_tenantRequestService.CheckDuplicateRequestByEmail(model.Email.ToLower()))
            {
                _logger.LogError($"Duplicate request:{model.Email}");
                ModelState.AddModelError(nameof(model.Email), $"Tenant already registered with email: {model.Email}");

                return BadRequest(ModelState);
            }
            else
            {
                model.Email = model.Email.ToLower();//convert email to lover before saving
                await _tenantRequestService.AddRequest(model);
                return Ok();
            }

        }

        /// <summary>
        /// Tenant Request Approval or Reject
        /// </summary>
        /// <param name="model">Request Approve Reject view model</param>
        /// <returns></returns>
        [Authorize(Roles = Roles.SUPERADMIN)]
        [HttpPost]
        [Route(ApiRoutes.TenantRequest.RequestAR)]
        public async Task<IActionResult> TenantRequestStatus([FromBody] TenantRequestApproveRejectVM model)
        {
            //add validations..
            var validator = new TenantRequestApvRjtVMValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            //check if the status is pending..
            if (_tenantRequestService.CheckRequestStatusById(model.TenantRequestId, TenantStatus.Pending))
            {
                await _tenantRequestService.UpdateRequestStatus(model.TenantRequestId, model.Status, model.Comments);
                return Ok();
            }
            else
            {
                _logger.LogError($"Invalid Tenant Request Id:{model.TenantRequestId} Status:{model.Status}");
                ModelState.AddModelError("Error", $"Invalid Tenant Request");
                return BadRequest(ModelState);
            }

        }


        /// <summary>
        /// Tenant Request Approval or Reject
        /// </summary>
        /// <param name="model">Request Approve Reject view model</param>
        /// <returns></returns>        
        [HttpPost]
        [Route(ApiRoutes.TenantRequest.RequestARInternal)]
        public async Task<IActionResult> TenantRequestInternalStatus([FromBody] TenantRequestApproveRejectVM model)
        {
            //add validations..
            var validator = new TenantRequestApvRjtVMValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            //check if the status is pending..
            if (_tenantRequestService.CheckRequestStatusById(model.TenantRequestId, TenantStatus.Pending))
            {
                await _tenantRequestService.UpdateRequestStatus(model.TenantRequestId, model.Status, model.Comments);
                return Ok(true);
            }
            else
            {
                _logger.LogError($"Invalid Tenant Request Id:{model.TenantRequestId} Status:{model.Status}");
                ModelState.AddModelError("Error", $"Invalid Tenant Request");
                return BadRequest(ModelState);
            }

        }

        /// <summary>
        /// Get Tenant requests by status
        /// </summary>
        /// <param name="status">Status of the request</param>
        /// <returns>List of teant requests</returns>
        [Authorize(Roles = Roles.SUPERADMIN)]
        [HttpGet]
        [Route(ApiRoutes.TenantRequest.RequestByStatus)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<TenantRequestsListVM>))]
        public IActionResult GetTenantRequestByStatus(string status)
        {
            var tenantRequests = _tenantRequestService.GetAllRequestsByStatus(status);
            return Ok(tenantRequests);
        }


        /// <summary>
        /// get tenant request by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Roles.SUPERADMIN)]
        [HttpGet]
        [Route(ApiRoutes.TenantRequest.GetRequest)]
        [Produces(AppContentTypes.ContentType, Type = typeof(TenantRequestsListVM))]
        public async Task<IActionResult> GetTenantRequestById(long Id)
        {
            var tenantRequest = await _tenantRequestService.GetRequestById(Id);
            if (tenantRequest == null)
            {
                return NotFound();
            }
            return Ok(tenantRequest);
        }


    }
}
