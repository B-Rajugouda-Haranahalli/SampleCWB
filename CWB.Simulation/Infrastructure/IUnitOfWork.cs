using System.Threading.Tasks;

namespace CWB.Simulation.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        int Commit();
    }
}
