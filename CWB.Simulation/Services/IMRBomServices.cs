using CWB.Simulation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IMRBomServices
    {
        IEnumerable<MRBomVM> GetMRBomsByTenant(long TenantId);
        MRBomVM GetMRBomsById(long Id);
        Task AddMRBom(MRBomVM model);
        Task UpdateMRBom(long MRBomId, MRBomVM model);
    }
}