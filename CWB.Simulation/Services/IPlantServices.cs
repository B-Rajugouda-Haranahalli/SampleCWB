using CWB.Simulation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IPlantServices
    {
        IEnumerable<PlantVM> GetPlants(long TenantID);
        Task AddPlant(PlantVM model);
        Task UpdatePlant(long PlantID, PlantVM model);
    }
}
