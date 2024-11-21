using CWB.Simulation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IMachineTypeServices
    {
        IEnumerable<MachineTypeVM> GetMachineTypes(long TenantID);
        Task AddMachineType(MachineTypeVM model);
        Task UpdateMachineType(long MachineTypeID, MachineTypeVM model);

    }
}
