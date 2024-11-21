using CWB.CommonUtils.Common;
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
    public class TenantController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ITenantService _tenantService;

        public TenantController(ILoggerManager logger, ITenantService tenantService)
        {
            _logger = logger;
            _tenantService = tenantService;
        }


        /// <summary>
        /// Get All tenants
        /// </summary>
        /// <returns>List of Tenants</returns>
        [Authorize(Roles = Roles.SUPERADMIN)]
        [HttpGet]
        [Route(ApiRoutes.Tenants.GetTenants)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<TenantsListVM>))]
        public async Task<IActionResult> GetTenants()
        {
            var tenantRequests = await _tenantService.GetAllTenants();
            return Ok(tenantRequests);
        }

        /// <summary>
        /// Get Tenant by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>returns Tenant</returns>
        [Authorize(Roles = Roles.SUPERADMIN)]
        [HttpGet]
        [Route(ApiRoutes.Tenants.GetTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<TenantsListVM>))]
        public async Task<IActionResult> GetTenant(long Id)
        {
            var tenant = await _tenantService.GetTenantById(Id);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }

        /// <summary>
        /// Tenant Status change
        /// </summary>
        /// <param name="tenantStatusVM"></param>
        /// <returns></returns>
        [Authorize(Roles = Roles.SUPERADMIN)]
        [HttpPost]
        [Route(ApiRoutes.Tenants.TenantStatus)]
        public async Task<IActionResult> TenantStatus([FromBody] TenantStatusVM tenantStatusVM)
        {

            var validator = new TenantStatusVMValidator();
            var validationResult = await validator.ValidateAsync(tenantStatusVM);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _tenantService.UpdateTenantStatus(tenantStatusVM);
            return Ok();
        }
        /// <summary>
        /// Add Tenant
        /// </summary>
        /// <param name="tenantVM"></param>
        /// <returns></returns>
        [Authorize(Roles = Roles.SUPERADMIN)]
        [HttpPost]
        [Route(ApiRoutes.Tenants.Tenant)]
        public async Task<IActionResult> Tenant([FromBody] TenantVM tenantVM)
        {
            var validator = new TenantVMValidator();
            var validationResult = await validator.ValidateAsync(tenantVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (_tenantService.CheckDuplicateTenantByEmail(tenantVM.Email.ToLower()))
            {
                _logger.LogError($"Duplicate request:{tenantVM.Email}");
                ModelState.AddModelError(nameof(tenantVM.Email), $"Tenant already registered with email: {tenantVM.Email}");

                return BadRequest(ModelState);
            }
            else
            {
                var duplicateCode = true;
                var tenantCode = string.Empty;
                //makesure tenant code is unique
                while (duplicateCode)
                {
                    tenantCode = TenantUtil.GenerateTenantCode(tenantVM.CompanyName);
                    if (!_tenantService.CheckDuplicateTenantByCode(tenantCode))
                    {
                        duplicateCode = false;
                    }
                }
                await _tenantService.AddTenant(tenantVM, tenantCode);
                return Ok();
            }

        }

        [HttpPost]
        [Route(ApiRoutes.Tenants.TenantInternal)]
        public async Task<IActionResult> TenantInternal([FromBody] TenantVM tenantVM)
        {
            var validator = new TenantVMValidator();
            var validationResult = await validator.ValidateAsync(tenantVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (_tenantService.CheckDuplicateTenantByEmail(tenantVM.Email.ToLower()))
            {
                _logger.LogError($"Duplicate request:{tenantVM.Email}");
                ModelState.AddModelError(nameof(tenantVM.Email), $"Tenant already registered with email: {tenantVM.Email}");

                return BadRequest(ModelState);
            }
            else
            {
                var duplicateCode = true;
                var tenantCode = string.Empty;
                //makesure tenant code is unique
                while (duplicateCode)
                {
                    tenantCode = TenantUtil.GenerateTenantCode(tenantVM.CompanyName);
                    if (!_tenantService.CheckDuplicateTenantByCode(tenantCode))
                    {
                        duplicateCode = false;
                    }
                }
                await _tenantService.AddTenant(tenantVM, tenantCode);
                return Ok(true);
            }

        }

    }
}
