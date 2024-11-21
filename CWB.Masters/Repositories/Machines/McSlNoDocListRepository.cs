using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.Machines
{
    public class McSlNoDocListRepository : Repository<McSlNoDocList>, IMcSlNoDocListRepository
    {
        public McSlNoDocListRepository(MastersDbContext context)
        : base(context)
        { }
    }
}
