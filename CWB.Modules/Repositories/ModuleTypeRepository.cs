using CWB.CommonUtils.Common.Repositories;
using CWB.Modules.Domain;
using CWB.Modules.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Modules.Repositories
{
    public class ModuleTypeRepository : Repository<ModuleType>, IModuleTypeRepository
    {
        private readonly DbSet<ModuleType> _dbSet;
        public ModuleTypeRepository(ModuleDbContext context)
        : base(context)
        {
            _dbSet = context.Set<ModuleType>();
        }

        public async Task<IEnumerable<ModuleType>> GetAllModulesByTypesAsync(bool onlyActive = false)
        {
            if (onlyActive)
            {
                return await _dbSet.Include(m => m.Modules.Where(o => o.IsActive == true)).ToListAsync();
            }
            else
            {
                return await _dbSet.Include(m => m.Modules).ToListAsync();
            }
        }
    }
}
