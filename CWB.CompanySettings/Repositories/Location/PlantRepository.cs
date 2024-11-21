using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.Infrastructure;

namespace CWB.CompanySettings.Repositories.Location
{
    public class PlantRepository : Repository<Plant>, IPlantRepository
    {
        public PlantRepository(CompanySettingsDbContext context)
         : base(context)
        { }
    }
}