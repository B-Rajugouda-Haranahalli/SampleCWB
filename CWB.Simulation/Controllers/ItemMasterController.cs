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
    public class ItemMasterController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IItemMasterServices _itemMasterServices;

        public ItemMasterController(ILoggerManager logger, IItemMasterServices itemMasterServices)
        {
            _logger = logger;
            _itemMasterServices = itemMasterServices;
        }

        /// <summary>
        /// Get Item Master By Tenant
        /// </summary>
        /// <param name="tenantID">Tenant Id of type long</param>
        /// <returns>List of Item Master view model</returns>
        [HttpGet]
        [Route(ApiRoutes.ItemMaster.ItemMasterByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ItemMasterVM))]
        public IActionResult GetItemMasterByTenant(long tenantID)
        {
            var itemMasters = _itemMasterServices.GetItemMasterByTenant(tenantID);

            if (itemMasters == null)
            {
                return NotFound();
            }

            return Ok(itemMasters);
        }

        /// <summary>
        /// Add Item Master
        /// </summary>
        /// <param name="model">Item Master view model</param>
        /// <returns>Ok on Success</returns>
        [HttpPost]
        [Route(ApiRoutes.ItemMaster.AddItemMaster)]
        public async Task<IActionResult> AddItemMaster([FromBody] ItemMasterVM model)
        {
            var validator = new ItemMasterVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _itemMasterServices.AddItemMaster(model);
            return Ok();
        }

        /// <summary>
        /// Update Item Master
        /// </summary>
        /// <param name="model">Item Master view model</param>
        /// <returns>Ok on Success</returns>
        [HttpPost]
        [Route(ApiRoutes.ItemMaster.UpdateItemMaster)]
        public async Task<IActionResult> UpdateItemMaster([FromBody] ItemMasterVM model)
        {
            var validator = new ItemMasterVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.ItemMasterId == 0)
            {
                ModelState.AddModelError(nameof(model.ItemMasterId), $"Id Cannot be null for ItemMaster: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _itemMasterServices.UpdateItemMaster(model.ItemMasterId, model);
            return Ok();
        }
    }
}
