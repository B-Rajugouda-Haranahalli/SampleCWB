using CWB.Simulation.ViewModels;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IWorkDayMasterServices
    {
        Task<WorkDayMasterVM> GetWorkDayMaster(long TenantID);
        Task AddWorkDayMaster(WorkDayMasterVM model);
        Task UpdateWorkDayMaster(long WorkDayMasterID, WorkDayMasterVM model);
    }
}
