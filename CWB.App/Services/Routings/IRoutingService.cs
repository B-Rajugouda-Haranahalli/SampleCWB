using CWB.App.Models.Contacts;
using CWB.App.Models.ItemMaster;
using CWB.App.Models.Routing;
using CWB.App.Models.Routings;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.Routings
{
    public interface IRoutingService
    {
        Task<List<RoutingListItemVM>> GetRoutingListItems();
        Task<RoutingVM> Routing(RoutingVM routingVM);
        Task<RoutingVM> AltRouting(RoutingVM routingVM);
        
        Task<RoutingVM> DeleteRouting (int routingId);
        Task<RoutingVM> DeleteWS(int subConWSId);
        Task<RoutingVM> PreferredRouting(RoutingVM routingVM);
        Task<SubConDetailsVM> PreferredSubCon(int subConDetailsId);
        Task<RoutingStepVM> RoutingStep(RoutingStepVM routingStepVM);
        Task<IEnumerable<RoutingStepVM>> ChangeRoutingStepSequence(IEnumerable<RoutingStepVM> routingStepVMs);
        Task<bool> DeleteStep(int stepId);
        Task<bool> DeleteMachine(int stepId, int machineId);
        Task<bool> DeleteSupplier(int stepId, int supplierId);

        Task<RoutingStepPartVM> RoutingStepPart(RoutingStepPartVM routingStepPartVM);
        Task<RoutingStepSupplierVM> RoutingStepSupplier(RoutingStepSupplierVM routingStepSupplierVM);
        Task<RoutingStepMachineVM> RoutingStepMachine(RoutingStepMachineVM routingStepMachineVM);
        Task<RoutingStepMachineVM> PreferredStepMachine(string routingStepId, string routingStepMachineId, int maxMachineCount);
        

        Task<IEnumerable<RoutingStepVM>> RoutingSteps(int routingId);
        Task<IEnumerable<RoutingVM>> Routings(int manufPartId);
        
        Task<IEnumerable<RoutingStepPartVM>> StepParts(int stepId);
        Task<IEnumerable<RoutingStatusLogVM>> GetRoutingStatusLog(long routingId);
        Task<IEnumerable<RoutingStepPartVM>> StepPartsByManufId(int manufId);
        Task<IEnumerable<RoutingStepSupplierVM>> StepSuppliers(int stepId);
        Task<IEnumerable<RoutingStepMachineVM>> StepMachines(int stepId);

        Task<IEnumerable<SubConDetailsVM>> SubCons(int stepId);
        Task<IEnumerable<SubConWorkStepDetailsVM>> SubConWSS(int stepId,int subConDetailsId);

        Task<bool> DeleteStepPart(int stepId, int stepPartId);
        Task<bool> DeleteSubCon(int stepId, int subConDetailsId);
        Task<bool> DeleteSubConWS(int stepId, int subConWSDetailsId);

        Task<SubConDetailsVM> SubConDetails(SubConDetailsVM subConDetailsVM);
        Task<SubConWorkStepDetailsVM> SubConWSDetails(SubConWorkStepDetailsVM subConWorkStepDetailsVM);


    }
}
