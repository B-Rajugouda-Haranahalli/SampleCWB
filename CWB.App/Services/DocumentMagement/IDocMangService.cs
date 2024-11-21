using CWB.App.Models.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Services.DocumentMagement
{
   public interface IDocMangService
    {
        Task<IEnumerable<DocumentTypeVM>> GetAllDocumentType();
        Task<DocumentTypeVM> PostDocumentType(DocumentTypeVM documentTypeVMs);
        Task<IEnumerable<DocCategoryVM>> GetAllDocCategory();
        Task<IEnumerable<ExtnInfoVM>> GetAllFileExtn();
        Task<ExtnInfoVM> PostFileExtn(ExtnInfoVM extnInfoVM);
        Task<IEnumerable<CustRetnDataVM>> GetAllCustRet();
        Task<IEnumerable<RefDocLogVM>> GetRefDocLogOfDoclistId();
        Task<CustRetnDataVM> PostCustRetndata(CustRetnDataVM custRetnDataVM);
        Task<RefDocReasonListVM> PostDocReason(RefDocReasonListVM custRetnDataVM);
        Task<List<DocUploadVM>> PostDocUpload(IEnumerable<DocUploadVM> docUploadVMs);
        Task<IEnumerable<DocUploadVM>> GetAllDocUpload();
        Task<IEnumerable<RefDocReasonListVM>> Getallreasonlist();
        Task<IEnumerable<DocViewVM>> GetAllDocView();

        Task<List<DocViewVM>> PostDocView(IEnumerable<DocViewVM> docViewVMs);
        Task<IEnumerable<DocListVM>> GetAllDocList();
        Task<DocListVM> PostDocList(DocListVM docListVM);
        Task<RefDocLogVM> PostRefDocReason(RefDocLogVM refDocLogVM);
        Task<DocListVM> GetOneDoclist(long doclistId);
        Task<bool> DeleteDocType(long doctypeId);
        Task<bool> DeleteCustRetData(long custRetId);
        Task<bool> DeleteExtnInfo(long extnId);
        Task<bool> DeleteDocList(long docListId);
        Task<bool> CheckDocTypeName(string docTypeName);
        Task<bool> CheckExtnName(string extnName);
    }
}
