using System.Threading.Tasks;

namespace CWB.CompanySettings.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        int Commit();
    }
}
