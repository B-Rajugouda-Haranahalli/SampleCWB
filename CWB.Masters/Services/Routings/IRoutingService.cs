using CWB.Masters.ViewModels.Routings;
using CWB.Masters.Domain.Routings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Services.Routings
{
    public interface IRoutingService
    {
        Task<RoutingVM> Routing(RoutingVM routingVM);
        Task<RoutingVM> AltRouting(RoutingVM routingVM);
        Task CopySteps(int fromRoutingId, int toRoutingId);
        Task CopyStepMachines(int fromRoutingId, int toRoutingId);
        Task CopySubCons(int fromRoutingId, int toRoutingId);
        Task CopySubConsWSs(int fromRoutingId, int toRoutingId);
        Task CopyStepParts(int fromRoutingId, int toRoutingId);



        Task<RoutingVM> DeleteRouting(int routingId);
        
        Task<RoutingVM> PreferredRouting(RoutingVM routingVM);
        Task<RoutingStepVM> RoutingStep(RoutingStepVM routingStepVM);
        IEnumerable<RoutingStatusLogVM> GetRoutingStatusLog(long routingId, long tenantId);
        Task<RoutingStatusLogVM> PostRoutingStatusLog(RoutingStatusLogVM routingStatusLogVM);
        Task<IEnumerable<RoutingStepVM>> ChangeSequence(IEnumerable<RoutingStepVM> routingStepVM);
        Task<RoutingStepPartVM> RoutingStepPart(RoutingStepPartVM routingStepPartVM);
        IEnumerable<RoutingVM> GetRoutingsForManufId(int manufId);
        IEnumerable<RoutingStepVM> GetStepsForRoutingId(int routingId);
        IEnumerable<RoutingStepPartVM> GetPartsForStepId(int stepId);
        Task<bool> DeleteStep(int stepId);
        Task<bool> DeleteStepMachine(int stepId, int machineId);
        Task<bool> DeleteStepPart(int stepId, int stepPartId);
        Task<bool> DeleteStepSupplier(int stepId,int supplierId);
        Task<RoutingStepVM> GetStep(int stepId);
        IEnumerable<RoutingStepPartVM> GetPartsForManufId(int manufID);
        Task<IEnumerable<Routing>> GetAllRoutings();

        Task<IEnumerable<RoutingStepMachineVM>> StepMachines(int stepId);
        Task<IEnumerable<RoutingStepSupplierVM>> StepSuppliers(int stepId);

        Task<RoutingStepMachineVM> PreferredStepMachine(string routingStepId, string routingStepMachineId,int maxMachineCount);
        Task<RoutingStepMachineVM> RoutingStepMachine(RoutingStepMachineVM routingStepMachineVM);
        Task<RoutingStepSupplierVM> RoutingStepSupplier(RoutingStepSupplierVM routingStepSupplierVM);


        Task<bool> DeleteSubConDetails(int subConDetailsId,int stepId);
        Task<bool> DeleteSubConWSDetails(int subConWSId);
        Task<SubConDetailsVM> AddSubConDetails(SubConDetailsVM subConDetailsVM);
        Task<SubConWorkStepDetailsVM> AddSubConWorkStepDetails(SubConWorkStepDetailsVM subConWSDetailsVM);
        Task<IEnumerable<SubConDetailsVM>> SubCons(int stepId);
        Task<IEnumerable<SubConWorkStepDetailsVM>> SubConWSS(int stepId,  int subConDetailsId);
        Task<SubConDetailsVM> PreferredSubCon(int subConDetailsId);


    }
}
