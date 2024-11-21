using CWB.App.Models.OperationList;
using CWB.App.Services.DocumentMagement;
using CWB.App.Services.Masters;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class OperationListController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IOperationService _operationService;
        private readonly IDocMangService _docMangService;

        public OperationListController(ILoggerManager logger, IOperationService operationService,
            IDocMangService docMangService)
        {
            _logger = logger;
            _operationService = operationService;
            _docMangService = docMangService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _operationService.GetOperationsList();
            foreach (var item in result)
            {
                if(item.IsMultiplePartsOfBOMUsed == true)
                {
                    item.Bom = "Y";
                }
                else
                {
                    item.Bom = "N";
                }
                if(item.Inhouse == 1)
                {
                    item.InhouseStr = "Y";
                }
                else
                {
                    item.InhouseStr = "N";
                }
                if(item.Subcon == 1)
                {
                    item.SubConstr = "Y";
                }
                else
                {
                    item.SubConstr = "N";
                }
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Operations()
        {
            var result = await _operationService.GetOperationsList();
            foreach (var item in result)
            {
                if (item.IsMultiplePartsOfBOMUsed == true)
                {
                    item.Bom = "Y";
                }
                else
                {
                    item.Bom = "N";
                }
                if (item.Inhouse == 1)
                {
                    item.InhouseStr = "Y";
                }
                else
                {
                    item.InhouseStr = "N";
                }
                if (item.Subcon == 1)
                {
                    item.SubConstr = "Y";
                }
                else
                {
                    item.SubConstr = "N";
                }
            }
            return Ok(result);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Operation(OperationListVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _operationService.Operation(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Operation(long Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _operationService.Operation(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<JsonResult> IsOperationExist(long? OperationId, string Operation)
        {
            var result = await _operationService.CheckIfOperationExisit(OperationId.HasValue ? OperationId.Value : 0, Operation);
            return Json(!result);
        }

        //[HttpGet]
        //public async Task<JsonResult> GetDocTypes(long Id)
        //{
        //    var result = await _operationService.GetOperationDocTypes(Id);
        //    return Json(result);
        //}

        [HttpGet]
        public async Task<JsonResult> GetOperationalDocuments(long Id)
        {
            //var result = await _operationService.GetOperationDocTypesList(Id);
            var result = await _operationService.GetOperationalDocTypesByOptId(Id);
            var doctype = await _docMangService.GetAllDocumentType();
            foreach (var item in result)
            {
                foreach (var doc in doctype)
                {
                    if (item.DocumentTypeId ==doc.DocumentTypeId)
                    {
                        item.DocumentType = doc.DocumentName;
                    }
                }
                if (item.IsMandatory == true)
                {
                    item.IsMandatoryStr = "Y";
                }
                else
                {
                    item.IsMandatoryStr = "N";
                }
            }
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeletOperationDoc(long opDocId)
        {
            var result = await _operationService.DeletOperationDoc(opDocId);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OperationalDocuments(OperationDocumentTypeVM operationDocumentTypeVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _operationService.OperationDocument(operationDocumentTypeVM);
            return Ok(result);
        }
    }
}
