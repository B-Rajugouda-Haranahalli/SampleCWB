using System.Threading.Tasks;

namespace CWB.BusinessProcesses.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        int Commit();
    }
}
