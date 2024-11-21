using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class ItemMasterContentRepository : Repository<ItemMasterContent>, IItemMasterContentRepository
    {
        public ItemMasterContentRepository(MastersDbContext context)
         : base(context)
        { }
    }
}
