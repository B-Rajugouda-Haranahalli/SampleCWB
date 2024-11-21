using CWB.App.Models.CoSettings;
using CWB.App.Models.Plants;
using CWB.App.Services.CompanySettings;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class PlantController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IPlantService _plantService;

        public PlantController(ILoggerManager logger, IPlantService plantService)
        {
            _logger = logger;
            _plantService = plantService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetPlants()
        {
            var result = await _plantService.GetPlants();
            foreach (var item in result)
            {
                var wd = await _plantService.GetPlantWD(item.PlantId);
                item.NoOfShifts = wd.NoOfShifts;
                item.WeeklyOff1 = wd.WeeklyOff1;
                item.FirstShiftStartTime = wd.FirstShiftStartTime;
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Plant(PlantVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _plantService.PostPlant(model);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> PlantWd(PlantWorkingDetailsVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _plantService.PostPlantWD(model);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostCity(CityVM model)
        {
            var result = await _plantService.PostCity(model);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostCountry(CountryVM model)
        {
            var result = await _plantService.PostCountry(model);
            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> GetCitys()
        {
            var result = await _plantService.GetCities();
            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> GetCountrys()
        {
            var result = await _plantService.GetCountries();
            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> CheckCity(string city)
        {
            var result = await _plantService.CheckCity(city);
            return Json(!result);
        }
        [HttpGet]
        public async Task<JsonResult> CheckCountry(string city)
        {
            var result = await _plantService.CheckCountry(city);
            return Json(!result);
        }
        [HttpPost]
        public async Task<IActionResult> PlantHoliday(HolidayVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _plantService.PlantHoliday(model);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetPlant(long plantId)
        {
            var result = await _plantService.GetPlant(plantId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> DelPlant(long plantId)
        {
            var result = await _plantService.DelPlant(plantId);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetPlantWD(long plantId)
        {
            var result = await _plantService.GetPlantWD(plantId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetHolidays(long plantId)
        {
            var result = await _plantService.GetHolidays(plantId);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> DelHoliday(long holidayId)
        {
            var result = await _plantService.DelHoliday(holidayId);
            return Json(result);
        }


    }
}
