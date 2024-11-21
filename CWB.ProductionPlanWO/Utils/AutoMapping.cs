using AutoMapper;
using CWB.ProductionPlanWO.Domain;
using CWB.ProductionPlanWO.ViewModels;

namespace CWB.ProductionPlanWO.Utils
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<WorkOrders, WorkOrdersVM>()
               .ForMember(m => m.WOID, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.SalesOrderId))
               .ForMember(m => m.WONumber, m => m.MapFrom(src => src.WONumber))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.PartType, m => m.MapFrom(src => src.PartType))
               .ForMember(m => m.Parentlevel, m => m.MapFrom(src => src.Parentlevel))
               .ForMember(m => m.CalcWOQty, m => m.MapFrom(src => src.CalcWOQty))
               .ForMember(m => m.BuildToStock, m => m.MapFrom(src => src.BuildToStock))
               .ForMember(m => m.PlanCompletionDate, m => m.MapFrom(src => src.PlanCompletionDate))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
               .ForMember(m => m.RoutingId, m => m.MapFrom(src => src.RoutingId))
               .ForMember(m => m.StartingOpNo, m => m.MapFrom(src => src.StartingOpNo))
               .ForMember(m => m.EndingOpNo, m => m.MapFrom(src => src.EndingOpNo))
               .ForMember(m => m.For_Ref, m => m.MapFrom(src => src.For_Ref))
               .ForMember(m => m.ReloadOption, m => m.MapFrom(src => src.ReloadOption))
               .ForMember(m => m.Active, m => m.MapFrom(src => src.Active))
               .ForMember(m => m.ParentWoId, m => m.MapFrom(src => src.ParentWoId))
               .ForMember(m => m.WODate, m => m.MapFrom(src => src.WODate));

            CreateMap<WorkOrdersVM, WorkOrders>()
               .ForMember(m => m.Id, m => m.MapFrom(src => src.WOID))
               .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.SalesOrderId))
               .ForMember(m => m.WONumber, m => m.MapFrom(src => src.WONumber))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.PartType, m => m.MapFrom(src => src.PartType))
               .ForMember(m => m.Parentlevel, m => m.MapFrom(src => src.Parentlevel))
               .ForMember(m => m.CalcWOQty, m => m.MapFrom(src => src.CalcWOQty))
               .ForMember(m => m.BuildToStock, m => m.MapFrom(src => src.BuildToStock))
               .ForMember(m => m.PlanCompletionDate, m => m.MapFrom(src => src.PlanCompletionDate))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
               .ForMember(m => m.WODate, m => m.MapFrom(src => src.WODate))
               .ForMember(m => m.RoutingId, m => m.MapFrom(src => src.RoutingId))
               .ForMember(m => m.StartingOpNo, m => m.MapFrom(src => src.StartingOpNo))
               .ForMember(m => m.EndingOpNo, m => m.MapFrom(src => src.EndingOpNo))
               .ForMember(m => m.For_Ref, m => m.MapFrom(src => src.For_Ref))
               .ForMember(m => m.ReloadOption, m => m.MapFrom(src => src.ReloadOption))
               .ForMember(m => m.Active, m => m.MapFrom(src => src.Active))
               .ForMember(m => m.ParentWoId, m => m.MapFrom(src => src.ParentWoId))
               .ForMember(m => m.WODate, m => m.MapFrom(src => src.WODate));

            CreateMap<WOSOVM, WOSO>()
               .ForMember(m => m.Id, m => m.MapFrom(src => src.WOSOId))
               .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.SalesOrderId))
               .ForMember(m => m.Active, m => m.MapFrom(src => src.Active))
               .ForMember(m => m.WorkOrderId, m => m.MapFrom(src => src.WorkOrderId));

            CreateMap<WOSO, WOSOVM>()
               .ForMember(m => m.WOSOId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.SalesOrderId))
               .ForMember(m => m.Active, m => m.MapFrom(src => src.Active))
               .ForMember(m => m.WorkOrderId, m => m.MapFrom(src => src.WorkOrderId));

            CreateMap<BOMTempVM, BOMTemp>()
              .ForMember(m => m.Id, m => m.MapFrom(src => src.BomTempId))
              .ForMember(m => m.WorkOrderId, m => m.MapFrom(src => src.WorkOrderId))
              .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
              .ForMember(m => m.PartType, m => m.MapFrom(src => src.PartType))
              .ForMember(m => m.Parentlevel, m => m.MapFrom(src => src.Parentlevel));

            CreateMap<BOMTemp, BOMTempVM>()
             .ForMember(m => m.BomTempId, m => m.MapFrom(src => src.Id))
             .ForMember(m => m.WorkOrderId, m => m.MapFrom(src => src.WorkOrderId))
             .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
             .ForMember(m => m.PartType, m => m.MapFrom(src => src.PartType))
             .ForMember(m => m.Parentlevel, m => m.MapFrom(src => src.Parentlevel));

            CreateMap<ProcPlanVM, ProcPlan>()
            .ForMember(m => m.Id, m => m.MapFrom(src => src.ProcPlanId))
            .ForMember(m => m.Reference, m => m.MapFrom(src => src.Reference))
            .ForMember(m => m.TestData, m => m.MapFrom(src => src.TestData))
            .ForMember(m => m.For_Ref, m => m.MapFrom(src => src.For_Ref))
            .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
            .ForMember(m => m.WorkOrderId, m => m.MapFrom(src => src.WorkOrderId))
            .ForMember(m => m.PartType, m => m.MapFrom(src => src.PartType))
            .ForMember(m => m.UOMId, m => m.MapFrom(src => src.UOMId))
            .ForMember(m => m.Calc_Proc_Qnty, m => m.MapFrom(src => src.Calc_Proc_Qnty))
            .ForMember(m => m.AddnOtyUser, m => m.MapFrom(src => src.AddnOtyUser))
            .ForMember(m => m.Plan_Proc_Qnty, m => m.MapFrom(src => src.Plan_Proc_Qnty))
            .ForMember(m => m.PlanReceiptDate, m => m.MapFrom(src => src.PlanReceiptDate))
            .ForMember(m => m.CalcReceiptDate, m => m.MapFrom(src => src.CalcReceiptDate))
            .ForMember(m => m.CriticalPart, m => m.MapFrom(src => src.CriticalPart))
            .ForMember(m => m.Changed, m => m.MapFrom(src => src.Changed))
            .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));

            CreateMap<ProcPlan, ProcPlanVM> ()
            .ForMember(m => m.ProcPlanId, m => m.MapFrom(src => src.Id))
            .ForMember(m => m.Reference, m => m.MapFrom(src => src.Reference))
            .ForMember(m => m.TestData, m => m.MapFrom(src => src.TestData))
            .ForMember(m => m.For_Ref, m => m.MapFrom(src => src.For_Ref))
            .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
            .ForMember(m => m.WorkOrderId, m => m.MapFrom(src => src.WorkOrderId))
            .ForMember(m => m.PartType, m => m.MapFrom(src => src.PartType))
            .ForMember(m => m.UOMId, m => m.MapFrom(src => src.UOMId))
            .ForMember(m => m.Calc_Proc_Qnty, m => m.MapFrom(src => src.Calc_Proc_Qnty))
            .ForMember(m => m.AddnOtyUser, m => m.MapFrom(src => src.AddnOtyUser))
            .ForMember(m => m.Plan_Proc_Qnty, m => m.MapFrom(src => src.Plan_Proc_Qnty))
            .ForMember(m => m.PlanReceiptDate, m => m.MapFrom(src => src.PlanReceiptDate))
            .ForMember(m => m.CalcReceiptDate, m => m.MapFrom(src => src.CalcReceiptDate))
            .ForMember(m => m.CriticalPart, m => m.MapFrom(src => src.CriticalPart))
            .ForMember(m => m.Changed, m => m.MapFrom(src => src.Changed))
            .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));

            CreateMap<BOMListVM, BOMList>()
            .ForMember(m => m.Id, m => m.MapFrom(src => src.BomListId))
            .ForMember(m => m.ParentWoId, m => m.MapFrom(src => src.ParentWoId))
            .ForMember(m => m.Child_Part_No_ID, m => m.MapFrom(src => src.Child_Part_No_ID))
            .ForMember(m => m.Child_Part_No_Type, m => m.MapFrom(src => src.Child_Part_No_Type))
            .ForMember(m => m.Manf_RM_Link_ID, m => m.MapFrom(src => src.Manf_RM_Link_ID))
            .ForMember(m => m.Calc_Qnty, m => m.MapFrom(src => src.Calc_Qnty))
            .ForMember(m => m.QtyOnHand, m => m.MapFrom(src => src.QtyOnHand))
            .ForMember(m => m.Plan_Qnty, m => m.MapFrom(src => src.Plan_Qnty))
            .ForMember(m => m.Plan_Start_Dt, m => m.MapFrom(src => src.Plan_Start_Dt))
            .ForMember(m => m.Plan_Compl_Dt, m => m.MapFrom(src => src.Plan_Compl_Dt))
            .ForMember(m => m.PlanReceiptDate, m => m.MapFrom(src => src.PlanReceiptDate))
            .ForMember(m => m.CalcReceiptDate, m => m.MapFrom(src => src.CalcReceiptDate))
            .ForMember(m => m.Manf_Days_Avl, m => m.MapFrom(src => src.Manf_Days_Avl))
            .ForMember(m => m.Manf_Days_Reqd, m => m.MapFrom(src => src.Manf_Days_Reqd))
            .ForMember(m => m.CriticalPart, m => m.MapFrom(src => src.CriticalPart))
            .ForMember(m => m.AddnQtyUser, m => m.MapFrom(src => src.AddnQtyUser))
            .ForMember(m => m.ChildWoId, m => m.MapFrom(src => src.ChildWoId))
            .ForMember(m => m.TestData, m => m.MapFrom(src => src.TestData))
            .ForMember(m => m.ProcPlanId, m => m.MapFrom(src => src.ProcPlanId))
            .ForMember(m => m.SaNestLevel, m => m.MapFrom(src => src.SaNestLevel))
            .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));

            CreateMap<BOMList, BOMListVM>()
             .ForMember(m => m.BomListId, m => m.MapFrom(src => src.Id))
             .ForMember(m => m.ParentWoId, m => m.MapFrom(src => src.ParentWoId))
             .ForMember(m => m.Child_Part_No_ID, m => m.MapFrom(src => src.Child_Part_No_ID))
             .ForMember(m => m.Child_Part_No_Type, m => m.MapFrom(src => src.Child_Part_No_Type))
             .ForMember(m => m.Manf_RM_Link_ID, m => m.MapFrom(src => src.Manf_RM_Link_ID))
             .ForMember(m => m.Calc_Qnty, m => m.MapFrom(src => src.Calc_Qnty))
             .ForMember(m => m.QtyOnHand, m => m.MapFrom(src => src.QtyOnHand))
             .ForMember(m => m.Plan_Qnty, m => m.MapFrom(src => src.Plan_Qnty))
             .ForMember(m => m.Plan_Start_Dt, m => m.MapFrom(src => src.Plan_Start_Dt))
             .ForMember(m => m.Plan_Compl_Dt, m => m.MapFrom(src => src.Plan_Compl_Dt))
             .ForMember(m => m.PlanReceiptDate, m => m.MapFrom(src => src.PlanReceiptDate))
             .ForMember(m => m.CalcReceiptDate, m => m.MapFrom(src => src.CalcReceiptDate))
             .ForMember(m => m.Manf_Days_Avl, m => m.MapFrom(src => src.Manf_Days_Avl))
             .ForMember(m => m.Manf_Days_Reqd, m => m.MapFrom(src => src.Manf_Days_Reqd))
             .ForMember(m => m.CriticalPart, m => m.MapFrom(src => src.CriticalPart))
             .ForMember(m => m.AddnQtyUser, m => m.MapFrom(src => src.AddnQtyUser))
             .ForMember(m => m.ChildWoId, m => m.MapFrom(src => src.ChildWoId))
             .ForMember(m => m.TestData, m => m.MapFrom(src => src.TestData))
             .ForMember(m => m.ProcPlanId, m => m.MapFrom(src => src.ProcPlanId))
             .ForMember(m => m.SaNestLevel, m => m.MapFrom(src => src.SaNestLevel))
             .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));


            CreateMap<ProductionPlan_WO, ProductionPlan_WOVM>()
               .ForMember(m => m.ProductionPlanId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.PPNumber, m => m.MapFrom(src => src.PPNumber))
               .ForMember(m => m.WoId, m => m.MapFrom(src => src.WoId))
               .ForMember(m => m.ParentWoId, m => m.MapFrom(src => src.ParentWoId))
               .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.SalesOrderId))
               .ForMember(m => m.WONumber, m => m.MapFrom(src => src.WONumber))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.PartType, m => m.MapFrom(src => src.PartType))
               .ForMember(m => m.Parentlevel, m => m.MapFrom(src => src.Parentlevel))
               .ForMember(m => m.CalcWOQty, m => m.MapFrom(src => src.CalcWOQty))
               .ForMember(m => m.BuildToStock, m => m.MapFrom(src => src.BuildToStock))
               .ForMember(m => m.PlanCompletionDate, m => m.MapFrom(src => src.PlanCompletionDate))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
               .ForMember(m => m.RoutingId, m => m.MapFrom(src => src.RoutingId))
               .ForMember(m => m.StartingOpNo, m => m.MapFrom(src => src.StartingOpNo))
               .ForMember(m => m.EndingOpNo, m => m.MapFrom(src => src.EndingOpNo))
               .ForMember(m => m.For_Ref, m => m.MapFrom(src => src.For_Ref))
               .ForMember(m => m.Active, m => m.MapFrom(src => src.Active))
               .ForMember(m => m.WODate, m => m.MapFrom(src => src.WODate));

            CreateMap<ProductionPlan_WOVM, ProductionPlan_WO>()
               .ForMember(m => m.Id, m => m.MapFrom(src => src.ProductionPlanId))
               .ForMember(m => m.PPNumber, m => m.MapFrom(src => src.PPNumber))
               .ForMember(m => m.WoId, m => m.MapFrom(src => src.WoId))
               .ForMember(m => m.ParentWoId, m => m.MapFrom(src => src.ParentWoId))
               .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.SalesOrderId))
               .ForMember(m => m.WONumber, m => m.MapFrom(src => src.WONumber))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.PartType, m => m.MapFrom(src => src.PartType))
               .ForMember(m => m.Parentlevel, m => m.MapFrom(src => src.Parentlevel))
               .ForMember(m => m.CalcWOQty, m => m.MapFrom(src => src.CalcWOQty))
               .ForMember(m => m.BuildToStock, m => m.MapFrom(src => src.BuildToStock))
               .ForMember(m => m.PlanCompletionDate, m => m.MapFrom(src => src.PlanCompletionDate))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
               .ForMember(m => m.RoutingId, m => m.MapFrom(src => src.RoutingId))
               .ForMember(m => m.StartingOpNo, m => m.MapFrom(src => src.StartingOpNo))
               .ForMember(m => m.EndingOpNo, m => m.MapFrom(src => src.EndingOpNo))
               .ForMember(m => m.For_Ref, m => m.MapFrom(src => src.For_Ref))
               .ForMember(m => m.Active, m => m.MapFrom(src => src.Active))
               .ForMember(m => m.WODate, m => m.MapFrom(src => src.WODate));


            CreateMap<WOStatusVM, WOStatus>()
               .ForMember(m => m.Id, m => m.MapFrom(src => src.StatusId))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status));

            CreateMap<WOStatus, WOStatusVM>()
               .ForMember(m => m.StatusId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status));

            CreateMap<ChildWoRelVM, ChildWoRel>()
               .ForMember(m => m.Id, m => m.MapFrom(src => src.ChildWoRelId))
               .ForMember(m => m.WoId, m => m.MapFrom(src => src.WoId))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.Qnty, m => m.MapFrom(src => src.Qnty))
               .ForMember(m => m.CameFrom, m => m.MapFrom(src => src.CameFrom));

            CreateMap<ChildWoRel, ChildWoRelVM>()
                .ForMember(m => m.ChildWoRelId, m => m.MapFrom(src => src.Id))
                .ForMember(m => m.WoId, m => m.MapFrom(src => src.WoId))
                .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
                .ForMember(m => m.Qnty, m => m.MapFrom(src => src.Qnty))
                .ForMember(m => m.CameFrom, m => m.MapFrom(src => src.CameFrom));

            CreateMap<McTimeListVM, McTimeList>()
           .ForMember(m => m.Id, m => m.MapFrom(src => src.McTimeListId))
           .ForMember(m => m.WoId, m => m.MapFrom(src => src.WoId))
           .ForMember(m => m.Routing_StepId, m => m.MapFrom(src => src.Routing_StepId))
           .ForMember(m => m.CompanyId, m => m.MapFrom(src => src.CompanyId))
           .ForMember(m => m.MachineId, m => m.MapFrom(src => src.MachineId))
           .ForMember(m => m.MachineTypeId, m => m.MapFrom(src => src.MachineTypeId))
           .ForMember(m => m.PlanQnty, m => m.MapFrom(src => src.PlanQnty))
           .ForMember(m => m.TotalPlanTime, m => m.MapFrom(src => src.TotalPlanTime))
           .ForMember(m => m.McPlanStartTime, m => m.MapFrom(src => src.McPlanStartTime))
           .ForMember(m => m.McPlanEndTime, m => m.MapFrom(src => src.McPlanEndTime))
           .ForMember(m => m.McActStartTime, m => m.MapFrom(src => src.McActStartTime))
           .ForMember(m => m.McActEndTime, m => m.MapFrom(src => src.McActEndTime))
           .ForMember(m => m.ActQnty, m => m.MapFrom(src => src.ActQnty))
           .ForMember(m => m.TotalActTime, m => m.MapFrom(src => src.TotalActTime))
           .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));

            CreateMap<McTimeList, McTimeListVM>()
            .ForMember(m => m.McTimeListId, m => m.MapFrom(src => src.Id))
           .ForMember(m => m.WoId, m => m.MapFrom(src => src.WoId))
           .ForMember(m => m.Routing_StepId, m => m.MapFrom(src => src.Routing_StepId))
           .ForMember(m => m.CompanyId, m => m.MapFrom(src => src.CompanyId))
           .ForMember(m => m.MachineId, m => m.MapFrom(src => src.MachineId))
           .ForMember(m => m.MachineTypeId, m => m.MapFrom(src => src.MachineTypeId))
           .ForMember(m => m.PlanQnty, m => m.MapFrom(src => src.PlanQnty))
           .ForMember(m => m.TotalPlanTime, m => m.MapFrom(src => src.TotalPlanTime))
           .ForMember(m => m.McPlanStartTime, m => m.MapFrom(src => src.McPlanStartTime))
           .ForMember(m => m.McPlanEndTime, m => m.MapFrom(src => src.McPlanEndTime))
           .ForMember(m => m.McActStartTime, m => m.MapFrom(src => src.McActStartTime))
           .ForMember(m => m.McActEndTime, m => m.MapFrom(src => src.McActEndTime))
           .ForMember(m => m.ActQnty, m => m.MapFrom(src => src.ActQnty))
           .ForMember(m => m.TotalActTime, m => m.MapFrom(src => src.TotalActTime))
           .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));



            CreateMap<POStatusVM, POStatus>()
               .ForMember(m => m.Id, m => m.MapFrom(src => src.StatusId))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status));

            CreateMap<POStatus, POStatusVM>()
               .ForMember(m => m.StatusId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status));

            CreateMap<PODetailsVM, PODetails>()
          .ForMember(m => m.Id, m => m.MapFrom(src => src.PoDetailsId))
          .ForMember(m => m.POReference, m => m.MapFrom(src => src.POReference))
          .ForMember(m => m.ProcPlanId, m => m.MapFrom(src => src.ProcPlanId))
          .ForMember(m => m.AddHocPO, m => m.MapFrom(src => src.AddHocPO))
          .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
          .ForMember(m => m.PoQnty, m => m.MapFrom(src => src.PoQnty))
          .ForMember(m => m.PoDate, m => m.MapFrom(src => src.PoDate))
          .ForMember(m => m.CompanyId, m => m.MapFrom(src => src.CompanyId))
          .ForMember(m => m.PlanPoReceiptDate, m => m.MapFrom(src => src.PlanPoReceiptDate))
          .ForMember(m => m.PoSent, m => m.MapFrom(src => src.PoSent))
          .ForMember(m => m.PoQntyRecd, m => m.MapFrom(src => src.PoQntyRecd))
          .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
          .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));

            CreateMap<PODetails, PODetailsVM>()
            .ForMember(m => m.PoDetailsId, m => m.MapFrom(src => src.Id))
          .ForMember(m => m.POReference, m => m.MapFrom(src => src.POReference))
          .ForMember(m => m.ProcPlanId, m => m.MapFrom(src => src.ProcPlanId))
          .ForMember(m => m.AddHocPO, m => m.MapFrom(src => src.AddHocPO))
          .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
          .ForMember(m => m.PoQnty, m => m.MapFrom(src => src.PoQnty))
          .ForMember(m => m.PoDate, m => m.MapFrom(src => src.PoDate))
          .ForMember(m => m.CompanyId, m => m.MapFrom(src => src.CompanyId))
          .ForMember(m => m.PlanPoReceiptDate, m => m.MapFrom(src => src.PlanPoReceiptDate))
          .ForMember(m => m.PoSent, m => m.MapFrom(src => src.PoSent))
          .ForMember(m => m.PoQntyRecd, m => m.MapFrom(src => src.PoQntyRecd))
          .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
          .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));


            CreateMap<POHeaderVM, POHeader>()
          .ForMember(m => m.Id, m => m.MapFrom(src => src.PoHeaderId))
          .ForMember(m => m.PoDetailsId, m => m.MapFrom(src => src.PoDetailsId))
          .ForMember(m => m.SupplierId, m => m.MapFrom(src => src.SupplierId))
          .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
          .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));

            CreateMap<POHeader, POHeaderVM>()
          .ForMember(m => m.PoHeaderId, m => m.MapFrom(src => src.Id))
          .ForMember(m => m.PoDetailsId, m => m.MapFrom(src => src.PoDetailsId))
          .ForMember(m => m.SupplierId, m => m.MapFrom(src => src.SupplierId))
          .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
          .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));
        }
    }
}
