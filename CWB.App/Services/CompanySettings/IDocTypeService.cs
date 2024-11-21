using CWB.App.Models.CoSettings;
using CWB.App.Models.Plants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.CompanySettings
{
    public interface IDocTypeService
    {
        Task<IEnumerable<DocumentTypeVM>> GetDocTypes();
        Task<DocumentTypeVM> PostDocType(DocumentTypeVM model);
        Task<bool> DelDocType(long docTypeId);
        Task<DocumentTypeVM> GetDocType(long docTypeId);
    }
}
