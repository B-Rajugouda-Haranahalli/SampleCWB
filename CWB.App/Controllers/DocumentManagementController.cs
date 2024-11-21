using CWB.App.AppUtils;
using CWB.App.Models.DocumentManagement;
using CWB.App.Services.CompanySettings;
using CWB.App.Services.DocumentMagement;
using CWB.App.Services.Masters;
using CWB.App.Services.Routings;
using CWB.Constants.UserIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CWB.App.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class DocumentManagementController : Controller
    {
        private readonly ILogger<DocumentManagementController> _logger;
        private readonly IDocMangService _docMangService;
        private readonly IMastersServices _masterService;
        private readonly IDepartmentService _departmentService;
        private readonly IRoutingService _routingService;

        public DocumentManagementController(ILogger<DocumentManagementController> logger, IMastersServices masterServices, 
            IDepartmentService departmentService, IDocMangService docMangService, IRoutingService routingService

            )
        {
            _logger = logger;
            _docMangService = docMangService;
            _masterService = masterServices;
            _departmentService = departmentService;
            _routingService = routingService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewDocument()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocumentType()
        {
            var doctype = await _docMangService.GetAllDocumentType();
            var docUploads = await _docMangService.GetAllDocUpload();
            var docViews = await _docMangService.GetAllDocView();
            var fileextn = await _docMangService.GetAllFileExtn();
            var dept = await _departmentService.GetDepartments(1);
            int noOfFiles = 0;
            foreach (var item in doctype)
            {
                foreach (var filext in fileextn)
                {
                    if(filext.ExtnId == item.ExtnId)
                    {
                        item.FileExtnName = filext.ExtnName;
                    }
                }
                foreach (var upload in docUploads)
                {
                    if (upload.DocumentTypeId == item.DocumentTypeId)
                    {
                        foreach (var dt in dept)
                        {
                            if (upload.DepartmentId == dt.DepartmentId)
                            {
                                item.DeptUploadName = dt.Name;
                            }
                        }

                    }
                }
                foreach (var views in docViews)
                {
                    if (views.DocumentTypeId == item.DocumentTypeId)
                    {
                        foreach (var dt in dept)
                        {
                            if (views.DepartmentId == dt.DepartmentId)
                            {
                                item.DeptViewName = dt.Name;
                            }
                        }
                        noOfFiles++;
                    }
                }
                item.NoOfFiles = noOfFiles;
                noOfFiles = 0;
            }
            return Ok(doctype);
        }


        [HttpPost]
        public async Task<IActionResult> PostDocumentType([FromBody]DocumentTypeVM documentTypeVMs)
        {
            var result = await _docMangService.PostDocumentType(documentTypeVMs);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocCategory()
        {
            var doccat = await _docMangService.GetAllDocCategory();
            return Ok(doccat);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFileExtn()
        {
            var doccat = await _docMangService.GetAllFileExtn();
            return Ok(doccat);
        }

        [HttpPost]
        public async Task<IActionResult> PostFileExtn([FromBody] ExtnInfoVM extnInfoVM)
        {
            var result = await _docMangService.PostFileExtn(extnInfoVM);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartMent()
        {
            var doccat = await _departmentService.GetDepartments(1);
            return Ok(doccat);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDocList()
        {
            var docListVMs = await _docMangService.GetAllDocList();
            var doctype = await _docMangService.GetAllDocumentType();
            var custRetnDataVMs = await _docMangService.GetAllCustRet();
            var companies = await _masterService.GetCompanies();
            var manufacturedPartNos = await _masterService.ItemMasterParts();

            foreach (var item in docListVMs)
            {
                foreach (var doc in doctype)
                {
                    if (item.DocumentTypeId == doc.DocumentTypeId)
                    {
                        item.DocumentTypeName = doc.DocumentName;
                        item.DataReqdByCust = doc.DataReqdByCust;
                        item.DocCat = doc.DocuCategory;
                    }
                }
                foreach (var cust in custRetnDataVMs)
                {
                    foreach (var comp in companies)
                    {
                        if (item.DocumentTypeId == cust.DocumentTypeId)
                        {
                            if (cust.ComapanyId == comp.CompanyId)
                            {
                                item.CompanyName = comp.CompanyName;
                            }
                        }
                    }
                }

                var partbyid = await _masterService.ItemMasterPartById((int)item.PartId);
                item.PartNo = partbyid.PartNo;
                item.PartDesc = partbyid.PartDescription;
                var routingListItems = await _routingService.Routings((int)item.PartId);
                foreach (var route in routingListItems)
                {
                    if(route.PreferredRouting == 1)
                    {
                        if (route.RoutingId == item.RoutingId)
                        {
                            item.RoutingName = route.RoutingName;
                        }
                    }
                }
                foreach (var part in manufacturedPartNos)
                {
                    if(item.PartId == part.PartId)
                    {
                        

                        
                    }
                }
                if(item.StorageLocation == "/Archive")
                {
                    item.Archive = 'Y';
                }
                else
                {
                    item.Archive = 'N';
                }
                ClaimsPrincipal userClaim = HttpContext.User; // Assuming you're in a controller or middleware
                string fullName = AppUtil.GetFullName(userClaim);
                item.UpdatedOnStr = item.CreationDt.ToString("MM-dd-yyyy");
                item.UploadedBy = fullName;
            }
            return Ok(docListVMs);
        }

        [HttpPost]
        public async Task<IActionResult> PostDocList([FromBody] DocListVM docListVM)
        {
            var result = await _docMangService.PostDocList(docListVM);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustRet()
        {
            var custRetnDataVMs = await _docMangService.GetAllCustRet();
            var doctype = await _docMangService.GetAllDocumentType();
            var companies = await _masterService.GetCompanies();
            var docViews = await _docMangService.GetAllDocView();
            int noOfFiles = 0;
            foreach (var item in custRetnDataVMs)
            {
                foreach (var doc in doctype)
                {
                    if(item.DocumentTypeId == doc.DocumentTypeId)
                    {
                        item.DocumentTypeName = doc.DocumentName;
                    }
                    foreach (var views in docViews)
                    {
                        if (views.DocumentTypeId == doc.DocumentTypeId)
                        {
                            noOfFiles++;
                        }
                    }
                    item.NoOfFiles = noOfFiles;
                    noOfFiles = 0;
                }
                foreach (var comp in companies)
                {
                    if(item.ComapanyId == comp.CompanyId)
                    {
                        item.CompanyName = comp.CompanyName;
                    }
                }
            }
            return Ok(custRetnDataVMs);
        }


        [HttpPost]
        public async Task<IActionResult> PostCustRetndata([FromBody] CustRetnDataVM custRetnDataVM)
        {
            var result = await _docMangService.PostCustRetndata(custRetnDataVM);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> PostDocUpload([FromBody] IEnumerable<DocUploadVM> docUploadVMs)
        {
            var result = await _docMangService.PostDocUpload(docUploadVMs);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostDocView([FromBody] IEnumerable<DocViewVM> docViewVMs)
        {
            var result = await _docMangService.PostDocView(docViewVMs);
            return Ok(result);
        }

        public async Task<IActionResult> DeleteDocType(long doctypeId)
        {
            var result = await _docMangService.DeleteDocType(doctypeId);
            return Ok(result);
        }
        public async Task<IActionResult> DeleteCustRetData(long custRetId)
        {

            var result = await _docMangService.DeleteCustRetData(custRetId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<JsonResult> CheckDocTypeName(string docTypeName)
        {
            var result = await _docMangService.CheckDocTypeName(docTypeName);
            return Json(!result);
        }
        [HttpGet]
        public async Task<JsonResult> CheckExtnName(string extnName)
        {
            var result = await _docMangService.CheckExtnName(extnName);
            return Json(!result);
        }
        [HttpGet]
        public async Task<JsonResult> NoFilesExtn(string extn)
        {
            bool result = false;
            var docListVMs = await _docMangService.GetAllDocList();
            foreach (var item in docListVMs)
            {
                if(item.FileName.ToLower().Contains(extn.ToLower()))
                {
                    result = true;
                    return Json(result);
                }
            }
            return Json(result);
        }
        public async Task<IActionResult> DeleteExtnInfo(long extnId)
        {
            var result = await _docMangService.DeleteExtnInfo(extnId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDocListAndFile([FromBody] IEnumerable<DocListVM> docListVMs)
        {
            var result = false;
            foreach (var item in docListVMs)
            {

                var filePath = Path.Combine("/filestorage/Active", item.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    //return Ok("File deleted successfully");
                }
                else
                {
                    // return NotFound("File not found");
                }
                result = await _docMangService.DeleteDocList(item.DocListId);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocViewDepartMent(long docTypeId)
        {
            var docListVMs = await _docMangService.GetAllDocView();
            var filteredDocListVMs = docListVMs.Where(vm => vm.DocumentTypeId == docTypeId);
            return Ok(filteredDocListVMs);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocUploadDepartMent(long docTypeId)
        {
            var docListVMs = await _docMangService.GetAllDocUpload();
            var filteredDocListVMs = docListVMs.Where(vm => vm.DocumentTypeId == docTypeId);
            return Ok(filteredDocListVMs);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRefReson()
        {
            var docListVMs = await _docMangService.Getallreasonlist();
            return Ok(docListVMs);
        }
        [HttpPost]
        public async Task<IActionResult> PostDocReason([FromBody] RefDocReasonListVM custRetnDataVM)
        {
            var result = await _docMangService.PostDocReason(custRetnDataVM);
            return Ok(result);
        }


    }
}
