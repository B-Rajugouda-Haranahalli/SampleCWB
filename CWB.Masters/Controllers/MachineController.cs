using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.Masters.MastersUtils;
using CWB.Masters.Services.Machines;
using CWB.Masters.ViewModels.Machines;
using CWB.Masters.ViewModelValidators.Machines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Controllers
{

    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class MachineController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMachineService _machineService;

        public MachineController(ILoggerManager logger, IMachineService machineService)
        {
            _logger = logger;
            _machineService = machineService;
        }

        /// <summary>
        /// Get Machine Type by tenant id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Machine.GetMachineTypes)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<MachineTypeVM>))]
        public IActionResult GetMachineTypes(long Id)
        {
            var machineTypes = _machineService.GetMachineTypes(Id);
            return Ok(machineTypes);
        }

        /// <summary>
        /// Add or Edit Machine Type
        /// </summary>
        /// <param name="machineTypeVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Machine.PostMachineType)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MachineTypeVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostMachineType([FromBody] MachineTypeVM machineTypeVM)
        {
            var validator = new MachineTypeVMValidator();
            var validationResult = await validator.ValidateAsync(machineTypeVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            //check if duplicate
            var machineTypeExist = _machineService.CheckMachineType(machineTypeVM);
            if (machineTypeExist)
            {
                ModelState.AddModelError("Name", $"Machine Type: {machineTypeVM.MachineTypeName} Already Exist");
                return BadRequest(ModelState);
            }
            var result = await _machineService.MachineType(machineTypeVM);
            return Ok(result);
        }

        /// <summary>
        /// Check if Machine Type exist
        /// </summary>
        /// <param name="machineTypeVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Machine.IsMachineTypeExist)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> CheckMachineType([FromBody] MachineTypeVM machineTypeVM)
        {
            var validator = new MachineTypeVMValidator();
            var validationResult = await validator.ValidateAsync(machineTypeVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            //check if duplicate
            var machineTypeExist = _machineService.CheckMachineType(machineTypeVM);
            return Ok(machineTypeExist);
        }

        /// <summary>
        /// Check if Machine Exist
        /// </summary>
        /// <param name="checkMachineVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Machine.IsMachineExist)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> CheckMachine([FromBody] CheckMachineVM checkMachineVM)
        {
            var validator = new CheckMachineVMValidator();
            var validationResult = await validator.ValidateAsync(checkMachineVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            //check if duplicate
            if (checkMachineVM.CompareKey == "Name")
            {
                var result = _machineService.CheckMachineByName(checkMachineVM.CompareValue, checkMachineVM.MachineId, checkMachineVM.TenantId);
                return Ok(result);
            }
            else
            {
                var result = _machineService.CheckMachineBySlNo(checkMachineVM.CompareValue, checkMachineVM.MachineId, checkMachineVM.TenantId);
                return Ok(result);
            }

        }


        /// <summary>
        /// Add Edit Machine
        /// </summary>
        /// <param name="machineVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Machine.PostMachine)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MachineVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostMachine([FromBody] MachineVM machineVM)
        {
            var validator = new MachineVMValidator();
            var validationResult = await validator.ValidateAsync(machineVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            //check if duplicate
            var machineExist = _machineService.CheckMachine(machineVM.MachineMachineName, machineVM.MachineMachineSlNo,
                machineVM.MachineMachineId, machineVM.TenantId);
            if (machineExist)
            {
                ModelState.AddModelError("MachineMachineName", $"Machine Already Exist");
                return BadRequest(ModelState);
            }
            var result = await _machineService.Machine(machineVM);
            return Ok(result);
        }

        /// <summary>
        /// Get machine details by machine 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Machine.GetMachine)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MachineVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult GetMachine(long Id, long tenantId)
        {
            var machine = _machineService.GetMachine(Id, tenantId);
            if (machine == null)
            {
                return NotFound();
            }
            return Ok(machine);

        }
        /// <summary>
        /// Get Machine list by tenant Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Machine.GetMachines)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<MachineListVM>))]
        public IActionResult GetMachines(long Id)
        {
            var machines = _machineService.GetMachines(Id);
            return Ok(machines);
        }

        /// <summary>
        /// Get Machine Process Documents by Machine Id and tenant Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Machine.GetMachineProcDocs)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<MachineProcDocumentListVM>))]
        public IActionResult GetMachineProcDocs(long Id, long tenantId)
        {
            var machineProcDocs = _machineService.GetMachineProcDocuments(Id, tenantId);
            return Ok(machineProcDocs);
        }

        [HttpPost]
        [Route(ApiRoutes.Machine.PostMachineProcDoc)]
        [Produces(AppContentTypes.ContentType, Type = typeof(MachineProcDocumentVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostMachineProcDoc([FromBody] MachineProcDocumentVM machineProcDocumentVM)
        {
            var validator = new MachineProcDocumentVMValidator();
            var validationResult = await validator.ValidateAsync(machineProcDocumentVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            //check if duplicate
            var machineProDocExist = _machineService.CheckMachineProcDoc(machineProcDocumentVM.MachineProcDocumentMachineId, machineProcDocumentVM.MachineProcDocumentId,
                machineProcDocumentVM.TenantId, machineProcDocumentVM.MachineProcDocumentTypeId);
            if (machineProDocExist)
            {
                ModelState.AddModelError("MachineProcDocumentTypeId", $"Machine Process Document Already Exist");
                return BadRequest(ModelState);
            }
            var result = await _machineService.MachineProcDoc(machineProcDocumentVM);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.Machine.GetMcTypeDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<McTypeDocListVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult GetMcTypeDocList(long tenantId)
        {
            var machineProcDocs = _machineService.GetMcTypeDocList(tenantId);
            return Ok(machineProcDocs);
        }

        [HttpPost]
        [Route(ApiRoutes.Machine.PostMcTypeDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(McTypeDocListVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostMcTypeDocList([FromBody] McTypeDocListVM mcTypeDocListVM)
        {
            var machineTypeExist = await _machineService.PostMcTypeDocList(mcTypeDocListVM);
            return Ok(machineTypeExist);
        }

        [HttpGet]
        [Route(ApiRoutes.Machine.DeleteMcTypDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> DeleteMcTypDocList(long mcTypeDocListId, long tenantId)
        {
            var result = await _machineService.DeleteMcTypDocList(mcTypeDocListId, tenantId);
            return Ok(result);
        }


        [HttpGet]
        [Route(ApiRoutes.Machine.GetMcSlNoDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<McSlNoDocListVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult GetMcSlNoDocList(long tenantId)
        {
            var machineProcDocs = _machineService.GetMcSlNoDocList(tenantId);
            return Ok(machineProcDocs);
        }

        [HttpPost]
        [Route(ApiRoutes.Machine.PostMcSlNoDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(McSlNoDocListVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostMcSlNoDocList([FromBody] McSlNoDocListVM mcTypeDocListVM)
        {
            var machineTypeExist = await _machineService.PostMcSlNoDocList(mcTypeDocListVM);
            return Ok(machineTypeExist);
        }

        [HttpGet]
        [Route(ApiRoutes.Machine.DeleteMcSlNoDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> DeleteMcSlNoDocList(long mcSlNoDocListId, long tenantId)
        {
            var result = await _machineService.DeleteMcSlNoDocList(mcSlNoDocListId, tenantId);
            return Ok(result);
        }
    }
}
