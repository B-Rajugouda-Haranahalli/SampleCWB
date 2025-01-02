using CWB.App.AppUtils;
using CWB.App.Models.Contacts;
using CWB.App.Models.ItemMaster;
using CWB.CommonUtils.Common;
using CWB.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.Masters
{
    public class MastersServices : IMastersServices
    {
        private readonly ILoggerManager _logger;
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly long tenantId;

        public MastersServices(ILoggerManager logger, ApiUrls apiUrlsOptions, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _apiUrls = apiUrlsOptions;
            _httpContextAccessor = httpContextAccessor;
            tenantId = long.Parse(AppUtil.GetTenantId(_httpContextAccessor.HttpContext.User));
        }

        public async Task<bool> CheckIfCompanyExisit(long CompanyId, string CompanyName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/check-company");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            var data = new { CompanyId, CompanyName, TenantId = tenantId };
            return await RestHelper<bool>.PostAsync(uri, data, headers);
        }

        public async Task<bool> CheckIfDivisionExisit(long CompanyId, long DivisionId, string DivisionName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/check-division");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            var data = new { CompanyId, DivisionId, DivisionName, TenantId = tenantId };
            return await RestHelper<bool>.PostAsync(uri, data, headers);
        }

        public async Task<CompanyVM> Company(CompanyVM companyVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/company");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            companyVM.TenantId = tenantId;
            return await RestHelper<CompanyVM>.PostAsync(uri, companyVM, headers);
        }

        public async Task<bool> DeleteCompany(long companyId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/deletecompany/{companyId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
           
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<bool> DeleteDivision(long divisionId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/deletedivision/{divisionId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);

            return await RestHelper<bool>.GetAsync(uri, headers);
        }



        public async Task<IEnumerable<UOMVM>> GetUOMs()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getuoms/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<UOMVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<ContactsVM>> GetCompanies()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/companies/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<ContactsVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<RawMateriaTypeVM>> GetRMTypes()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rmtypes");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<RawMateriaTypeVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<RawMaterialSpecVM>> GetRMSpecs()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rmspecs");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<RawMaterialSpecVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<RawMaterialStandardVM>> GetRMStandards()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rmstandards");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<RawMaterialStandardVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<BaseRawMatrialVM>> GetBaseRMs()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/baserms");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<BaseRawMatrialVM>>.GetAsync(uri, headers);
        }




        public async Task<IEnumerable<ManufacturedPartNoDetailVM>> GetManufacturedPartNoDetailList(long ManufPartType,string companyName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getmanufacturedpartnodetailList/{ManufPartType}/{companyName}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<ManufacturedPartNoDetailVM>>.GetAsync(uri, headers);
        }

      


        public async Task<IEnumerable<ContactsVM>> GetDivisionsByCompanyId(long Id)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/divisions/{Id}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<ContactsVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<CompanyTypeVM>> GetCompanyTypes()
        {
            var uri = new Uri(_apiUrls.Gateway + "/cwbms/company-types");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<CompanyTypeVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<PartStatusVM>> GetStatuses()
        {
            var uri = new Uri(_apiUrls.Gateway + "/cwbms/statuses");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<PartStatusVM>>.GetAsync(uri, headers);
        }

        public async Task<ManufacturedPartNoDetailVM> ManufacturedPartNoDetail(ManufacturedPartNoDetailVM manufacturedPartNoDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/manufacturedpartnodetail");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            manufacturedPartNoDetailVM.TenantId = tenantId;
            return await RestHelper<ManufacturedPartNoDetailVM>.PostAsync(uri, manufacturedPartNoDetailVM, headers);
        }
        public async Task<MPMakeFromVM> MPMakeFrom(MPMakeFromVM manufacturedPartNoDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/mpmakefrom");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            manufacturedPartNoDetailVM.TenantId = tenantId;
            return await RestHelper<MPMakeFromVM>.PostAsync(uri, manufacturedPartNoDetailVM, headers);
        }
        public async Task<PartPurchaseDetailsVM> PreferredSupplier(PartPurchaseDetailsVM partPurchaseDetails)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/preferredsupplierpart");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            partPurchaseDetails.TenantId = tenantId;
            return await RestHelper<PartPurchaseDetailsVM>.PostAsync(uri, partPurchaseDetails, headers);
        }
        public async Task<MPMakeFromVM> MPPreferredMK(MPMakeFromVM manufacturedPartNoDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/preferredmpmakefrom");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            manufacturedPartNoDetailVM.TenantId = tenantId;
            return await RestHelper<MPMakeFromVM>.PostAsync(uri, manufacturedPartNoDetailVM, headers);
        }
        public async Task<MPBomVM> MPBOM(MPBomVM manufacturedPartNoDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/mpbom");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            manufacturedPartNoDetailVM.TenantId = tenantId;
            return await RestHelper<MPBomVM>.PostAsync(uri, manufacturedPartNoDetailVM, headers);
        }

        public async Task<IEnumerable<PartUOMVM>> GetPartsUOMs()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/partsuoms/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<PartUOMVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<MPMakeFromVM>> GetMPMakeFromListByPartId(string partId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/mpmakefromlist/{partId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<MPMakeFromVM>>.GetAsync(uri, headers);
        }

        public async Task<MPMakeFromVM> GetMakeFrom(string id)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getmakefrom/{id}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<MPMakeFromVM>.GetAsync(uri, headers);
        }

        public async Task<MPBomVM> GetBOM(string id)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getbom/{id}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<MPBomVM>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<MPBomVM>> BOMS(string partId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/boms/{partId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<MPBomVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<BoughtOutFinishDetailVM>> BOFS()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/bofs/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<BoughtOutFinishDetailVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<RawMaterialDetailVM>> SupplierRMS(string supplierId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/supplierrms/{supplierId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<RawMaterialDetailVM>>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<ManufacturedPartNoDetailVM>> GetAllManufacturedPartNoDetailList()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/mfdlist/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<ManufacturedPartNoDetailVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<ItemMasterDocListVM>> Getallitemmasterdoclist()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getallitemmasterdoclist/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<ItemMasterDocListVM>>.GetAsync(uri, headers);
        }

        public async Task<bool> CheckPartNo(string partNo)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/check-partno/{partNo}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<ManufacturedPartNoDetailVM> GetManufPart(int partId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getmanufpart/{partId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<ManufacturedPartNoDetailVM>.GetAsync(uri, headers);
            //
        }
        public async Task<RawMaterialDetailVM> GetRMPart(int partId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getrmpart/{partId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<RawMaterialDetailVM>.GetAsync(uri, headers);
        }

        public async Task<BoughtOutFinishDetailVM> GetBOFPart(int partId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getbofpart/{partId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<BoughtOutFinishDetailVM>.GetAsync(uri, headers);
        }

        


        public async Task<RawMaterialDetailVM> RawMaterialDetail(RawMaterialDetailVM rawMaterialDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rawmaterialdetail");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            rawMaterialDetailVM.TenantId = tenantId;
            return await RestHelper<RawMaterialDetailVM>.PostAsync(uri, rawMaterialDetailVM, headers);
        }
        public async Task<BoughtOutFinishDetailVM> BoughtOutFinishDetail(BoughtOutFinishDetailVM boughtOutFinishDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/boughtoutfinishdetail");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            boughtOutFinishDetailVM.TenantId = tenantId;
            return await RestHelper<BoughtOutFinishDetailVM>.PostAsync(uri, boughtOutFinishDetailVM, headers);
        }

        public async Task<PartPurchaseDetailsVM> PartPurchase(PartPurchaseDetailsVM purchaseDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/partpurchase");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            purchaseDetailVM.TenantId = tenantId;
            return await RestHelper<PartPurchaseDetailsVM>.PostAsync(uri, purchaseDetailVM, headers);
        }

        
        

        public async Task<IEnumerable<PartPurchaseDetailsVM>> PartPurchases()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/partpurchases/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<PartPurchaseDetailsVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<PartPurchaseDetailsVM>> PartPurchasesFor(int partId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/purchasesbypartId/{partId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<PartPurchaseDetailsVM>>.GetAsync(uri, headers);
        }
        

        public async Task<PartPurchaseDetailsVM> GetPartPurchase(int partPurchaseId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getpartpurchase/{partPurchaseId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<PartPurchaseDetailsVM>.GetAsync(uri, headers);
        }

        public async Task<IEnumerable<ItemMasterContentVM>> ItemMasterContents()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getallitemmastercontent");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<ItemMasterContentVM>>.GetAsync(uri, headers);
        }
        public async Task<ItemMasterDocListVM> PostItemMasteDocList(ItemMasterDocListVM purchaseDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/postitemmasterdoclist");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            purchaseDetailVM.TenantId = tenantId;
            return await RestHelper<ItemMasterDocListVM>.PostAsync(uri, purchaseDetailVM, headers);
        }
        public async Task<bool> DeleteItemMasterDocList(long itemMasterDocListId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/deleteitemmasterdoc/{itemMasterDocListId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<ItemMasterPartVM>> ItemMasterParts()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/itemmasterparts/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<ItemMasterPartVM>>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<PartStatusChangeLogVM>> GetPartStatus()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/getpartstatus/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<PartStatusChangeLogVM>>.GetAsync(uri, headers);
        }
        //itemmasterpartsbyid
        public async Task<MasterPartVM> ItemMasterPartById(int partid)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/itemmasterpartsbyid/{partid}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<MasterPartVM>.GetAsync(uri, headers);
        }
        public async Task<IEnumerable<SelectPartVM>> SelectParts()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/selectparts/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<SelectPartVM>>.GetAsync(uri, headers);
        }

        //redundant as of 08/11/2023
        //'Search Raw Materials' uses partpurchases instead
        public async Task<IEnumerable<RawMaterialDetailVM>> OwnRMS()
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/ownrms/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<List<RawMaterialDetailVM>>.GetAsync(uri, headers);
        }


       

        public async Task<BaseRawMatrialVM> BaseRM(BaseRawMatrialVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/baserm");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<BaseRawMatrialVM>.PostAsync(uri, model, headers);
        }

        public async Task<RawMaterialStandardVM> RMStandard(RawMaterialStandardVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rmstandard");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<RawMaterialStandardVM>.PostAsync(uri, model, headers);

        }

        public async Task<RawMaterialSpecVM> RMSpec(RawMaterialSpecVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rmspec");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<RawMaterialSpecVM>.PostAsync(uri, model, headers);

        }

        public async Task<RawMateriaTypeVM> RMType(RawMateriaTypeVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rmtype");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<RawMateriaTypeVM>.PostAsync(uri, model, headers);
        }

        public async Task<MPMakeFromVM> RemMakeFrom(MPMakeFromVM purchaseDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/remmakefrom");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            purchaseDetailVM.TenantId = tenantId;
            return await RestHelper<MPMakeFromVM>.PostAsync(uri, purchaseDetailVM, headers);
        }

        public async Task<MPBomVM> RemBOM(MPBomVM bomvm)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rembom");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            bomvm.TenantId = tenantId;
            return await RestHelper<MPBomVM>.PostAsync(uri, bomvm, headers);
        }

        public async Task<UOMVM> AddUOM(UOMVM model)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/adduom");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            model.TenantId = tenantId;
            return await RestHelper<UOMVM>.PostAsync(uri, model, headers);
        }
        public async Task<bool> CheckUOM(string uomName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkuom/{uomName}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> CheckRmType(string rmTypeName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkrmtype/{rmTypeName}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> CheckBaseRm(string rmBaseName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkbaserm/{rmBaseName}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> CheckRmStandard(string rmStName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkrmstandard/{rmStName}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> CheckRmSpec(string rmSpecName)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkrmspec/{rmSpecName}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<PartPurchaseDetailsVM> RemPartPurchase(PartPurchaseDetailsVM purchaseDetailVM)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/rempartpurchase");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            purchaseDetailVM.TenantId = tenantId;
            return await RestHelper<PartPurchaseDetailsVM>.PostAsync(uri, purchaseDetailVM, headers);
        }


        public async Task<bool> CheckPartNoInDocList(long partId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkpartnoindoclist/{partId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }
        public async Task<bool> CheckDocumentTypeInItemMaster(long documentTypeId, long contentId)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/checkdoctypeinitemdoc/{documentTypeId}/{contentId}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

        public async Task<bool> CheckDocTypeInDocList(long docTypeid)
        {
            var uri = new Uri(_apiUrls.Gateway + $"/cwbms/doctypeindoclist/{docTypeid}/{tenantId}");
            var headers = await AppUtil.GetAuthToken(_httpContextAccessor.HttpContext);
            return await RestHelper<bool>.GetAsync(uri, headers);
        }

    }
}
