using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Repositories.Location
{
    public class DepartmentRepository : Repository<ShopDepartment>, IDepartmentRepository
    {
        private readonly DbSet<ShopDepartment> _dbSet;
        public DepartmentRepository(CompanySettingsDbContext context)
        : base(context)
        {
            _dbSet = context.Set<ShopDepartment>();
        }

        
        public async Task<IEnumerable<ShopDepartment>> GetAllDepartmentsByPlantNTenantAsync(long plantId, long tenantId)
        {
            return await _dbSet.Include(d => d.Plant).Where(t => t.TenantId == tenantId && t.PlantId == plantId).ToListAsync();
        }

        public async Task<IEnumerable<ShopDepartment>> GetAllDepartmentsByTenantAsync(long tenantId)
        {
            return await _dbSet.Include(d => d.Plant).Where(t => t.TenantId == tenantId).ToListAsync();
        }

        public IEnumerable<ShopDepartment> GetDepartmentsWithPlant(List<long> DepartmentIds, long TenantId)
        {
            return _dbSet.Include(d => d.Plant).Where(t => t.TenantId == TenantId && DepartmentIds.Contains(t.Id)).AsEnumerable();

        }
    }
}
