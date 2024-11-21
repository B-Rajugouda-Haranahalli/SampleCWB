using CWB.CommonUtils.Common;
using CWB.Logging;
using CWB.Simulation.Services;
using CWB.Simulation.SimulationUtils;
using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CWB.Simulation.Controllers
{
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IPlantServices _plantServices;

        public PlantController(ILoggerManager logger, IPlantServices plantServices)
        {
            _logger = logger;
            _plantServices = plantServices;
        }

        /// <summary>
        /// Get Plants
        /// </summary>
        /// <param name="tenantID">Tenant Id of type long</param>
        /// <returns>List of Plant view model</returns>
        [HttpGet]
        [Route(ApiRoutes.Plant.PlantByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(PlantVM))]
        public IActionResult GetPlants(long tenantID)
        {
            var plants = _plantServices.GetPlants(tenantID);

            if (plants == null)
            {
                return NotFound();
            }

            return Ok(plants);
        }

        /// <summary>
        /// Add Plant
        /// </summary>
        /// <param name="model">Plant view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Plant.AddPlant)]
        public async Task<IActionResult> AddPlant([FromBody] PlantVM model)
        {
            var validator = new PlantVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _plantServices.AddPlant(model);
            return Ok();
        }

        /// <summary>
        /// Update Plant
        /// </summary>
        /// <param name="model">Plant view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Plant.UpdatePlant)]
        public async Task<IActionResult> UpdatePlant([FromBody] PlantVM model)
        {
            var validator = new PlantVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.PlantId == 0)
            {
                ModelState.AddModelError(nameof(model.PlantId), $"Id Cannot be null for Plant: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _plantServices.UpdatePlant(model.PlantId, model);
            return Ok();
        }
    }
}
