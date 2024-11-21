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
    public class MachineController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMachineServices _machineServices;
        public MachineController(ILoggerManager logger, IMachineServices machineServices)
        {
            _logger = logger;
            _machineServices = machineServices;    
        }

        /// <summary>
        /// Get Machines
        /// </summary>
        /// <param name="tenantID">Tenant Id of type lon</param>
        /// <returns>List of Machine view model</returns>
        [HttpGet]
        [Route(ApiRoutes.Machine.MachineByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MachineVM))]
        public IActionResult GetMachines(long tenantID)
        {
            var machines = _machineServices.GetMachines(tenantID);

            if (machines == null)
            {
                return NotFound();
            }

            return Ok(machines);
        }

        /// <summary>
        /// Add Machine
        /// </summary>
        /// <param name="model">Machine view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Machine.AddMachine)]
        public async Task<IActionResult> AddMachine([FromBody] MachineVM model)
        {
            var validator = new MachineVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _machineServices.AddMachine(model);
            return Ok();
        }

        /// <summary>
        /// Update Machine
        /// </summary>
        /// <param name="model">Machine view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Machine.UpdateMachine)]
        public async Task<IActionResult> UpdateMachine([FromBody] MachineVM model)
        {
            var validator = new MachineVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.MachineId == 0)
            {
                ModelState.AddModelError(nameof(model.MachineId), $"Id Cannot be null for Machine: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _machineServices.UpdateMachine(model.MachineId, model);
            return Ok();
        }
    }
}
