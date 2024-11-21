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
    public class MRBomController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMRBomServices _mRBomServices;

        public MRBomController(ILoggerManager logger, IMRBomServices mRBomServices)
        {
            _logger = logger;
            _mRBomServices = mRBomServices;
        }

        /// <summary>
        /// Get Manufacturing Resources BOM (MRBOM) by Tenant
        /// </summary>
        /// <param name="tenantID">Tenant ID of type long</param>
        /// <returns>List of MRBom view model</returns>
        [HttpGet]
        [Route(ApiRoutes.MRBom.MRBomByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MRBomVM))]
        public IActionResult GetMRBomsByTenant(long tenantID)
        {
            var mRBoms = _mRBomServices.GetMRBomsByTenant(tenantID);

            if (mRBoms == null)
            {
                return NotFound();
            }

            return Ok(mRBoms);
        }

        /// <summary>
        /// Get Manufacturing Resources BOM (MRBOM) By Id
        /// </summary>
        /// <param name="Id">Id of type long</param>
        /// <returns>MRBom view model</returns>
        [HttpGet]
        [Route(ApiRoutes.MRBom.MRBomById)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MRBomVM))]
        public IActionResult GetMRBomsById(long Id)
        {
            var mRBoms = _mRBomServices.GetMRBomsById(Id);

            if (mRBoms == null)
            {
                return NotFound();
            }

            return Ok(mRBoms);
        }

        /// <summary>
        /// Add MR Bom
        /// </summary>
        /// <param name="model">MRBom view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.MRBom.AddMRBom)]
        public async Task<IActionResult> AddMRBom([FromBody] MRBomVM model)
        {
            var validator = new MRBomVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _mRBomServices.AddMRBom(model);
            return Ok();
        }

        /// <summary>
        /// Update MR Bom
        /// </summary>
        /// <param name="model">MRBom view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.MRBom.UpdateMRBom)]
        public async Task<IActionResult> UpdateMRBom([FromBody] MRBomVM model)
        {
            var validator = new MRBomVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.MRBomId == 0)
            {
                ModelState.AddModelError(nameof(model.MRBomId), $"Id Cannot be null for MRBom: {model.TenantId}");
                return BadRequest(ModelState);
            }

            await _mRBomServices.UpdateMRBom(model.MRBomId, model);
            return Ok();
        }
    }
}
