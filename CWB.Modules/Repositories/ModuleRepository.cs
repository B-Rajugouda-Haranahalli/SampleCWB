using CWB.CommonUtils.Common.Repositories;
using CWB.Modules.Domain;
using CWB.Modules.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Modules.Repositories
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(ModuleDbContext context)
        : base(context)
        {
        }
        
    }
}
