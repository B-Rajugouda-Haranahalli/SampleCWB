using CWB.App.AppUtils;
using CWB.App.Models.CoSettings;
using CWB.App.Models.Plants;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CWB.App.Services.CompanySettings
{
    public class PlantService : IPlantService
    {
        private readonly ILoggerManager _logger;
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long tenantId;

        public PlantService(ILoggerManager logger,ApiUrls apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiUrls = apiUrlsOptions;
            _httpContextAccessor = httpContextAccessor;
            tenantId = long.Parse(AppUtil.GetTenantId(_httpContextAccessor.HttpContext.User));
        }
        public async Task<IEnumerable<PlantVM>> GetPlants()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/plants/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<PlantVM>>.GetAsync(uri, headers);
        }

        public async Task<PlantVM> PostPlant(PlantVM plantVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/plant");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            plantVM.TenantId = tenantId;
            return await RestHelper<PlantVM>.PostAsync(uri, plantVM, headers);
        }
        public async Task<bool> DelPlant(long plantId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/delplant/{plantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<PlantVM> GetPlant(long plantId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/getplant/{plantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<PlantVM>.GetAsync(uri, headers);
        }

        public async Task<PlantWorkingDetailsVM> PostPlantWD(PlantWorkingDetailsVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/plantwd");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<PlantWorkingDetailsVM>.PostAsync(uri, model, headers);
        }



        public async Task<IEnumerable<CityVM>> GetCities()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/getcitys/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<CityVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<CountryVM>> GetCountries()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/getcountry/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<CountryVM>>.GetAsync(uri, headers);
        }
        public async Task<CityVM> PostCity(CityVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/postcity");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<CityVM>.PostAsync(uri, model, headers);
        }
        public async Task<CountryVM> PostCountry(CountryVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/postcountry");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<CountryVM>.PostAsync(uri, model, headers);
        }
        public async Task<bool> CheckCity(string city)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/checkcity/{city}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> CheckCountry(string country)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/checkcountry/{country}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<HolidayVM> PlantHoliday(HolidayVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/holiday");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<HolidayVM>.PostAsync(uri, model, headers);
        }

        public async  Task<IEnumerable<HolidayVM>> GetHolidays(long plantId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/holidays/{tenantId}/{plantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<HolidayVM>>.GetAsync(uri, headers);
        }

        public async Task<bool> DelHoliday(long holidayId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/delholiday/{tenantId}/{holidayId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<PlantWorkingDetailsVM> GetPlantWD(long plantId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbcs/getplantwd/{tenantId}/{plantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<PlantWorkingDetailsVM>.GetAsync(uri, headers);
        }


    }
}
