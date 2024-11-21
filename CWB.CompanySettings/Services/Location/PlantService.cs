using AutoMapper;
using CWB.CompanySettings.Infrastructure;
using CWB.CompanySettings.Repositories.Location;
using CWB.CompanySettings.ViewModels.Location;
using CWB.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Services.Location
{
    public class PlantService : IPlantService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlantRepository _plantRepository;
        private readonly IPlantWDRepository _plantWDRepository;
        private readonly IHolidayRepository _holidayRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public PlantService(ILoggerManager logger 
            ,IMapper mapper, IUnitOfWork unitOfWork 
            ,IPlantRepository plantRepository
            ,IPlantWDRepository plantWDRepository
            ,IHolidayRepository holidayRepository, ICityRepository cityRepository,
            ICountryRepository countryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _plantRepository = plantRepository;
            _plantWDRepository = plantWDRepository;
            _holidayRepository = holidayRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public bool CheckPlantExisit(CheckPlantVM checkPlantVM)
        {
            var plants = _plantRepository.GetRangeAsync(p => p.Name == checkPlantVM.Name &&
            p.TenantId == checkPlantVM.TenantId);
            if (!plants.Any())
            {
                return false;
            }
            return (plants.First().Id != checkPlantVM.PlantId);
        }

        public IEnumerable<PlantVM> GetPlants(long TenantId)
        {
            var plants = _plantRepository.GetRangeAsync(p => p.TenantId == TenantId);
            return _mapper.Map<IEnumerable<PlantVM>>(plants);
        }

        public async Task<IEnumerable<PlantVM>> GetPlantsWithWorkDetails(long tenantID)
        {
            var plants = await _plantWDRepository.GetPlantWorkingDetails(tenantID);
            var plantwd= _mapper.Map<IEnumerable<PlantVM>>(plants);
            return plantwd;
        }

        public async Task<PlantVM> Plant(PlantVM plantVM)
        {
            var plant = _mapper.Map<Domain.Plant>(plantVM);
            if (plant.Id == 0)
            {
                if (plant.PanNo == null)
                {
                    plant.PanNo = string.Empty;
                }
                if (plant.Pincode == null)
                {
                    plant.Pincode = string.Empty;
                }
                if (plant.GstNo == null)
                {
                    plant.GstNo = string.Empty;
                }
                await _plantRepository.AddAsync(plant);
            }
            else
            {
                if (plant.PanNo == null)
                {
                    plant.PanNo = string.Empty;
                }
                if (plant.Pincode == null)
                {
                    plant.Pincode = string.Empty;
                }
                if (plant.GstNo == null)
                {
                    plant.GstNo = string.Empty;
                }
                plant = await _plantRepository.UpdateAsync(plant.Id, plant);
            }
            await _unitOfWork.CommitAsync();
            plantVM.PlantId = plant.Id;
            return plantVM;
        }

        public async Task<PlantVM> GetPlant(long plantId)
        {
            var plant = await _plantRepository.SingleOrDefaultAsync(d => d.Id == plantId);
            if (plant == null)
            {
                plant = new Domain.Plant { Id = -1 };
            }
            return _mapper.Map<PlantVM>(plant);
        }
        public async Task<bool> DelPlant(long plantId)
        {
            try
            {
                var plant = await _plantRepository.SingleOrDefaultAsync(d => d.Id == plantId);
                if (plant != null)
                {
                    if (plant.Id > 0)
                    {
                        _plantRepository.Remove(plant);
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DelHoliday(long holidayId)
        {
            try
            {
                var holiday = await _holidayRepository.SingleOrDefaultAsync(d => d.Id == holidayId);
                if (holiday != null)
                {
                    if (holiday.Id > 0)
                    {
                        _holidayRepository.Remove(holiday);
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<HolidayVM> Holidays(long plantId)
        {
            var holidays = _holidayRepository.GetRangeAsync(p => p.PlantId == plantId);
            return _mapper.Map<IEnumerable<HolidayVM>>(holidays);
        }

        public async Task<HolidayVM> PostHoliday(HolidayVM holidayVM)
        {
            var holiday = _mapper.Map<Domain.Holiday>(holidayVM);
            if (holiday.Id == 0)
            {
                await _holidayRepository.AddAsync(holiday);
            }
            else
            {
                holiday = await _holidayRepository.UpdateAsync(holiday.Id, holiday);
            }
            await _unitOfWork.CommitAsync();
            holidayVM.HolidayId = holiday.Id;
            return holidayVM;
        }

        public async Task<PlantWorkingDetailsVM> GetPlantWD(long tenantId, long plantId)
        {
            var plantWd = await _plantWDRepository.SingleOrDefaultAsync(m=>m.PlantId == plantId);
            if (plantWd == null)
            {
                return new PlantWorkingDetailsVM { WDId = 0, PlantId = 0 };
            }
            return _mapper.Map<PlantWorkingDetailsVM>(plantWd);
        }

        public IEnumerable<CityVM> GetCitys(long TenantId)
        {
            var plants = _cityRepository.GetRangeAsync(p => p.TenantId == TenantId);
            return _mapper.Map<IEnumerable<CityVM>>(plants);
        }
        public IEnumerable<CountryVM> GetCountrys(long TenantId)
        {
            var plants = _countryRepository.GetRangeAsync(p => p.TenantId == TenantId);
            return _mapper.Map<IEnumerable<CountryVM>>(plants);
        }
        public async Task<CityVM> PostCity(CityVM plantWdVM)
        {
            var plantWd = _mapper.Map<Domain.City>(plantWdVM);
            if (plantWd.Id == 0)
            {
                await _cityRepository.AddAsync(plantWd);
            }
            else
            {
                plantWd = await _cityRepository.UpdateAsync(plantWd.Id, plantWd);
            }
            await _unitOfWork.CommitAsync();
            plantWdVM.CityId = plantWd.Id;
            return plantWdVM;
        }
        public async Task<CountryVM> PostCountry(CountryVM plantWdVM)
        {
            var plantWd = _mapper.Map<Domain.Country>(plantWdVM);
            if (plantWd.Id == 0)
            {
                await _countryRepository.AddAsync(plantWd);
            }
            else
            {
                plantWd = await _countryRepository.UpdateAsync(plantWd.Id, plantWd);
            }
            await _unitOfWork.CommitAsync();
            plantWdVM.CountryId = plantWd.Id;
            return plantWdVM;
        }
        public async Task<bool> CheckCity(string city)
        {
            try
            {
                var documentTypes = await _cityRepository.SingleOrDefaultAsync(c => c.Name == (city));
                if (documentTypes != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }
        public async Task<bool> CheckCountry(string country)
        {
            try
            {
                var documentTypes = await _countryRepository.SingleOrDefaultAsync(c => c.Name == (country));
                if (documentTypes != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }
        public async Task<PlantWorkingDetailsVM> PostPlantWD(PlantWorkingDetailsVM plantWdVM)
        {
            var plantWd = _mapper.Map<Domain.PlantWorkingDetails>(plantWdVM);
            if (plantWd.Id == 0)
            {
                await _plantWDRepository.AddAsync(plantWd);
            }
            else
            {
                plantWd = await _plantWDRepository.UpdateAsync(plantWd.Id, plantWd);
            }
            await _unitOfWork.CommitAsync();
            plantWdVM.WDId = plantWd.Id;
            return plantWdVM;
        }
    }
}
