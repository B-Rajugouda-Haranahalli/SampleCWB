using CWB.ProductionPlanWO.Domain;
using CWB.ProductionPlanWO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Services
{
    public interface IWOService
    {
        Task<WorkOrdersVM> WorkOrder(WorkOrdersVM workOrdersVM);
        Task<IEnumerable<WorkOrdersVM>> AllWorkOrders(long tenantId);
        Task<IEnumerable<WorkOrdersVM>> AllParentChildWo(long ParentWoId, long tenantId);
        Task<IEnumerable<WOSOVM>> GetSoWo(long workOrderId);
        Task<WorkOrdersVM> GetSingleWO(long Id, long tenantId);
        Task<List<WorkOrdersVM>> MultipleWorkOrder(List<WorkOrdersVM> workOrdersVM);
        Task<List<WorkOrdersVM>> UpdateMultipleWorkOrder(List<WorkOrdersVM> workOrdersVM);
        Task<List<WOSOVM>> PostWOSO(List<WOSOVM> woso);
        Task<List<ProcPlanVM>> PostProcPlan(List<ProcPlanVM> proc);
        Task<List<BOMListVM>> PostBomList(List<BOMListVM> bomlist);
        Task<List<BOMTempVM>> BOMTempPost(List<BOMTempVM> bomVm);
        Task<IEnumerable<ProcPlanVM>> AllProcPlan(long tenantId);
        Task<IEnumerable<BOMListVM>> AllBomList(long tenantId);
        Task<List<ProductionPlan_WOVM>> PostProductionPlan_Wo(List<ProductionPlan_WOVM> productions);
        Task<IEnumerable<ProductionPlan_WOVM>> AllProductionWo(long tenantId);
        Task<WOStatusVM> GetWOStatus(long Id);
        Task<List<ChildWoRelVM>> PostChildWoRel(List<ChildWoRelVM> childWos);
        Task<List<McTimeListVM>> PostMcTimeList(List<McTimeListVM> mcTimeLists);
        Task<IEnumerable<McTimeListVM>> GetAllMcTimeListVMs(long tenantId);
        Task<POStatusVM> GetPOStatus(long Id);
        Task<List<PODetailsVM>> MultiplePODetails(List<PODetailsVM> pODetailsVM);
        Task<List<POHeaderVM>> MultiplePOHeaders(List<POHeaderVM> pOHeaderVMs);
        string HelloWorld();
    }
}
