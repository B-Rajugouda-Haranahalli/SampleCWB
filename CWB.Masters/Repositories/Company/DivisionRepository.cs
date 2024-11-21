using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.Company
{
    public class DivisionRepository : Repository<Division>, IDivisionRepository
    {
        private readonly DbSet<Division> _dbSet;
        public DivisionRepository(MastersDbContext context)
         : base(context)
        {
            _dbSet = context.Set<Division>();
        }

        public async Task<IEnumerable<Division>> GetAllDivisionByCompanyNTenantAsync(long companyID, long tenantID)
        {
            return await _dbSet.Include(m => m.Company).Where(t => t.TenantId == tenantID && t.CompanyId == companyID).ToListAsync();
        }

        public async Task<IEnumerable<Division>> GetAllDivisionByTenantAsync(long tenantID)
        {
            return await _dbSet.Include(m => m.Company).Where(t => t.TenantId == tenantID).ToListAsync();
        }
    }
}