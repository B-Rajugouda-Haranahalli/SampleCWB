using CWB.BusinessAquisition.Domain;
using CWB.BusinessAquisition.Infrastructure;
using CWB.CommonUtils.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.BusinessAquisition.Repositories
{
    public class BAStatusRepository : Repository<BAStatus>, IBAStatusRepository
    {
        public BAStatusRepository(BusinessAquisitionDbContext context)
         : base(context)
        { }
    }
}
