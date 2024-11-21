using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IManufacturedPartNoDetailRepository : IRepository<ManufacturedPartNoDetail>
    {
        Task<IEnumerable<ManufacturedPartNoDetail>> GetAllManuFByPartTypeCompany(long ManuPartType, string companyName);    
        //Task<IEnumerable<ManufacturedPartNoDetail>> GetAllObjectsByManyTypeTenantAsync(long ManuTypeId, long tenantID);
        //public async Task<IEnumerable<Division>> GetAllDivisionByCompanyNTenantAsync(long companyID, long tenantID)
        //{
          //  return await _dbSet.Include(m => m.Company).Where(t => t.TenantId == tenantID && t.CompanyId == companyID).ToListAsync();
        //}
    }
}
