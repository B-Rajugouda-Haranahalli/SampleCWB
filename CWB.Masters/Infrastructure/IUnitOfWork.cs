using System.Threading.Tasks;

namespace CWB.Masters.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        int Commit();
    }
}
