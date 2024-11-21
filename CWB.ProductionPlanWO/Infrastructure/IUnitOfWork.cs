using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        int Commit();
    }
}
