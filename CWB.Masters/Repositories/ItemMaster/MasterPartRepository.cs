using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class MasterPartRepository : Repository<Domain.ItemMaster.MasterPart>, IMasterPartRepository
    {
        private readonly DbSet<MasterPart> _dbSet;
        public MasterPartRepository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<MasterPart>();
        }
    }
}
