using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using CWB.Masters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.Routings
{
    public class RoutingStatusLogRepository : Repository<RoutingStatusLog>, IRoutingStatusLogRepository
    {
        public RoutingStatusLogRepository(MastersDbContext context)
         : base(context)
        { }
    }
}
