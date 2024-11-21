using CWB.Simulation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IMachineServices
    {
        IEnumerable<MachineVM> GetMachines(long TenantID);
        Task AddMachine(MachineVM model);
        Task UpdateMachine(long MachineID, MachineVM model);

    }
}
