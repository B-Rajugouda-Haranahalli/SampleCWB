using CWB.CompanySettings.ViewModels.DocType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Services.DocType
{
    public interface IDocumentTypeService
    {
        IEnumerable<DocumentTypeVM> GetDocumentTypes(long TenantId);
        Task<DocumentTypeVM> DocumentType(DocumentTypeVM documentTypeVM);
        bool CheckDocumentTypeExisit(DocumentTypeVM checkDocumentTypeVM);

        Task<DocumentTypeVM> GetDocumentType(long docTypeId);
        Task<bool> DelDocumentType(long docTypeId);
    }
}
