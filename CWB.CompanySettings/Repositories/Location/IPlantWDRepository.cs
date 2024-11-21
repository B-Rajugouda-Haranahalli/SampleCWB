using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Repositories.Location
{
    public interface IPlantWDRepository : IRepository<PlantWorkingDetails>
    {
        Task<IEnumerable<PlantWorkingDetails>> GetPlantWorkingDetails(long tenantID);

    }
}
