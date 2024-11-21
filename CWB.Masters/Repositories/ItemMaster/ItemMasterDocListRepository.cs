using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using CWB.Masters.ViewModels.ItemMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class ItemMasterDocListRepository : Repository<ItemMasterDocList>, IItemMasterDocListRepository
    {
        public ItemMasterDocListRepository(MastersDbContext context)
         : base(context)
        { }
    }
}
