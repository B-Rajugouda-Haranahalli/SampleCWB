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
    public class ShopDepartmentController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IShopDepartmentServices _shopDepartmentServices;
        public ShopDepartmentController(ILoggerManager logger, IShopDepartmentServices shopDepartmentServices)
        {
            _logger = logger;
            _shopDepartmentServices = shopDepartmentServices;
        }

        /// <summary>
        /// Get Shop Department By Tenant
        /// </summary>
        /// <param name="tenantID">Tenant Id of type long</param>
        /// <returns>List of Shop Department view model</returns>
        [HttpGet]
        [Route(ApiRoutes.ShopDepartment.ShopDepartmentByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ShopDepartmentVM))]
        public IActionResult GetPlants(long tenantID)
        {
            var shopDepartments = _shopDepartmentServices.GetShopDepartmentByTenant(tenantID);

            if (shopDepartments == null)
            {
                return NotFound();
            }

            return Ok(shopDepartments);
        }

        /// <summary>
        /// Get Shop Department by plant
        /// </summary>
        /// <param name="tenantID">Tenant Id of type long</param>
        /// <param name="plantID">Plant Id of type long</param>
        /// <returns>List of Shop Department view model</returns>
        [HttpGet]
        [Route(ApiRoutes.ShopDepartment.ShopDepartmentByPlant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ShopDepartmentVM))]
        public IActionResult GetShopDepartmentByPlant(long tenantID, long plantID)
        {
            var shopDepartments = _shopDepartmentServices.GetShopDepartmentByPlant(tenantID, plantID);

            if (shopDepartments == null)
            {
                return NotFound();
            }

            return Ok(shopDepartments);
        }

        /// <summary>
        /// Add Shop Department
        /// </summary>
        /// <param name="model">Shop Department view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.ShopDepartment.AddShopDepartment)]
        public async Task<IActionResult> AddShopDepartment([FromBody] ShopDepartmentVM model)
        {
            var validator = new ShopDepartmentVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _shopDepartmentServices.AddShopDepartment(model);
            return Ok();
        }

        /// <summary>
        /// Update Shop Department
        /// </summary>
        /// <param name="model">Shop Department view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.ShopDepartment.UpdateShopDepartment)]
        public async Task<IActionResult> UpdateShopDepartment([FromBody] ShopDepartmentVM model)
        {
            var validator = new ShopDepartmentVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.ShopDepartmentId == 0)
            {
                ModelState.AddModelError(nameof(model.ShopDepartmentId), $"Id Cannot be null for Shop Department: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _shopDepartmentServices.UpdateShopDepartment(model.ShopDepartmentId, model);
            return Ok();
        }
    }
}
