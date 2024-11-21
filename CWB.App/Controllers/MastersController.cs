using CWB.App.AppUtils;
using CWB.App.Models.Contacts;
using CWB.App.Models.DocumentManagement;
using CWB.App.Models.ItemMaster;
using CWB.App.Services.DocumentMagement;
using CWB.App.Services.Masters;
using CWB.App.Services.Routings;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace CWB.App.Controllers
{
    public class MastersController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IMastersServices _mastersService;
        private readonly IMachineService _machineService;
        private readonly IDocMangService _docMangService;
        private readonly IRoutingService _routingService;
        private readonly IOperationService _operationService;
        IHostingEnvironment _hostingEnvironment = null;
        public MastersController(ILoggerManager logger, IMachineService machineService, IMastersServices mastersService, IDocMangService docMangService,
           IRoutingService routingService, IHostingEnvironment hostingEnvironment
            , IHttpContextAccessor httpContextAccessor, IOperationService operationService)
        {
            _logger = logger;
            _mastersService = mastersService;
            _machineService = machineService;
            _docMangService = docMangService;
            _routingService = routingService;
            _operationService = operationService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            indexViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileToFtp(IFormFile uploadedFile)//, [FromServices] IHostingEnvironment hostingEnvironment
        {
            string ftpUrl = "/filestorage/Active/";
            string ftpFullPath = ftpUrl + uploadedFile.FileName;

            // Write the byte array to the file
            using (var stream = new FileStream(ftpFullPath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(stream);
                //string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{uploadedFile.FileName}";
                //using (FileStream fileStream = System.IO.File.Create(fileName))
                //{
                //    uploadedFile.CopyTo(fileStream);
                //    fileStream.Flush();
                //}
            }
            return Ok("Uploaded Sucess");
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(string fileName)
        {
            // Check if the file name is null or empty
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File name is required");
            }

            // Get the file path
            var filePath = Path.Combine("/filestorage/Active", fileName);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            // Read the file contents
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var stream = new FileStream(filePath, FileMode.Open);
            // Return the file contents as a response
            return File(fileBytes, GetContentType(filePath), fileName);
            //return File(stream, "application/octet-stream", fileName);
        }

        // Helper method to get the content type of a file
        private string GetContentType(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);
            var contentType = "application/octet-stream";

            switch (fileExtension.ToLower())
            {
                case ".txt":
                    contentType = "text/plain";
                    break;
                case ".pdf":
                    contentType = "application/pdf";
                    break;
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".bmp":
                    contentType = "image/bmp";
                    break;
                case ".doc":
                case ".docx":
                    contentType = "application/msword";
                    break;
                case ".xls":
                case ".xlsx":
                    contentType = "application/vnd.ms-excel";
                    break;
                case ".ppt":
                case ".pptx":
                    contentType = "application/vnd.ms-powerpoint";
                    break;
            }

            return contentType;
        }

        [HttpGet]
        public async Task<JsonResult> CheckPartNoInDocList(long partId)
        {
            var result = await _mastersService.CheckPartNoInDocList(partId);
            return Json(result);
        }

        public async Task<IActionResult> MasterDetails()
        {
            await StatusViewBagForManuf();
            return View();
        }

        public IActionResult EditPart(long partId, string partType)
        {
            string encodepartid = CWBAppUtils.EncodeLong(partId);

            if (partType.Equals("BOF"))
            {
                return RedirectToAction("EditBOF", new { partId = encodepartid });
            }
            else if (partType.Equals("RawMaterial"))
            {
                return RedirectToAction("EditRawMaterial", new { partId = encodepartid });
            }
            return RedirectToAction("EditManufPart", new { partId = encodepartid });
        }

        //editmanufpart
        public async Task<IActionResult> EditManufPart(string partId)
        {
            int decodedManufPartId = (int)CWBAppUtils.DecodeString(partId.ToString());
            ManufacturedPartNoDetailVM manuf = await _mastersService.GetManufPart(decodedManufPartId);
            await CustomerViewBag();
            await CompaniesViewBagForManuF(manuf);
            await StatusViewBagForManuf(manuf);
            return View(manuf);
        }

        public async Task<IActionResult> ManufPart(string Id)
        {

            await CustomerViewBag();
            await CompaniesViewBagForManuF();
            await StatusViewBagForManuf();
            return View(new ManufacturedPartNoDetailVM { MasterPartType = "0", PartId = 0, ManufacturedPartNoDetailId = 0, ManufacturedPartType = 1 });
        }

        public async Task<IActionResult> EditBOF(string partId)
        {
            int decodedPartId = (int)CWBAppUtils.DecodeString(partId);
            BoughtOutFinishDetailVM manuf = await _mastersService.GetBOFPart(decodedPartId);
            await CompaniesViewBagForBOF();
            await SupplierViewBag();
            await StatusViewBagForBOF();
            return View(manuf);
        }

        [HttpPost]
        public long DecodePartId(string partId)
        {
            long decodepartID = 0;
            if (partId != null && partId != "0")
            {
                decodepartID = CWBAppUtils.DecodeString(partId);
            }
            return decodepartID;
        }

        public async Task<IActionResult> EditRawMaterial(string partId)
        {
            int decodedPartId = (int)CWBAppUtils.DecodeString(partId);
            RawMaterialDetailVM manuf = await _mastersService.GetRMPart(decodedPartId);
            await SupplierViewBag();
            await CompaniesViewBagForRawMaterial(manuf);
            await StatusViewBagForRawMaterial(manuf);
            await RawMaterialViewBags(manuf);
            return View(manuf);
        }



        public async Task<IActionResult> RawMaterial(string Id)
        {
            await SupplierViewBag();
            await CompaniesViewBagForRawMaterial();
            await StatusViewBagForManuf();
            await RawMaterialViewBags();
            return View(new RawMaterialDetailVM { RawMaterialDetailId = 0, PartId = 0 });
        }

        public async Task<IActionResult> BOF(string Id)
        {
            await CompaniesViewBagForBOF();
            await StatusViewBagForBOF();
            await SupplierViewBag();
            ViewBag.BOFId = CWBAppUtils.DecodeString(Id);
            return View(new BoughtOutFinishDetailVM { BoughtOutFinishDetailId = 0, PartId = 0 });
        }

        #region Private Function
        private void indexViewBag()
        {
            ViewBag.IndexId = CWBAppUtils.EncodeLong(0);
        }

        public async Task<IActionResult> Customers()
        {
            var companies = await _mastersService.GetCompanies();
            return Json(companies.Where(m => m.CompanyType.Equals("Both") || m.CompanyType.Equals("Customer")));
        }
        public async Task<IActionResult> Suppliers()
        {
            var companies = await _mastersService.GetCompanies();
            return Json(companies.Where(m => m.CompanyType.Equals("Both") || m.CompanyType.Equals("Supplier")));
        }

        [HttpGet]
        public async Task<IActionResult> Companies()
        {
            var companies = await _mastersService.GetCompanies();

            return Json(companies);

        }

        [HttpGet]
        public async Task<IActionResult> RMTypes()
        {
            var companies = await _mastersService.GetRMTypes();

            return Json(companies);

        }

        [HttpGet]
        public async Task<IActionResult> RMSpecs()
        {
            var companies = await _mastersService.GetRMSpecs();

            return Json(companies);

        }

        [HttpGet]
        public async Task<IActionResult> RMStandards()
        {
            var companies = await _mastersService.GetRMStandards();

            return Json(companies);

        }

        [HttpGet]
        public async Task<IActionResult> BaseRMs()
        {
            var companies = await _mastersService.GetBaseRMs();

            return Json(companies);

        }

        [HttpGet]
        public async Task<IActionResult> ManufacturedPartNoDetailList(long ManufPartType, string companyName)
        {
            var mfpdList = await _mastersService.GetManufacturedPartNoDetailList(ManufPartType, companyName);
            return Json(mfpdList);
        }

        /*[HttpGet]
        public async Task<IActionResult> HelloWorld(long Id)
        {
            var mfpdList = await _mastersService.HelloWorld();
            return Json(mfpdList);
        }*/

        [HttpGet]
        public async Task<IActionResult> GetUOMs()
        {
            var mfpdList = await _mastersService.GetUOMs();
            return Json(mfpdList);
        }

        [HttpPost]
        public async Task<IActionResult> AddUOM(UOMVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.AddUOM(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<JsonResult> CheckUOM(string uomName)
        {
            var result = await _mastersService.CheckUOM(uomName);
            return Json(!result);
        }
        [HttpGet]
        public async Task<JsonResult> CheckRmType(string uomName)
        {
            var result = await _mastersService.CheckRmType(uomName);
            return Json(!result);
        }
        [HttpGet]
        public async Task<JsonResult> CheckRmStandard(string uomName)
        {
            var result = await _mastersService.CheckRmStandard(uomName);
            return Json(!result);
        }
        [HttpGet]
        public async Task<JsonResult> CheckBaseRm(string uomName)
        {
            var result = await _mastersService.CheckBaseRm(uomName);
            return Json(!result);
        }
        [HttpGet]
        public async Task<JsonResult> CheckRmSpec(string uomName)
        {
            var result = await _mastersService.CheckRmSpec(uomName);
            return Json(!result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManufacturedPartNoDetail(ManufacturedPartNoDetailVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.ManufacturedPartNoDetail(model);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MPMakeFrom(MPMakeFromVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.MPMakeFrom(model);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MPBOM(MPBomVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.MPBOM(model);
            return Ok(result);
        }



        [HttpGet]
        public async Task<IActionResult> GetMakeFrom(string Id)
        {
            var mpmakefromlist = await _mastersService.GetMakeFrom(Id);
            return Ok(mpmakefromlist);

        }
        [HttpGet]
        public async Task<IActionResult> GetBOM(string Id)
        {
            var mpmakefromlist = await _mastersService.GetBOM(Id);
            return Ok(mpmakefromlist);

        }


        [HttpGet]
        public async Task<IActionResult> BOFS()
        {
            var mpmakefromlist = await _mastersService.BOFS();
            return Ok(mpmakefromlist);

        }

        [HttpGet]
        public async Task<IActionResult> SupplierRMS(string supplierId)
        {
            var mfpdList = await _mastersService.SupplierRMS(supplierId);
            var rmTypes = await _mastersService.GetRMTypes();
            foreach (var item in mfpdList)
            {
                foreach (var rmtype in rmTypes)
                {
                    if (item.RawMaterialTypeId == rmtype.RawMaterialTypeId)
                    {
                        item.MultiplePartsMadeFrom1InputRM = rmtype.MultiplePartsMadeFrom1InputRM;
                    }
                }
            }
            return Ok(mfpdList);
        }

        [HttpGet]
        public async Task<IActionResult> mpdlist()
        {
            var mfpdList = await _mastersService.GetAllManufacturedPartNoDetailList();
            return Json(mfpdList);
        }
        //GetAllManufacturedPartNoDetailList


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RawMaterialDetail(RawMaterialDetailVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.RawMaterialDetail(model);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PartPurchase(PartPurchaseDetailsVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.PartPurchase(model);
            return Ok(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemPartPurchase(PartPurchaseDetailsVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.RemPartPurchase(model);
            return Ok(result);
        }



        [HttpGet]
        public async Task<IActionResult> PartPurchases()
        {
            var mfpdList = await _mastersService.PartPurchases();
            return Json(mfpdList);
        }

        [HttpGet]
        public async Task<IActionResult> PartPurchasesFor(int partId)
        {
            var mfpdList = await _mastersService.PartPurchasesFor(partId);
            return Json(mfpdList);
        }

        //partpurchasesbypartNo

        [HttpGet]
        public async Task<IActionResult> GetPartPurchase(int partPurchaseId)
        {
            var mfpdList = await _mastersService.GetPartPurchase(partPurchaseId);
            return Json(mfpdList);
        }
        [HttpGet]
        public async Task<IActionResult> DocTypes()
        {
            var doctype = await _docMangService.GetAllDocumentType();
            return Ok(doctype);
        }
        [HttpGet]
        public async Task<IActionResult> ItemMasterContent()
        {
            var content = await _mastersService.ItemMasterContents();
            return Ok(content);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItemMasterDocLists()
        {
            var result = await _mastersService.Getallitemmasterdoclist();
            var doctype = await _docMangService.GetAllDocumentType();
            foreach (var item in result)
            {
                foreach (var doc in doctype)
                {
                    if (item.DocumentTypeId == doc.DocumentTypeId)
                    {
                        item.DocumentTypeName = doc.DocumentName;
                    }
                }
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostItemMasterDocList(ItemMasterDocListVM masterDocListVM)
        {
            var result = await _mastersService.PostItemMasteDocList(masterDocListVM);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteItemMasterDocList(long itemMasterDocListId)
        {
            var result = await _mastersService.DeleteItemMasterDocList(itemMasterDocListId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ManufAssemCompLinq()
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var groupedByCompany = mfpdList.GroupBy(item => item.Company);
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();

            // Get routing list items once for efficiency
            var routingListItemVMs = await _routingService.GetRoutingListItems();
            var allDocuments = await _mastersService.Getallitemmasterdoclist();
            var allDocListVMs = await _docMangService.GetAllDocList();

            foreach (var companyGroup in groupedByCompany)
            {
                // Initialize counts
                int manufActive = 0, manufInactive = 0, assemActive = 0, assemInactive = 0;
                int finalpart = 0, woRm = 0, woBom = 0, woRoute = 0;
                int docnotavl = 0;
                // Cache manufactured parts
                var manufPartsCache = new Dictionary<int, ManufacturedPartNoDetailVM>();

                // Process each item in the company group
                var tasks = companyGroup.Select(async item =>
                {
                    if (!manufPartsCache.TryGetValue((int)item.PartId, out var manuf))
                    {
                        manuf = await _mastersService.GetManufPart((int)item.PartId);
                        manufPartsCache[(int)item.PartId] = manuf;
                    }

                    if (item.MasterPartType == "ManufacturedPart")
                    {
                        var mk = await _mastersService.GetMPMakeFromListByPartId(manuf.ManufacturedPartNoDetailId.ToString());

                        var mandatoryDocCount = allDocuments.Count(d => d.ContentId == 1);
                        var uploadedDocCount = allDocListVMs.Count(docList => allDocuments.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId));
                        if(uploadedDocCount == 0)
                        {
                            docnotavl++;
                        }
                        if(routingListItemVMs.Count(route => route.ManufacturedPartId == manuf.PartId && route.NoOfRoutes == 0) == 0)
                        {
                            woRoute++;
                        }
                        if (!mk.Any())
                        {
                            woRm++;
                        }

                        if (item.Status == "Active") manufActive++; else manufInactive++;
                    }
                    else if (item.MasterPartType == "Assembly")
                    {
                        var mpmakefromlist = await _mastersService.BOMS(manuf.ManufacturedPartNoDetailId.ToString());

                        var mandatoryDocCount = allDocuments.Count(d => d.ContentId == 2);
                        var uploadedDocCount = allDocListVMs.Count(docList => allDocuments.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId));
                        if (uploadedDocCount == 0)
                        {
                            docnotavl++;
                        }
                        if (!mpmakefromlist.Any())
                        {
                            woBom++;
                        }

                        // Check for routing
                        if (routingListItemVMs.Count(route => route.ManufacturedPartId == manuf.PartId && route.NoOfRoutes == 0) == 0)
                        {
                            woRoute++;
                        }
                        if (item.Status == "Active") assemActive++; else assemInactive++;
                    }

                    if (manuf.FinalPartNosoldtoCustomer == 1)
                    {
                        finalpart++;
                    }
                });

                await Task.WhenAll(tasks); // Await all tasks to complete

                // Create a summary for the company
                var firstItem = companyGroup.First();
                var companySummary = new ItemMasterPartVM
                {
                    Company = firstItem.Company,
                    FinalPart = finalpart.ToString(),
                    NoOfManufActive = manufActive.ToString(),
                    NoOfAssemblyActive = assemActive.ToString(),
                    NoOfManufInActive = manufInactive.ToString(),
                    NoOfAssemblyInActive = assemInactive.ToString(),
                    RmAvl = woRm.ToString(),
                    BomAvl = woBom.ToString(),
                    RoutingNotAvl = woRoute.ToString()
                };

                // Count mandatory documents not uploaded
                
                companySummary.MandocAvl = docnotavl.ToString(); // Count of mandatory docs not uploaded

                if (firstItem.MasterPartType == "Assembly" || firstItem.MasterPartType == "ManufacturedPart")
                {
                    result.Add(companySummary);
                }
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ManufAssByComp()
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var groupedByCompany = mfpdList.GroupBy(item => item.Company);
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();

            foreach (var companyGroup in groupedByCompany)
            {
                int manufActive = 0;
                int manufInactive = 0;
                int assemActive = 0;
                int assemInactive = 0;
                int finalpart = 0;
                int woRm = 0;
                int woBom = 0;
                int woRoute = 0;

                // Aggregating counts for each type and status within the company
                foreach (var item in companyGroup)
                {
                    if (item.MasterPartType == "ManufacturedPart")
                    {
                        var manuf = await _mastersService.GetManufPart((int)item.PartId);
                        var mk = await _mastersService.GetMPMakeFromListByPartId(manuf.ManufacturedPartNoDetailId.ToString());
                        var routingListItemVMs = await _routingService.GetRoutingListItems();
                        foreach (var route in routingListItemVMs)
                        {
                            if (route.ManufacturedPartId == manuf.PartId && route.NoOfRoutes == 0)
                            {
                                woRoute++;
                            }
                        }
                        if (mk.Count() > 0)
                        {
                        }
                        else
                        {
                            woRm++;
                        }
                        if (item.Status == "Active")
                            manufActive++;
                        else
                            manufInactive++;
                    }
                    else if (item.MasterPartType == "Assembly")
                    {

                        var asmanuf = await _mastersService.GetManufPart((int)item.PartId);
                        var mpmakefromlist = await _mastersService.BOMS(asmanuf.ManufacturedPartNoDetailId.ToString());
                        var count = mpmakefromlist.Count().ToString();
                        if (mpmakefromlist.Count() > 0)
                        {
                            
                        }
                        else
                        {
                            woBom++;
                        }
                        var routingListItemVMs = await _routingService.GetRoutingListItems();
                        foreach (var route in routingListItemVMs)
                        {
                            if (route.ManufacturedPartId == asmanuf.PartId && route.NoOfRoutes == 0)
                            {
                                woRoute++;
                            }
                        }
                        if (item.Status == "Active")
                            assemActive++;
                        else
                            assemInactive++;
                    }
                    var singmanuf = await _mastersService.GetManufPart((int)item.PartId);
                    if(singmanuf.FinalPartNosoldtoCustomer == 1)
                    {
                        finalpart++;
                    }
                }

                // Create a single summary entry for the company
                var firstItem = companyGroup.First(); // Use the first item to initialize a representative object
                var companySummary = new ItemMasterPartVM
                {
                    Company = firstItem.Company,
                    NoOfManufActive = manufActive.ToString(),
                    NoOfManufInActive = manufInactive.ToString(),
                    NoOfAssemblyActive = assemActive.ToString(),
                    RmAvl = woRm.ToString(),
                    BomAvl = woBom.ToString(),
                    RoutingNotAvl = woRoute.ToString(),
                    NoOfAssemblyInActive = assemInactive.ToString()
                };

                var docmand = await _mastersService.Getallitemmasterdoclist();
                if (docmand.Any(d => d.ContentId == (firstItem.MasterPartType == "ManufacturedPart" ? 1 : 2)))
                {
                    var docListVMs = await _docMangService.GetAllDocList();
                    companySummary.MandocAvl = docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)) ? "Yes" : "No";
                }
                else
                {
                    companySummary.MandocAvl = "N/A";
                }

                companySummary.FinalPart = finalpart.ToString();
                if(firstItem.MasterPartType == "Assembly" || firstItem.MasterPartType == "ManufacturedPart")
                {
                    result.Add(companySummary);
                } // Add only one entry per company
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ManufAssemlist(string company)
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var result = new List<ItemMasterPartVM>();
            var filteredItems = mfpdList.Where(item => item.Company == company).ToList();

            var allDocuments = await _mastersService.Getallitemmasterdoclist();
            var allDocListVMs = await _docMangService.GetAllDocList();
            var partStatuses = await _mastersService.GetPartStatus();

            // Ensure unique MasterPartId entries
            var partStatusDict = partStatuses
                .GroupBy(pr => pr.MasterPartId)
                .Select(g => g.First()) // or g.Last(), depending on your needs
                .ToDictionary(pr => pr.MasterPartId, pr => pr.UpdateDate.ToString("MM-dd-yyyy"));

            var tasks = filteredItems.Select(async item =>
            {
                var manuf = await _mastersService.GetManufPart((int)item.PartId);
                item.FinalPart = manuf.FinalPartNosoldtoCustomer != 0 ? "Yes" : "No";

                var contentId = item.MasterPartType == "ManufacturedPart" ? 1 : 2;
                item.MandocAvl = allDocuments.Any(d => d.ContentId == contentId) &&
                                 allDocListVMs.Any(docList => allDocuments.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId))
                                 ? "Yes" : "No";

                var mk = await _mastersService.GetMPMakeFromListByPartId(manuf.ManufacturedPartNoDetailId.ToString());
                item.RmAvl = mk.Any() ? "Yes" : "No";
                item.BomAvl = item.MasterPartType == "Assembly" && (await _mastersService.BOMS(manuf.ManufacturedPartNoDetailId.ToString())).Any() ? "Yes" : "No";
                item.BomAvl = item.MasterPartType == "ManufacturedPart" ? "-" : item.BomAvl;
                item.RmAvl = item.MasterPartType == "Assembly" ? "-" : item.RmAvl;
                item.MasterDisplay = item.MasterPartType;

                var routinglist = await _routingService.Routings(manuf.ManufacturedPartNoDetailId);
                item.RoutingNotAvl = routinglist.Any() ? "Yes" : "No";
                item.RoutingDocNotAvl = "-"; // Default value

                if (routinglist.Any())
                {
                    var oprnos = await _routingService.RoutingSteps(routinglist.First().RoutingId);
                    foreach (var op in oprnos)
                    {
                        var rodocmand = await _operationService.GetOperationalDocTypesByOptId(Convert.ToInt64(op.StepOperation));
                        if (rodocmand.Any() &&
                            allDocListVMs.Any(docList => rodocmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.RoutingId == routinglist.First().RoutingId)))
                        {
                            item.RoutingDocNotAvl = "Yes";
                        }
                        else
                        {
                            item.RoutingDocNotAvl = "No";
                        }
                    }
                }

                if (partStatusDict.TryGetValue((long)item.PartId, out var updateDate))
                {
                    item.StatusChangeDate = updateDate;
                }

                return item;
            });

            result.AddRange(await Task.WhenAll(tasks));
            return Json(result);
        }
        /* 
            var mfpdList = await _mastersService.ItemMasterParts();
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();
            foreach (var item in mfpdList)
            {
                if (item.Company == company)
                {
                    if (item.MasterPartType == "ManufacturedPart")
                    {
                        var manuf = await _mastersService.GetManufPart((int)item.PartId);
                        var mk = await _mastersService.GetMPMakeFromListByPartId(manuf.ManufacturedPartNoDetailId.ToString());
                        if (manuf.FinalPartNosoldtoCustomer == 0)
                        {
                            item.FinalPart = "No";
                        }
                        else
                        {
                            item.FinalPart = "Yes";
                        }
                        var docmand = await _mastersService.Getallitemmasterdoclist();
                        if (docmand.Any(d => d.ContentId == 1))
                        {
                            var docListVMs = await _docMangService.GetAllDocList();
                            if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId)))
                            {
                                item.MandocAvl = "Yes";
                            }
                            else
                            {
                                item.MandocAvl = "No";
                            }
                        }
                        else
                        {
                            item.MandocAvl = "N/A";
                        }
                        if (mk.Count() > 0)
                        {
                            item.RmAvl = "Yes";
                        }
                        else
                        {
                            item.RmAvl = "No";
                        }
                        item.SupplierAvl = "N/A";
                        item.BomAvl = "-";
                        item.MasterDisplay = "ManufacturedPart";
                        var routinglist = await _routingService.Routings(manuf.ManufacturedPartNoDetailId);
                        item.RoutingNotAvl = mk.Count() > 0 ? "Yes" : "No";
                        if (routinglist.Count() > 0)
                        {
                            var oprnos = await _routingService.RoutingSteps(routinglist.FirstOrDefault().RoutingId);
                            foreach (var op in oprnos)
                            {
                                var rodocmand = await _operationService.GetOperationalDocTypesByOptId(Convert.ToInt64(op.StepOperation));
                                if (rodocmand.Any())
                                {
                                    var docListVMs = await _docMangService.GetAllDocList();
                                    if (docListVMs.Any(docList => rodocmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.RoutingId == routinglist.FirstOrDefault().RoutingId)))
                                    {
                                        item.RoutingDocNotAvl = "Yes";
                                    }
                                    else
                                    {
                                        item.RoutingDocNotAvl = "No";
                                    }
                                }
                                else
                                {
                                    item.RoutingDocNotAvl = "-";
                                }
                            }
                        }
                        else
                        {
                            item.RoutingDocNotAvl = "-";
                        }
                        var partstatus = await _mastersService.GetPartStatus();
                        foreach (var pr in partstatus)
                        {
                            if(pr.MasterPartId == item.PartId)
                            {
                                item.StatusChangeDate = pr.UpdateDate.ToString("MM-dd-yyyy");
                            }
                        }
                        result.Add(item);
                    }
                    else if (item.MasterPartType == "Assembly")
                    {
                        var manuf = await _mastersService.GetManufPart((int)item.PartId);
                        var mpmakefromlist = await _mastersService.BOMS(manuf.ManufacturedPartNoDetailId.ToString());
                        if (manuf.FinalPartNosoldtoCustomer == 0)
                        {
                            item.FinalPart = "No";
                        }
                        else
                        {
                            item.FinalPart = "Yes";
                        }
                        var docmand = await _mastersService.Getallitemmasterdoclist();
                        if (docmand.Any(d => d.ContentId == 2))
                        {
                            var docListVMs = await _docMangService.GetAllDocList();
                            if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId)))
                            {
                                item.MandocAvl = "Yes";
                            }
                            else
                            {
                                item.MandocAvl = "No";
                            }
                        }
                        else
                        {
                            item.MandocAvl = "N/A";
                        }
                        if (mpmakefromlist.Count() > 0)
                        {
                            item.BomAvl = "Yes";
                        }
                        else
                        {
                            item.BomAvl = "No";
                        }
                        item.RmAvl = "-";
                        item.SupplierAvl = "N/A";
                        item.MasterDisplay = "Assembly";

                        var routinglist = await _routingService.Routings(manuf.ManufacturedPartNoDetailId);

                        var oprnos = await _routingService.RoutingSteps(routinglist.FirstOrDefault().RoutingId);
                        foreach (var op in oprnos)
                        {
                            var rodocmand = await _operationService.GetOperationalDocTypesByOptId(Convert.ToInt64(op.StepOperation));
                            if (rodocmand.Any())
                            {
                                var docListVMs = await _docMangService.GetAllDocList();
                                if (docListVMs.Any(docList => rodocmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.RoutingId == routinglist.FirstOrDefault().RoutingId)))
                                {
                                    item.RoutingDocNotAvl = "Yes";
                                }
                                else
                                {
                                    item.RoutingDocNotAvl = "No";
                                }
                            }
                            else
                            {
                                item.RoutingDocNotAvl = "N/A";
                            }
                        }
                        item.RoutingNotAvl = routinglist.Count() > 0 ? "Yes" : "No";
                        result.Add(item);
                    }
                }
            }
            return Json(result);*/

        [HttpGet]
        public async Task<IActionResult> RMList()
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var result = new List<ItemMasterPartVM>();
            var docmand = await _mastersService.Getallitemmasterdoclist();
            var docListVMs = await _docMangService.GetAllDocList();
            var rmTypes = await _mastersService.GetRMTypes();
            var baseRms = await _mastersService.GetBaseRMs();
            var specs = await _mastersService.GetRMSpecs();

            foreach (var item in mfpdList.Where(i => i.MasterPartType == "RawMaterial"))
            {
                if (docmand.Any(d => d.ContentId == 3 || d.ContentId == 4 || d.ContentId == 5))
                {
                    item.MandocAvl = docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId)) ? "Yes" : "No";
                }
                else
                {
                    item.MandocAvl = "N/A";
                }
                var rm = await _mastersService.GetRMPart((int)item.PartId);
                item.MasterDisplay = rm.RawMaterialMadeType == 1 ? "Own Purchased RM" : "Customer Supplied RM";
                item.ReorderQnty = rm.ReorderQnty?.ToString() ?? string.Empty;
                item.ReorderLevel = rm.ReorderLevel?.ToString() ?? string.Empty;
                var rmType = rmTypes.FirstOrDefault(x => x.RawMaterialTypeId == rm.RawMaterialTypeId);
                item.RmTypes = rmType?.Name;
                var baseRm = baseRms.FirstOrDefault(x => x.BaseRawMaterialId == rm.BaseRawMaterialId);
                item.BaseRm = baseRm?.Name;
                var spec = specs.FirstOrDefault(x => x.MaterialSpecId == rm.MaterialSpecId);
                item.RmSpecs = spec?.Name;
                var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                if (mk.Any())
                {
                    item.SupplierAvl = "Yes";
                    item.Partused = mk.Count().ToString();
                    item.Price = mk.Select(x => int.TryParse(x.Price, out var price) ? price : 0).Sum().ToString();
                    item.LeadTime = mk.Sum(x => x.LeadTimeInDays).ToString();
                }
                else
                {
                    item.SupplierAvl = "No";
                }
                result.Add(item);
            }

            return Ok(result);
        }
        /*
            var mfpdList = await _mastersService.ItemMasterParts();
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();
            foreach (var item in mfpdList)
            {
               if (item.MasterPartType == "RawMaterial")
                {
                    var docmand = await _mastersService.Getallitemmasterdoclist();
                    if (docmand.Any(d => d.ContentId == 3 || d.ContentId == 4 || d.ContentId == 5))
                    {
                        var docListVMs = await _docMangService.GetAllDocList();
                        if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)))
                        {
                            item.MandocAvl = "Yes";
                        }
                        else
                        {
                            item.MandocAvl = "No";
                        }
                    }
                    else
                    {
                        item.MandocAvl = "N/A";
                    }
                    var rm = await _mastersService.GetRMPart((int)item.PartId);
                    if (rm.RawMaterialMadeType == 1)
                    {
                        item.MasterDisplay = "Own Purchased RM";
                    }
                    else
                    {
                        item.MasterDisplay = "Customer Supplied RM";
                    }
                    item.ReorderQnty = rm.ReorderQnty == null ? string.Empty : rm.ReorderQnty ;
                    item.ReorderLevel = rm.ReorderLevel == null ? string.Empty : rm.ReorderLevel;
                    var rmTypes = await _mastersService.GetRMTypes();
                    var rmname = rmTypes.Where(x => x.RawMaterialTypeId == rm.RawMaterialTypeId).First();
                    item.RmTypes = rmname.Name;
                    var baseRms = await _mastersService.GetBaseRMs();
                    var basename = baseRms.Where(x => x.BaseRawMaterialId == rm.BaseRawMaterialId).First();
                    item.BaseRm = basename.Name;
                    var specs = await _mastersService.GetRMSpecs();
                    var specname = specs.Where(x => x.MaterialSpecId == rm.MaterialSpecId).First();
                    item.RmSpecs = specname.Name;
                    var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                    if (mk.Count() > 0)
                    {
                        item.SupplierAvl = "Yes";
                        item.Partused = mk.Count().ToString();
                        item.Price = mk
                            .Select(x => int.TryParse(x.Price, out var price) ? price : 0) // Parse each Price, defaulting to 0 if invalid
                            .Sum()
                            .ToString();
                        item.LeadTime = mk.Sum(x => x.LeadTimeInDays).ToString();
                    }
                    else
                    {
                        item.SupplierAvl = "No";
                    }
                    item.BomAvl = "N/A";
                    item.RmAvl = "N/A";
                    result.Add(item);
                }
            }
            return Ok(result);
         */

        [HttpGet]
        public async Task<IActionResult> GetAllManufByRM(long partid)
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var result = new List<ItemMasterPartVM>();
            var manufacturedParts = mfpdList
                .Where(item => item.MasterPartType == "ManufacturedPart")
                .ToList();

            var manufTasks = manufacturedParts.Select(async item =>
            {
                var manuf = await _mastersService.GetManufPart((int)item.PartId);
                var mk = await _mastersService.GetMPMakeFromListByPartId(manuf.ManufacturedPartNoDetailId.ToString());

                var preferredMaterial = mk.FirstOrDefault(m => m.MPPartId == partid);
                if (preferredMaterial != null)
                {
                    item.Preferred = preferredMaterial.PreferedRawMaterial ? "Yes" : "No";
                    return item;
                }
                return null; // Return null if no preferred material found
            });
            result.AddRange(await Task.WhenAll(manufTasks) ?? Enumerable.Empty<ItemMasterPartVM>());

            return Ok(result.Where(item => item != null)); // Filter out nulls before returning
        }
        /*
            var mfpdList = await _mastersService.ItemMasterParts();
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();
            foreach (var item in mfpdList)
            {
                if (item.MasterPartType == "ManufacturedPart")
                {
                    var manuf = await _mastersService.GetManufPart((int)item.PartId);
                    var mk = await _mastersService.GetMPMakeFromListByPartId(manuf.ManufacturedPartNoDetailId.ToString());
                    foreach (var m in mk)
                    {
                        if (m.MPPartId == partid)
                        {
                            item.Preferred = m.PreferedRawMaterial ? "Yes" : "No";
                            result.Add(item);
                        }
                    }
                }
            }
            return Ok(result); 
         */

        [HttpGet]
        public async Task<IActionResult> AllBofList()
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var docmand = await _mastersService.Getallitemmasterdoclist();
            var docListVMs = await _docMangService.GetAllDocList();

            var result = new List<ItemMasterPartVM>();

            // Filter for BOF items and process them
            var bofItems = mfpdList.Where(item => item.MasterPartType == "BOF").ToList();

            var bofTasks = bofItems.Select(async item =>
            {
                // Check for document availability
                if (docmand.Any(d => d.ContentId == 6 || d.ContentId == 7 || d.ContentId == 8))
                {
                    item.MandocAvl = docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)) ? "Yes" : "No";
                }
                else
                {
                    item.MandocAvl = "N/A";
                }

                // Get BOF part details
                var rm = await _mastersService.GetBOFPart((int)item.PartId);
                item.MasterDisplay = rm.BoughtOutFinishMadeType switch
                {
                    1 => "Standard BOF",
                    2 => "Catalog BOF",
                    _ => "Purchased Made to Print BOF"
                };
                item.ReorderQnty = rm.ReorderQnty;
                item.ReorderLevel = rm.ReorderLevel;

                // Check for supplier availability
                var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                if (mk.Any())
                {
                    item.SupplierAvl = "Yes";
                    item.Partused = mk.Count().ToString();
                    item.Price = mk
                        .Select(x => int.TryParse(x.Price, out var price) ? price : 0)
                        .Sum()
                        .ToString();
                    item.LeadTime = mk.Sum(x => x.LeadTimeInDays).ToString();
                }
                else
                {
                    item.SupplierAvl = "No";
                }

                item.Notes = "Supplier"; // This seems to override the previous assignment, ensure this is intended
                return item;
            });

            // Await all tasks and add results to the final list
            result.AddRange(await Task.WhenAll(bofTasks));

            // Return the result
            return Ok(result);
        }

        /*
            var mfpdList = await _mastersService.ItemMasterParts();
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();
            foreach (var item in mfpdList)
            {
                if (item.MasterPartType == "BOF")
                {
                    var docmand = await _mastersService.Getallitemmasterdoclist();
                    if (docmand.Any(d => d.ContentId == 6 || d.ContentId == 7 || d.ContentId == 8))
                    {
                        var docListVMs = await _docMangService.GetAllDocList();
                        if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)))
                        {
                            item.MandocAvl = "Yes";
                        }
                        else
                        {
                            item.MandocAvl = "No";
                        }
                    }
                    else
                    {
                        item.MandocAvl = "N/A";
                    }
                    var rm = await _mastersService.GetBOFPart((int)item.PartId);
                    if (rm.BoughtOutFinishMadeType == 1)
                    {
                        item.MasterDisplay = "Standard BOF";
                    }
                    else if (rm.BoughtOutFinishMadeType == 2)
                    {
                        item.MasterDisplay = "Catalog BOF";
                    }
                    else
                    {
                        item.MasterDisplay = "Purchased Made to Print BOF";
                    }
                    item.ReorderQnty = rm.ReorderQnty;
                    item.ReorderLevel = rm.ReorderLevel;
                    var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                    if (mk.Count() > 0)
                    {
                        item.SupplierAvl = "Yes";
                        item.Partused = mk.Count().ToString();
                        item.Price = mk
                            .Select(x => int.TryParse(x.Price, out var price) ? price : 0) // Parse each Price, defaulting to 0 if invalid
                            .Sum()
                            .ToString();
                        item.LeadTime = mk.Sum(x => x.LeadTimeInDays).ToString();
                    }
                    else
                    {
                        item.SupplierAvl = "No";
                    }
                    item.MasterDisplay = "Supplier";
                    result.Add(item);
                }
            }
            return Ok(result); 
         */
        [HttpGet]
        public async Task<IActionResult> GetAllAssemByBof(long partid)
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var result = new List<ItemMasterPartVM>();
            var assemblyItems = mfpdList.Where(item => item.MasterPartType == "Assembly").ToList();
            var assemblyTasks = assemblyItems.Select(async item =>
            {
                var manuf = await _mastersService.GetManufPart((int)item.PartId);
                var mpmakefromlist = await _mastersService.BOMS(manuf.ManufacturedPartNoDetailId.ToString());
                var matchingBOM = mpmakefromlist.FirstOrDefault(m => m.BOMPartId == partid);
                if (matchingBOM != null)
                {
                    item.Preferred = mpmakefromlist.Count() == 1 ? "Yes" : "No";
                    return item; // Return the modified item
                }
                return null; // Return null if no match found
            });
            var processedItems = await Task.WhenAll(assemblyTasks);
            result.AddRange(processedItems.Where(item => item != null)); 
            return Ok(result);
        }
        /* 
         * 
            var mfpdList = await _mastersService.ItemMasterParts();
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();
            foreach (var item in mfpdList)
            {
                if (item.MasterPartType == "Assembly")
                {
                    var manuf = await _mastersService.GetManufPart((int)item.PartId);
                    var mpmakefromlist = await _mastersService.BOMS(manuf.ManufacturedPartNoDetailId.ToString());
                    foreach (var m in mpmakefromlist)
                    {
                        if (m.BOMPartId == partid)
                        {
                            item.Preferred = mpmakefromlist.Count() == 1 ? "Yes" : "No";
                            result.Add(item);
                        }
                    }
                }
            }
            return Ok(result);
         */

        [HttpGet]
        public async Task<IActionResult> RmByComp()
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var docmand = await _mastersService.Getallitemmasterdoclist();
            var docListVMs = await _docMangService.GetAllDocList();

            var result = mfpdList
                .GroupBy(item => item.Company)
                .Select(async companyGroup =>
                {
                    int rawMaterialActive = companyGroup.Count(item => item.MasterPartType == "RawMaterial" && item.Status == "Active");
                    int rawMaterialInactive = companyGroup.Count(item => item.MasterPartType == "RawMaterial" && item.Status != "Active");

                    int manufActive = 0;
                    int manufInactive = 0;
                    int assemActive = 0;
                    int preferred = 0;
                    int partsupp = 0;
                    string doc = "";
                    var tasks = companyGroup
                        .Where(item => item.MasterPartType == "RawMaterial")
                        .Select(async item =>
                        {
                            var rms = await _mastersService.GetRMPart((int)item.PartId);
                            var supplier = await _mastersService.PartPurchasesFor((int)rms.PartId);
                            doc = docmand.Any(d => d.ContentId == 3 || d.ContentId == 4 || d.ContentId == 5)
                           ? docListVMs.Count(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId)).ToString()
                           : "0";
                            preferred += supplier.Count(x => x.PreferredSupplier == 1);
                            partsupp += supplier.Count();

                            if (rms.RawMaterialMadeType == 1)
                            {
                                if (rms.RawMaterialMadeSubType == 1)
                                {
                                    manufActive++;
                                }
                                else
                                {
                                    manufInactive++;
                                }
                            }
                            else
                            {
                                assemActive++;
                            }
                        });

                    await Task.WhenAll(tasks); // Await all tasks

        var firstItem = companyGroup.First(); // Use the first item to initialize a representative object
        var companySummary = new ItemMasterPartVM
                    {
                        Company = firstItem.Company,
                        MasterDisplay = "Supplier",
                        NoOfManufActive = manufActive.ToString(),
                        NoOfAssemblyActive = manufInactive.ToString(),
                        NoOfManufInActive = assemActive.ToString(),
                        NoOfActive = rawMaterialActive.ToString(),
                        Preferred = preferred.ToString(),
                        PartSupplied = partsupp.ToString(),
                        NoOfInActive = rawMaterialInactive.ToString(),
                        MandocAvl = doc
                    };

                    return rawMaterialActive > 0 || rawMaterialInactive > 0 ? companySummary : null; // Return null if no relevant data
    });

            // Await all summaries and filter out nulls
            var summaries = await Task.WhenAll(result);
            return Ok(summaries.Where(summary => summary != null)); // Filter out nulls before returning
        }
        /*
            var mfpdList = await _mastersService.ItemMasterParts();
            var groupedByCompany = mfpdList.GroupBy(item => item.Company);
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();

            foreach (var companyGroup in groupedByCompany)
            {
                int rawMaterialActive = 0;
                int rawMaterialInactive = 0;
                int manufActive = 0;
                int manufInactive = 0;
                int assemActive = 0;
                int preferred = 0;
                int partsupp = 0;

                // Aggregate counts for raw material status within the company
                foreach (var item in companyGroup)
                {
                    if (item.MasterPartType == "RawMaterial")
                    {
                        if (item.Status == "Active")
                            rawMaterialActive++;
                        else
                            rawMaterialInactive++;
                        var rms = await _mastersService.GetRMPart((int)item.PartId);
                        var supplier = await _mastersService.PartPurchasesFor((int)rms.PartId);
                        preferred = supplier.Where(x => x.PreferredSupplier == 1).Count();
                        if (rms.RawMaterialMadeType == 1)
                        {
                            if (rms.RawMaterialMadeSubType == 1)
                            {
                                manufActive++;
                            }
                            else
                            {
                                manufInactive++;
                            }
                        }
                        else
                        {
                            assemActive++;
                        }
                        partsupp = supplier.Count();
                    }
                }

                // Create a summary entry for the company with raw material data
                var firstItem = companyGroup.First(); // Use the first item to initialize a representative object
                var companySummary = new ItemMasterPartVM
                {
                    Company = firstItem.Company,
                    MasterDisplay = "Supplier",
                    NoOfManufActive = manufActive.ToString(),
                    NoOfAssemblyActive = manufInactive.ToString(),
                    NoOfManufInActive = assemActive.ToString(),
                    NoOfActive = rawMaterialActive.ToString(),
                    Preferred = preferred.ToString(),
                    PartSupplied = partsupp.ToString(),
                    NoOfInActive = rawMaterialInactive.ToString()
                };

                var docmand = await _mastersService.Getallitemmasterdoclist();
                if (docmand.Any(d => d.ContentId == 3 || d.ContentId == 4 || d.ContentId == 5))
                {
                    var docListVMs = await _docMangService.GetAllDocList();

                    // Count the matching documents based on DocumentTypeId
                    var count = docListVMs.Count(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId));
                    companySummary.MandocAvl = count.ToString(); // Assign the count to MandocAvl as a string
                }
                else
                {
                    companySummary.MandocAvl = "0"; // Set to "0" if no relevant ContentId is found
                }
                if (rawMaterialActive > 0 || rawMaterialInactive > 0) // Only add if there's relevant raw material data
                {
                    result.Add(companySummary);
                }
            }

            return Ok(result); 
         */
        [HttpGet]
        public async Task<IActionResult> RmSupplierList(string company)
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var docmand = await _mastersService.Getallitemmasterdoclist();
            var docListVMs = await _docMangService.GetAllDocList();
            var rmTypes = await _mastersService.GetRMTypes();
            var baseRms = await _mastersService.GetBaseRMs();
            var specs = await _mastersService.GetRMSpecs();
            var partStatus = await _mastersService.GetPartStatus();

            var result = new List<ItemMasterPartVM>();

            foreach (var item in mfpdList.Where(item => item.Company == company && item.MasterPartType == "RawMaterial"))
            {
                // Determine document availability
                if (docmand.Any(d => d.ContentId == 3 || d.ContentId == 4 || d.ContentId == 5))
                {
                    item.MandocAvl = docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId)) ? "Yes" : "No";
                }
                else
                {
                    item.MandocAvl = "N/A";
                }

                // Get raw material details
                var rm = await _mastersService.GetRMPart((int)item.PartId);
                item.MasterDisplay = rm.RawMaterialMadeType == 1 ? "Own Purchased RM" : "Customer Supplied RM";
                item.ReorderQnty = rm.ReorderQnty?.ToString() ?? string.Empty;
                item.ReorderLevel = rm.ReorderLevel?.ToString() ?? string.Empty;

                // Set RM Types, Base RM, and Specs
                item.RmTypes = rmTypes.FirstOrDefault(x => x.RawMaterialTypeId == rm.RawMaterialTypeId)?.Name ?? "Unknown";
                item.BaseRm = baseRms.FirstOrDefault(x => x.BaseRawMaterialId == rm.BaseRawMaterialId)?.Name ?? "Unknown";
                item.RmSpecs = specs.FirstOrDefault(x => x.MaterialSpecId == rm.MaterialSpecId)?.Name ?? "Unknown";

                // Check supplier availability
                var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                if (mk.Any())
                {
                    item.SupplierAvl = "Yes";
                    item.Partused = mk.Count().ToString();
                    item.Price = mk.Select(x => int.TryParse(x.Price, out var price) ? price : 0).Sum().ToString();
                    item.LeadTime = mk.Sum(x => x.LeadTimeInDays).ToString();
                }
                else
                {
                    item.SupplierAvl = "No";
                }

                // Set BOM and RM availability
                item.BomAvl = "N/A";
                item.RmAvl = "N/A";

                // Set status change date
                var status = partStatus.FirstOrDefault(pr => pr.MasterPartId == item.PartId);
                item.StatusChangeDate = status?.UpdateDate.ToString("MM-dd-yyyy") ?? string.Empty;

                result.Add(item);
            }

            return Ok(result);
        }
        /*
            var mfpdList = await _mastersService.ItemMasterParts();
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();
            foreach (var item in mfpdList)
            {
                if (item.Company == company)
                {
                    if (item.MasterPartType == "RawMaterial")
                    {
                        var docmand = await _mastersService.Getallitemmasterdoclist();
                        if (docmand.Any(d => d.ContentId == 3 || d.ContentId == 4 || d.ContentId == 5))
                        {
                            var docListVMs = await _docMangService.GetAllDocList();
                            if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)))
                            {
                                item.MandocAvl = "Yes";
                            }
                            else
                            {
                                item.MandocAvl = "No";
                            }
                        }
                        else
                        {
                            item.MandocAvl = "N/A";
                        }
                        var rm = await _mastersService.GetRMPart((int)item.PartId);
                        if (rm.RawMaterialMadeType == 1)
                        {
                            item.MasterDisplay = "Own Purchased RM";
                        }
                        else
                        {
                            item.MasterDisplay = "Customer Supplied RM";
                        }
                        item.ReorderQnty = rm.ReorderQnty == null ? string.Empty : rm.ReorderQnty;
                        item.ReorderLevel = rm.ReorderLevel == null ? string.Empty : rm.ReorderLevel;
                        var rmTypes = await _mastersService.GetRMTypes();
                        var rmname = rmTypes.Where(x => x.RawMaterialTypeId == rm.RawMaterialTypeId).First();
                        item.RmTypes = rmname.Name;
                        var baseRms = await _mastersService.GetBaseRMs();
                        var basename = baseRms.Where(x => x.BaseRawMaterialId == rm.BaseRawMaterialId).First();
                        item.BaseRm = basename.Name;
                        var specs = await _mastersService.GetRMSpecs();
                        var specname = specs.Where(x => x.MaterialSpecId == rm.MaterialSpecId).First();
                        item.RmSpecs = specname.Name;
                        var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                        if (mk.Count() > 0)
                        {
                            item.SupplierAvl = "Yes";
                            item.Partused = mk.Count().ToString();
                            item.Price = mk
                                .Select(x => int.TryParse(x.Price, out var price) ? price : 0) // Parse each Price, defaulting to 0 if invalid
                                .Sum()
                                .ToString();
                            item.LeadTime = mk.Sum(x => x.LeadTimeInDays).ToString();
                        }
                        else
                        {
                            item.SupplierAvl = "No";
                        }
                        item.BomAvl = "N/A";
                        item.RmAvl = "N/A";
                        var partstatus = await _mastersService.GetPartStatus();
                        foreach (var pr in partstatus)
                        {
                            if (pr.MasterPartId == item.PartId)
                            {
                                item.StatusChangeDate = pr.UpdateDate.ToString("MM-dd-yyyy");
                            }
                        }
                        result.Add(item);
                    }
                }
            }
            return Ok(result); 
        */
        [HttpGet]
        public async Task<IActionResult> BofByComp()
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var docmand = await _mastersService.Getallitemmasterdoclist();
            var docListVMs = await _docMangService.GetAllDocList();
            var result = new List<ItemMasterPartVM>();

            var groupedByCompany = mfpdList
                .Where(item => item.MasterPartType == "BOF")
                .GroupBy(item => item.Company);

            foreach (var companyGroup in groupedByCompany)
            {
                int rawMaterialActive = companyGroup.Count(item => item.Status == "Active");
                int rawMaterialInactive = companyGroup.Count(item => item.Status != "Active");
                int manufActive = 0;
                int manufInactive = 0;
                int assemActive = 0;
                int preferred = 0;
                int partsupp = 0;
                string docCount = "0"; // Default to "0"
                var tasks = companyGroup.Select(async item =>
                {
                    var rms = await _mastersService.GetBOFPart((int)item.PartId);
                    var supplier = await _mastersService.PartPurchasesFor((int)rms.PartId);
                    preferred += supplier.Count(x => x.PreferredSupplier == 1);
                    partsupp += supplier.Count();
                    switch (rms.BoughtOutFinishMadeType)
                    {
                        case 1:
                            manufActive++;
                            break;
                        case 2:
                            manufInactive++;
                            break;
                        default:
                            assemActive++;
                            break;
                    }
                    if (docmand.Any(d => d.ContentId == 6 || d.ContentId == 7 || d.ContentId == 8))
                    {
                        docCount = (docListVMs.Count(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId))).ToString();
                    }

                    return (manufActive, manufInactive, assemActive, preferred, partsupp, docCount);
                });
                var results = await Task.WhenAll(tasks);
                //foreach (var (manufActiveCount, manufInactiveCount, assemActiveCount, preferredCount, partsuppCount, docCountValue) in results)
                //{
                //    manufActive += manufActiveCount;
                //    manufInactive += manufInactiveCount;
                //    assemActive += assemActiveCount;
                //    preferred += preferredCount;
                //    partsupp += partsuppCount;
                //    docCount = docCountValue; // Use the last counted docCount
                //}
                var companySummary = new ItemMasterPartVM
                {
                    Company = companyGroup.Key,
                    MasterDisplay = "Supplier",
                    NoOfManufActive = manufActive.ToString(),
                    NoOfAssemblyActive = manufInactive.ToString(),
                    NoOfManufInActive = assemActive.ToString(),
                    NoOfActive = rawMaterialActive.ToString(),
                    Preferred = preferred.ToString(),
                    PartSupplied = partsupp.ToString(),
                    MandocAvl = docCount,
                    NoOfInActive = rawMaterialInactive.ToString()
                };
                if (rawMaterialActive > 0 || rawMaterialInactive > 0)
                {
                    result.Add(companySummary);
                }
            }

            return Ok(result);
        }
        /*
         * 
            var mfpdList = await _mastersService.ItemMasterParts();
            var groupedByCompany = mfpdList.GroupBy(item => item.Company);
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();

            foreach (var companyGroup in groupedByCompany)
            {
                int rawMaterialActive = 0;
                int rawMaterialInactive = 0;
                int manufActive = 0;
                int manufInactive = 0;
                int assemActive = 0;
                int preferred = 0;
                int partsupp = 0;
                string doc = string.Empty;

                // Aggregate counts for raw material status within the company
                foreach (var item in companyGroup)
                {
                    if (item.MasterPartType == "BOF")
                    {
                        if (item.Status == "Active")
                            rawMaterialActive++;
                        else
                            rawMaterialInactive++;
                        var rms = await _mastersService.GetBOFPart((int)item.PartId);
                        var supplier = await _mastersService.PartPurchasesFor((int)rms.PartId);
                        preferred = supplier.Where(x => x.PreferredSupplier == 1).Count();
                        if (rms.BoughtOutFinishMadeType == 1)
                        {
                                manufActive++;
                        }
                        else if(rms.BoughtOutFinishMadeType == 2)
                        {
                            manufInactive++;
                        }
                        else
                        {
                            assemActive++;
                        }
                        partsupp = supplier.Count();
                        var docmand = await _mastersService.Getallitemmasterdoclist();
                        if (docmand.Any(d => d.ContentId == 6 || d.ContentId == 7 || d.ContentId == 8))
                        {
                            var docListVMs = await _docMangService.GetAllDocList();

                            // Count the matching documents based on DocumentTypeId
                            var count = docListVMs.Count(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId));
                            doc = count.ToString(); // Assign the count to MandocAvl as a string
                        }
                        else
                        {
                            doc = "0"; // Set to "0" if no relevant ContentId is found
                        }
                    }
                }

                // Create a summary entry for the company with raw material data
                var firstItem = companyGroup.First(); // Use the first item to initialize a representative object
                var companySummary = new ItemMasterPartVM
                {
                    Company = firstItem.Company,
                    MasterDisplay = "Supplier",
                    NoOfManufActive = manufActive.ToString(),
                    NoOfAssemblyActive = manufInactive.ToString(),
                    NoOfManufInActive = assemActive.ToString(),
                    NoOfActive = rawMaterialActive.ToString(),
                    Preferred = preferred.ToString(),
                    PartSupplied = partsupp.ToString(),
                    MandocAvl = doc,
                    NoOfInActive = rawMaterialInactive.ToString()
                };

                if (rawMaterialActive > 0 || rawMaterialInactive > 0) // Only add if there's relevant raw material data
                {
                    result.Add(companySummary);
                }
            }

            return Ok(result);
         */
        [HttpGet]
        public async Task<IActionResult> BofSumByComp(string company)
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            var docmand = await _mastersService.Getallitemmasterdoclist();
            var docListVMs = await _docMangService.GetAllDocList();
            var partStatusList = await _mastersService.GetPartStatus();

            var result = mfpdList
                .Where(item => item.Company == company && item.MasterPartType == "BOF")
                .Select(async item =>
                {
                    var rm = await _mastersService.GetBOFPart((int)item.PartId);
                    item.MasterDisplay = rm.BoughtOutFinishMadeType switch
                    {
                        1 => "Standard BOF",
                        2 => "Catalog BOF",
                        _ => "Purchased Made to Print BOF"
                    };

                    item.ReorderQnty = rm.ReorderQnty;
                    item.ReorderLevel = rm.ReorderLevel;

                    var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                    if (mk.Any())
                    {
                        item.SupplierAvl = "Yes";
                        item.Partused = mk.Count().ToString();
                        item.Price = mk
                            .Select(x => int.TryParse(x.Price, out var price) ? price : 0)
                            .Sum()
                            .ToString();
                        item.LeadTime = mk.Sum(x => x.LeadTimeInDays).ToString();
                    }
                    else
                    {
                        item.SupplierAvl = "No";
                    }

        // Determine document availability
        item.MandocAvl = docmand.Any(d => d.ContentId == 6 || d.ContentId == 7 || d.ContentId == 8) &&
                                     docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId && docList.PartId == item.PartId))
                        ? "Yes"
                        : (docmand.Any(d => d.ContentId == 6 || d.ContentId == 7 || d.ContentId == 8) ? "No" : "N/A");

        // Set status change date
        var status = partStatusList.FirstOrDefault(pr => pr.MasterPartId == item.PartId);
                    item.StatusChangeDate = status?.UpdateDate.ToString("MM-dd-yyyy") ?? string.Empty;

                    return item; // Return the modified item
    });

            var finalResult = await Task.WhenAll(result);
            return Ok(finalResult);
        }
        /* 
         * 
            var mfpdList = await _mastersService.ItemMasterParts();
            List<ItemMasterPartVM> result = new List<ItemMasterPartVM>();
            foreach (var item in mfpdList)
            {
                if (item.Company == company)
                {
                    if (item.MasterPartType == "BOF")
                    {
                        var docmand = await _mastersService.Getallitemmasterdoclist();
                        if (docmand.Any(d => d.ContentId == 6 || d.ContentId == 7 || d.ContentId == 8))
                        {
                            var docListVMs = await _docMangService.GetAllDocList();
                            if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)))
                            {
                                item.MandocAvl = "Yes";
                            }
                            else
                            {
                                item.MandocAvl = "No";
                            }
                        }
                        else
                        {
                            item.MandocAvl = "N/A";
                        }
                        var rm = await _mastersService.GetBOFPart((int)item.PartId);
                        if (rm.BoughtOutFinishMadeType == 1)
                        {
                            item.MasterDisplay = "Standard BOF";
                        }
                        else if (rm.BoughtOutFinishMadeType == 2)
                        {
                            item.MasterDisplay = "Catalog BOF";
                        }
                        else
                        {
                            item.MasterDisplay = "Purchased Made to Print BOF";
                        }
                        item.ReorderQnty = rm.ReorderQnty;
                        item.ReorderLevel = rm.ReorderLevel;
                        var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                        if (mk.Count() > 0)
                        {
                            item.SupplierAvl = "Yes";
                            item.Partused = mk.Count().ToString();
                            item.Price = mk
                                .Select(x => int.TryParse(x.Price, out var price) ? price : 0) // Parse each Price, defaulting to 0 if invalid
                                .Sum()
                                .ToString();
                            item.LeadTime = mk.Sum(x => x.LeadTimeInDays).ToString();
                        }
                        else
                        {
                            item.SupplierAvl = "No";
                        }
                        var partstatus = await _mastersService.GetPartStatus();
                        foreach (var pr in partstatus)
                        {
                            if (pr.MasterPartId == item.PartId)
                            {
                                item.StatusChangeDate = pr.UpdateDate.ToString("MM-dd-yyyy");
                            }
                        }
                        result.Add(item);
                    }
                }
            }
            return Ok(result);
         * */
        [HttpGet]
        public async Task<IActionResult> MasterParts()
        {
            var mfpdList = await _mastersService.ItemMasterParts();
            foreach (var item in mfpdList)
            {
                if (item.MasterPartType == "ManufacturedPart")
                {
                    var manuf = await _mastersService.GetManufPart((int)item.PartId);
                    var mk = await _mastersService.GetMPMakeFromListByPartId(manuf.ManufacturedPartNoDetailId.ToString());
                    if (manuf.FinalPartNosoldtoCustomer == 0)
                    {
                        item.FinalPart = "N";
                    }
                    else
                    {
                        item.FinalPart = "Y";
                    }
                    var docmand = await _mastersService.Getallitemmasterdoclist();
                    if (docmand.Any(d => d.ContentId == 1))
                    {
                        var docListVMs = await _docMangService.GetAllDocList();
                        if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)))
                        {
                            item.MandocAvl = "Yes";
                        }
                        else
                        {
                            item.MandocAvl = "No";
                        }
                    }
                    else
                    {
                        item.MandocAvl = "N/A";
                    }
                    if (mk.Count() > 0)
                    {
                        item.RmAvl = "Yes";
                    }
                    else
                    {
                        item.RmAvl = "No";
                    }
                    item.SupplierAvl = "N/A";
                    item.BomAvl = "N/A";
                    item.MasterDisplay = "ManufacturedPart";
                }
                else if (item.MasterPartType == "Assembly")
                {
                    var manuf = await _mastersService.GetManufPart((int)item.PartId);
                    var mpmakefromlist = await _mastersService.BOMS(manuf.ManufacturedPartNoDetailId.ToString());
                    if (manuf.FinalPartNosoldtoCustomer == 0)
                    {
                        item.FinalPart = "N";
                    }
                    else
                    {
                        item.FinalPart = "Y";
                    }
                    var docmand = await _mastersService.Getallitemmasterdoclist();
                    if (docmand.Any(d => d.ContentId == 2))
                    {
                        var docListVMs = await _docMangService.GetAllDocList();
                        if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)))
                        {
                            item.MandocAvl = "Yes";
                        }
                        else
                        {
                            item.MandocAvl = "No";
                        }
                    }
                    else
                    {
                        item.MandocAvl = "N/A";
                    }
                    if (mpmakefromlist.Count() > 0)
                    {
                        item.BomAvl = "Yes";
                    }
                    else
                    {
                        item.BomAvl = "No";
                    }
                    item.RmAvl = "N/A";
                    item.SupplierAvl = "N/A";
                    item.MasterDisplay = "Assembly";
                }
                else if (item.MasterPartType == "BOF")
                {
                    var docmand = await _mastersService.Getallitemmasterdoclist();
                    if (docmand.Any(d => d.ContentId == 6 || d.ContentId == 7 || d.ContentId == 8))
                    {
                        var docListVMs = await _docMangService.GetAllDocList();
                        if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)))
                        {
                            item.MandocAvl = "Yes";
                        }
                        else
                        {
                            item.MandocAvl = "No";
                        }
                    }
                    else
                    {
                        item.MandocAvl = "N/A";
                    }
                    if (item.BoughtOutFinishMadeType == 1)
                    {
                        item.MasterDisplay = "Standard BOF";
                    }
                    else if (item.BoughtOutFinishMadeType == 2)
                    {
                        item.MasterDisplay = "Catalog BOF";
                    }
                    else
                    {
                        item.MasterDisplay = "Purchased Made to Print BOF";
                    }
                    var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                    if (mk.Count() > 0)
                    {
                        item.SupplierAvl = "Yes";
                    }
                    else
                    {
                        item.SupplierAvl = "No";
                    }
                    item.BomAvl = "N/A";
                    item.RmAvl = "N/A";

                }
                else if (item.MasterPartType == "RawMaterial")
                {
                    var docmand = await _mastersService.Getallitemmasterdoclist();
                    if (docmand.Any(d => d.ContentId == 3 || d.ContentId == 4 || d.ContentId == 5))
                    {
                        var docListVMs = await _docMangService.GetAllDocList();
                        if (docListVMs.Any(docList => docmand.Any(docMand => docList.DocumentTypeId == docMand.DocumentTypeId)))
                        {
                            item.MandocAvl = "Yes";
                        }
                        else
                        {
                            item.MandocAvl = "No";
                        }
                    }
                    else
                    {
                        item.MandocAvl = "N/A";
                    }
                    var rm = await _mastersService.GetRMPart((int)item.PartId);
                    if (rm.RawMaterialMadeType == 1)
                    {
                        item.MasterDisplay = "Own Purchased RM";
                    }
                    else
                    {
                        item.MasterDisplay = "Customer Supplied RM";
                    }
                    var mk = await _mastersService.PartPurchasesFor((int)item.PartId);
                    if (mk.Count() > 0)
                    {
                        item.SupplierAvl = "Yes";
                    }
                    else
                    {
                        item.SupplierAvl = "No";
                    }
                    item.BomAvl = "N/A";
                    item.RmAvl = "N/A";
                }
            }
            return Json(mfpdList);
        }

        [HttpGet]
        public async Task<IActionResult> SelectParts()
        {
            var mfpdList = await _mastersService.SelectParts();
            return Json(mfpdList);
        }

        [HttpGet]
        public async Task<IActionResult> OwnRMS()
        {
            var mfpdList = await _mastersService.OwnRMS();
            var rmTypes = await _mastersService.GetRMTypes();
            foreach (var item in mfpdList)
            {
                foreach (var rmtype in rmTypes)
                {
                    if (item.RawMaterialTypeId == rmtype.RawMaterialTypeId)
                    {
                        item.MultiplePartsMadeFrom1InputRM = rmtype.MultiplePartsMadeFrom1InputRM;
                    }
                }
            }
            return Ok(mfpdList);
        }

        [HttpPost]
        public async Task<IActionResult> PreferredSupplier(PartPurchaseDetailsVM partPurchaseDetails)
        {
            var result = await _mastersService.PreferredSupplier(partPurchaseDetails);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> MPPreferredMK(MPMakeFromVM makeFromVM)
        {
            var result = await _mastersService.MPPreferredMK(makeFromVM);
            return Ok(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BaseRM(BaseRawMatrialVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.BaseRM(model);
            return Ok(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RMType(RawMateriaTypeVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.RMType(model);
            return Ok(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RMSpec(RawMaterialSpecVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.RMSpec(model);
            return Ok(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RMStandard(RawMaterialStandardVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.RMStandard(model);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> BOMS(string partId)//pass Id from ManufNoPartDetails
        {
            var mpmakefromlist = await _mastersService.BOMS(partId);
            var partsuoms = await _mastersService.GetPartsUOMs();

            foreach (MPBomVM mpvm in mpmakefromlist)
            {
                foreach (PartUOMVM partu in partsuoms)
                {
                    if (mpvm.BOMPartId == partu.PartId)
                    {
                        mpvm.UOM = partu.UOMName;
                    }
                }
            }
            return Ok(mpmakefromlist);

        }
        [HttpGet]
        public async Task<IActionResult> MPMakeFromList(string partId)
        {
            var mpmakefromlist = await _mastersService.GetMPMakeFromListByPartId(partId);
            var partsuoms = await _mastersService.GetPartsUOMs();

            foreach (MPMakeFromVM mpvm in mpmakefromlist)
            {
                foreach (PartUOMVM partu in partsuoms)
                {
                    if (mpvm.MPPartId == partu.PartId)
                    {
                        mpvm.UOM = partu.UOMName;
                    }
                }
                if (mpvm.MPPartMadeFrom == 1)
                {
                    mpvm.MadeFrom = "Customer Supplied Raw Material";
                } else if (mpvm.MPPartMadeFrom == 2)
                {
                    mpvm.MadeFrom = "Own Raw Material";
                }
                else
                {
                    mpvm.MadeFrom = "Other Manufactured Part";
                }
            }
            return Ok(mpmakefromlist);
        }

        [HttpGet]
        public async Task<IActionResult> SortedMPMakeFromList(string partId)
        {
            try
            {
                //getmaterparts
                var mf = await _mastersService.GetMPMakeFromListByPartId(partId);
                var mpart = await _mastersService.ItemMasterParts();
                List<MPMakeFromVM> MFList = new List<MPMakeFromVM>();
                var query = from mfl in mf
                            join mp in mpart on mfl.MPPartId equals mp.PartId
                            select new MPMakeFromVM
                            {
                                MPPartId = mfl.MPPartId,
                                MPPartMadeFrom = mfl.MPPartMadeFrom,
                                InputWeight = mfl.InputWeight,
                                ScrapGenerated = mfl.ScrapGenerated,
                                QuantityPerInput = mfl.QuantityPerInput,
                                YieldNotes = mfl.YieldNotes,
                                PreferedRawMaterial = mfl.PreferedRawMaterial,
                                ManufPartId = mfl.ManufPartId,
                                MPMakeFromId = mfl.MPMakeFromId,
                                MFDescription = mfl.MFDescription,
                                InputPartNo = mp.PartNo,
                                UOM = mfl.UOM,
                                TenantId = mfl.TenantId
                            };
                MFList = query
                     .OrderBy(x => x.PreferedRawMaterial)
                    .ToList();
                return Ok(MFList);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemMakeFrom(MPMakeFromVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.RemMakeFrom(model);
            return Ok(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemBOM(MPBomVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.RemBOM(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Getallitemmasterdoclist()
        {
            var result = await _mastersService.Getallitemmasterdoclist();
            List<DocListVM> docListVM = new List<DocListVM>();
            var docListVMs = await _docMangService.GetAllDocList();
            var doctype = await _docMangService.GetAllDocumentType();
            var custRetnDataVMs = await _docMangService.GetAllCustRet();
            var companies = await _mastersService.GetCompanies();
            var manufacturedPartNos = await _mastersService.ItemMasterParts();
            var extns = await _docMangService.GetAllFileExtn();

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
                    foreach (var extn in extns)
                    {
                        if (doc.ExtnId == extn.ExtnId)
                        {
                            item.FileExtnName = extn.ExtnName;
                        }
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
                var partbyid = await _mastersService.GetManufPart((int)item.PartId);
                item.PartNo = partbyid.PartNo;
                item.PartDesc = partbyid.PartDescription;
                //var routingListItems = await _routingService.Routings((int)item.PartId);
                //foreach (var route in routingListItems)
                //{
                //    if (route.PreferredRouting == 1)
                //    {
                //        if (route.RoutingId == item.RoutingId)
                //        {
                //            item.RoutingName = route.RoutingName;
                //        }
                //    }
                //}
                foreach (var part in manufacturedPartNos)
                {
                    if (item.PartId == part.PartId)
                    {



                    }
                }
                if (item.StorageLocation == "/Archive")
                {
                    item.Archive = 'Y';
                }
                else
                {
                    item.Archive = 'N';
                }
                foreach (var master in result)
                {
                    if (item.DocumentTypeId == master.DocumentTypeId)
                    {
                        item.UpdatedOnStr = master.UpdatedOnStr;
                        item.Mandatory = master.Mandatory;
                        docListVM.Add(item);
                    }
                }
            }
            return Ok(docListVM);
        }
        [HttpGet]
        public async Task<IActionResult> CheckDocumentTypeInItemMaster(long documentTypeId, long contentId)
        {
            var result = await _mastersService.CheckDocumentTypeInItemMaster(documentTypeId, contentId);
            return Json(!result);
        }

        [HttpGet]
        public async Task<IActionResult> CheckDocTypeInDocList(long docTypeid)
        {
            var result = await _mastersService.CheckDocTypeInDocList(docTypeid);
            return Json(!result);
        }

        [HttpGet]
        public async Task<IActionResult> BOFDoclist(int contentid, long partid)
        {
            var result = await _mastersService.Getallitemmasterdoclist();
            var docListVMs = await _docMangService.GetAllDocList();
            var doctype = await _docMangService.GetAllDocumentType();
            var custRetnDataVMs = await _docMangService.GetAllCustRet();
            var companies = await _mastersService.GetCompanies();
            var extns = await _docMangService.GetAllFileExtn();

            List<DocListVM> docListVM = new List<DocListVM>();
            List<DocListVM> empdocLists = new List<DocListVM>();

            if (partid == 0)
            {
                // Show all document types related to contentid when partid is 0
                foreach (var master in result.Where(m => m.ContentId == contentid))
                {
                    DocListVM empDoc = new DocListVM();
                    PopulateItemFields(empDoc, master, doctype, extns, custRetnDataVMs, companies);
                    empDoc.Comments = string.Empty;
                    empDoc.DeletionDate = DateTime.Now;
                    empdocLists.Add(empDoc);
                }
                return Ok(empdocLists);
            }
            else
            {
                // When partid != 0, show docListVMs related to contentid and partid
                bool hasEntries = false;
                foreach (var item in docListVMs.Where(d => d.PartId == partid && result.Any(m => m.ContentId == contentid && m.DocumentTypeId == d.DocumentTypeId)))
                {
                    PopulateItemFields(item, result.First(m => m.ContentId == contentid && m.DocumentTypeId == item.DocumentTypeId), doctype, extns, custRetnDataVMs, companies);
                    ClaimsPrincipal userClaim = HttpContext.User; // Assuming you're in a controller or middleware
                    string fullName = AppUtil.GetFullName(userClaim);
                    item.UpdatedOnStr =item.CreationDt.ToString("MM-dd-yyyy"); 
                    item.UploadedBy = fullName;

                    docListVM.Add(item);
                    hasEntries = true;
                }

                // If there are document types related to contentid without entries in docListVMs, add to empdocLists
                if (!hasEntries || docListVM.Count < result.Count(m => m.ContentId == contentid))
                {
                    foreach (var master in result.Where(m => m.ContentId == contentid && !docListVM.Any(d => d.DocumentTypeId == m.DocumentTypeId)))
                    {
                        DocListVM empDoc = new DocListVM();
                        PopulateItemFields(empDoc, master, doctype, extns, custRetnDataVMs, companies);
                        empDoc.DeletionDate = DateTime.Now;
                        empDoc.Comments = string.Empty;
                        empdocLists.Add(empDoc);
                    }
                }

                // Return combination of docListVM and empdocLists when partid != 0
                return Ok(docListVM.Concat(empdocLists).ToList());
            }
        }

        // Helper method to populate item fields
        void PopulateItemFields(DocListVM item, dynamic master, IEnumerable<DocumentTypeVM> doctype,
                                IEnumerable<ExtnInfoVM> extns, IEnumerable<CustRetnDataVM> custRetnDataVMs,
                                IEnumerable<ContactsVM> companies)
        {
            var doc = doctype.FirstOrDefault(d => d.DocumentTypeId == master.DocumentTypeId);
            if (doc != null)
            {
                item.DocumentTypeName = doc.DocumentName;
                item.DocumentTypeId = doc.DocumentTypeId;
                item.DataReqdByCust = doc.DataReqdByCust;
                item.DocCat = doc.DocuCategory;
                item.Mandatory = master.Mandatory;
                var extn = extns.FirstOrDefault(e => e.ExtnId == doc.ExtnId);
                item.FileExtnName = extn?.ExtnName;

                // Calculate DeletionDate if required
                if (doc.DefaultRetPerYear != 0 && doc.DefaultRetPerMon != 0)
                {
                    DateTime currentDate = DateTime.Now;
                    DateTime futureDate = currentDate.AddYears(doc.DefaultRetPerYear).AddMonths(doc.DefaultRetPerMon);
                    item.DeletionDate = futureDate;
                }
            }

            var cust = custRetnDataVMs.FirstOrDefault(c => c.DocumentTypeId == master.DocumentTypeId);
            if (cust != null)
            {
                var comp = companies.FirstOrDefault(c => c.CompanyId == cust.ComapanyId);
                item.CompanyName = comp?.CompanyName;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostRefDocLog([FromBody] RefDocLogVM refDocLogVM)
        {
            var result = await _docMangService.PostRefDocReason(refDocLogVM);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRefDocLogOfDoclistId(long doclistid)
        {
            var result = await _docMangService.GetRefDocLogOfDoclistId();
            var finalres = result.Where(x => x.DocListId == doclistid);
            ClaimsPrincipal userClaim = HttpContext.User; // Assuming you're in a controller or middleware
            string fullName = AppUtil.GetFullName(userClaim);
            var reasons = await _docMangService.Getallreasonlist();
            foreach (var item in finalres)
            {
                item.UploadedByStr = fullName;
                foreach (var re in reasons)
                {
                    if(re.RefDocReasonListId == item.DocReasonId)
                    {
                        item.ReasonName = re.DocReason;
                    }
                }
                if (item.ReasonName == null)
                {
                    item.ReasonName = string.Empty;
                }
                item.Date = item.UploadedOn.ToString("MM-dd-yyyy"); // Format as "MM/DD/YYYY"
            }
            return Ok(finalres);
        }

        [HttpPost]
        public async Task<IActionResult> PostDocList(IFormFile uploadedFile, DocListVM docListVM)
        {
            string ftpUrl = "/filestorage/Active/";
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileNameWithTimestamp = Path.GetFileNameWithoutExtension(uploadedFile.FileName)
                                           + "_" + timestamp
                                           + Path.GetExtension(uploadedFile.FileName);

            string ftpFullPath = ftpUrl + fileNameWithTimestamp;

            // Write the byte array to the file
            using (var stream = new FileStream(ftpFullPath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(stream);
            }
            docListVM.FileName = fileNameWithTimestamp;
            docListVM.StorageLocation = "/Active";
            var result = await _docMangService.PostDocList(docListVM);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> MoveFileToArchive(IFormFile uploadedFile, DocListVM docListVM)
        {
            // Active and Archive folder paths
            string activeFolderPath = "/filestorage/Active/";
            string archiveFolderPath = "/filestorage/Archive/";
            var findDoc =await _docMangService.GetOneDoclist(docListVM.DocListId);

            // Full file paths for Active and Archive folders
            string activeFilePath = Path.Combine(activeFolderPath, findDoc.FileName);
            string archiveFilePath = Path.Combine(archiveFolderPath, findDoc.FileName);

            string ftpUrl = "/filestorage/Active/";
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileNameWithTimestamp = Path.GetFileNameWithoutExtension(uploadedFile.FileName)
                                           + "_" + timestamp
                                           + Path.GetExtension(uploadedFile.FileName);

            string ftpFullPath = ftpUrl + fileNameWithTimestamp;

            // Write the byte array to the file
            try
            {
                // Check if the file exists in the Active folder
                if (System.IO.File.Exists(activeFilePath))
                {
                    System.IO.File.Move(activeFilePath, archiveFilePath);
                    using (var stream = new FileStream(ftpFullPath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(stream);
                    }
                    docListVM.FileName = fileNameWithTimestamp;
                    docListVM.StorageLocation = "/Active";
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
            var result = await _docMangService.PostDocList(docListVM);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDocListAndFile(int doclistid)
        {
            var finddoc =await _docMangService.GetOneDoclist(doclistid);
            var filePath = Path.Combine("/filestorage/Active", finddoc.FileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                //return Ok("File deleted successfully");
            }
            else
            {
                // return NotFound("File not found");
            }
            var result = await _docMangService.DeleteDocList(doclistid);
            return Ok(result);
        }

        //Not used
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BoughtOutFinishDetail(BoughtOutFinishDetailVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mastersService.BoughtOutFinishDetail(model);
            return Ok(result);
        }
        #endregion

        public async Task<JsonResult> CheckPartNo(string partNo,string partNumber="")
        {
            if(partNumber.Length>0)
            {
                partNo = partNumber;
            }
            var result = await _mastersService.CheckPartNo(partNo);
            return Json(!result);
        }

        [HttpGet]
        public async Task<JsonResult> CheckPartNo(string partNo)
        {
            var result = await _mastersService.CheckPartNo(partNo);
            return Json(!result);
        }

        #region Private Functions - ViewBag
        private async Task SupplierViewBag()
        {
            var companyTypes = await _mastersService.GetCompanyTypes();
            List<CompanyTypeVM> types = new List<CompanyTypeVM>();
            foreach (var co in companyTypes)
            {
                if (co.CompanyTypeValue.Equals("Customer"))
                    continue;
                if (co.CompanyType.Equals("Customer"))
                    continue;
                CompanyTypeVM companyTypeVM = new CompanyTypeVM();
                companyTypeVM.CompanyType = co.CompanyType;
                companyTypeVM.CompanyTypeValue = co.CompanyTypeValue;
                types.Add(companyTypeVM);
            }
            ViewBag.CompanyTypes = types.Select(c => new SelectListItem { Text = c.CompanyType, Value = c.CompanyTypeValue }).ToList();
        }
        private async Task CustomerViewBag()
        {
            var companyTypes = await _mastersService.GetCompanyTypes();
            List<CompanyTypeVM> types = new List<CompanyTypeVM>();
            foreach (var co in companyTypes)
            {
                if (co.CompanyTypeValue.Equals("Supplier"))
                    continue;
                if (co.CompanyType.Equals("Supplier"))
                    continue;
                CompanyTypeVM companyTypeVM = new CompanyTypeVM();
                companyTypeVM.CompanyType = co.CompanyType;
                companyTypeVM.CompanyTypeValue = co.CompanyTypeValue;
                types.Add(companyTypeVM);
            }
            ViewBag.CompanyTypes = types.Select(c => new SelectListItem { Text = c.CompanyType, Value = c.CompanyTypeValue }).ToList();
        }

        private async Task CompaniesViewBagForManuF(ManufacturedPartNoDetailVM manuf=null)
        {
            var uoms = await _mastersService.GetUOMs();
            var companies = await _mastersService.GetCompanies();
            companies = companies.Where(m => m.CompanyType.Equals("Both") || m.CompanyType.Equals("Customer"));
            if (manuf != null)
            {
                ViewBag.Companies = companies.Select(c => new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString(), Selected = (c.CompanyId == manuf.CompanyId) }).ToList();
                ViewBag.UOMs = uoms.Select(c => new SelectListItem { Text = c.Name, Value = c.UOMId.ToString(),Selected=(c.UOMId == manuf.UOMId) }).ToList();
            }
            else
            {
                ViewBag.Companies = companies.Select(c => new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() }).ToList();
                ViewBag.UOMs = uoms.Select(c => new SelectListItem { Text = c.Name, Value = c.UOMId.ToString() }).ToList();
            }
        }

        private async Task CompaniesViewBagForBOF(BoughtOutFinishDetailVM manuf = null)
        {
            var uoms = await _mastersService.GetUOMs();
            var companies = await _mastersService.GetCompanies();
            companies = companies.Where(m => m.CompanyType.Equals("Both") || m.CompanyType.Equals("Supplier"));
            if (manuf != null)
            {
                ViewBag.Companies = companies.Select(c => new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() }).ToList();
                ViewBag.UOMs = uoms.Select(c => new SelectListItem { Text = c.Name, Value = c.UOMId.ToString(), Selected = (c.UOMId == manuf.UOMId) }).ToList();
            }
            else
            {
                ViewBag.Companies = companies.Select(c => new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() }).ToList();
                ViewBag.UOMs = uoms.Select(c => new SelectListItem { Text = c.Name, Value = c.UOMId.ToString() }).ToList();
            }
        }


        private async Task CompaniesViewBagForRawMaterial(RawMaterialDetailVM manuf = null)
        {
            var uoms = await _mastersService.GetUOMs();
            var companies = await _mastersService.GetCompanies();
            companies = companies.Where(m => m.CompanyType.Equals("Both") || m.CompanyType.Equals("Supplier"));
            if (manuf != null)
            {
                ViewBag.Suppliers = companies.Select(c => new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString(), Selected = (c.CompanyId == manuf.SupplierId) }).ToList();
                ViewBag.UOMs = uoms.Select(c => new SelectListItem { Text = c.Name, Value = c.UOMId.ToString(), Selected = (c.UOMId == manuf.UOMId) }).ToList();
            }
            else
            {
                ViewBag.Suppliers = companies.Select(c => new SelectListItem { Text = c.CompanyName, Value = c.CompanyId.ToString() }).ToList();
                ViewBag.UOMs = uoms.Select(c => new SelectListItem { Text = c.Name, Value = c.UOMId.ToString() }).ToList();
            }
        }

        private async Task StatusViewBagForManuf(ManufacturedPartNoDetailVM manuf = null)
        {
            var statuses = await _mastersService.GetStatuses();
            List<PartStatusVM> types = new List<PartStatusVM>();
            foreach (var co in statuses)
            {
                PartStatusVM partStatusVM = new PartStatusVM();
                partStatusVM.Status = co.Status;
                partStatusVM.StatusValue = co.StatusValue;
                types.Add(partStatusVM);
            }
            if (manuf != null)
            {
                ViewBag.Statuses = types.Select(c => new SelectListItem { Text = c.Status, Value = c.StatusValue,Selected=(c.Status.Equals(manuf.Status)) }).ToList();
            }
            else
            {
                ViewBag.Statuses = types.Select(c => new SelectListItem { Text = c.Status, Value = c.StatusValue }).ToList();
            }
            
        }
        private async Task StatusViewBagForBOF(BoughtOutFinishDetailVM manuf = null)
        {
            var statuses = await _mastersService.GetStatuses();
            List<PartStatusVM> types = new List<PartStatusVM>();
            foreach (var co in statuses)
            {
                PartStatusVM partStatusVM = new PartStatusVM();
                partStatusVM.Status = co.Status;
                partStatusVM.StatusValue = co.StatusValue;
                types.Add(partStatusVM);
            }
            if (manuf != null)
            {
                ViewBag.Statuses = types.Select(c => new SelectListItem { Text = c.Status, Value = c.StatusValue, Selected = (c.Status.Equals(manuf.Status)) }).ToList();
            }
            else
            {
                ViewBag.Statuses = types.Select(c => new SelectListItem { Text = c.Status, Value = c.StatusValue }).ToList();
            }

        }
        private async Task StatusViewBagForRawMaterial(RawMaterialDetailVM manuf = null)
        {
            var statuses = await _mastersService.GetStatuses();
            List<PartStatusVM> types = new List<PartStatusVM>();
            foreach (var co in statuses)
            {
                PartStatusVM partStatusVM = new PartStatusVM();
                partStatusVM.Status = co.Status;
                partStatusVM.StatusValue = co.StatusValue;
                types.Add(partStatusVM);
            }
            if (manuf != null)
            {
                ViewBag.Statuses = types.Select(c => new SelectListItem { Text = c.Status, Value = c.StatusValue, Selected = (c.Status.Equals(manuf.Status)) }).ToList();
            }
            else
            {
                ViewBag.Statuses = types.Select(c => new SelectListItem { Text = c.Status, Value = c.StatusValue }).ToList();
            }

        }

        private async Task RawMaterialViewBags(RawMaterialDetailVM rmVm=null)
        {
            var rmTypes = await _mastersService.GetRMTypes();
            var standards = await _mastersService.GetRMStandards();
            var specs = await _mastersService.GetRMSpecs();
            var baseRms = await _mastersService.GetBaseRMs();
            if (rmVm != null)
            {
                ViewBag.RMTypes = rmTypes.Select(c => new SelectListItem { Text = c.Name, Value = c.RawMaterialTypeId.ToString(), Selected = (c.RawMaterialTypeId == rmVm.RawMaterialTypeId) }).ToList();
                ViewBag.Standards = standards.Select(c => new SelectListItem { Text = c.Name, Value = c.Standard.ToString(), Selected = (c.Standard == rmVm.Standard) }).ToList();
                ViewBag.Specs = specs.Select(c => new SelectListItem { Text = c.Name, Value = c.MaterialSpecId.ToString(), Selected = (c.MaterialSpecId == rmVm.MaterialSpecId) }).ToList();
                ViewBag.BaseRMs = baseRms.Select(c => new SelectListItem { Text = c.Name, Value = c.BaseRawMaterialId.ToString(), Selected = (c.BaseRawMaterialId == rmVm.BaseRawMaterialId) }).ToList();
            }
            else
            {
                ViewBag.RMTypes = rmTypes.Select(c => new SelectListItem { Text = c.Name, Value = c.RawMaterialTypeId.ToString() }).ToList();
                ViewBag.Standards = standards.Select(c => new SelectListItem { Text = c.Name, Value = c.Standard.ToString() }).ToList();
                ViewBag.Specs = specs.Select(c => new SelectListItem { Text = c.Name, Value = c.MaterialSpecId.ToString() }).ToList();
                ViewBag.BaseRMs = baseRms.Select(c => new SelectListItem { Text = c.Name, Value = c.BaseRawMaterialId.ToString() }).ToList();
            }
        }
        #endregion
    }


}
