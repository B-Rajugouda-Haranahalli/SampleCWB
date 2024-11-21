using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.Infrastructure;

namespace CWB.CompanySettings.Repositories.Location
{
    public class HolidayRepository : Repository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(CompanySettingsDbContext context)
         : base(context)
        { }
    }
}