using CWB.App.Models.BusinessProcesses;
using CWB.App.Models.ItemMaster;
using CWB.App.Models.Plants;
using CWB.App.Models.WorkOrder;
using CWB.App.Services.BusinessProcesses;
using CWB.App.Services.CompanySettings;
using CWB.App.Services.Masters;
using CWB.App.Services.ProductionPlanWo;
using CWB.App.Services.Routings;
using CWB.Constants.UserIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class WorkOrderController : Controller
    {
        private readonly ILogger<WorkOrderController> _logger;
        private readonly IWOService _woService;
        private readonly IBAService _baService;
        private readonly IRoutingService _routingService;
        private readonly IMastersServices _masterService;
        private readonly IPlantService _plantService;
        private readonly IMachineService _machineService;
        private readonly IDepartmentService _departmentService;
        private readonly IOperationService _operationService;
        public WorkOrderController(ILogger<WorkOrderController> logger, IWOService wOService, 
            IBAService baService, IRoutingService routingService, IMastersServices masterServices, IPlantService plantService
            , IMachineService machineService, IDepartmentService departmentService, IOperationService operationService)
        {
            _logger = logger;
            _woService = wOService;
            _baService = baService;
            _routingService = routingService;
            _masterService = masterServices;
            _plantService = plantService;
            _machineService = machineService;
            _departmentService = departmentService;
            _operationService = operationService;
        }
        public IActionResult Index()
        {
            _logger.LogTrace("WO--Index--Loading");
            return View();
        }

        [Route("~/C!O$N%1#23t! ")]
        public IActionResult SoToWo()
        {
            return View();
        }

        [Route("~/D#1@122P I%5$3T I")]
        public IActionResult DetailedProcPlan()
        {
            return View();
        }
        [Route("~/S@A!E0#% T%1P W ")]
        public IActionResult SalesOrderList()
        {
            return View();
        }

        [Route("~/W@A!E0#% U%1X#Q ")]
        public IActionResult WorkOrderList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllSalesOrders()
        {
            var salesorders = await _baService.AllSalesOrders();
            var masterparts = await _masterService.ItemMasterParts();
            var customer = await _baService.GetCustomerOrders();
            foreach (SalesOrderVM sovm in salesorders)
            {
                foreach (ItemMasterPartVM impvm in masterparts)
                {
                    if (sovm.PartId == impvm.PartId)
                    {
                        var bastatus = await _baService.GetBAStatus(sovm.Status);
                        sovm.StrStatus = bastatus.Status;
                        sovm.PartNo = impvm.PartNo;
                        sovm.PartDesc = impvm.Description;
                    }
                }
                foreach (CustomerOrderVM cu in customer)
                {
                    if (sovm.CustomerOrderId == cu.CustomerOrderId)
                    {
                        sovm.Customer = cu.CustomerName;
                        sovm.PoNumber = cu.PONumber;
                    }
                }
            }
            return Ok(salesorders);
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
                        var wostatus = await _woService.GetWOStatus(item.Status);
                        item.StrStatus = wostatus.Status;
                        item.PartNo = imp.PartNo;
                        item.PartDesc = imp.Description;
                    }
                }
            }
            return Ok(workOrders);
        }

        [HttpGet]
        public async Task<IActionResult> AllParentChildWos(long parentWoId)
        {
            var allparentwos = await _woService.AllParentChildWos(parentWoId);
            return Ok(allparentwos);
        }

        [HttpGet]
        public async Task<IActionResult> ReloadWo(string reloadoption, long partid)
        {
            List<WorkOrdersVM> listwo = new List<WorkOrdersVM>();
            var workOrders = await _baService.AllWorkOrders();
            foreach (var item in workOrders)
            {
                if (item.ReloadOption == reloadoption && item.PartId == partid)
                {
                    listwo.Add(item);
                }
            }
            return Ok(listwo);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutings(int manufPartId)
        {
            ManufacturedPartNoDetailVM mf = await _masterService.GetManufPart(manufPartId);
            var resultList = await _routingService.Routings(mf.ManufacturedPartNoDetailId); 
            var sortedList = resultList.OrderByDescending(x => x.PreferredRouting == 1).ThenBy(x => x.PreferredRouting).ToList();
            return Ok(sortedList);
        }

        [HttpGet]
        public async Task<IActionResult> RoutingSteps(int routingId)
        {
            var result = await _routingService.RoutingSteps(routingId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSoWo(long workOrderId)
        {
            var resultList = await _woService.GetSoWoRel(workOrderId);
            return Ok(resultList);
        }

        [HttpPost]
        public async Task<IActionResult> GetOneSO([FromBody] IEnumerable<WOSOVM> saleOrderId)
        {
            List<SalesOrderVM> setSO = new List<SalesOrderVM>();
            foreach (var item in saleOrderId)
            {
                //if(item.Active != 2)
                //{
                setSO.Add(await _baService.GetOneSO(item.SalesOrderId));
                //}

            }
            return Ok(setSO);
        }

        public async Task<IActionResult> GetSoNumber(long soid)
        {
            var so = await _baService.GetOneSO(soid);
            return Ok(so);
        }

        [HttpPost]
        public async Task<IActionResult> ProcPlan()
        {
            var workOrders = await _baService.AllWorkOrders();
            List<ProductionPlan_WoVM> productions = new List<ProductionPlan_WoVM>();
            foreach (var item in workOrders)
            {
                if (item.Active != 2 && item.PPStatus != "PP")
                {
                    ProductionPlan_WoVM production = new ProductionPlan_WoVM
                    {
                        WONumber = item.WONumber,
                        WoId = item.WOID,
                        SalesOrderId = item.SalesOrderId,
                        PartId = item.PartId,
                        PartType = item.PartType,
                        Parentlevel = item.Parentlevel,
                        BuildToStock = item.BuildToStock,
                        TestData = item.TestData,
                        CalcWOQty = item.CalcWOQty,
                        PlanCompletionDate = item.PlanCompletionDate,
                        RoutingId = item.RoutingId,
                        StartingOpNo = item.StartingOpNo,
                        EndingOpNo = item.EndingOpNo,
                        ReloadOption = "",
                        TenantId = item.TenantId,
                    };
                    productions.Add(production);
                }
            }

            var procdutionpost = await _woService.ProductionPlanWoPost(productions);
            if (procdutionpost.Any())
            {
                List<ProcPlanVM> listprocplan = new List<ProcPlanVM>();
                List<BOMListVM> listbom = new List<BOMListVM>();
                List<ProductionPlan_WoVM> childwos = new List<ProductionPlan_WoVM>();
                List<ChildWoRelVM> childWoRels = new List<ChildWoRelVM>();
                List<McTimeListVM> mcTimeListVMs = new List<McTimeListVM>();

                var updatewo = await _woService.UpdateMultipleWorkOrder(workOrders);
                int totalLeadTime = 0;
                foreach (var item in procdutionpost)
                {
                    if (item.TestData == 'Y')
                    {
                        ManufacturedPartNoDetailVM mf = await _masterService.GetManufPart((int)item.PartId);
                        if (mf.ManufacturedPartType == 1)
                        {
                            var mpmakefromlist = await _masterService.GetMPMakeFromListByPartId(mf.ManufacturedPartNoDetailId.ToString());
                            foreach (var mpmakefrom in mpmakefromlist)
                            {
                                ChildWoRelVM cwo = new ChildWoRelVM()
                                {
                                    WoId = item.WoId,
                                    PartId = mpmakefrom.MPPartId,
                                    Qnty = decimal.TryParse(mpmakefrom.InputWeight, out decimal quantity) ? quantity : 0,
                                    CameFrom = "MakeFromPart"
                                };
                                childWoRels.Add(cwo);
                            }
                            var groupedResults = mpmakefromlist.GroupBy(x => x.MPPartId)
                                   .Select(g => new
                                   {
                                       PartId = g.Key,
                                       TotalQuantity = g.Sum(x =>
                                       {
                                           decimal quantity;
                                           return decimal.TryParse(x.InputWeight, out quantity) ? quantity : 0;
                                       }),

                                   })
                                    .ToList();
                            foreach (var grouped in groupedResults)
                            {
                                var mfpdList = await _masterService.PartPurchasesFor(grouped.PartId);
                                var ptype = await _masterService.GetRMPart(grouped.PartId);
                                totalLeadTime = mfpdList.Sum(x => x.LeadTimeInDays);
                                DateTime nextworkdingdate = (DateTime)item.PlanCompletionDate;
                                nextworkdingdate = nextworkdingdate.AddDays(totalLeadTime);
                                decimal intermediateResult = grouped.TotalQuantity * item.CalcWOQty;
                                if (groupedResults != null)
                                {
                                    ProcPlanVM ppdata = new ProcPlanVM
                                    {
                                        PartId = grouped.PartId,
                                        PartType = ptype.MasterPartType,
                                        Calc_Proc_Qnty = (int)intermediateResult,
                                        UOMId = mf.UOMId,
                                        CalcReceiptDate = nextworkdingdate,
                                        WorkOrderId = item.WoId
                                    };
                                    listprocplan.Add(ppdata);
                                    BOMListVM bomdata = new BOMListVM
                                    {
                                        ParentWoId = item.WoId,
                                        Child_Part_No_ID = grouped.PartId,
                                        Child_Part_No_Type = ptype.MasterPartType.ToString(),
                                        Calc_Qnty = (int)intermediateResult,
                                        Plan_Qnty = item.CalcWOQty,
                                        //Plan_Start_Dt = planstartdt,
                                        Plan_Compl_Dt = item.PlanCompletionDate.GetValueOrDefault(),
                                        CalcReceiptDate = nextworkdingdate,
                                        //Manf_Days_Avl = manfDays,
                                        ProcPlanId = item.ProductionPlanId,
                                        //SaNestLevel = Sa_Nest_level
                                    };
                                    listbom.Add(bomdata);
                                }
                            }
                        }
                        else if (mf.ManufacturedPartType == 2)
                        {
                            var bomlst = await _masterService.BOMS(mf.ManufacturedPartNoDetailId.ToString());
                            foreach (var bomVM in bomlst)
                            {
                                ChildWoRelVM cwo = new ChildWoRelVM()
                                {
                                    WoId = item.WoId,
                                    PartId = bomVM.BOMPartId,
                                    Qnty = Convert.ToInt32(bomVM.Quantity),
                                    CameFrom = "BOM"
                                };
                                childWoRels.Add(cwo);
                            }
                            var bomgroupedResults = bomlst.GroupBy(x => x.BOMPartId)
                                   .Select(g => new
                                   {
                                       PartId = g.Key,
                                       TotalQuantity = g.Sum(x => x.Quantity)
                                   })
                                    .ToList();
                            foreach (var bomgrp in bomgroupedResults)
                            {
                                var mp = await _masterService.ItemMasterPartById(bomgrp.PartId);
                                var workdetails = await _plantService.GetPlantWD(1);
                                var holidaylist = await _plantService.GetHolidays(1);
                                string weekOff1 = workdetails.WeeklyOff1;
                                string weekOff2 = workdetails.WeeklyOff2;
                                var resultList = await _routingService.Routings(mf.ManufacturedPartNoDetailId);
                                int minutes = 0;
                                foreach (var rote in resultList)
                                {
                                    var result = await _routingService.RoutingSteps(rote.RoutingId);
                                    foreach (var step in result)
                                    {
                                        var stepdetails = await _routingService.StepMachines((int)step.StepId);
                                        var processingTimeSum = stepdetails
                                                        .GroupBy(sd => sd.RoutingStepId)
                                                        .Select(g => new
                                                        {
                                                            RoutingStepId = g.Key,
                                                            TotalProcessingTime = g.Sum(sd => TimeSpan.Parse(sd.FirstPieceProcessingTime).TotalMinutes)
                                                        });
                                        foreach (var min in processingTimeSum)
                                        {
                                            minutes = (int)min.TotalProcessingTime;
                                        }
                                    }
                                }
                                int assyTime = (minutes * item.CalcWOQty) / workdetails.NoOfShifts;
                                int assyTimeInDays = assyTime / 1440;
                                DateTime planstartdt = item.PlanCompletionDate.Value.AddDays(-assyTimeInDays);
                                switch (mp.MasterPartType)
                                {
                                    case MasterPartType.ManufacturedPart:

                                        var manufchild = await _masterService.GetManufPart((int)bomgrp.PartId);
                                        var manfDays = 0;
                                        int Sa_Nest_level = 0;
                                        while (!IsWorkDay(planstartdt, holidaylist, weekOff1, weekOff2))
                                        {
                                            planstartdt = planstartdt.AddDays(1);
                                        }
                                        if (manufchild.ManufacturedPartType == 1)
                                        {
                                            DateTime planstdt = planstartdt;
                                            DateTime plancpldt = item.PlanCompletionDate.GetValueOrDefault();
                                            manfDays = Math.Max(0, (plancpldt - planstdt).Days);
                                            var mpmakefromlist = await _masterService.GetMPMakeFromListByPartId(manufchild.ManufacturedPartNoDetailId.ToString());
                                            foreach (var mpmakefrom in mpmakefromlist)
                                            {
                                                ChildWoRelVM subcwo = new ChildWoRelVM()
                                                {
                                                    WoId = item.WoId,
                                                    PartId = mpmakefrom.MPPartId,
                                                    Qnty = decimal.TryParse(mpmakefrom.InputWeight, out decimal quantity) ? quantity : 0,
                                                    CameFrom = "BOM"
                                                };
                                                childWoRels.Add(subcwo);
                                            }
                                            var groupedResults = mpmakefromlist.GroupBy(x => x.MPPartId)
                                                .Select(g => new
                                                {
                                                    PartId = g.Key,
                                                    TotalQuantity = g.Sum(x =>
                                                    {
                                                        decimal quantity;
                                                        return decimal.TryParse(x.InputWeight, out quantity) ? quantity : 0;
                                                    })
                                                })
                                                .ToList();
                                            foreach (var grouped in groupedResults)
                                            {
                                                var submfpdList = await _masterService.PartPurchasesFor(grouped.PartId);
                                                var subptype = await _masterService.GetRMPart(grouped.PartId);
                                                totalLeadTime = submfpdList.Sum(x => x.LeadTimeInDays);
                                                DateTime subnextworkdingdate = planstartdt;
                                                subnextworkdingdate = subnextworkdingdate.AddDays(totalLeadTime);
                                                decimal intermediateResult = grouped.TotalQuantity * item.CalcWOQty;
                                                if (groupedResults != null)
                                                {
                                                    ProcPlanVM subppdata = new ProcPlanVM
                                                    {
                                                        PartId = grouped.PartId,
                                                        PartType = subptype.MasterPartType,
                                                        Calc_Proc_Qnty = (int)intermediateResult,
                                                        UOMId = manufchild.UOMId,
                                                        CalcReceiptDate = subnextworkdingdate,
                                                        WorkOrderId = item.WoId
                                                    };
                                                    listprocplan.Add(subppdata);
                                                    BOMListVM subbomdata = new BOMListVM
                                                    {
                                                        ParentWoId = item.WoId,
                                                        Child_Part_No_ID = grouped.PartId,
                                                        Child_Part_No_Type = subptype.MasterPartType.ToString(),
                                                        Calc_Qnty = (int)intermediateResult,
                                                        Plan_Qnty = item.CalcWOQty,
                                                        //Plan_Start_Dt = planstartdt,
                                                        Plan_Compl_Dt = item.PlanCompletionDate.GetValueOrDefault(),
                                                        CalcReceiptDate = subnextworkdingdate,
                                                        //Manf_Days_Avl = manfDays,
                                                        ProcPlanId = item.ProductionPlanId,
                                                        //SaNestLevel = Sa_Nest_level
                                                    };
                                                    listbom.Add(subbomdata);

                                                }
                                            }
                                        }
                                        else
                                        {
                                            var asyy = await _masterService.GetManufPart((int)bomgrp.PartId);
                                            if (asyy.ManufacturedPartType == 2)
                                            {
                                                Sa_Nest_level = 1;

                                            }
                                            else
                                            {
                                                Sa_Nest_level = 2;
                                            }

                                        }
                                        BOMListVM bomdata = new BOMListVM
                                        {
                                            ParentWoId = item.WoId,
                                            Child_Part_No_ID = bomgrp.PartId,
                                            Child_Part_No_Type = mp.MasterPartType.ToString(),
                                            Calc_Qnty = (int)bomgrp.TotalQuantity * item.CalcWOQty,
                                            Plan_Qnty = item.CalcWOQty,
                                            Plan_Start_Dt = planstartdt,
                                            Plan_Compl_Dt = planstartdt,
                                            CalcReceiptDate = planstartdt,
                                            Manf_Days_Avl = manfDays,
                                            ProcPlanId = item.ProductionPlanId,
                                            SaNestLevel = Sa_Nest_level
                                        };
                                        listbom.Add(bomdata);
                                        ProductionPlan_WoVM cwo = new ProductionPlan_WoVM()
                                        {
                                            WoId = item.WoId,
                                            ParentWoId = item.WoId,
                                            SalesOrderId = item.SalesOrderId,
                                            PartId = bomdata.Child_Part_No_ID,
                                            PartType = (int)manufchild.ManufacturedPartType,
                                            Parentlevel = item.Parentlevel,
                                            BuildToStock = item.BuildToStock,
                                            TestData = item.TestData,
                                            CalcWOQty = bomdata.Calc_Qnty,
                                            PlanCompletionDate = planstartdt,
                                            RoutingId = item.RoutingId,
                                            StartingOpNo = item.StartingOpNo,
                                            EndingOpNo = item.EndingOpNo,
                                            For_Ref = 'N',
                                            ReloadOption = "",
                                            TenantId = item.TenantId,
                                        };
                                        childwos.Add(cwo);
                                        break;
                                    //case MasterPartType.BOM:

                                    //    break;
                                    case MasterPartType.BOF:
                                        var bofpdList = await _masterService.PartPurchasesFor(bomgrp.PartId);
                                        //var bofptype = await _masterService.GetRMPart(bomgrp.PartId);
                                        BoughtOutFinishDetailVM manuf = await _masterService.GetBOFPart(bomgrp.PartId);
                                        totalLeadTime = bofpdList.Sum(x => x.LeadTimeInDays);
                                        DateTime bofnextworkdingdate = item.PlanCompletionDate.GetValueOrDefault();
                                        bofnextworkdingdate = bofnextworkdingdate.AddDays(totalLeadTime);
                                        BOMListVM bofbomdata = new BOMListVM
                                        {
                                            ParentWoId = item.WoId,
                                            Child_Part_No_ID = bomgrp.PartId,
                                            Child_Part_No_Type = mp.MasterPartType.ToString(),
                                            Calc_Qnty = (int)bomgrp.TotalQuantity * item.CalcWOQty,
                                            Plan_Qnty = item.CalcWOQty,
                                            Plan_Compl_Dt = planstartdt,
                                            CalcReceiptDate = bofnextworkdingdate,
                                            ProcPlanId = item.ProductionPlanId
                                        };
                                        listbom.Add(bofbomdata);
                                        ProcPlanVM ppdata = new ProcPlanVM
                                        {
                                            PartId = bomgrp.PartId,
                                            PartType = mp.MasterPartType.ToString(),
                                            Calc_Proc_Qnty = (int)bomgrp.TotalQuantity * item.CalcWOQty,
                                            UOMId = manuf.UOMId,
                                            CalcReceiptDate = bofnextworkdingdate,
                                            WorkOrderId = item.WoId
                                        };
                                        listprocplan.Add(ppdata);

                                        break;
                                    case MasterPartType.RawMaterial:
                                        var mfpdList = await _masterService.PartPurchasesFor(bomgrp.PartId);
                                        var ptype = await _masterService.GetRMPart(bomgrp.PartId);
                                        totalLeadTime = mfpdList.Sum(x => x.LeadTimeInDays);
                                        DateTime nextworkdingdate = (DateTime)item.PlanCompletionDate;
                                        nextworkdingdate = nextworkdingdate.AddDays(totalLeadTime);

                                        BOMListVM rmbomdata = new BOMListVM
                                        {
                                            ParentWoId = item.WoId,
                                            Child_Part_No_ID = bomgrp.PartId,
                                            Child_Part_No_Type = mp.MasterPartType.ToString(),
                                            Calc_Qnty = (int)bomgrp.TotalQuantity * item.CalcWOQty,
                                            Plan_Qnty = item.CalcWOQty,
                                            Plan_Compl_Dt = planstartdt,
                                            CalcReceiptDate = nextworkdingdate,
                                            ProcPlanId = item.ProductionPlanId
                                        };
                                        listbom.Add(rmbomdata);

                                        break;
                                    default:
                                        break;
                                }
                            }

                        }

                    }

                    //McTimeList---
                    ManufacturedPartNoDetailVM mcmf = await _masterService.GetManufPart((int)item.PartId);
                    var mcworkdetails = await _plantService.GetPlantWD(1);
                    var mcholidaylist = await _plantService.GetHolidays(1);
                    string mcweekOff1 = mcworkdetails.WeeklyOff1;
                    string mcweekOff2 = mcworkdetails.WeeklyOff2;
                    var mcresultList = await _routingService.Routings(mcmf.ManufacturedPartNoDetailId);
                    int mcminutes = 0;
                    foreach (var rote in mcresultList)
                    {
                        var result = await _routingService.RoutingSteps(rote.RoutingId);
                        foreach (var step in result)
                        {
                            var stepdetails = await _routingService.StepMachines((int)step.StepId);
                            var processingTimeSum = stepdetails
                                            .GroupBy(sd => sd.RoutingStepId)
                                            .Select(g => new
                                            {
                                                RoutingStepId = g.Key,
                                                TotalProcessingTime = g.Sum(sd => TimeSpan.Parse(sd.FirstPieceProcessingTime).TotalMinutes)
                                            });
                            foreach (var min in processingTimeSum)
                            {
                                mcminutes = (int)min.TotalProcessingTime;
                            }
                        }
                    }
                    int mcassyTime = (mcminutes * item.CalcWOQty) / mcworkdetails.NoOfShifts;
                    int mcassyTimeInDays = mcassyTime / 1440;
                    DateTime mcplanstartdt = item.PlanCompletionDate.Value.AddDays(-mcassyTimeInDays);
                    var routingstep = await _routingService.RoutingSteps((int)item.RoutingId);
                    var oneroutingstep = routingstep.FirstOrDefault();
                    if (oneroutingstep != null)
                    {
                        var stepmachine = await _routingService.StepMachines((int)oneroutingstep.StepId);

                        var onestepmachine = stepmachine.FirstOrDefault();
                        if (onestepmachine != null)
                        {
                            var machine = await _machineService.GetMachine((int)onestepmachine?.MachineId);
                            // var result = await _departmentService.GetDepartments(1);
                            //Total_Plan_time = Setup_time + (1st_Pc_Process_time+Cycle time  x (WO_Plan_Qnty-1))/No_of_parts_per_loading
                            //TimeSpan.Parse(sd.FirstPieceProcessingTime).TotalMinutes

                            var departments = await _departmentService.GetDepartments(1);
                            var department = departments.FirstOrDefault(d => d.DepartmentId == machine.MachineDepartmentId);
                            int Totalplantime = Convert.ToInt32(TimeSpan.Parse(onestepmachine.SetupTime).TotalMinutes) + ((Convert.ToInt32(TimeSpan.Parse(onestepmachine.FirstPieceProcessingTime).TotalMinutes) + department.NoOfShifts) * (item.CalcWOQty - 1)) / onestepmachine.NoOfPartsPerLoading;
                            int TotalplantimeInHoursRounded = (int)Math.Round(Totalplantime / 60.0);
                            McTimeListVM mcTimeList = new McTimeListVM()
                            {
                                WoId = item.WoId,
                                Routing_StepId = oneroutingstep.StepId,
                                CompanyId = Convert.ToInt64(oneroutingstep.StepLocation),
                                MachineId = onestepmachine.MachineId,
                                MachineTypeId = machine.MachineMachineTypeId,
                                PlanQnty = item.CalcWOQty,
                                TotalPlanTime = TotalplantimeInHoursRounded,
                                McPlanStartTime = mcplanstartdt,
                                McPlanEndTime = (DateTime)item.PlanCompletionDate,
                            };
                            mcTimeListVMs.Add(mcTimeList);
                        }
                    }
                }
                if (listprocplan.Any())
                {
                    var result = await _woService.ProcPlanPost(listprocplan);

                }
                if (listbom.Any())
                {
                    var bomresult = await _woService.BomListPost(listbom);
                }
                var childworels = await _woService.PostChildWoRel(childWoRels);
                var machinetimepost = await _woService.PostMcTimeList(mcTimeListVMs);
                var childproductionwopost = await _woService.ProductionPlanWoPost(childwos);
                if (childproductionwopost.Any())
                {
                    List<ProcPlanVM> sublistprocplan = new List<ProcPlanVM>();
                    List<BOMListVM> sublistbom = new List<BOMListVM>();
                    List<ProductionPlan_WoVM> subchildwos = new List<ProductionPlan_WoVM>();
                    List<ChildWoRelVM> subchildWoRels = new List<ChildWoRelVM>();
                    List<McTimeListVM> submcTimeListVMs = new List<McTimeListVM>();
                    int subtotalLeadTime = 0;
                    foreach (var item in childproductionwopost)
                    {
                        if (item.TestData == 'Y')
                        {
                            ManufacturedPartNoDetailVM mf = await _masterService.GetManufPart((int)item.PartId);
                            if (mf.ManufacturedPartType == 1)
                            {
                                var mpmakefromlist = await _masterService.GetMPMakeFromListByPartId(mf.ManufacturedPartNoDetailId.ToString());
                                foreach (var mpmakefrom in mpmakefromlist)
                                {
                                    ChildWoRelVM cwo = new ChildWoRelVM()
                                    {
                                        WoId = item.WoId,
                                        PartId = mpmakefrom.MPPartId,
                                        Qnty = decimal.TryParse(mpmakefrom.InputWeight, out decimal quantity) ? quantity : 0,
                                        CameFrom = "MakeFromPart"
                                    };
                                    
                                    subchildWoRels.Add(cwo);
                                }
                                var groupedResults = mpmakefromlist.GroupBy(x => x.MPPartId)
                                       .Select(g => new
                                       {
                                           PartId = g.Key,
                                           TotalQuantity = g.Sum(x =>
                                           {
                                               decimal quantity;
                                               return decimal.TryParse(x.InputWeight, out quantity) ? quantity : 0;
                                           })
                                       })
                                        .ToList();
                                foreach (var grouped in groupedResults)
                                {
                                    var mfpdList = await _masterService.PartPurchasesFor(grouped.PartId);
                                    var ptype = await _masterService.GetRMPart(grouped.PartId);
                                    subtotalLeadTime = mfpdList.Sum(x => x.LeadTimeInDays);
                                    DateTime nextworkdingdate = (DateTime)item.PlanCompletionDate;
                                    nextworkdingdate = nextworkdingdate.AddDays(subtotalLeadTime);
                                    decimal intermediateResult = grouped.TotalQuantity * item.CalcWOQty;
                                    if (groupedResults != null)
                                    {
                                        ProcPlanVM ppdata = new ProcPlanVM
                                        {
                                            PartId = grouped.PartId,
                                            PartType = ptype.MasterPartType,
                                            Calc_Proc_Qnty = (int)intermediateResult,
                                            UOMId = mf.UOMId,
                                            CalcReceiptDate = nextworkdingdate,
                                            WorkOrderId = item.WoId
                                        };
                                        sublistprocplan.Add(ppdata);
                                        BOMListVM bomdata = new BOMListVM
                                        {
                                            ParentWoId = item.WoId,
                                            Child_Part_No_ID = grouped.PartId,
                                            Child_Part_No_Type = ptype.MasterPartType.ToString(),
                                            Calc_Qnty = (int)intermediateResult,
                                            Plan_Qnty = item.CalcWOQty,
                                            //Plan_Start_Dt = planstartdt,
                                            Plan_Compl_Dt = item.PlanCompletionDate.GetValueOrDefault(),
                                            CalcReceiptDate = nextworkdingdate,
                                            //Manf_Days_Avl = manfDays,
                                            ProcPlanId = item.ProductionPlanId,
                                            //SaNestLevel = Sa_Nest_level
                                        };
                                        sublistbom.Add(bomdata);
                                    }
                                }
                            }
                            else if (mf.ManufacturedPartType == 2)
                            {
                                var bomlst = await _masterService.BOMS(mf.ManufacturedPartNoDetailId.ToString());
                                foreach (var bomVM in bomlst)
                                {
                                    ChildWoRelVM cwo = new ChildWoRelVM()
                                    {
                                        WoId = item.WoId,
                                        PartId = bomVM.BOMPartId,
                                        Qnty = Convert.ToInt32(bomVM.Quantity),
                                        CameFrom = "BOM"
                                    };
                                    subchildWoRels.Add(cwo);
                                }
                                var bomgroupedResults = bomlst.GroupBy(x => x.BOMPartId)
                                       .Select(g => new
                                       {
                                           PartId = g.Key,
                                           TotalQuantity = g.Sum(x => x.Quantity)
                                       })
                                        .ToList();
                                foreach (var bomgrp in bomgroupedResults)
                                {
                                    var mp = await _masterService.ItemMasterPartById(bomgrp.PartId);
                                    var workdetails = await _plantService.GetPlantWD(1);
                                    var holidaylist = await _plantService.GetHolidays(1);
                                    string weekOff1 = workdetails.WeeklyOff1;
                                    string weekOff2 = workdetails.WeeklyOff2;
                                    var resultList = await _routingService.Routings(mf.ManufacturedPartNoDetailId);
                                    int minutes = 0;
                                    foreach (var rote in resultList)
                                    {
                                        var result = await _routingService.RoutingSteps(rote.RoutingId);
                                        foreach (var step in result)
                                        {
                                            var stepdetails = await _routingService.StepMachines((int)step.StepId);
                                            var processingTimeSum = stepdetails
                                                            .GroupBy(sd => sd.RoutingStepId)
                                                            .Select(g => new
                                                            {
                                                                RoutingStepId = g.Key,
                                                                TotalProcessingTime = g.Sum(sd => TimeSpan.Parse(sd.FirstPieceProcessingTime).TotalMinutes)
                                                            });
                                            foreach (var min in processingTimeSum)
                                            {
                                                minutes = (int)min.TotalProcessingTime;
                                            }
                                        }
                                    }
                                    int assyTime = (minutes * item.CalcWOQty) / workdetails.NoOfShifts;
                                    int assyTimeInDays = assyTime / 1440;
                                    DateTime planstartdt = item.PlanCompletionDate.Value.AddDays(-assyTimeInDays);
                                    switch (mp.MasterPartType)
                                    {
                                        case MasterPartType.ManufacturedPart:
                                            

                                            var manufchild = await _masterService.GetManufPart((int)bomgrp.PartId);
                                            var manfDays = 0;
                                            int Sa_Nest_level = 0;
                                            while (!IsWorkDay(planstartdt, holidaylist, weekOff1, weekOff2))
                                            {
                                                planstartdt = planstartdt.AddDays(1);
                                            }
                                            if (manufchild.ManufacturedPartType == 1)
                                            {
                                                DateTime planstdt = planstartdt;
                                                DateTime plancpldt = item.PlanCompletionDate.GetValueOrDefault();
                                                manfDays = Math.Max(0, (plancpldt - planstdt).Days);
                                                //--changed RM 
                                                var mpmakefromlist = await _masterService.GetMPMakeFromListByPartId(manufchild.ManufacturedPartNoDetailId.ToString());
                                                foreach (var mpmakefrom in mpmakefromlist)
                                                {
                                                    ChildWoRelVM subcwo = new ChildWoRelVM()
                                                    {
                                                        WoId = item.WoId,
                                                        PartId = mpmakefrom.MPPartId,
                                                        Qnty = decimal.TryParse(mpmakefrom.InputWeight, out decimal quantity) ? quantity : 0,
                                                        CameFrom = "BOM"
                                                    };
                                                    subchildWoRels.Add(subcwo);
                                                }
                                                var groupedResults = mpmakefromlist.GroupBy(x => x.MPPartId)
                                           .Select(g => new
                                           {
                                               PartId = g.Key,
                                               TotalQuantity = g.Sum(x =>
                                               {
                                                   decimal quantity;
                                                   return decimal.TryParse(x.InputWeight, out quantity) ? quantity : 0;
                                               })
                                           })
                                            .ToList();
                                                foreach (var grouped in groupedResults)
                                                {
                                                    var submfpdList = await _masterService.PartPurchasesFor(grouped.PartId);
                                                    var subptype = await _masterService.GetRMPart(grouped.PartId);
                                                    subtotalLeadTime = submfpdList.Sum(x => x.LeadTimeInDays);
                                                    DateTime subnextworkdingdate = (DateTime)item.PlanCompletionDate;
                                                    subnextworkdingdate = subnextworkdingdate.AddDays(subtotalLeadTime);
                                                    decimal intermediateResult = grouped.TotalQuantity * item.CalcWOQty;
                                                    if (groupedResults != null)
                                                    {
                                                        ProcPlanVM subppdata = new ProcPlanVM
                                                        {
                                                            PartId = grouped.PartId,
                                                            PartType = subptype.MasterPartType,
                                                            Calc_Proc_Qnty = (int)intermediateResult,
                                                            UOMId = manufchild.UOMId,
                                                            CalcReceiptDate = subnextworkdingdate,
                                                            WorkOrderId = item.WoId
                                                        };
                                                        sublistprocplan.Add(subppdata);
                                                        BOMListVM subbomdata = new BOMListVM
                                                        {
                                                            ParentWoId = item.WoId,
                                                            Child_Part_No_ID = grouped.PartId,
                                                            Child_Part_No_Type = subptype.MasterPartType.ToString(),
                                                            Calc_Qnty = (int)intermediateResult,
                                                            Plan_Qnty = item.CalcWOQty,
                                                            //Plan_Start_Dt = planstartdt,
                                                            Plan_Compl_Dt = item.PlanCompletionDate.GetValueOrDefault(),
                                                            CalcReceiptDate = subnextworkdingdate,
                                                            //Manf_Days_Avl = manfDays,
                                                            ProcPlanId = item.ProductionPlanId,
                                                            //SaNestLevel = Sa_Nest_level
                                                        };
                                                        sublistbom.Add(subbomdata);
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                var asyy = await _masterService.GetManufPart((int)bomgrp.PartId);
                                                if (asyy.ManufacturedPartType == 2)
                                                {
                                                    Sa_Nest_level = 1;
                                                }
                                                else
                                                {
                                                    Sa_Nest_level = 2;
                                                }

                                            }
                                            BOMListVM bomdata = new BOMListVM
                                            {
                                                ParentWoId = item.WoId,
                                                Child_Part_No_ID = bomgrp.PartId,
                                                Child_Part_No_Type = mp.MasterPartType.ToString(),
                                                Calc_Qnty = (int)bomgrp.TotalQuantity * item.CalcWOQty,
                                                Plan_Qnty = item.CalcWOQty,
                                                Plan_Start_Dt = planstartdt,
                                                Plan_Compl_Dt = planstartdt,
                                                CalcReceiptDate = planstartdt,
                                                Manf_Days_Avl = manfDays,
                                                ProcPlanId = item.ProductionPlanId,
                                                SaNestLevel = Sa_Nest_level
                                            };
                                            sublistbom.Add(bomdata);
                                            ProductionPlan_WoVM cwo = new ProductionPlan_WoVM()
                                            {
                                                WoId = item.WoId,
                                                ParentWoId = item.WoId,
                                                SalesOrderId = item.SalesOrderId,
                                                PartId = bomdata.Child_Part_No_ID,
                                                PartType = (int)manufchild.ManufacturedPartType,
                                                Parentlevel = item.Parentlevel,
                                                BuildToStock = item.BuildToStock,
                                                TestData = item.TestData,
                                                CalcWOQty = bomdata.Calc_Qnty,
                                                PlanCompletionDate = planstartdt,
                                                RoutingId = item.RoutingId,
                                                StartingOpNo = item.StartingOpNo,
                                                EndingOpNo = item.EndingOpNo,
                                                For_Ref = 'N',
                                                ReloadOption = "",
                                                TenantId = item.TenantId,
                                            };
                                            subchildwos.Add(cwo);
                                            break;
                                        //case MasterPartType.BOM:

                                        //    break;
                                        case MasterPartType.BOF:
                                            var bofpdList = await _masterService.PartPurchasesFor(bomgrp.PartId);
                                            //var bofptype = await _masterService.GetRMPart(bomgrp.PartId);
                                            BoughtOutFinishDetailVM manuf = await _masterService.GetBOFPart(bomgrp.PartId);
                                            subtotalLeadTime = bofpdList.Sum(x => x.LeadTimeInDays);
                                            DateTime bofnextworkdingdate = (DateTime)item.PlanCompletionDate;
                                            bofnextworkdingdate = bofnextworkdingdate.AddDays(subtotalLeadTime);
                                            BOMListVM bofbomdata = new BOMListVM
                                            {
                                                ParentWoId = item.WoId,
                                                Child_Part_No_ID = bomgrp.PartId,
                                                Child_Part_No_Type = mp.MasterPartType.ToString(),
                                                Calc_Qnty = (int)bomgrp.TotalQuantity * item.CalcWOQty,
                                                Plan_Qnty = item.CalcWOQty,
                                                Plan_Compl_Dt = planstartdt,
                                                CalcReceiptDate = bofnextworkdingdate,
                                                ProcPlanId = item.ProductionPlanId
                                            };
                                            sublistbom.Add(bofbomdata);
                                            ProcPlanVM ppdata = new ProcPlanVM
                                            {
                                                PartId = bomgrp.PartId,
                                                PartType = mp.MasterPartType.ToString(),
                                                Calc_Proc_Qnty = (int)bomgrp.TotalQuantity * item.CalcWOQty,
                                                UOMId= manuf.UOMId,
                                                CalcReceiptDate = bofnextworkdingdate,
                                                WorkOrderId = item.WoId
                                            };
                                            sublistprocplan.Add(ppdata);
                                            
                                            break;
                                        case MasterPartType.RawMaterial:
                                            var mfpdList = await _masterService.PartPurchasesFor(bomgrp.PartId);
                                            var ptype = await _masterService.GetRMPart(bomgrp.PartId);
                                            subtotalLeadTime = mfpdList.Sum(x => x.LeadTimeInDays);
                                            DateTime nextworkdingdate = (DateTime)item.PlanCompletionDate;
                                            nextworkdingdate = nextworkdingdate.AddDays(subtotalLeadTime);

                                            BOMListVM rmbomdata = new BOMListVM
                                            {
                                                ParentWoId = item.WoId,
                                                Child_Part_No_ID = bomgrp.PartId,
                                                Child_Part_No_Type = mp.MasterPartType.ToString(),
                                                Calc_Qnty = (int)bomgrp.TotalQuantity * item.CalcWOQty,
                                                Plan_Qnty = item.CalcWOQty,
                                                Plan_Compl_Dt = planstartdt,
                                                CalcReceiptDate = nextworkdingdate,
                                                ProcPlanId = item.ProductionPlanId
                                            };
                                            sublistbom.Add(rmbomdata);
                                           
                                            break;
                                        default:
                                            break;
                                    }
                                }

                            }

                        }

                        //McTimeList---
                        ManufacturedPartNoDetailVM mcmf = await _masterService.GetManufPart((int)item.PartId);
                        var mcworkdetails = await _plantService.GetPlantWD(1);
                        var mcholidaylist = await _plantService.GetHolidays(1);
                        string mcweekOff1 = mcworkdetails.WeeklyOff1;
                        string mcweekOff2 = mcworkdetails.WeeklyOff2;
                        var mcresultList = await _routingService.Routings(mcmf.ManufacturedPartNoDetailId);
                        int mcminutes = 0;
                        foreach (var rote in mcresultList)
                        {
                            var result = await _routingService.RoutingSteps(rote.RoutingId);
                            foreach (var step in result)
                            {
                                var stepdetails = await _routingService.StepMachines((int)step.StepId);
                                var processingTimeSum = stepdetails
                                                .GroupBy(sd => sd.RoutingStepId)
                                                .Select(g => new
                                                {
                                                    RoutingStepId = g.Key,
                                                    TotalProcessingTime = g.Sum(sd => TimeSpan.Parse(sd.FirstPieceProcessingTime).TotalMinutes)
                                                });
                                foreach (var min in processingTimeSum)
                                {
                                    mcminutes = (int)min.TotalProcessingTime;
                                }
                            }
                        }
                        int mcassyTime = (mcminutes * item.CalcWOQty) / mcworkdetails.NoOfShifts;
                        int mcassyTimeInDays = mcassyTime / 1440;
                        DateTime mcplanstartdt = item.PlanCompletionDate.Value.AddDays(-mcassyTimeInDays);
                        var routingstep = await _routingService.RoutingSteps((int)item.RoutingId);
                        var oneroutingstep = routingstep.FirstOrDefault();
                        if (oneroutingstep != null)
                        {
                            var stepmachine = await _routingService.StepMachines((int)oneroutingstep.StepId);
                            var onestepmachine = stepmachine.FirstOrDefault();
                            if (onestepmachine != null)
                            {
                                var machine = await _machineService.GetMachine((int)onestepmachine?.MachineId);

                                var departments = await _departmentService.GetDepartments(1);
                                var department = departments.FirstOrDefault(d => d.DepartmentId == machine.MachineDepartmentId);
                                int Totalplantime = Convert.ToInt32(TimeSpan.Parse(onestepmachine.SetupTime).TotalMinutes) + ((Convert.ToInt32(TimeSpan.Parse(onestepmachine.FirstPieceProcessingTime).TotalMinutes) + department.NoOfShifts) * (item.CalcWOQty - 1)) / onestepmachine.NoOfPartsPerLoading;
                                int TotalplantimeInHoursRounded = (int)Math.Round(Totalplantime / 60.0);
                                McTimeListVM mcTimeList = new McTimeListVM()
                                {
                                    WoId = item.WoId,
                                    Routing_StepId = oneroutingstep.StepId,
                                    CompanyId = Convert.ToInt64(oneroutingstep.StepLocation),
                                    MachineId = onestepmachine.MachineId,
                                    MachineTypeId = machine.MachineMachineTypeId,
                                    PlanQnty = item.CalcWOQty,
                                    TotalPlanTime = TotalplantimeInHoursRounded,
                                    McPlanStartTime = mcplanstartdt,
                                    McPlanEndTime = (DateTime)item.PlanCompletionDate,
                                };
                                submcTimeListVMs.Add(mcTimeList);
                            }
                        }
                    }
                    if (sublistprocplan.Any())
                    {
                        var result = await _woService.ProcPlanPost(sublistprocplan);

                    }
                    if (sublistbom.Any())
                    {
                        var bomresult = await _woService.BomListPost(sublistbom);
                    }
                    var subchildworels = await _woService.PostChildWoRel(subchildWoRels);
                    var submachinetimepost = await _woService.PostMcTimeList(submcTimeListVMs);
                    var combinedSubchildren = subchildwos
                                .Where(sc => sc.PartType == 1)
                                .GroupBy(sc => sc.PartId)
                                .Select(g => new ProductionPlan_WoVM
                                {
                                    PartId = g.Key,
                                    CalcWOQty = g.Sum(sc => sc.CalcWOQty),
                                    PlanCompletionDate = g.Min(sc => sc.PlanCompletionDate)
                                })
                                .ToList();
                    var subchildproductionwopost = await _woService.ProductionPlanWoPost(subchildwos);
                }

            }

            //return RedirectToAction("DetailedProcPlan");
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> PostProcPlan([FromBody] IEnumerable<ProcPlanVM> procPlanVMs)
        {
            var result = await _woService.ProcPlanPost(procPlanVMs);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> AllProductionWo()
        {
            var productions = await _woService.AllProductionPlan_Wo();
            var masterparts = await _masterService.ItemMasterParts();
            foreach (ProductionPlan_WoVM item in productions)
            {
                foreach (ItemMasterPartVM imp in masterparts)
                {
                    if (item.PartId == imp.PartId)
                    {
                        item.PartNo = imp.PartNo;
                        item.PartDesc = imp.Description;
                    }
                }
            }
            return Ok(productions);
        }
        public async Task<ActionResult> DownloadProductionWoGridData()
        {
            var productions = await _woService.AllProductionPlan_Wo();
            var masterparts = await _masterService.ItemMasterParts();
            foreach (ProductionPlan_WoVM item in productions)
            {
                foreach (ItemMasterPartVM imp in masterparts)
                {
                    if (item.PartId == imp.PartId)
                    {
                        item.PartNo = imp.PartNo;
                        item.PartDesc = imp.Description;
                    }
                }
            }
            // Get the grid data from your database or data source
            //var gridData =productions;

            // Create a CSV string from the grid data   Addn Qnty from User	Plan WO Qnty	Input Matl Rcpt Date	Plan Start Date	Plan Compl Dt
            string csv = string.Empty;
            csv += "WO Number,Build To Stock,Part No/Description,Calc WO Qty,Qty On Hand,Addn Qnty from User,Plan WO Qnty,Plan Start Date,Plan Compl Dt\r\n";
            foreach (var item in productions)
            {
                csv += $"{item.WONumber},{item.BuildToStock},{item.PartNo}/{item.PartDesc},{item.CalcWOQty},{item.QtyOnHand},{item.AddnOtyUser},{item.PlanWOQnty},{""},{item.PlanCompletionDateStr}\r\n";
            }

            // Return the CSV file as a FileContentResult
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "ProductionPlanWoData.csv");
        }

        public async Task<ActionResult> DownloadMaterialProcPlanGridData()
        {
            var resultList = await _woService.GetAllProcPlan();
            foreach (ProcPlanVM item in resultList)
            {
                var mp = await _masterService.ItemMasterPartById((int)item.PartId);
                var mfpdList = await _masterService.PartPurchasesFor((int)item.PartId);
                var uoms = await _masterService.GetUOMs();
                foreach (var uom in uoms)
                {
                    if (item.UOMId == uom.UOMId)
                    {
                        item.UomName = uom.Name;
                    }
                }

                foreach (var purs in mfpdList)
                {
                    if (item.PartId == purs.PPartId)
                    {
                        item.Supplier = purs.PSupplier;
                        int subtotalLeadTime = mfpdList.Sum(x => x.LeadTimeInDays);
                        item.LeadTimeInDays = subtotalLeadTime.ToString();
                        int MOQ = mfpdList.Sum(x => x.MinimumOrderQuantity);
                        item.Moq = MOQ;
                    }
                }
                item.PartNo = mp.PartNo;
                item.PartDesc = mp.PartDescription;
            }
            string csv = string.Empty;
            csv += "Part No / Desc,Part Type,Supplier,PO Ref,Calculated Requirement,UOM,Qnty on Hand,Planned Purchase Qnty,MOQ,Date Reqd,Lead Time Days,Calculated Receipt Date\r\n";
            foreach (var item in resultList)
            {
                csv += $"{item.PartNo}/{item.PartDesc},{item.PartType},{item.Supplier},{""},{item.Calc_Proc_Qnty},{item.UomName},{item.OtyOnHand},{item.Plan_Proc_Qnty},{item.Moq},{""},{item.LeadTimeInDays},{item.CalcReceiptDateStr}\r\n";
            }

            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "MaterialProcPlanData.csv");
        }

        public async Task<ActionResult> DownloadBomListGridData()
        {
            var resultList = await _woService.GetAllBomlist();
            var workOrders = await _baService.AllWorkOrders();
            foreach (BOMListVM item in resultList)
            {
                var mp = await _masterService.ItemMasterPartById((int)item.Child_Part_No_ID);
                item.PartNo = mp.PartNo;
                item.PartDesc = mp.PartDescription;
                foreach (WorkOrdersVM wo in workOrders)
                {
                    if (item.ParentWoId == wo.WOID)
                    {
                        item.WoNumber = wo.WONumber;
                    }
                }
            }
            string csv = string.Empty;
            csv += "Child Part / Desc,Part Type,WO / PO Ref,Plan Qnty,Plan Compl / Rcpt Date,Act Qnty,Act Compl / Recpt Dt,Status,Critical Part\r\n";
            foreach (var item in resultList)
            {
                csv += $"{item.PartNo}/{item.PartDesc},{item.Child_Part_No_Type},{item.WoNumber},{item.Calc_Qnty},{item.CalcReceiptDateStr},{""},{""},{""},{""}\r\n";
            }

            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "BOMListData.csv");
        }

        public async Task<ActionResult> DownloadMachineListDetailGridData()
        {
            var mctimelist = await _woService.GetAllMcTimeList();
            var productions = await _woService.AllProductionPlan_Wo();
            var masterparts = await _masterService.ItemMasterParts();
            foreach (var mctime in mctimelist)
            {
                var machine = await _machineService.GetMachine((int)mctime.MachineId);
                var machineTypes = await _machineService.GetMachineTypes();
                foreach (ProductionPlan_WoVM item in productions)
                {
                    if (item.WoId == mctime.WoId && item.ParentWoId == 0)
                    {
                        foreach (ItemMasterPartVM imp in masterparts)
                        {
                            if (item.PartId == imp.PartId)
                            {
                                mctime.PartNo = imp.PartNo;
                                mctime.PartDesc = imp.Description;
                                mctime.PartType = imp.MasterPartType;
                                mctime.WoNumber = item.WONumber;
                            }
                        }
                    }
                }
                if (mctime.MachineId == machine.MachineMachineId)
                {
                    mctime.MachineName = machine.MachineMachineName;

                    var departments = await _departmentService.GetDepartments(1);
                    var department = departments.FirstOrDefault(d => d.DepartmentId == machine.MachineDepartmentId);
                    mctime.Location = department.PlantName;
                    var operation = await _operationService.Operation(machine.MachineOperationListId);
                    mctime.OprationNo = operation.Operation;
                    foreach (var machinetype in machineTypes)
                    {
                        if (mctime.MachineTypeId == machinetype.MachineTypeTypeId)
                        {
                            mctime.MachineTypeName = machinetype.MachineTypeName;
                        }
                    }
                }
            }
            string csv = string.Empty;
            csv += "Part No / Desc,Part Type,WO Ref,Location,Opr No,Machine Type,Machine,Plan Qnty,Start Date,End Date,Mc Hrs,Status,Critical Part\r\n";
            foreach (var item in mctimelist)
            {
                csv += $"{item.PartNo}/{item.PartDesc},{item.PartType},{item.WoNumber},{item.Location},{item.OprationNo},{item.MachineTypeName},{item.MachineName},{item.PlanQnty},{item.McPlanStartTimeStr},{item.McPlanEndTimeStr},{item.TotalPlanTime},{""},{""}\r\n";
            }

            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "MachineListDetailData.csv");
        }


        [HttpGet]
        public async Task<IActionResult> ReloadProductionWo(string reloadoption, long partid)
        {
            List<ProductionPlan_WoVM> listwo = new List<ProductionPlan_WoVM>();
            var productions = await _woService.AllProductionPlan_Wo();
            foreach (var item in productions)
            {
                if (item.ReloadOption == reloadoption && item.PartId == partid)
                {
                    listwo.Add(item);
                }
            }
            return Ok(listwo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProcPlan()
        {
            var resultList = await _woService.GetAllProcPlan();
            var uoms = await _masterService.GetUOMs(); // Fetch UOMs once

            // Gather all distinct PartIds from the result list
            var partIds = resultList.Select(item => (int)item.PartId).Distinct().ToList();

            // Parallel fetching of all ItemMasterParts and PartPurchases
            var masterPartsTasks = partIds.ToDictionary(partId => partId, partId => _masterService.ItemMasterPartById(partId));
            var partPurchasesTasks = partIds.ToDictionary(partId => partId, partId => _masterService.PartPurchasesFor(partId));

            await Task.WhenAll(masterPartsTasks.Values);
            await Task.WhenAll(partPurchasesTasks.Values);

            // Prepare UOM dictionary for quick lookup
            var uomDict = uoms.ToDictionary(uom => uom.UOMId, uom => uom.Name);

            foreach (var item in resultList)
            {
                // Get master part details
                var mp = masterPartsTasks[(int)item.PartId].Result;
                item.PartNo = mp.PartNo;
                item.PartDesc = mp.PartDescription;

                // Get UOM name if available
                if (uomDict.TryGetValue(item.UOMId, out var uomName))
                {
                    item.UomName = uomName;
                }

                // Process purchase details
                var mfpdList = partPurchasesTasks[(int)item.PartId].Result;
                var relevantPurchases = mfpdList.Where(purs => item.PartId == purs.PPartId).ToList();

                if (relevantPurchases.Any())
                {
                    item.Supplier = relevantPurchases.First().PSupplier;
                    item.SupplierId = relevantPurchases.First().PSupplierId;
                    item.LeadTimeInDays = relevantPurchases.Sum(x => x.LeadTimeInDays).ToString();
                    item.Moq = relevantPurchases.Sum(x => x.MinimumOrderQuantity);
                }
            }

            return Ok(resultList);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBomlist()
        {
            // Fetch all BOM list and Work Orders in parallel
            var resultListTask = _woService.GetAllBomlist();
            var workOrdersTask = _baService.AllWorkOrders();

            await Task.WhenAll(resultListTask, workOrdersTask);

            var resultList = resultListTask.Result;
            var workOrders = workOrdersTask.Result;

            // Prepare a dictionary for fast lookup of Work Orders by ID
            var workOrdersDict = workOrders.ToDictionary(wo => wo.WOID, wo => wo.WONumber);

            // Get all unique Child_Part_No_IDs from the result list
            var partIds = resultList.Select(item => (int)item.Child_Part_No_ID).Distinct().ToList();

            // Fetch all necessary ItemMasterParts in parallel
            var masterPartsTasks = partIds.ToDictionary(partId => partId, partId => _masterService.ItemMasterPartById(partId));
            await Task.WhenAll(masterPartsTasks.Values);

            foreach (var item in resultList)
            {
                // Get master part details
                var mp = masterPartsTasks[(int)item.Child_Part_No_ID].Result;
                item.PartNo = mp.PartNo;
                item.PartDesc = mp.PartDescription;

                // Assign Work Order number if it exists in the dictionary
                if (workOrdersDict.TryGetValue(item.ParentWoId, out var woNumber))
                {
                    item.WoNumber = woNumber;
                }
            }

            return Ok(resultList);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMcTimeList()
        {
            var mctimelist = await _woService.GetAllMcTimeList();
            var productions = await _woService.AllProductionPlan_Wo();
            var masterparts = await _masterService.ItemMasterParts();
            foreach (var mctime in mctimelist)
            {
                var machine = await _machineService.GetMachine((int)mctime.MachineId);
                var machineTypes = await _machineService.GetMachineTypes();
                foreach (ProductionPlan_WoVM item in productions)
                {
                    if (item.WoId == mctime.WoId && item.ParentWoId == 0)
                    {
                        foreach (ItemMasterPartVM imp in masterparts)
                        {
                            if (item.PartId == imp.PartId)
                            {
                                mctime.PartNo = imp.PartNo;
                                mctime.PartDesc = imp.Description;
                                mctime.PartType = imp.MasterPartType;
                                mctime.WoNumber = item.WONumber;
                            }
                        }
                    }
                }
                if (mctime.MachineId == machine.MachineMachineId)
                {
                    mctime.MachineName = machine.MachineMachineName;

                    var departments = await _departmentService.GetDepartments(1);
                    var department = departments.FirstOrDefault(d => d.DepartmentId == machine.MachineDepartmentId);
                    mctime.Location = department.PlantName;
                    var operation = await _operationService.Operation(machine.MachineOperationListId);
                    mctime.OprationNo = operation.Operation;
                    foreach (var machinetype in machineTypes)
                    {
                        if (mctime.MachineTypeId == machinetype.MachineTypeTypeId)
                        {
                            mctime.MachineTypeName = machinetype.MachineTypeName;
                        }
                    }
                }
            }
            return Ok(mctimelist);
        }


        [HttpPost]
        public async Task<IActionResult> MultipleProductionUpdateWOPost([FromBody] IEnumerable<ProductionPlan_WoVM> ppwos)
        {
            List<ProductionPlan_WoVM> listprod = new List<ProductionPlan_WoVM>();
            var productions = await _woService.AllProductionPlan_Wo();
            foreach (var item in productions)
            {
                var matchingPpwo = ppwos.FirstOrDefault(ppwo => ppwo.ProductionPlanId == item.ProductionPlanId);
                if (matchingPpwo != null)
                {
                    item.Status = matchingPpwo.Status;
                    listprod.Add(item);
                }
            }
            var procdutionpost = await _woService.ProductionPlanWoPost(listprod);
            return Ok(procdutionpost);
        }

        [HttpPost]
        public async Task<IActionResult> MulitplePOdetails([FromBody] IEnumerable<PODetailsVM> pODetails)
        {
            var postPODetails = await _woService.PODetails(pODetails);
            if (postPODetails.Any())
            {
                var groupedData = postPODetails.GroupBy(x => x.CompanyId)
                                .Select(grp => new POHeaderVM
                                {
                                    SupplierId = grp.Key,
                                    PoHeaderId = 0,
                                    PoDetailsId = grp.Select(x => x.PoDetailsId).FirstOrDefault(),
                                    PartId = grp.Select(x => x.PartId).FirstOrDefault(),
                                })
                                .ToList();
                var postPOHeader = await _woService.POHeader(groupedData);
            }
            return Ok(postPODetails);
        }

        [HttpPost]
        public async Task<IActionResult> MultipleProductionWOPost([FromBody] IEnumerable<ProductionPlan_WoVM> ppwos)
        {
            var procdutionpost = await _woService.ProductionPlanWoPost(ppwos);
            return Ok(procdutionpost);
        }

        [HttpGet]
        public async Task<IActionResult> SplitWo(long woid, string initialDate, int numDays, int quantity, long salersorderId, int partId, int partType)
        {
            var inltialDt = DateTime.Parse(initialDate);
            List<ProductionPlan_WoVM> previousWorkdays = new List<ProductionPlan_WoVM>();
            DateTime currentDate = inltialDt;
            var workdetails = await _plantService.GetPlantWD(1);
            var holidaylist = await _plantService.GetHolidays(1);
            string weekOff1 = workdetails.WeeklyOff1;
            string weekOff2 = workdetails.WeeklyOff2;
            int quantityPerDay = quantity / numDays;

            for (int i = 0; i < numDays; i++)
            {
                do
                {
                    currentDate = currentDate.AddDays(-1);
                } while (!IsWorkDay(currentDate, holidaylist, weekOff1, weekOff2));

                ProductionPlan_WoVM dailywo = new ProductionPlan_WoVM
                {
                    ParentWoId = woid,
                    SalesOrderId = salersorderId,
                    CalcWOQty = quantityPerDay,
                    PlanCompletionDate = currentDate,
                    PartId = partId,
                    PartType = partType,
                    //For_Ref = 'N',
                    ReloadOption = "Split"
                };
                previousWorkdays.Add(dailywo);
            }
            //var postWO = await _baService.MultiplePostWO(previousWorkdays);
            var procdutionpost = await _woService.ProductionPlanWoPost(previousWorkdays);
            return Ok(procdutionpost);
        }

        [HttpPost]
        public async Task<IActionResult> ProductionPlanPost(ProductionPlan_WoVM production)
        {
            List<ProductionPlan_WoVM> productions = new List<ProductionPlan_WoVM>();
            if (production.PartType == 1)
            {
                production.Parentlevel = 'N';
            }
            else
            {
                var mpBOM = await _masterService.BOMS(production.PartId.ToString());
                if (mpBOM != null && mpBOM.Any())
                {
                    foreach (var bom in mpBOM)
                    {
                        var assy = await _masterService.GetManufPart((int)bom.BOMPartId);
                        if (assy != null)
                        {
                            production.Parentlevel = 'Y';
                        }
                        else
                        {
                            production.Parentlevel = 'N';
                        }
                    }
                }
                else
                {
                    production.Parentlevel = 'Y';
                }
            }
            productions.Add(production);
            var procdutionpost = await _woService.ProductionPlanWoPost(productions);
            return Ok(procdutionpost);
        }

        [HttpGet]
        public async Task<IActionResult> CalculateWOQuantity(string dispatchStartDate, string soCompletionDate, int balanceToManufacture, string dispatchOption)
        {
            var dispatchStartDt = DateTime.Parse(dispatchStartDate);
            var soCompletionDt = DateTime.Parse(soCompletionDate);

            var workdetails = await _plantService.GetPlantWD(1);
            var holidaylist = await _plantService.GetHolidays(1);
            string weekOff1 = workdetails.WeeklyOff1;
            string weekOff2 = workdetails.WeeklyOff2;

            int workDays = 0;

            for (var date = dispatchStartDt; date <= soCompletionDt; date = date.AddDays(1))
            {
                // Check if the day is a work day
                if (IsWorkDay(date, holidaylist, weekOff1, weekOff2))
                {
                    workDays++;
                }
            }

            int noOfWeeks = (int)Math.Ceiling((double)workDays / 7); // calculate number of weeks
            int noOfMonths = (int)Math.Ceiling((double)workDays / 30); // calculate number of months

            List<WorkOrdersVM> woDetails = new List<WorkOrdersVM>();

            switch (dispatchOption)
            {
                case "Daily":
                    int workDaysAdjusted = 0;
                    DateTime tempDate = dispatchStartDt.AddDays(1);
                    while (workDaysAdjusted < workDays)
                    {
                        if (!IsNonWorkingDay(tempDate, weekOff1, weekOff2, holidaylist))
                        {
                            workDaysAdjusted++;
                            WorkOrdersVM dailywo = new WorkOrdersVM
                            {
                                WONumber ="",
                                CalcWOQty = balanceToManufacture / workDays,
                                PlanCompletionDate = tempDate
                            };
                            woDetails.Add(dailywo);
                        }
                        tempDate = tempDate.AddDays(1);
                    }
                    break;
                case "Weekly":
                    //if (workDays > 22)
                    //{
                    int noOfWeeksAdjusted = 0;
                    tempDate = dispatchStartDt.AddDays(7);
                    while (noOfWeeksAdjusted < noOfWeeks)
                    {
                        int weeklyWoQuantity = balanceToManufacture / noOfWeeks;
                        while (!IsWorkDay(tempDate, holidaylist, weekOff1, weekOff2))
                        {
                            tempDate = tempDate.AddDays(1);
                        }
                        WorkOrdersVM wo = new WorkOrdersVM
                        {
                            WONumber = "",
                            CalcWOQty = weeklyWoQuantity,
                            PlanCompletionDate = tempDate
                        };
                        woDetails.Add(wo);
                        tempDate = tempDate.AddDays(7);
                        noOfWeeksAdjusted++;
                    }
                    //}
                    //else
                    //{
                    //    return BadRequest("Error: Number of work days is less than 22. Please use Manual Multiple selection.");
                    //}
                    break;
                case "Monthly":
                    if (workDays > 95)
                    {
                        int noOfMonthsAdjusted = 0;
                        tempDate = dispatchStartDt.AddMonths(1);
                        while (noOfMonthsAdjusted < noOfMonths)
                        {
                            int monthlyWoQuantity = balanceToManufacture / noOfMonths;

                            while (!IsWorkDay(tempDate, holidaylist, weekOff1, weekOff2))
                            {
                                tempDate = tempDate.AddDays(1);
                            }
                            WorkOrdersVM monthlywo = new WorkOrdersVM
                            {
                                WONumber = "",
                                CalcWOQty = monthlyWoQuantity,
                                PlanCompletionDate = tempDate
                            };
                            woDetails.Add(monthlywo);
                            tempDate = tempDate.AddMonths(1);
                            noOfMonthsAdjusted++;
                        }
                    }
                    else
                    {
                        return BadRequest("Error: Number of work days is less than 95. Please use Manual Multiple selection.");
                    }
                    break;
                default:
                    return BadRequest("Error: Invalid dispatch option.");
            }

            return Ok(woDetails);
        }

        private bool IsWorkDay(DateTime date, IEnumerable<HolidayVM> holidaylist, string weekOff1, string weekOff2)
        {
            // Check if the day is a holiday
            if (holidaylist.Any(h => h.HolidayDate == date))
            {
                return false;
            }

            // Check if the day is a weekly off
            var dayOfWeek = date.DayOfWeek.ToString();
            if (dayOfWeek == weekOff1 || dayOfWeek == weekOff2)
            {
                return false;
            }

            // If none of the above conditions are true, it's a work day
            return true;
        }

        bool IsNonWorkingDay(DateTime date, string weekOff1, string weekOff2, IEnumerable<HolidayVM> holidaylist)
        {
            DayOfWeek day = date.DayOfWeek;
            string dayName = date.DayOfWeek.ToString();

            //if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
            //{
            //    return true; // Weekend
            //}
            if (dayName == weekOff1 || dayName == weekOff2)
            {
                return true; // Weekoff1 or Weekoff2
            }
            else if (holidaylist.Any(h => h.HolidayDate == date.Date))
            {
                return true; // Holiday
            }
            return false; // Working day
        }
    }
}
