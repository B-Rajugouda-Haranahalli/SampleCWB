using CWB.App.Models.Machine;
using CWB.App.Models.OperationList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.Masters
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineTypeVM>> GetMachineTypes();
        Task<MachineTypeVM> MachineType(MachineTypeVM machineTypeVM);
        Task<bool> CheckMachineType(MachineTypeVM machineTypeVM);
        Task<bool> CheckMachine(CheckMachineVM checkMachineVM);
        Task<MachineVM> Machine(MachineVM machineVM);
        Task<MachineVM> GetMachine(long Id);
        Task<IEnumerable<MachineListVM>> GetMachinesList();
        Task<IEnumerable<McTypeDocListVM>> GetMcTypeDocList();
        Task<IEnumerable<McSlNoDocListVM>> GetMcProcDocList();
        Task<McTypeDocListVM> PostMcTypeDocList(McTypeDocListVM machineVM);
        Task<McSlNoDocListVM> PostMcProcDocList(McSlNoDocListVM machineVM);
        Task<bool> DeleteMcProcDoc(long mcSlNoDocListId);
        Task<bool> DeleteMcTypeDoc(long mcTypeDocListId);
        Task<IEnumerable<MachineProcDocumentListVM>> GetMachineProcsDocLists(long MachineId);
        Task<IEnumerable<DocumentTypeListVM>> GetMachineDocTypes(long MachineId);
        Task<MachineProcDocumentVM> MachineProcDoc(MachineProcDocumentVM machineProcDocumentVM);
    }
}
