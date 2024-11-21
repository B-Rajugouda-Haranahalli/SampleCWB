using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Infrastructure;

namespace CWB.Masters.Repositories.Company
{
    public class CompanyRepository : Repository<Domain.Company>, ICompanyRepository
    {
        public CompanyRepository(MastersDbContext context)
         : base(context)
        { }
    }
}