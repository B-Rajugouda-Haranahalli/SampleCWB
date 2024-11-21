using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Repositories.Location
{
    public class PlantWDRepository : Repository<PlantWorkingDetails>, IPlantWDRepository
    {
        private readonly DbSet<PlantWorkingDetails> _dbSet;
        public PlantWDRepository(CompanySettingsDbContext context)
         : base(context)
        {
            _dbSet = context.Set<PlantWorkingDetails>();
        }

        public async Task<IEnumerable<PlantWorkingDetails>> GetPlantWorkingDetails(long tenantID)
        {
            var query = _dbSet.Include(m => m.Plant).Where(t => t.TenantId == tenantID);
            return await query.ToListAsync();
        }
                
    }
}