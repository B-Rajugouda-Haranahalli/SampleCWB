using CWB.CommonUtils.Common.Repositories;
using CWB.ProductionPlanWO.Domain;
using CWB.ProductionPlanWO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Repositories
{
    public class POStatusRepository : Repository<POStatus>, IPOStatusRepository
    {
        public POStatusRepository(WODbContext context) : base(context)
        {

        }
    }
}
