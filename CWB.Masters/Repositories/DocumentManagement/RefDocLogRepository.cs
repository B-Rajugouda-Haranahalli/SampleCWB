using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.DocumentManagement;
using CWB.Masters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.DocumentManagement
{
    public class RefDocLogRepository : Repository<RefDocLog>, IRefDocLogRepository
    {
        public RefDocLogRepository(MastersDbContext context)
         : base(context)
        { }
    }
}
