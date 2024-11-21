using CWB.App.Models.OperationList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.Masters
{
    public interface IOperationService
    {
        Task<IEnumerable<OperationListVM>> GetOperationsList();
        Task<OperationListVM> Operation(OperationListVM operationVM);
        Task<bool> CheckIfOperationExisit(long OperationId, string Operation);

        Task<OperationListVM> Operation(long Id);

        Task<IEnumerable<DocumentTypeListVM>> GetOperationDocTypes(long OperationId);

        Task<IEnumerable<OperationalListDocmentsVM>> GetOperationDocTypesList(long OperationId);

        Task<OperationDocumentTypeVM> OperationDocument(OperationDocumentTypeVM operationDocumentTypeVM);
        Task<bool> DeletOperationDoc(long opDocId);
        Task<IEnumerable<OperationalDocumentListVM>> GetOperationalDocTypesByOptId(long Id);
    }
}
