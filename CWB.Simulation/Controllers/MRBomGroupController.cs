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
    public class MRBomGroupController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMRBomGroupServices _mRBomGroupServices;

        public MRBomGroupController(ILoggerManager logger, IMRBomGroupServices mRBomGroupServices)
        {
            _logger = logger;
            _mRBomGroupServices = mRBomGroupServices;
        }

        /// <summary>
        /// Get Manufacturing Resources BOM group (MRBomGroup)
        /// </summary>
        /// <param name="tenantID">Tenant Id of type long</param>
        /// <returns>List of MRBomGroup view model</returns>
        [HttpGet]
        [Route(ApiRoutes.MRBomGroup.MRBomGroupByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MRBomGroupVM))]
        public IActionResult GetMRBomGroups(long tenantID)
        {
            var mRBomGroups = _mRBomGroupServices.GetMRBomGroupsByTenant(tenantID);

            if (mRBomGroups == null)
            {
                return NotFound();
            }

            return Ok(mRBomGroups);
        }

        /// <summary>
        /// Add MR Bom group 
        /// </summary>
        /// <param name="model">MRBom Group view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.MRBomGroup.AddMRBomGroup)]
        public async Task<IActionResult> AddMRBomGroup([FromBody] MRBomGroupVM model)
        {
            var validator = new MRBomGroupVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _mRBomGroupServices.AddMRBomGroup(model);
            return Ok();
        }

        /// <summary>
        /// Update MR Bom gorup
        /// </summary>
        /// <param name="model">MRBom Group view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.MRBomGroup.UpdateMRBomGroup)]
        public async Task<IActionResult> UpdateMRBomGroup([FromBody] MRBomGroupVM model)
        {
            var validator = new MRBomGroupVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.MRBomGroupId == 0)
            {
                ModelState.AddModelError(nameof(model.MRBomGroupId), $"Id Cannot be null for MRBomGroup: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _mRBomGroupServices.UpdateMRBomGroup(model.MRBomGroupId, model);
            return Ok();
        }
    }
}
