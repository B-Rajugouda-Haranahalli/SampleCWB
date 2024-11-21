using AutoMapper;
using CWB.Logging;
using CWB.Masters.Domain.DocumentManagement;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.DocumentManagement;
using CWB.Masters.ViewModels.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Services.DocumentManagement
{
    public class DocumentManagementService : IDocumentManagementService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly ICustRetnDataRepository _custRetnDataRepository;
        private readonly IExtnInfoRepository _extnInfoRepository;
        private readonly IDocUploadRepository _docUploadRepository;
        private readonly IDocViewRepository _docViewRepository;
        private readonly IDocCategoryRepository _docCategoryRepository;
        private readonly IDocListRepository _docListRepository;
        private readonly IUiListrepository _uiListrepository;
        private readonly IDocStatusRepository _docStatusRepository;
        private readonly IRefDocReasonListRepository _reasonListRepository;
        private readonly IRefDocLogRepository _refDocLogRepository;
        public DocumentManagementService(ILoggerManager logger, IMapper mapper,IUnitOfWork unitOfWork, IDocumentTypeRepository documentTypeRepository,
            ICustRetnDataRepository custRetnDataRepository,IExtnInfoRepository extnInfoRepository, IDocUploadRepository docUploadRepository, IDocViewRepository docViewRepository,
            IDocCategoryRepository docCategoryRepository, IDocListRepository docListRepository, IDocStatusRepository docStatusRepository
            , IUiListrepository uiListrepository, IRefDocReasonListRepository reasonListRepository,
            IRefDocLogRepository refDocLogRepository) //
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _documentTypeRepository = documentTypeRepository;
            _custRetnDataRepository = custRetnDataRepository;
            _extnInfoRepository = extnInfoRepository;
            _docUploadRepository = docUploadRepository;
            _docViewRepository = docViewRepository;
            _docCategoryRepository = docCategoryRepository;
            _docListRepository = docListRepository;
            _uiListrepository = uiListrepository;
            _docStatusRepository = docStatusRepository;
            _reasonListRepository = reasonListRepository;
            _refDocLogRepository = refDocLogRepository;
        }
        public async Task<IEnumerable<DocumentTypeVM>> GetDocumentType(long tenantId)
        {
            var allDocuType =_documentTypeRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<DocumentTypeVM>>(allDocuType);
        }

        public async Task<DocumentTypeVM> PostDocumentType(DocumentTypeVM documentType)
        {
            var doctype = _mapper.Map<DocumentType>(documentType);
            if(doctype.Id == 0)
            {
                try
                {
                    doctype.RetentionDays = (doctype.DefaultRetPerMon * 30) + (doctype.DefaultRetPerMon * 365);
                    await _documentTypeRepository.AddAsync(doctype);
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                var doctypeid = await _documentTypeRepository.SingleOrDefaultAsync(x => x.Id == doctype.Id);
                if(doctypeid == null)
                {
                    return documentType;
                }
                doctype = await _documentTypeRepository.UpdateAsync(doctype.Id, doctype);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            documentType.DocumentTypeId = doctype.Id;
            return documentType;
        }
        public async Task<IEnumerable<CustRetnDataVM>> GetCustRetnData(long tenantId)
        {
            var allDocuType = _custRetnDataRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<CustRetnDataVM>>(allDocuType);
        }

        public async Task<CustRetnDataVM> PostCustRetndata(CustRetnDataVM documentType)
        {
            var doctype = _mapper.Map<CustRetnData>(documentType);
            if (doctype.Id == 0)
            {
                try
                {
                    if(doctype.DocumentTypeId > 0)
                    {
                        var doctypebyid = await _documentTypeRepository.SingleOrDefaultAsync(x => x.Id == doctype.DocumentTypeId);
                        if(doctypebyid != null)
                        {
                            if(doctypebyid.DefaultRetPerMon != doctype.RetPerMon)
                            {
                                doctypebyid.DefaultRetPerMon = doctype.RetPerMon;
                            }
                            if(doctypebyid.DefaultRetPerYear != doctype.RetPerYear)
                            {
                                doctypebyid.DefaultRetPerYear = doctype.RetPerYear;
                            }
                            doctypebyid = await _documentTypeRepository.UpdateAsync(doctypebyid.Id, doctypebyid);
                        }
                    }
                    await _custRetnDataRepository.AddAsync(doctype);
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                var doctypeid = await _custRetnDataRepository.SingleOrDefaultAsync(x => x.Id == doctype.Id);
                if (doctypeid == null)
                {
                    return documentType;
                }

                var doctypebyid = await _documentTypeRepository.SingleOrDefaultAsync(x => x.Id == doctype.DocumentTypeId);
                if (doctypebyid != null)
                {
                    if (doctypebyid.DefaultRetPerMon != doctype.RetPerMon)
                    {
                        doctypebyid.DefaultRetPerMon = doctype.RetPerMon;
                    }
                    if (doctypebyid.DefaultRetPerYear != doctype.RetPerYear)
                    {
                        doctypebyid.DefaultRetPerYear = doctype.RetPerYear;
                    }
                    doctypebyid = await _documentTypeRepository.UpdateAsync(doctypebyid.Id, doctypebyid);
                }
                doctype = await _custRetnDataRepository.UpdateAsync(doctype.Id, doctype);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            documentType.DocumentTypeId = doctype.Id;
            return documentType;
        }

        public async Task<IEnumerable<ExtnInfoVM>> GetExtn(long tenantId)
        {
            var extns = _extnInfoRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<ExtnInfoVM>>(extns);
        }

        public async Task<ExtnInfoVM> PostExtnInfo(ExtnInfoVM extnInfo)
        {
            var extn = _mapper.Map<ExtnInfo>(extnInfo);
            if (extn.Id == 0)
            {
                try
                {
                    await _extnInfoRepository.AddAsync(extn);
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                var extnid = await _extnInfoRepository.SingleOrDefaultAsync(x => x.Id == extn.Id);
                if (extnid == null)
                {
                    return extnInfo;
                }
                extn = await _extnInfoRepository.UpdateAsync(extn.Id, extn);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            extnInfo.ExtnId = extn.Id;
            return extnInfo;
        }
        public async Task<IEnumerable<DocUploadVM>> GetAllDocUpload(long tenantId)
        {
            var allDocuType = _docUploadRepository.GetRangeAsync(d =>d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<DocUploadVM>>(allDocuType);
        }
        public async Task<List<DocUploadVM>> PostDocUpload(List<DocUploadVM> docUpload)
        {
            foreach (DocUploadVM item in docUpload)
            {
                var postdocupload = _mapper.Map<DocUpload>(item);
                if (postdocupload.Id == 0)
                {
                    try
                    {
                        await _docUploadRepository.AddAsync(postdocupload);
                    }
                    catch (Exception ex)
                    {
                        Exception exa = ex.InnerException;
                        string msg = ex.Message;
                    }
                }
                try
                {
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
                item.DocUploadId = postdocupload.Id;
            }
            //else
            //{
            //    var docUpload1 = await _docUploadRepository.SingleOrDefaultAsync(x => x.Id == postdocupload.Id);
            //    if (docUpload1 == null)
            //    {
            //        return docUpload;
            //    }
            //    postdocupload = await _docUploadRepository.UpdateAsync(docUpload1.Id, postdocupload);
            //}
            return docUpload;
        }
        public async Task<IEnumerable<DocViewVM>> GetAllDocView(long tenantId)
        {
            var allDocuType = _docViewRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<DocViewVM>>(allDocuType);
        }

        public async Task<List<DocViewVM>> PostDocView(List<DocViewVM> docView)
        {
            foreach (DocViewVM item in docView)
            {
                var postdocview = _mapper.Map<DocView>(item);
                if (postdocview.Id == 0)
                {
                    try
                    {
                        await _docViewRepository.AddAsync(postdocview);
                    }
                    catch (Exception ex)
                    {
                        Exception exa = ex.InnerException;
                        string msg = ex.Message;
                    }
                }
                try
                {
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
                item.DocViewId = postdocview.Id;
            }
            //else
            //{
            //    var docUpload1 = await _docViewRepository.SingleOrDefaultAsync(x => x.Id == postdocview.Id);
            //    if (docUpload1 == null)
            //    {
            //        return docView;
            //    }
            //    postdocview = await _docViewRepository.UpdateAsync(docUpload1.Id, postdocview);
            //}
            return docView;
        }

        public async Task<IEnumerable<DocCategoryVM>> GetAllDocCategory()
        {
            var allDocuType =await _docCategoryRepository.GetAllAsync();   
            return _mapper.Map<IEnumerable<DocCategoryVM>>(allDocuType);
        }



        public async Task<DocListVM> PostDocList(DocListVM docList)
        {
            var postdoclist = _mapper.Map<DocList>(docList);
            if (postdoclist.Id == 0)
            {
                try
                {
                    postdoclist.Status = 1;
                    await _docListRepository.AddAsync(postdoclist);
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                var doclistupdate = await _docListRepository.SingleOrDefaultAsync(x => x.Id == postdoclist.Id);
                if (doclistupdate == null)
                {
                    return docList;
                }
                //doclistupdate.Status = 2;
                doclistupdate.DocumentTypeId = postdoclist.DocumentTypeId;
                doclistupdate.FileName = postdoclist.FileName;
                doclistupdate.Comments = postdoclist.Comments;
                doclistupdate.DeletionDate = postdoclist.DeletionDate;
                doclistupdate.CreationDate = DateTime.Now;
                postdoclist = await _docListRepository.UpdateAsync(doclistupdate.Id, doclistupdate);
                //doclistupdate.Id = 0;
                //doclistupdate.Status = 1;
                //await _docListRepository.AddAsync(doclistupdate);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            if (docList.DocListId == 0)
            {
                RefDocLog refDoc = new RefDocLog();
                refDoc.DocListId = postdoclist.Id;
                refDoc.PartId = postdoclist.PartId;
                refDoc.TenantId  = postdoclist.TenantId;
                refDoc.DocReasonId = 0;
                refDoc.Comments = string.Empty;
                refDoc.Action = "New Entry";
                refDoc.UploadedOn = DateTime.Now;
                await _refDocLogRepository.AddAsync(refDoc);
                try
                {
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            docList.DocListId = postdoclist.Id;
            return docList;
        }
        public async Task<IEnumerable<DocListVM>> GetAllDocList(long tenantId)
        {
            var docLists = _docListRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<DocListVM>>(docLists);
        }
        public async Task<DocListVM> GetOneDocList(long doclistId,long tenantId)
        {
            var docLists =await _docListRepository.SingleOrDefaultAsync(d => d.Id==doclistId && d.TenantId == tenantId);
            if (docLists != null)
            {
                return _mapper.Map<DocListVM>(docLists);
            }
            return new DocListVM();
        }

        public async Task<bool> CheckPartNoInDocList(long partId, long tenantId)
        {
            var docLists = _docListRepository.GetRangeAsync(c => c.PartId == partId && c.TenantId == tenantId);

            if (docLists == null || !docLists.Any())
            {
                return false;
            }
            return true;
        }


        public async Task<IEnumerable<UiListVM>> GetAllUiName(long tenantId)
        {
            var docLists = _uiListrepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<UiListVM>>(docLists);
        }
        public async Task<IEnumerable<RefDocLogVM>> GetAllRefDoc(long tenantId)
        {
            var docLists = _refDocLogRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<RefDocLogVM>>(docLists);
        }
        public async Task<IEnumerable<RefDocReasonListVM>> GetReasonList(long tenantId)
        {
            var docLists = _reasonListRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<RefDocReasonListVM>>(docLists);
        }
        public async Task<RefDocLogVM> PostDocLog(RefDocLogVM uiList)
        {
            var postuiname = _mapper.Map<RefDocLog>(uiList);
            if (postuiname.Id == 0)
            {
                try
                {
                    await _refDocLogRepository.AddAsync(postuiname);
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                var docUpload1 = await _refDocLogRepository.SingleOrDefaultAsync(x => x.Id == postuiname.Id);
                if (docUpload1 == null)
                {
                    return uiList;
                }
                postuiname = await _refDocLogRepository.UpdateAsync(docUpload1.Id, postuiname);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            uiList.RefDocLogId = postuiname.Id;
            return uiList;
        }
        public async Task<RefDocReasonListVM> PostDocReason(RefDocReasonListVM uiList)
        {
            var postuiname = _mapper.Map<RefDocReasonList>(uiList);
            if (postuiname.Id == 0)
            {
                try
                {
                    await _reasonListRepository.AddAsync(postuiname);
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                var docUpload1 = await _reasonListRepository.SingleOrDefaultAsync(x => x.Id == postuiname.Id);
                if (docUpload1 == null)
                {
                    return uiList;
                }
                postuiname = await _reasonListRepository.UpdateAsync(docUpload1.Id, postuiname);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            uiList.RefDocReasonListId = postuiname.Id;
            return uiList;
        }
        public async Task<UiListVM> PostUiName(UiListVM uiList)
        {
            var postuiname = _mapper.Map<UiList>(uiList);
            if (postuiname.Id == 0)
            {
                try
                {
                    await _uiListrepository.AddAsync(postuiname);
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                var docUpload1 = await _uiListrepository.SingleOrDefaultAsync(x => x.Id == postuiname.Id);
                if (docUpload1 == null)
                {
                    return uiList;
                }
                postuiname = await _uiListrepository.UpdateAsync(docUpload1.Id, postuiname);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            uiList.UiListId = postuiname.Id;
            return uiList;
        }


        public async Task<DocStatusVM> GetDocStatus(long statusid)
        {
            var docStatus = await _docStatusRepository.SingleOrDefaultAsync(x => x.Id == statusid);
            if (docStatus == null)
            {
                return new DocStatusVM();
            }
            return _mapper.Map<DocStatusVM>(docStatus);
        }

        public async Task<bool> DeleteDocType(long doctypeId, long tenantId)
        {
            var co = await _documentTypeRepository.SingleOrDefaultAsync(m => m.Id == doctypeId && m.TenantId == tenantId);
            if (co != null)
            {
                try
                {
                    _documentTypeRepository.Remove(co);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                catch (Exception ex) { }
            }
            return false;
        }


        public async Task<bool> DeleteCustRetData(long custRetId, long tenantId)
        {
            var co = await _custRetnDataRepository.SingleOrDefaultAsync(m => m.Id == custRetId && m.TenantId == tenantId);
            if (co != null)
            {
                try
                {
                    _custRetnDataRepository.Remove(co);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                catch (Exception ex) { }
            }
            return false;
        }
        public async Task<bool> DeleteExtndata(long extnId, long tenantId)
        {
            var co = await _extnInfoRepository.SingleOrDefaultAsync(m => m.Id == extnId && m.TenantId == tenantId);
            if (co != null)
            {
                try
                {
                    _extnInfoRepository.Remove(co);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                catch (Exception ex) { }
            }
            return false;
        }
        public async Task<bool> DeleteDocListdata(long docListId, long tenantId)
        {
            var co = await _docListRepository.SingleOrDefaultAsync(m => m.Id == docListId && m.TenantId == tenantId);
            if (co != null)
            {
                try
                {
                    _docListRepository.Remove(co);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                catch (Exception ex) { }
            }
            return false;
        }

        public async Task<bool> CheckDocTypeName(string docTypeName)
        {
            var documentTypes = await _documentTypeRepository.SingleOrDefaultAsync(c => c.DocumentName == (docTypeName));
            if (documentTypes != null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CheckExtnName(string extnName)
        {
            try
            {
                var documentTypes = await _extnInfoRepository.SingleOrDefaultAsync(c => c.ExtnName == (extnName)); 
                if (documentTypes != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }
        public async Task<bool> DocumentTypeInDoclist(long docTypeid, long tenantId)
        {
            try
            {
                var docLists = _docListRepository.GetRangeAsync(c => c.DocumentTypeId == docTypeid && c.TenantId == tenantId);

                if (docLists == null || !docLists.Any())
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
        }
    }
}
