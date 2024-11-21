using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.Infrastructure;

namespace CWB.CompanySettings.Repositories.DocType
{
    public class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(CompanySettingsDbContext context)
         : base(context)
        { }
    }
}