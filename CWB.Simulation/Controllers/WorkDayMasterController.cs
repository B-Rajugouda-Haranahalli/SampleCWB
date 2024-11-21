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
    public class WorkDayMasterController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IWorkDayMasterServices _workDayMasterServices;

        public WorkDayMasterController(ILoggerManager logger, IWorkDayMasterServices workDayMasterServices)
        {
            _logger = logger;
            _workDayMasterServices = workDayMasterServices;
        }

        /// <summary>
        /// Get Work Day Master
        /// </summary>
        /// <param name="tenantID">Tenant ID of type long</param>
        /// <returns>Work Day Master VM</returns>
        [HttpGet]
        [Route(ApiRoutes.WorkDayMaster.WorkDayMasterByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(WorkDayMasterVM))]
        public async Task<IActionResult> GetWorkDayMaster(long tenantID)
        {
            var workDayMaster = await _workDayMasterServices.GetWorkDayMaster(tenantID);

            if (workDayMaster == null)
            {
                return NotFound();
            }

            return Ok(workDayMaster);
        }

        /// <summary>
        /// Add Work Day Master
        /// </summary>
        /// <param name="model">Work Day Master view model</param>
        /// <returns>Ok result on success</returns>
        [HttpPost]
        [Route(ApiRoutes.WorkDayMaster.AddWorkDayMaster)]
        public async Task<IActionResult> AddWorkDayMaster([FromBody] WorkDayMasterVM model)
        {
            var validator = new WorkDayMasterVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _workDayMasterServices.AddWorkDayMaster(model);
            return Ok();
        }

        /// <summary>
        /// Update Work Day Master
        /// </summary>
        /// <param name="model">Work Day Master view model</param>
        /// <returns>Ok result on success</returns>
        [HttpPost]
        [Route(ApiRoutes.WorkDayMaster.UpdateWorkDayMaster)]
        public async Task<IActionResult> UpdateWorkDayMaster([FromBody] WorkDayMasterVM model)
        {
            var validator = new WorkDayMasterVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.WorkDayMasterId == 0)
            {
                ModelState.AddModelError(nameof(model.WorkDayMasterId), $"Id Cannot be null for Work Day Master: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _workDayMasterServices.UpdateWorkDayMaster(model.WorkDayMasterId, model);
            return Ok();
        }
    }
}
