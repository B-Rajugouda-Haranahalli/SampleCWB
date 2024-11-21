using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.ProductionPlanWO.Services;
using CWB.ProductionPlanWO.Utils;
using CWB.ProductionPlanWO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Controllers
{
    [ApiController]
    [Authorize]
    public class WOController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IWOService _woSerivce;
        public WOController(ILoggerManager logger, IWOService woSerivce)
        {
            _logger = logger;
            _woSerivce = woSerivce;
        }
        [HttpGet]
        [Route(ApiRoutes.WO.HelloWorld)]
        [Produces(AppContentTypes.ContentType, Type = typeof(string))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<string> HelloWorld()
        {
            var pologs = _woSerivce.HelloWorld();
            return pologs;
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostWorkOrder)]
        [Produces(AppContentTypes.ContentType,Type=typeof(WorkOrdersVM))]
        public async Task<IActionResult> PostWorkOrder([FromBody]WorkOrdersVM workOrdersVM)
        {
            var result = await _woSerivce.WorkOrder(workOrdersVM);
            return Ok(result);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostMultipleWorkOrder)]
        [Produces(AppContentTypes.ContentType, Type = typeof(WorkOrdersVM))]
        public async Task<IActionResult> PostMultipleWorkOrder([FromBody] List<WorkOrdersVM> workOrdersVM)
        {
            var result = await _woSerivce.MultipleWorkOrder(workOrdersVM);
            return Ok(result);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostUpdateMultipleWorkOrder)]
        [Produces(AppContentTypes.ContentType, Type = typeof(WorkOrdersVM))]
        public async Task<IActionResult> PostUpdateMultipleWorkOrder([FromBody] List<WorkOrdersVM> workOrdersVM)
        {
            var result = await _woSerivce.UpdateMultipleWorkOrder(workOrdersVM);
            return Ok(result);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostWOSORel)]
        [Produces(AppContentTypes.ContentType, Type = typeof(WOSOVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostWOSORel([FromBody] List<WOSOVM> wOSOVMs)
        {
            var woso = await _woSerivce.PostWOSO(wOSOVMs);
            return Ok(woso);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostBOMTemp)]
        [Produces(AppContentTypes.ContentType, Type = typeof(WOSOVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> BOMPost([FromBody] List<BOMTempVM> bomtemp)
        {
            var woso = await _woSerivce.BOMTempPost(bomtemp);
            return Ok(woso);
        }


        [HttpGet]
        [Route(ApiRoutes.WO.AllWorkOrders)]
        [Produces(AppContentTypes.ContentType,Type=typeof(List<WorkOrdersVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> AllWorkOrders(long tenantId)
        {
            var allwo = await _woSerivce.AllWorkOrders(tenantId);
            return Ok(allwo);
        }

        [HttpGet]
        [Route(ApiRoutes.WO.AllParentChildWos)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<WorkOrdersVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> AllParentWo(long parentWoId, long tenantId)
        {
            var allwo = await _woSerivce.AllParentChildWo(parentWoId, tenantId);
            return Ok(allwo);
        }

        [HttpGet]
        [Route(ApiRoutes.WO.GetSingleWorkOrder)]
        [Produces(AppContentTypes.ContentType,Type=typeof(WorkOrdersVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetSingleWO(long Id,long tenantId)
        {
            WorkOrdersVM singleWO = await _woSerivce.GetSingleWO(Id, tenantId);
            return Ok(singleWO);
        }

        [HttpGet]
        [Route(ApiRoutes.WO.GetSoWo)]
        [Produces(AppContentTypes.ContentType,Type =typeof(List<WOSOVM>))]
        [Authorize(Roles=Roles.ADMIN)]
        public async Task<IActionResult> GetSoWo(long workOrderId)
        {
            var so = await _woSerivce.GetSoWo(workOrderId);
            return Ok(so);
        }


        [HttpPost]
        [Route(ApiRoutes.WO.PostProcPlan)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ProcPlanVM))] //changed
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostProcPlan([FromBody] List<ProcPlanVM> procplan)
        {
            var proc = await _woSerivce.PostProcPlan(procplan);
            return Ok(proc);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostBomList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(BOMListVM))]   //changed
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostBomList([FromBody] List<BOMListVM> bOMListVMs)
        {
            var proc = await _woSerivce.PostBomList(bOMListVMs);
            return Ok(proc);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostProductionPlan_Wo)]
        [Produces(AppContentTypes.ContentType, Type =typeof(ProductionPlan_WOVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostProductionPlan_Wo([FromBody] List<ProductionPlan_WOVM> productions)
        {
            var productionPlan = await _woSerivce.PostProductionPlan_Wo(productions);
            return Ok(productionPlan);
        }

        [HttpGet]
        [Route(ApiRoutes.WO.AllProductionPlanWo)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ProductionPlan_WOVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> AllProductionWos(long tenantId)
        {
            var allwo = await _woSerivce.AllProductionWo(tenantId);
            return Ok(allwo);
        }

        [HttpGet]
        [Route(ApiRoutes.WO.AllProcPlan)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<ProcPlanVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetAllProcPlan(long tenantId)
        {
            var allprocplan = await _woSerivce.AllProcPlan(tenantId);
            return Ok(allprocplan);
        }

        [HttpGet]
        [Route(ApiRoutes.WO.AllBomList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<BOMListVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetAllBomList(long tenantId)
        {
            var allprocplan = await _woSerivce.AllBomList(tenantId);
            return Ok(allprocplan);
        }

        [HttpGet]
        [Route(ApiRoutes.WO.GetWoStatus)]
        [Produces(AppContentTypes.ContentType, Type = typeof(WOStatusVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetWoStatus(long Id)
        {
            var allprocplan = await _woSerivce.GetWOStatus(Id);
            return Ok(allprocplan);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostChildWoRel)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ChildWoRelVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostChildWoRel([FromBody] List<ChildWoRelVM> childWoRels)
        {
            var woso = await _woSerivce.PostChildWoRel(childWoRels);
            return Ok(woso);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostMcTimeList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<McTimeListVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> PostMcTimeList([FromBody] List<McTimeListVM> mcTimeListVMs)
        {
            var mctime = await _woSerivce.PostMcTimeList(mcTimeListVMs);
            return Ok(mctime);
        }
        [HttpGet]
        [Route(ApiRoutes.WO.GetAllMctimeList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<McTimeListVM>))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetAllMcTimeList(long tenantId)
        {
            var mctime = await _woSerivce.GetAllMcTimeListVMs(tenantId);
            return Ok(mctime);
        }


        [HttpPost]
        [Route(ApiRoutes.WO.PostMultiplePODetails)]
        [Produces(AppContentTypes.ContentType, Type = typeof(PODetailsVM))]
        public async Task<IActionResult> PostMultiplePODetails([FromBody] List<PODetailsVM> pODetailsVMs)
        {
            var result = await _woSerivce.MultiplePODetails(pODetailsVMs);
            return Ok(result);
        }

        [HttpPost]
        [Route(ApiRoutes.WO.PostMultiplePOHeaders)]
        [Produces(AppContentTypes.ContentType, Type = typeof(POHeaderVM))]
        public async Task<IActionResult> PostMultiplePOHeaders([FromBody] List<POHeaderVM> poHeaders)
        {
            var result = await _woSerivce.MultiplePOHeaders(poHeaders);
            return Ok(result);
        }


        [HttpGet]
        [Route(ApiRoutes.WO.GetPoStatus)]
        [Produces(AppContentTypes.ContentType, Type = typeof(POStatusVM))]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> GetPoStatus(long Id)
        {
            var pOStatus = await _woSerivce.GetPOStatus(Id);
            return Ok(pOStatus);
        }
    }
}
