using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Company;
using CWB.Masters.Repositories.DocumentManagement;
using CWB.Masters.Repositories.ItemMaster;
using CWB.Masters.Repositories.Machines;
using CWB.Masters.Repositories.OperationList;
using CWB.Masters.Repositories.Routings;
using CWB.Masters.Services.Company;
using CWB.Masters.Services.DocumentManagement;
using CWB.Masters.Services.ItemMaster;
using CWB.Masters.Services.Machines;
using CWB.Masters.Services.OperationList;
using CWB.Masters.Services.Routings;
using Microsoft.Extensions.DependencyInjection;

namespace CWB.Masters.MastersUtils
{
    public static class AppDIExtensions
    {
        public static void ConfigureAppDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDivisionRepository, DivisionRepository>();
            services.AddTransient<IOperationListRepository, OperationListRepository>();
            services.AddTransient<IOperationalDocumentRepository, OperationalDocumentRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IOperationListService, OperationListService>();
            services.AddTransient<IMachineTypeRepository, MachineTypeRepository>();
            services.AddTransient<IMachineRepository, MachineRepository>();
            services.AddTransient<IMachineProcessDocumentRepository, MachineProcessDocumentRepository>();
            services.AddTransient<IMachineService, MachineService>();
            services.AddTransient<IManufacturedPartNoDetailRepository, ManufacturedPartNoDetailRepository>();
            services.AddTransient<IPartStatusChangeLogRepository, PartStatusChangeLogRepository>();
            services.AddTransient<IManufacturedPartNoDetailService, ManufacturedPartNoDetailService>();
            services.AddTransient<IRawMaterialDetailRepository, RawMaterialDetailRepository>();
            services.AddTransient<IRawMaterialTypeRepository, RawMaterialTypeRespository>();
            services.AddTransient<IRawMaterialSpecRepository, RawMaterialSpecRespository>();
            services.AddTransient<IRawMaterialStandardRepository, RawMaterialStandardRespository>();
            services.AddTransient<IBaseRawMaterialRepository, BaseMaterialRespository>();
            services.AddTransient<IRawMaterialDetailService, RawMaterialDetailService>();
            services.AddTransient<IPartPurchaseDetailRepository, PartPurchaseDetailRepository>();
            services.AddTransient<IBoughtOutFinishDetailRepository, BoughtOutFinishDetailRepository>();
            services.AddTransient<IBoughtOutFinishDetailService, BoughtOutFinishDetailService>();
            services.AddTransient<IMPMakeFromRepository, MPMakeFromRepository>();
            services.AddTransient<IMPBOMRepository, MPBOMRepository>();
            services.AddTransient<IUOMRepository, UOMRepository>();
            services.AddTransient<IMasterPartService, MasterPartService>();
            services.AddTransient<IMasterPartRepository, MasterPartRepository>();
            services.AddTransient<IRoutingService, RoutingService>();
            services.AddTransient<IRoutingRepository, RoutingRepository>();
            services.AddTransient<IRoutingStepRepository, RoutingStepRepository>();
            services.AddTransient<ISubConDetailsRepository, SubConDetailsRepository>();
            services.AddTransient<ISubConWorkStepDetailsRepository, SubConWorkStepDetailsRepository>();
            services.AddTransient<IRoutingStepPartRepository, RoutingStepPartRepository>();
            services.AddTransient<IRoutingStepMachineRepository, RoutingStepMachineRepository>();
            services.AddTransient<IRoutingStepSupplierRepository, RoutingStepSupplierRepository>();
            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddTransient<ICustRetnDataRepository, CustRetnDataRepository>();
            services.AddTransient<IExtnInfoRepository, ExtnInfoRepository>();
            services.AddTransient<IDocUploadRepository, DocUploadRepository>();
            services.AddTransient<IDocViewRepository, DocViewRepository>();
            services.AddTransient<IDocCategoryRepository, DocCategoryRepository>();
            services.AddTransient<IDocListRepository, DocListRepository>();
            services.AddTransient<IDocStatusRepository, DocStatusRepository>();
            services.AddTransient<IUiListrepository, UiListrepository>();
            services.AddTransient<IItemMasterDocListRepository, ItemMasterDocListRepository>();
            services.AddTransient<IItemMasterContentRepository, ItemMasterContentRepository>();
            services.AddTransient<IMcTypeDocListRepository, McTypeDocListRepository>();
            services.AddTransient<IMcSlNoDocListRepository, McSlNoDocListRepository>();
            services.AddTransient<IRefDocReasonListRepository, RefDocReasonListRepository>();
            services.AddTransient<IRefDocLogRepository, RefDocLogRepository>();
            services.AddTransient<IRoutingStatusLogRepository, RoutingStatusLogRepository>();
            services.AddTransient<IDocumentManagementService, DocumentManagementService>();
        }
    }
}
