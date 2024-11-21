using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.Masters.MastersUtils;
using CWB.Masters.Services.DocumentManagement;
using CWB.Masters.ViewModels.DocumentManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class DocumentManagementController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IDocumentManagementService _documentManagementService;
        public DocumentManagementController(ILoggerManager logger,IDocumentManagementService documentManagementService)
        {
            _logger = logger;
            _documentManagementService = documentManagementService;
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.AllDocumentType)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocumentTypeVM))]
        public async Task<IActionResult> GetDocumentType(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetDocumentType(tenantId);
            return Ok(documentTypeVM);
        }
        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostDocumentType)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocumentTypeVM))]
        public async Task<IActionResult> PostDocumentType([FromBody] DocumentTypeVM documentType)
        {
            var result = await _documentManagementService.PostDocumentType(documentType);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.AllCustRetnData)]
        [Produces(AppContentTypes.ContentType, Type = typeof(CustRetnDataVM))]
        public async Task<IActionResult> GetCustRetnData(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetCustRetnData(tenantId);
            return Ok(documentTypeVM);
        }
        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostCustRetnData)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocumentTypeVM))]
        public async Task<IActionResult> PostCustRetndata([FromBody] CustRetnDataVM documentType)
        {
            var result = await _documentManagementService.PostCustRetndata(documentType);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetAllExtn)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ExtnInfoVM))]
        public async Task<IActionResult> GetAllExtn(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetExtn(tenantId);
            return Ok(documentTypeVM);
        }

        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostExtn)]
        [Produces(AppContentTypes.ContentType, Type = typeof(ExtnInfoVM))]
        public async Task<IActionResult> PostExtn([FromBody] ExtnInfoVM extnInfo)
        {
            var documentTypeVM = await _documentManagementService.PostExtnInfo(extnInfo);
            return Ok(documentTypeVM);
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetAllDocUpload)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocViewVM))]
        public async Task<IActionResult> GetAllDocUpload(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetAllDocUpload(tenantId);
            return Ok(documentTypeVM);
        }

        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostDocUpload)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocUploadVM))]
        public async Task<IActionResult> PostDocUpload([FromBody] List<DocUploadVM> docUpload)
        {
            var documentTypeVM = await _documentManagementService.PostDocUpload(docUpload);
            return Ok(documentTypeVM);
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetAllDocView)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocViewVM))]
        public async Task<IActionResult> GetAllDocView(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetAllDocView(tenantId);
            return Ok(documentTypeVM);
        }
        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostDocView)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocViewVM))]
        public async Task<IActionResult> PostDocView([FromBody] List<DocViewVM> docUpload)
        {
            var documentTypeVM = await _documentManagementService.PostDocView(docUpload);
            return Ok(documentTypeVM);
        }


        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetAllDocCategory)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocCategoryVM))]
        public async Task<IActionResult> GetAllDocCategory()
        {
            var docCategoryVMs = await _documentManagementService.GetAllDocCategory();
            return Ok(docCategoryVMs);
        }


        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetOneDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocListVM))]
        public async Task<IActionResult> GetOneDocList(long doclistId, long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetOneDocList(doclistId,tenantId);
            return Ok(documentTypeVM);
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetAllDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocListVM))]
        public async Task<IActionResult> GetAllDocList(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetAllDocList(tenantId);
            return Ok(documentTypeVM);
        }
        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocListVM))]
        public async Task<IActionResult> PostDocList([FromBody] DocListVM docList)
        {
            var documentTypeVM = await _documentManagementService.PostDocList(docList);
            return Ok(documentTypeVM);
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.CheckPartNoInDocList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckPartNoInDocList(long partId, long tenantId)
        {
            bool exists = false;
            exists =await _documentManagementService.CheckPartNoInDocList(partId,tenantId);
            return Ok(exists);
        }


        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetAllUiName)]
        [Produces(AppContentTypes.ContentType, Type = typeof(UiListVM))]
        public async Task<IActionResult> GetAllUiName(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetAllUiName(tenantId);
            return Ok(documentTypeVM);
        }
        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostUiName)]
        [Produces(AppContentTypes.ContentType, Type = typeof(UiListVM))]
        public async Task<IActionResult> PostUiName([FromBody] UiListVM uiList)
        {
            var documentTypeVM = await _documentManagementService.PostUiName(uiList);
            return Ok(documentTypeVM);
        }


        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetAllRefDoc)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RefDocLogVM))]
        public async Task<IActionResult> GetAllRefDoc(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetAllRefDoc(tenantId);
            return Ok(documentTypeVM);
        }
        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostDocLog)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RefDocLogVM))]
        public async Task<IActionResult> PostDocLog([FromBody] RefDocLogVM uiList)
        {
            var documentTypeVM = await _documentManagementService.PostDocLog(uiList);
            return Ok(documentTypeVM);
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetReasonList)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RefDocReasonListVM))]
        public async Task<IActionResult> GetReasonList(long tenantId)
        {
            var documentTypeVM = await _documentManagementService.GetReasonList(tenantId);
            return Ok(documentTypeVM);
        }
        [HttpPost]
        [Route(ApiRoutes.DocumentManagement.PostDocReason)]
        [Produces(AppContentTypes.ContentType, Type = typeof(RefDocReasonListVM))]
        public async Task<IActionResult> PostDocReason([FromBody] RefDocReasonListVM uiList)
        {
            var documentTypeVM = await _documentManagementService.PostDocReason(uiList);
            return Ok(documentTypeVM);
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.GetDocStatus)]
        [Produces(AppContentTypes.ContentType, Type = typeof(DocStatusVM))]
        public async Task<IActionResult> GetDocStatus(long statusid)
        {
            var documentTypeVM = await _documentManagementService.GetDocStatus(statusid);
            return Ok(documentTypeVM);
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.DeleteDocType)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DeleteDocType(long doctypeId, long tenantId)
        {
            var result = await _documentManagementService.DeleteDocType(doctypeId, tenantId);
            return Ok(result);
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.DeleteCustRetdata)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCustRetData(long custRetId, long tenantId)
        {
            var result = await _documentManagementService.DeleteCustRetData(custRetId, tenantId);
            return Ok(result);
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.DeleteExtndata)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DeleteExtndata(long extnId, long tenantId)
        {
            var result = await _documentManagementService.DeleteExtndata(extnId, tenantId);
            return Ok(result);
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.DeleteDocListdata)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DeleteDocListdata(long docListId, long tenantId)
        {
            var result = await _documentManagementService.DeleteDocListdata(docListId, tenantId);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.CheckDocTypeName)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckDocTypeName(string docTypeName)
        {
            bool exists = false;
            exists = await _documentManagementService.CheckDocTypeName(docTypeName);
            return Ok(exists);
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.DocumentTypeInDoclist)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DocumentTypeInDoclist(long docTypeid, long tenantId)
        {
            bool exists = false;
            exists = await _documentManagementService.DocumentTypeInDoclist(docTypeid,tenantId);
            return Ok(exists);
        }
        [HttpGet]
        [Route(ApiRoutes.DocumentManagement.CheckExtnName)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckExtnName(string extnName)
        {
            bool exists = false;
            exists = await _documentManagementService.CheckExtnName(extnName);
            return Ok(exists);
        }
    }
}
