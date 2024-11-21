using CWB.CommonUtils.Common.Repositories;
using CWB.ProductionPlanWO.Domain;
using CWB.ProductionPlanWO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Repositories
{
    public class POHeaderRepository : Repository<POHeader>, IPOHeaderRepository
    {
        public POHeaderRepository(WODbContext context)
       : base(context)
        { }
    }
}
