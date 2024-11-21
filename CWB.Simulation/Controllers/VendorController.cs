using CWB.CommonUtils.Common;
using CWB.Logging;
using CWB.Simulation.Services;
using CWB.Simulation.SimulationUtils;
using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CWB.Simulation.Controllers
{
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IVendorServices _vendorServices;

        public VendorController(ILoggerManager logger, IVendorServices vendorServices)
        {
            _logger = logger;
            _vendorServices = vendorServices;
        }

        /// <summary>
        /// Get all vendors
        /// </summary>
        /// <param name="tenantID">Tenant Id of type long</param>
        /// <returns>List of Vendor view model</returns>
        [HttpGet]
        [Route(ApiRoutes.Vendor.VendorsByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(VendorVM))]
        public IActionResult GetVendors(long tenantID)
        {
            var vendors = _vendorServices.GetVendor(tenantID);

            if (vendors == null)
            {
                return NotFound();
            }

            return Ok(vendors);
        }

        /// <summary>
        /// Get all vendors by type
        /// </summary>
        /// <param name="type">vendor type of type string</param>
        /// <returns>List of Vendor view model</returns>
        [HttpGet]
        [Route(ApiRoutes.Vendor.VendorsByType)]
        [Produces(AppContentTypes.ContentType, Type = typeof(VendorVM))]
        public IActionResult GetVendorsByType(string type)
        {
            var vendors = _vendorServices.GetVendorByType(type);

            if (vendors == null)
            {
                return NotFound();
            }

            return Ok(vendors);
        }

        /// <summary>
        /// Add Vendor
        /// </summary>
        /// <param name="model">Vendor view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Vendor.AddVendor)]
        public async Task<IActionResult> AddVendor([FromBody] VendorVM model)
        {
            var validator = new VendorVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _vendorServices.AddVendor(model);
            return Ok();
        }

        /// <summary>
        /// Update Vendor
        /// </summary>
        /// <param name="model">Vendor view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Vendor.UpdateVendor)]
        public async Task<IActionResult> UpdateVendor([FromBody] VendorVM model)
        {
            var validator = new VendorVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.VendorId == 0)
            {
                ModelState.AddModelError(nameof(model.VendorId), $"Id Cannot be null for Vendor: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _vendorServices.UpdateVendor(model.VendorId, model);
            return Ok();
        }
    }
}
