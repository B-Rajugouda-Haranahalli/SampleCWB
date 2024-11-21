using CWB.App.Models.CoSettings;
using CWB.App.Models.Plants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.CompanySettings
{
    public interface IPlantService
    {
        Task<IEnumerable<PlantVM>> GetPlants();
        Task<PlantVM> PostPlant(PlantVM model);

        Task<bool> DelPlant (long plantId);
        Task<PlantVM> GetPlant(long plantId);
        Task<PlantWorkingDetailsVM> GetPlantWD(long plantId);
        
        Task<PlantWorkingDetailsVM> PostPlantWD(PlantWorkingDetailsVM model);
        Task<HolidayVM> PlantHoliday(HolidayVM model);
        Task<IEnumerable<CityVM>> GetCities();
        Task<IEnumerable<CountryVM>> GetCountries();
        Task<CityVM> PostCity(CityVM model);
        Task<bool> CheckCity(string city);
        Task<bool> CheckCountry(string country);
        Task<CountryVM> PostCountry(CountryVM model);

        Task<IEnumerable<HolidayVM>> GetHolidays(long plantId);
        Task<bool> DelHoliday(long holidayId);


    }
}
