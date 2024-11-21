using CWB.App.Models.BusinessProcesses;
using CWB.App.Models.ItemMaster;
using CWB.App.Models.Routing;
using CWB.App.Services.BusinessProcesses;
using CWB.App.Services.Masters;
using CWB.App.Services.Routings;
using CWB.Constants.UserIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{

    /**
     *    public const string GetPOLogs = Base + "/getpologs/{tenantId}";
            public const string GetSalesOrders = Base + "/getsalesorders/{tenantId}/{customerOderId}";
            public const string GetCustomerOrders = Base + "/getcustomerorders/{tenantId}";
            public const string GetSchedules = Base + "/getdeliveryschedules/{tenantId}/{customerOrderId}";
            public const string HelloWorld = Base + "/helloworld/{tenantId}";

            public const string PostSalesOrder = Base + "/salesorder";
            public const string PostCustomerOrder = Base + "/custorder";
            public const string PostDeliverySchedule = Base + "/deliveryschedule";
            public const string PostOrderStatus = Base + "/orderstatus";
            public const string PostPOLog = Base + "/polog";

            public const string RemoveSalesOrder = Base + "/removesalesorder/{tenantId}/{salesOrderId}";
            public const string RemoveCustomerOder = Base + "/removecustomerorder/{tenantId}/{customerOrderId}";
            public const string RemoveDeliverSchedule = Base + "/removedeliveryschedule/{tenantId}/{scheduleId}";
            public const string RemoveOrderStatus = Base + "/removeorderstatus/{tenantId}/{orderStatusId}";
     */

    [Authorize(Roles = Roles.ADMIN)]
    public class BusinessAquisitionController : Controller
    {
        private readonly ILogger<BusinessAquisitionController> _logger;
        private readonly IBAService _baService;
        private readonly IMastersServices _masterService;
        private readonly IRoutingService _routingService;
        public BusinessAquisitionController(ILogger<BusinessAquisitionController> logger, IBAService baService,IMastersServices masterServices, IRoutingService routingService)
        {
            _logger = logger;
            _baService = baService;
            _masterService = masterServices;
            _routingService = routingService;
        }
        public IActionResult Index()
        {
            _logger.LogTrace("BA--Index--Loading");
            return View();
        }

        //[HttpGet]
        //public IActionResult WO()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> WOpost(WorkOrdersVM workOrdersVM)
        {
            ManufacturedPartNoDetailVM manuf = await _masterService.GetManufPart((int)workOrdersVM.PartId);
            workOrdersVM.PartType = (int)manuf.ManufacturedPartType;
            RoutingVM rout = new RoutingVM();
            var resultList = await _routingService.Routings((int)manuf.ManufacturedPartNoDetailId);
            foreach (var item in resultList)
            {
                if(item.PreferredRouting == 1)
                {
                    rout = item;
                }
                else
                {
                    rout= (resultList).Take(1).FirstOrDefault();
                }
            }
            var result = (await _routingService.RoutingSteps(rout.RoutingId)).Take(1).FirstOrDefault();
            workOrdersVM.RoutingId = rout.RoutingId;
            workOrdersVM.StartingOpNo = result?.StepOperation != null
                                        ? int.TryParse(result.StepOperation, out int opNo) ? opNo : 0
                                        : 0;
            workOrdersVM.EndingOpNo = result?.StepOperation != null
                                        ? int.TryParse(result.StepOperation, out int eopNo) ? eopNo : 0
                                        : 0;
            if (workOrdersVM.PartType == 1)
            {
                workOrdersVM.Parentlevel = 'N';
                
            }
            else
            {
                var mpBOM = await _masterService.BOMS(workOrdersVM.PartId.ToString());
                if (mpBOM != null && mpBOM.Any())
                {
                    foreach (var bom in mpBOM)
                    {
                        var assy = await _masterService.GetManufPart((int)bom.BOMPartId);
                        if (assy != null)
                        {
                            workOrdersVM.Parentlevel = 'Y';
                        }
                        else
                        {
                            workOrdersVM.Parentlevel = 'N';
                        }
                    }
                }
                else
                {
                    workOrdersVM.Parentlevel = 'Y';
                }
            }
            var postWO = await _baService.PostWO(workOrdersVM);
            List<BOMTempVM> bompost = new List<BOMTempVM>();
            if (postWO.WOID > 0)
            {

                ManufacturedPartNoDetailVM mf = await _masterService.GetManufPart((int)postWO.PartId);
                string partype = "";
                if (postWO.PartType == 1)
                {
                    partype = "Manf";
                }
                else if (postWO.PartType == 2)
                {
                    partype = "Assy";
                }
                else
                {
                    partype = manuf.MasterPartType;
                }
                BOMTempVM bomdata = new BOMTempVM
                {
                    WorkOrderId = postWO.WOID,
                    PartId = postWO.PartId,
                    PartType = partype,
                    Parentlevel = postWO.Parentlevel,
                    TenantId = postWO.TenantId
                };
                bompost.Add(bomdata);
                SalesOrderVM salesOrderVM = new SalesOrderVM() {
                    SalesOrderId = postWO.SalesOrderId,
                    WorkOrderId =postWO.WOID,
                    WorkOrderNo = postWO.WONumber
                };
                var salesOrder = await _baService.PostSalesOrder(salesOrderVM);

            }
            var postbom = await _baService.BOMTempPOst(bompost);
            return Ok(postWO);
        }

        [HttpPost]
        public async Task<IActionResult> MultipleWOPost([FromBody] IEnumerable<WorkOrdersVM> listworkOrdersVM)
        {
            foreach (var workOrdersVM in listworkOrdersVM)
            {
                ManufacturedPartNoDetailVM manuf = await _masterService.GetManufPart((int)workOrdersVM.PartId);
                workOrdersVM.PartType = (int)manuf.ManufacturedPartType;
                RoutingVM rout = new RoutingVM();
                var resultList = await _routingService.Routings((int)manuf.ManufacturedPartNoDetailId);
                foreach (var item in resultList)
                {
                    if (item.PreferredRouting == 1)
                    {
                        rout = item;
                    }
                    else
                    {
                        rout = (resultList).Take(1).FirstOrDefault();
                    }
                }
                var result = (await _routingService.RoutingSteps(rout.RoutingId)).Take(1).FirstOrDefault();
                workOrdersVM.RoutingId = rout.RoutingId;
                workOrdersVM.StartingOpNo = result?.StepOperation != null
                                            ? int.TryParse(result.StepOperation, out int opNo) ? opNo : 0
                                            : 0;
                workOrdersVM.EndingOpNo = result?.StepOperation != null
                                            ? int.TryParse(result.StepOperation, out int eopNo) ? eopNo : 0
                                            : 0;
                if (workOrdersVM.PartType == 1)
                {
                    workOrdersVM.Parentlevel = 'N';
                }
                else
                {
                    //workOrdersVM.Parentlevel = 'Y';
                    var mpBOM = await _masterService.BOMS(workOrdersVM.PartId.ToString());
                    if(mpBOM != null && mpBOM.Any() )
                    {
                        foreach (var bom in mpBOM)
                        {
                            var assy = await _masterService.GetManufPart((int)bom.BOMPartId);
                            if (assy != null)
                            {
                                workOrdersVM.Parentlevel = 'Y';
                            }
                            else
                            {
                                workOrdersVM.Parentlevel = 'N';
                            }
                        }
                    }
                    else
                    {
                        workOrdersVM.Parentlevel = 'Y';
                    }
                }
            }
            var postWO = await _baService.MultiplePostWO(listworkOrdersVM);
           
            List<BOMTempVM> bompost = new List<BOMTempVM>();
            foreach (var item in postWO)
            {
                if (item.WOID > 0)
                {

                    ManufacturedPartNoDetailVM manuf = await _masterService.GetManufPart((int)item.PartId);
                    string partype = "";
                    if(item.PartType == 1)
                    {
                        partype = "Manf";
                    }
                    else if(item.PartType == 2)
                    {
                        partype = "Assy";
                    }
                    else
                    {
                        partype = manuf.MasterPartType;
                    }
                    BOMTempVM bomdata = new BOMTempVM
                    {
                        WorkOrderId = item.WOID,
                        PartId = item.PartId,
                        PartType = partype,
                        Parentlevel = item.Parentlevel,
                        TenantId = item.TenantId
                    };
                    bompost.Add(bomdata);
                    SalesOrderVM salesOrderVM = new SalesOrderVM()
                    {
                        SalesOrderId = item.SalesOrderId,
                        WorkOrderId = item.WOID,
                        WorkOrderNo =  item.WONumber
                    };
                    var salesOrder = await _baService.PostSalesOrder(salesOrderVM);
                }
            }
            var postbom = await _baService.BOMTempPOst(bompost);
            return Ok(postWO);
            //return Ok(listworkOrdersVM);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostWoSoRel([FromBody] IEnumerable<WOSOVM> wOSOVMs)
        {
            var postwoso = await _baService.PostWoSoRel(wOSOVMs);
            return Ok(postwoso);
        }

        [HttpGet]
        public async Task<IActionResult> AllWorkOrders()
        {
            var workOrders = await _baService.AllWorkOrders();
            var masterparts = await _masterService.ItemMasterParts();
            foreach (WorkOrdersVM item in workOrders)
            {
                foreach (ItemMasterPartVM imp in masterparts)
                {
                    if (item.PartId == imp.PartId)
                    {
                        item.PartNo = imp.PartNo;
                    }
                }
            }
            return Ok(workOrders);
        }

        [Route("~/C@S3t 2oP# ! ")]
        public IActionResult OrderEntry()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPOLogs(long customerOrderId)
        {
            var pologs = await _baService.GetPOLogs(customerOrderId);
            return Ok(pologs);

        }
        [HttpGet]
        public async Task<string> HelloWorld()
        {
            return await _baService.HelloWorld();
        }

        [HttpGet]
        public async Task<IActionResult> AllSalesOrders()
        {
            var salesorders = await _baService.AllSalesOrders();
            var masterparts = await _masterService.ItemMasterParts();
            var customer= await _baService.GetCustomerOrders();
            foreach (SalesOrderVM sovm in salesorders)
            {
                foreach (ItemMasterPartVM impvm in masterparts)
                {
                    if (sovm.PartId == impvm.PartId)
                    {
                        sovm.PartNo = impvm.PartNo;
                    }
                }
                foreach(CustomerOrderVM cu in customer)
                {
                    if (sovm.CustomerOrderId == cu.CustomerOrderId)
                    {
                        sovm.Customer = cu.CustomerName;
                    }
                }
            }
            return Ok(salesorders);
        }

            [HttpGet]
        public async Task<IActionResult> GetSalesOrders(long customerOrderId,long partId=0)
        {
            var salesorders = await _baService.GetSalesOrders(customerOrderId);
            var masterparts = await _masterService.ItemMasterParts();
            foreach(SalesOrderVM sovm in salesorders)
            {
                foreach(ItemMasterPartVM impvm in masterparts)
                {
                    if(sovm.PartId == impvm.PartId)
                    {
                        var bastatus = await _baService.GetBAStatus(sovm.Status);
                        sovm.StrStatus = bastatus.Status;
                        sovm.PartNo = impvm.PartNo;
                    }
                }
            }

            return Ok(salesorders);

        }

        [HttpGet]
        public async Task<IActionResult> GetCustOrders()
        {
            var custOrders = await _baService.GetCustomerOrders();
            return Ok(custOrders);
        }


        [HttpGet]
        public async Task<IActionResult> GetPOLines(long customerOrderId)
        {
            var salesorders = await _baService.GetSalesOrders(customerOrderId);
            var masterparts = await _masterService.ItemMasterParts();
            List<POLineVM> pOLines = new List<POLineVM>();
            foreach (SalesOrderVM sovm in salesorders)
            {
                foreach (ItemMasterPartVM impvm in masterparts)
                {
                    if (sovm.PartId == impvm.PartId)
                    {
                        sovm.PartNo = impvm.PartNo;
                        sovm.PartId = impvm.PartId.GetValueOrDefault();
                    }
                }
            }
            foreach (SalesOrderVM sovm in salesorders)
            {
                if (pOLines.Count() == 0)
                {
                    POLineVM polineVM = new POLineVM();
                    polineVM.PartNo = sovm.PartNo;
                    polineVM.PartId = sovm.PartId;
                    polineVM.TotalQty = sovm.RequiredQuantity;
                    polineVM.Status = sovm.Status;
                    polineVM.Matl = sovm.Matl;
                    polineVM.Hold = sovm.Hold;
                    polineVM.Plan = sovm.Plan;
                    polineVM.Matl = sovm.Matl;
                    polineVM.WIP = sovm.WIP;
                    polineVM.NumSalesOrder = 1;
                    polineVM.WONumber = sovm.WorkOrderNo;
                    pOLines.Add(polineVM);
                }
                else
                {
                    bool foundPOLine = false;
                    foreach (POLineVM povm in pOLines)
                    {
                        if (sovm.PartNo == povm.PartNo)
                        {
                            povm.TotalQty += sovm.RequiredQuantity;
                            foundPOLine = true;
                            povm.NumSalesOrder += 1;
                        }
                    }
                    if (foundPOLine) { }
                    else
                    {
                        POLineVM polineVM = new POLineVM();
                        polineVM.PartNo = sovm.PartNo;
                        polineVM.PartId = sovm.PartId;
                        polineVM.TotalQty = sovm.RequiredQuantity;
                        polineVM.Status = sovm.Status;
                        polineVM.Matl = sovm.Matl;
                        polineVM.Hold = sovm.Hold;
                        polineVM.Plan = sovm.Plan;
                        polineVM.Matl = sovm.Matl;
                        polineVM.WIP = sovm.WIP;
                        polineVM.NumSalesOrder = 1;
                        polineVM.WONumber = sovm.WorkOrderNo;
                        pOLines.Add(polineVM);
                    }
                }
            }
            return Ok(pOLines);
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedules(long customerOrderId)
        {
            var schedules = await _baService.GetSchedules(customerOrderId);
            var masterparts = await _masterService.ItemMasterParts();
            foreach (DeliveryScheduleVM sovm in schedules)
            {
                foreach (ItemMasterPartVM impvm in masterparts)
                {
                    if (sovm.DSPartId == impvm.PartId)
                    {
                        sovm.PartNo = impvm.PartNo;
                    }
                }
            }
            return Ok(schedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetSOAggregate(long customerOrderId)
        {
            var pologs = await _baService.GetSOAggregate(customerOrderId);
            return Ok(pologs);

        }

        [HttpPost]
        public async Task<IActionResult> POLog(POLogVM pOLogVM)
        {
            var salesOrder = await _baService.PostPOLog(pOLogVM);
            return Ok(salesOrder);
        }
        [HttpPost]
        public async Task<IActionResult> SOLog(POLogVM pOLogVM)
        {
            var salesOrder = await _baService.PostSOLog(pOLogVM);
            return Ok(salesOrder);
        }

        [HttpPost]
        public async Task<IActionResult> SalesOrder(SalesOrderVM salesOrderVM)
        {
            var salesOrder = await _baService.PostSalesOrder(salesOrderVM);
            return Ok(salesOrder);
        }

        [HttpPost]
        public async Task<IActionResult> CustomerOrder(CustomerOrderVM customerOrderVM)
        {
            var customerOrder = await _baService.PostCustomerOrder(customerOrderVM);
            return Ok(customerOrder);
        }

        [HttpPost]
        public async Task<IActionResult> DeliverySchedule(DeliveryScheduleVM deliveryScheduleVM)
        {
            var deliverySchedule = await _baService.PostDeliverySchedule(deliveryScheduleVM);
            return Ok(deliverySchedule);
        }

        [HttpPost]
        public async Task<IActionResult> SOAggregate(SOAggregateVM sOAggregateVM)
        {
            var deliverySchedule = await _baService.PostSOAggregate(sOAggregateVM);
            return Ok(deliverySchedule);
        }


        [HttpGet]
        public async Task<bool> RemoveCustomerOrder(long cutomerOrderId)
        {
            return await _baService.RemoveCustomerOder(cutomerOrderId);
        }
        [HttpGet]
        public async Task<bool> RemoveSalesOrder(long salesOrderId)
        {
            return await _baService.RemoveSalesOrder(salesOrderId);
        }
        [HttpGet]
        public async Task<bool> RemoveSchedule(long scheduleId)
        {
            return await _baService.RemoveDeliverySchedule(scheduleId);
        }

        [HttpGet]
        public async Task<bool> AddSalesOrders(long customerOrderId)
        {
            return await _baService.AddSalesOrders(customerOrderId);
        }

        [HttpGet]
        public async Task<JsonResult> CheckPartNo(long partId)
        {
            var result = await _baService.CheckPartNo(partId);
            return Json(!result);
        }

    }
}
