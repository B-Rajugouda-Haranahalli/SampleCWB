using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.Infrastructure;

namespace CWB.CompanySettings.Repositories.Designations
{
    public class DesignationRepository : Repository<Designation>, IDesignationRepository
    {
        public DesignationRepository(CompanySettingsDbContext context)
         : base(context)
        { }
    }
}