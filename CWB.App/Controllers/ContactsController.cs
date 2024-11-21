using CWB.App.Models.Contacts;
using CWB.App.Services.Masters;
using CWB.Constants.UserIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class ContactsController : Controller
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IMastersServices _mastersService;
        public ContactsController(ILogger<ContactsController> logger, IMastersServices mastersService)
        {
            _logger = logger;
            _mastersService = mastersService;
        }
        public async Task<IActionResult> Index()
        {
            await ContactsViewBag();
            var companies = await _mastersService.GetCompanies();
            return View(companies);
        }

        [HttpGet]
        public async Task<IActionResult> Companies()
        {
            var companies = await _mastersService.GetCompanies();
            return Ok(companies);

        }

        [HttpGet]
        public async Task<IActionResult> Divisions(long Id)
        {
            var divisions = await _mastersService.GetDivisionsByCompanyId(Id);
            return Ok(divisions);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Company(CompanyVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.Company(model);
            return Ok(result);
        }

       
        public async Task<IActionResult> DeleteCompany(long companyID)
        {
          
            var result = await _mastersService.DeleteCompany(companyID);
            return Ok(result);
        }

        public async Task<IActionResult> DeleteDivision(long divisionID)
        {

            var result = await _mastersService.DeleteDivision(divisionID);
            return Ok(result);
        }

        [HttpPost]
        public async Task<JsonResult> IsCompanyExist(long? CompanyId, string CompanyName)
        {
            var result = await _mastersService.CheckIfCompanyExisit(CompanyId.HasValue ? CompanyId.Value : 0, CompanyName);
            return Json(!result);
        }

        [HttpPost]
        public async Task<JsonResult> IsDivisionExist(long? DivisionId, long? CompanyId, string DivisionName)
        {
            var result = await _mastersService.CheckIfDivisionExisit(CompanyId.HasValue ? CompanyId.Value : 0,
                DivisionId.HasValue ? DivisionId.Value : 0, DivisionName);
            return Json(!result);
        }

        #region Private Functions - ViewBag
        private async Task ContactsViewBag()
        {
            var companyTypes = await _mastersService.GetCompanyTypes();
            ViewBag.CompanyTypes = companyTypes.Select(c => new SelectListItem { Text = c.CompanyType, Value = c.CompanyTypeValue }).ToList();
        }
        #endregion
    }
}
