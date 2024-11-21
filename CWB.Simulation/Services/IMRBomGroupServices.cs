using CWB.Simulation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IMRBomGroupServices
    {
        IEnumerable<MRBomGroupVM> GetMRBomGroupsByTenant(long TenantId);
        Task AddMRBomGroup(MRBomGroupVM model);
        Task UpdateMRBomGroup(long MRBomGroupId, MRBomGroupVM model);
    }
}
