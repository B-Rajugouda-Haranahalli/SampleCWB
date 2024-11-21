using CWB.CommonUtils.Common;
using CWB.Logging;
using CWB.Modules.ModulesUtils;
using CWB.Modules.Services;
using CWB.Modules.ViewModels;
using CWB.Modules.ViewModelValidators;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Modules.Controllers
{
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IModuleServices _moduleServices;

        public ModuleController(ILoggerManager logger, IModuleServices moduleServices)
        {
            _logger = logger;
            _moduleServices = moduleServices;
        }

        /// <summary>
        /// Get All Modules
        /// </summary>
        /// <returns>List of Modules</returns>
        [HttpGet]
        [Route(ApiRoutes.ModuleType.ModuleTypes)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ModuleTypesVM>))]
        public async Task<IActionResult> GetModules()
        {
            var modules = await _moduleServices.GetAllModuleWithTypes(true);
            return Ok(modules);
        }

        /// <summary>
        /// Get all modules by tenant
        /// </summary>
        /// <param name="tenantID">Tenant ID of type long</param>
        /// <returns>List of Modules</returns>
        [HttpGet]
        [Route(ApiRoutes.Module.ModulesByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ModulesVM>))]
        public async Task<IActionResult> GetModulesByTenant(long tenantID)
        {
            var modules = await _moduleServices.GetAllModulesWithTypesByTenant(tenantID);
            return Ok(modules);
        }

        /// <summary>
        /// Get module 
        /// </summary>
        /// <param name="moduleID">module id of type long</param>
        /// <returns>module view model</returns>
        [HttpGet]
        [Route(ApiRoutes.Module.GetModule)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ModulesVM))]
        public async Task<IActionResult> GetModule(long moduleID)
        {
            var module = await _moduleServices.GetModule(moduleID);
            if (module == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(module);
            }
        }

        /// <summary>
        /// Add Module
        /// </summary>
        /// <param name="model">module view model</param>
        /// <returns>Ok result on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Module.AddModule)]
        public async Task<IActionResult> AddModule([FromBody] ModulesVM model)
        {
            var validator = new ModulesVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (_moduleServices.CheckDuplicateModuleByName(model.Name) || _moduleServices.CheckDuplicateModuleByKey(model.ModuleKey))
            {
                _logger.LogError($"Duplicate request:{model.Name}");
                ModelState.AddModelError(nameof(model.Name), $"Module already created with name: {model.Name}");

                return BadRequest(ModelState);
            }
            else if (_moduleServices.CheckDuplicateModuleByKey(model.ModuleKey))
            {
                _logger.LogError($"Duplicate request:{model.ModuleKey}");
                ModelState.AddModelError(nameof(model.ModuleKey), $"Module already created with key: {model.ModuleKey}");

                return BadRequest(ModelState);
            }
            else
            {
                await _moduleServices.AddModule(model);
                return Ok();
            }
        }

        /// <summary>
        /// Update Module
        /// </summary>
        /// <param name="model">module view model</param>
        /// <returns>Ok result on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Module.UpdateModule)]
        public async Task<IActionResult> UpdateModule([FromBody] ModulesVM model)
        {
            var validator = new ModulesVMValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (model.ModuleId == 0)
            {
                ModelState.AddModelError(nameof(model.ModuleId), $"Id Cannot be null for the module: {model.Name}");
                return BadRequest(ModelState);
            }

            await _moduleServices.UpdateModule(model.ModuleId, model);
            return Ok();
        }

        /// <summary>
        /// Enable/Disable Module
        /// </summary>
        /// <param name="model">Module Tenant Config view model</param>
        /// <returns>Ok on success</returns>
        [HttpPost]
        [Route(ApiRoutes.Module.EnableorDisableModule)]
        public IActionResult EnableModule([FromBody] ModuleTenantConfigVM model)
        {
            _moduleServices.EnableorDisableModule(model); 
            return Ok();
        }

    }
}
