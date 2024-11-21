using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Company;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class BoughtOutFinishDetailRepository : Repository<BoughtOutFinishDetail>, IBoughtOutFinishDetailRepository
    {
       
        public BoughtOutFinishDetailRepository(MastersDbContext context)
        : base(context)
        {}
    }
}
