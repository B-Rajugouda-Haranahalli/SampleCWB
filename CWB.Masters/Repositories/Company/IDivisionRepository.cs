using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.Company
{
    public interface IDivisionRepository : IRepository<Division>
    {
        Task<IEnumerable<Division>> GetAllDivisionByTenantAsync(long tenantID);
        Task<IEnumerable<Division>> GetAllDivisionByCompanyNTenantAsync(long companyID, long tenantID);
    }
}
