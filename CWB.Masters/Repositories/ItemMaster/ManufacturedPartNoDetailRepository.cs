using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class ManufacturedPartNoDetailRepository : Repository<ManufacturedPartNoDetail>, IManufacturedPartNoDetailRepository
    {
        private readonly DbSet<ManufacturedPartNoDetail> _dbSet;
        public ManufacturedPartNoDetailRepository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<ManufacturedPartNoDetail>();
            //ManufacturedPartNoDetail var = _dbSet.Single();
        }

        public async Task<IEnumerable<ManufacturedPartNoDetail>> GetAllManuFByPartTypeCompany(long ManuPartType, string companyName)
        {
            return await _dbSet.Select(t => t).Where(t => t.ManufacturedPartType == ManuPartType && t.CompanyId.Equals(companyName)).ToListAsync();// && t => t.ManufacturedPartType == companyName).ToListAsync();
        }


    }
}
