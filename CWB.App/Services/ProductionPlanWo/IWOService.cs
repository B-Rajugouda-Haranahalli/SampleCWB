using CWB.App.Models.BusinessProcesses;
using CWB.App.Models.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Services.ProductionPlanWo
{
    public interface IWOService
    {
        Task<IEnumerable<WOSOVM>> GetSoWoRel(long workOrderId);
        Task<List<ProductionPlan_WoVM>> ProductionPlanWoPost(IEnumerable<ProductionPlan_WoVM> productions);
        Task<IEnumerable<ProductionPlan_WoVM>> AllProductionPlan_Wo();
        Task<List<ProcPlanVM>> ProcPlanPost(IEnumerable<ProcPlanVM> procPlans);
        Task<List<WorkOrdersVM>> UpdateMultipleWorkOrder(IEnumerable<WorkOrdersVM> workOrders);
        Task<List<BOMListVM>> BomListPost(IEnumerable<BOMListVM> bomlist);
        Task<IEnumerable<ProcPlanVM>> GetAllProcPlan();
        Task<IEnumerable<BOMListVM>> GetAllBomlist();
        Task<WOStatusVM> GetWOStatus(long Id);
        Task<List<ChildWoRelVM>> PostChildWoRel(IEnumerable<ChildWoRelVM> childWoRels);
        Task<List<McTimeListVM>> PostMcTimeList(IEnumerable<McTimeListVM> mcTimeListVMs);
        Task<IEnumerable<McTimeListVM>> GetAllMcTimeList();
        Task<IEnumerable<WorkOrdersVM>> AllParentChildWos(long parentWoId);

        Task<List<PODetailsVM>> PODetails(IEnumerable<PODetailsVM> pODetails);
        Task<List<POHeaderVM>> POHeader(IEnumerable<POHeaderVM> pOHeaderVMs);

    }
}
