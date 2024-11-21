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
    public class MachineTypeController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMachineTypeServices _machineTypeServices;

        public MachineTypeController(ILoggerManager logger, IMachineTypeServices machineTypeServices)
        {
            _logger = logger;
            _machineTypeServices = machineTypeServices; 
        }

        /// <summary>
        /// Get Machine Types
        /// </summary>
        /// <param name="tenantID">Tenant Id of type long</param>
        /// <returns>List of Machine Type view model</returns>
        [HttpGet]
        [Route(ApiRoutes.MachineType.MachineTypeByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MachineTypeVM))]
        public IActionResult GetMachineTypes(long tenantID)
        {
            var machineTypes = _machineTypeServices.GetMachineTypes(tenantID);

            if (machineTypes == null)
            {
                return NotFound();
            }

            return Ok(machineTypes);
        }

        /// <summary>
        /// Add Machine Type
        /// </summary>
        /// <param name="model">Machine Type view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.MachineType.AddMachineType)]
        public async Task<IActionResult> AddMachineType([FromBody] MachineTypeVM model)
        {
            var validator = new MachineTypeVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _machineTypeServices.AddMachineType(model);
            return Ok();
        }

        /// <summary>
        /// Update Machine Type
        /// </summary>
        /// <param name="model">Machine Type view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.MachineType.UpdateMachineType)]
        public async Task<IActionResult> UpdateMachineType([FromBody] MachineTypeVM model)
        {
            var validator = new MachineTypeVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.MachineTypeId == 0)
            {
                ModelState.AddModelError(nameof(model.MachineTypeId), $"Id Cannot be null for Machine Type: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _machineTypeServices.UpdateMachineType(model.MachineTypeId, model);
            return Ok();
        }
    }
}
