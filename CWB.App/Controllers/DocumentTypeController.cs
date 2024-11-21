using CWB.App.Models.CoSettings;
using CWB.App.Models.Routing;
using CWB.App.Services.CompanySettings;
using CWB.App.Services.Masters;
using CWB.App.Services.Routings;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class DocumentTypeController : Controller
    {

        private readonly ILoggerManager _logger;
        private readonly IDocTypeService _docTypeService;


        public DocumentTypeController(ILoggerManager logger,IDocTypeService docTypeService)
        {
            _logger = logger;
            _docTypeService = docTypeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetDocTypes()
        {
            var result = await _docTypeService.GetDocTypes();
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DocType(DocumentTypeVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _docTypeService.PostDocType(model);
            return Json(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetDocType(long docTypeId)
        {
            var result = await _docTypeService.GetDocType(docTypeId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> DelDocType(long docTypeId)
        {
            var result = await _docTypeService.DelDocType(docTypeId);
            return Json(result);
        }
    }

}
