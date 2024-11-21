using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.Infrastructure;

namespace CWB.CompanySettings.Repositories.Location
{
    public class HoldiayRepository : Repository<Holiday>, IHolidayRepository
    {
        public HoldiayRepository(CompanySettingsDbContext context)
         : base(context)
        { }
    }
}