using CWB.App.Models.Routing;
using CWB.App.Services.Masters;
using CWB.App.Services.Routings;
using CWB.App.Models.Designation;
using CWB.App.Services.CompanySettings;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class DesignationController : Controller
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IDesignationService _designationservice;
        public DesignationController(ILogger<ContactsController> logger, IDesignationService designationservice)
        {
            _logger = logger;
            _designationservice = designationservice;
        }
        public async Task<IActionResult> Index()
        {
            var designation = await _designationservice.GetDesignations();
            return View(designation);
        }

        [HttpGet]
        public async Task<JsonResult> Designations()
        {
            var designation = await _designationservice.GetDesignations();
            return Json(designation);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Designation(DesignationVM model)
        {
            if (!ModelState.IsValid)
        {
                return BadRequest(ModelState);
            }
            var result = await _designationservice.Designation(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<JsonResult> IsDesignationExist(long? DesignationId, string DesignationName)
        {
        
            var result = await _designationservice.CheckIfDesignationExisit(DesignationId.HasValue ? DesignationId.Value : 0, DesignationName);
            return Json(!result);
        }

        [HttpGet]
        public async Task<IActionResult> DelDesignation(long designationId)
        {
            var result = await _designationservice.DelDesignation(designationId);
            return Json(result);
        }
    }
}
