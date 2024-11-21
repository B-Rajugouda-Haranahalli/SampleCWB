using System.Threading.Tasks;

namespace CWB.BusinessAquisition.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        int Commit();
    }
}
