using CWB.App.AppUtils;
using CWB.App.Models.DocumentManagement;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Services.DocumentMagement
{
    public class DocMangService : IDocMangService
    {
        private readonly ILoggerManager _logger;
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long tenantId;
        public DocMangService(ILoggerManager logger, IOptions<ApiUrls> apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiUrls = apiUrlsOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            tenantId = long.Parse(AppUtil.GetTenantId(_httpContextAccessor.HttpContext.User));

        }


        public async Task<IEnumerable<DocumentTypeVM>> GetAllDocumentType()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/alldocumenttype/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DocumentTypeVM>>.GetAsync(uri, headers);
        }


        public async Task<DocumentTypeVM> PostDocumentType(DocumentTypeVM documentTypeVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postdocumenttype");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
                documentTypeVMs.TenantId = tenantId;
            return await RestHelper<DocumentTypeVM>.PostAsync(uri, documentTypeVMs, headers);
        }

        public async Task<IEnumerable<DocCategoryVM>> GetAllDocCategory()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getalldoccategory");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DocCategoryVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<ExtnInfoVM>> GetAllFileExtn()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getallextn/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<ExtnInfoVM>>.GetAsync(uri, headers);
        }

        public async Task<ExtnInfoVM> PostFileExtn(ExtnInfoVM extnInfoVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postextn");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            extnInfoVM.TenantId = tenantId;
            return await RestHelper<ExtnInfoVM>.PostAsync(uri, extnInfoVM, headers);
        }
        public async Task<IEnumerable<RefDocLogVM>> GetRefDocLogOfDoclistId()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getallrefdoc/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<RefDocLogVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<CustRetnDataVM>> GetAllCustRet()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/allcustretndata/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<CustRetnDataVM>>.GetAsync(uri, headers);
        }


        public async Task<RefDocReasonListVM> PostDocReason(RefDocReasonListVM custRetnDataVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postdocreason");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            custRetnDataVM.TenantId = tenantId;
            return await RestHelper<RefDocReasonListVM>.PostAsync(uri, custRetnDataVM, headers);
        }
        public async Task<CustRetnDataVM> PostCustRetndata(CustRetnDataVM custRetnDataVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postcustretndata");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            custRetnDataVM.TenantId = tenantId;
            return await RestHelper<CustRetnDataVM>.PostAsync(uri, custRetnDataVM, headers);
        }

        public async Task<IEnumerable<RefDocReasonListVM>> Getallreasonlist()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getallreasonlist/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<RefDocReasonListVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<DocUploadVM>> GetAllDocUpload()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getalldocupload/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DocUploadVM>>.GetAsync(uri, headers);
        }

        public async Task<List<DocUploadVM>> PostDocUpload(IEnumerable<DocUploadVM> docUploadVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postdocupload");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in docUploadVMs)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<DocUploadVM>>.PostAsync(uri, docUploadVMs, headers);
        }
        public async Task<IEnumerable<DocViewVM>> GetAllDocView()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getalldocview/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DocViewVM>>.GetAsync(uri, headers);
        }
        public async Task<List<DocViewVM>> PostDocView(IEnumerable<DocViewVM> docViewVMs)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postdocview");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            foreach (var item in docViewVMs)
            {
                item.TenantId = tenantId;
            }
            return await RestHelper<List<DocViewVM>>.PostAsync(uri, docViewVMs, headers);
        }


        public async Task<IEnumerable<DocViewVM>> GetAllDocViewDepartMent()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getalldoclist/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DocViewVM>>.GetAsync(uri, headers);
        }
        public async Task<DocListVM> GetOneDoclist(long doclistId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getonedoclist/{doclistId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<DocListVM>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<DocListVM>> GetAllDocList()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getalldoclist/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<DocListVM>>.GetAsync(uri, headers);
        }
        public async Task<RefDocLogVM> PostRefDocReason(RefDocLogVM refDocLogVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postrefdoc");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            refDocLogVM.TenantId = tenantId;
            return await RestHelper<RefDocLogVM>.PostAsync(uri, refDocLogVM, headers);
        }
        public async Task<DocListVM> PostDocList(DocListVM docListVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postdocList");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            docListVM.TenantId = tenantId;
            return await RestHelper<DocListVM>.PostAsync(uri, docListVM, headers);
        }
        public async Task<bool> DeleteDocType(long doctypeId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/deletedocumenttype/{doctypeId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> DeleteCustRetData(long custRetId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/deletecustretdata/{custRetId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> DeleteExtnInfo(long extnId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/deleteextndata/{extnId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> DeleteDocList(long docListId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/deletedoclistdata/{docListId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> CheckDocTypeName(string docTypeName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkdoctypename/{docTypeName}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> CheckExtnName(string extnName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkextnname/{extnName}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

    }
}
