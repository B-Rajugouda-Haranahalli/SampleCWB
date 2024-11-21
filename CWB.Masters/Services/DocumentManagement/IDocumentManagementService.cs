using CWB.Masters.ViewModels.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Services.DocumentManagement
{
    public interface IDocumentManagementService
    {
        Task<IEnumerable<DocumentTypeVM>> GetDocumentType(long tenantId);
        Task<DocumentTypeVM> PostDocumentType(DocumentTypeVM documentType);
        Task<IEnumerable<CustRetnDataVM>> GetCustRetnData(long tenantId);
        Task<CustRetnDataVM> PostCustRetndata(CustRetnDataVM documentType);
        Task<IEnumerable<ExtnInfoVM>> GetExtn(long tenantId);
        Task<ExtnInfoVM> PostExtnInfo(ExtnInfoVM extnInfo);
        Task<IEnumerable<DocUploadVM>> GetAllDocUpload(long tenantId);
        Task<List<DocUploadVM>> PostDocUpload(List<DocUploadVM> docUpload);
        Task<IEnumerable<DocViewVM>> GetAllDocView( long tenantId);
        Task<List<DocViewVM>> PostDocView(List<DocViewVM> docUpload);
        Task<IEnumerable<DocCategoryVM>> GetAllDocCategory();
        Task<IEnumerable<DocListVM>> GetAllDocList(long tenantId);
        Task<DocListVM> GetOneDocList(long doclistId, long tenantId);
        Task<DocListVM> PostDocList(DocListVM docUpload);
        Task<IEnumerable<UiListVM>> GetAllUiName(long tenantId);
        Task<IEnumerable<RefDocLogVM>> GetAllRefDoc(long tenantId);
        Task<RefDocLogVM> PostDocLog(RefDocLogVM uiList);
        Task<IEnumerable<RefDocReasonListVM>> GetReasonList(long tenantId);
        Task<UiListVM> PostUiName(UiListVM uiList);
        Task<RefDocReasonListVM> PostDocReason(RefDocReasonListVM uiList);
        Task<DocStatusVM> GetDocStatus(long statusid);
        Task<bool> CheckPartNoInDocList(long partId, long tenantId);
        Task<bool> DeleteDocType(long doctypeId, long tenantId);
        Task<bool> DeleteCustRetData(long custRetId, long tenantId);
        Task<bool> DeleteExtndata(long extnId, long tenantId);
        Task<bool> DeleteDocListdata(long docListId, long tenantId);
        Task<bool> CheckDocTypeName(string docTypeName);
        Task<bool> CheckExtnName(string extnName);
        Task<bool> DocumentTypeInDoclist(long docTypeid, long tenantId);

    }
}
