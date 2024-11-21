namespace CWB.Masters.MastersUtils
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Company
        {
            public const string GetCompanyTypes = Base + "/company-types";
            public const string GetDivisionsById = Base + "/divisions/{Id}/{tenantId}";
            public const string GetCompanies = Base + "/companies/{Id}";
            public const string PostCompany = Base + "/company";
            public const string IsCompanyExist = Base + "/check-company";
            public const string IsDivisionExist = Base + "/check-division";
            public const string DeleteCompany = Base + "/deletecompany/{companyID}/{tenantId}";
            public const string DeleteDivision = Base + "/deletedivision/{divisionID}/{tenantId}";

        }

        public static class OperationList
        {
            public const string GetOperationList = Base + "/operation-list/{Id}";
            public const string GetOperation = Base + "/operation-list/{Id}/{tenantId}";
            public const string GetOperationalDocumentTypes = Base + "/operation-list-doctypes/{Id}/{tenantId}";
            public const string PostOperation = Base + "/operation";
            public const string PostOperationalDocumentTypes = Base + "/operation-doctype";
            public const string IsOperationExist = Base + "/check-operation";
            public const string DeleteOperationDoc = Base + "/deleteopdoclist/{opDocId}/{tenantId}";
        }

        public static class Machine
        {
            public const string GetMachineTypes = Base + "/machine-types/{Id}";
            public const string PostMachineType = Base + "/machine-type";
            public const string PostMachine = Base + "/machine";
            public const string GetMachine = Base + "/machine/{Id}/{tenantId}";
            public const string GetMachines = Base + "/machines/{Id}";
            public const string IsMachineTypeExist = Base + "/machine-type-check";
            public const string IsMachineExist = Base + "/machine-check";
            public const string GetMachineProcDocs = Base + "/machine-procs-docs/{Id}/{tenantId}";
            public const string PostMachineProcDoc = Base + "/machine-procs-doc";
            public const string GetMcTypeDocList = Base + "/getmctypedoclist/{tenantId}";
            public const string PostMcTypeDocList = Base + "/postmctypedoclist";
            public const string DeleteMcTypDocList = Base + "/deletemctypdoclist/{mcTypeDocListId}/{tenantId}";
            public const string GetMcSlNoDocList = Base + "/getmcslnodoclist/{tenantId}";
            public const string PostMcSlNoDocList = Base + "/postmcslnodoclist";
            public const string DeleteMcSlNoDocList = Base + "/deletemcslnodoclist/{mcSlNoDocListId}/{tenantId}";
        }


        public static class Masters
        {
            public const string GetStatuses = Base + "/statuses";
            public const string GetAllItemMasterDocList = Base + "/getallitemmasterdoclist/{tenantId}";
            public const string PostItemMasterDocList = Base + "/postitemmasterdoclist";
            public const string GetAllItemMasterContent = Base + "/getallitemmastercontent";
            public const string DeleteItemMasterDoc = Base + "/deleteitemmasterdoc/{itemMasterDocListId}/{tenantId}";
            public const string CheckPartNoInDocList = Base + "/checkdoctypeinitemdoc/{documentTypeId}/{contentId}/{tenantId}";
        }

        public static class Routings {
            public const string RoutingListItems = Base + "/routinglistitems";
            //public const string RoutingListItem = Base + "/routinglistitem/{manufPartId}";
            public const string PostNewRouting = Base + "/newrouting";
            public const string AltRouting = Base + "/altrouting";
            
            public const string PostDeleteRouting = Base + "/deleterouting/{routingId}";
            public const string PreferredRouting = Base + "/preferredrouting";
            public const string PostRoutingStep = Base + "/routingstep";
            public const string PostStatusLog = Base + "/routingstatuslog";
            public const string GetStatusLog = Base + "/getstatuslog/{routingId}/{tenantId}";
            public const string ChangeRoutingStepSequence = Base + "/changeroutingstepsequence";
            
            
            public const string PostRoutingStepPart = Base + "/routingsteppart";
            public const string PostRoutingStepSupplier = Base + "/routingstepsupplier";
            public const string PostRoutingStepMachine = Base + "/routingstepmachine";
            public const string PreferredStepMachine = Base + "/preferredstepmachine/{routingStepId}/{routingStepMachineId}/{maxMachineCount}";
            public const string RoutingList = Base + "/routings/{manufPartId}";
            public const string RoutingSteps = Base + "/routingsteps/{routingId}";
            public const string StepSuppliers = Base + "/stepsuppliers/{stepId}";
            public const string StepMachines = Base + "/stepmachines/{stepId}";
            public const string StepParts = Base + "/stepparts/{stepId}";
            public const string DeleteStep = Base + "/deletestep/{stepId}";
            public const string GetRoutingStep = Base + "/getroutingstep/{stepId}";
            public const string DeleteStepMachine = Base + "/deletestepmachine/{stepId}/{machineId}";
            public const string DeleteStepPart = Base + "/deletesteppart/{stepId}/{stepPartId}";
            public const string DeleteStepSupplier = Base + "/deletestepsupplier/{stepId}/{supplierId}";
            public const string StepPartsByManufId = Base + "/steppartsbymanufid/{manufId}";

            public const string DeleteSubConDetails = Base + "/deletesubcon/{stepId}/{subConDetailsId}";
            public const string DeleteSubWSConDetails = Base + "/deletesubconws/{subConWSId}";

            public const string SubCons = Base + "/subcons/{stepId}";
            public const string SubConWSS = Base + "/subconswss/{stepId}/{subConDetailsId}";
            public const string SubConDetails = Base + "/subcondetails";
            public const string SubConWSDetails = Base + "/subconwsdetails";
            public const string PreferredSubCon = Base + "/preferredsubcon/{subConDetailsId}";

        }                                       

        public static class ManufacturedPartNoDetail
        {
            public const string PostManufacturedPartNoDetail = Base + "/manufacturedpartnodetail";
            public const string PostMPMakeFrom = Base + "/mpmakefrom";
            public const string PreferredMPMakeFrom = Base + "/preferredmpmakefrom";
            public const string PostMPBOM = Base + "/mpbom";
            // Added for Listing ManufacturedPartNoDetails
            public const string GetManufPart = Base + "/getmanufpart/{partId}/{tenantId}";
            public const string GetRMPart = Base + "/getrmpart/{partId}/{tenantId}";
            public const string GetBOFPart = Base + "/getbofpart/{partId}/{tenantId}";

            public const string GetManufacturedPartNoDetailList = Base + "/getmanufacturedpartnodetailList/{ManufPartType}/{companyName}/{tenantId}";
            public const string GetAllManufacturedPartNoDetailList = Base + "/mfdlist/{tenantId}";
            public const string GetMPMakeFromList = Base + "/mpmakefromlist/{partId}";//pass manufPartId from MPRawMeterials
            public const string GetMPMakeFrom = Base + "/getmakefrom/{Id}";
            public const string RemMakeFrom = Base + "/remmakefrom";
            public const string GetPartStatus = Base + "/getpartstatus/{tenantId}";

            public const string GetMPBOMList = Base + "/boms/{partId}";//pass manufPartId from MPBOMs
            public const string GetMPBOM = Base + "/getbom/{Id}";
            public const string RemBOM = Base + "/rembom";
          //  public const string HelloWorld = Base + "/helloworld";
            public const string GetUOMs = Base + "/getuoms/{tenantId}";
            public const string AddUOM = Base + "/adduom";
            public const string CheckUOM = Base + "/checkuom/{uomName}";
        }

        public static class RawMaterialDetail
        {
            //rawmateriadetail
            public const string PostRawMaterialDetail = Base + "/rawmaterialdetail";
            // Added for Listing RawMaterialDetails
            public const string GetRawMaterialDetailList = Base + "/getrawmaterialdetailList/{tenantId}";
            public const string GetRMTypes = Base + "/rmtypes";
            public const string GetRMSpecs = Base + "/rmspecs";
            public const string GetRMStandards = Base + "/rmstandards";
            public const string GetBaseRMs = Base + "/baserms";
            public const string BaseRM = Base + "/baserm";
            public const string CheckBaseRm = Base + "/checkbaserm/{rmName}";
            public const string CheckRMType = Base + "/checkrmtype/{rmTypeName}";
            public const string CheckRMSpec = Base + "/checkrmspec/{rmSpecName}";
            public const string CheckRMStandard = Base + "/checkrmstandard/{rmStName}";
            public const string RMType = Base + "/rmtype";
            public const string RMSpec = Base + "/rmspec";
            public const string RMStandard = Base + "/rmstandard";

            public const string GetPartPurchasesForPartId = Base + "/purchasesbypartId/{partId}/{tenantId}";
            
            //Get All objects
            public const string GetPartPurchases = Base + "/partpurchases/{tenantId}";
            //Add/Edit
            public const string PostPartPurchaseDetail = Base + "/partpurchase";
            public const string PostPartPreferredSupplier = Base + "/preferredsupplierpart";
            //Get a single object
            public const string GetPartPurchase = Base + "/getpartpurchase/{partPurchaseId}/{tenantId}";
            //public const string RemovePartPurchase = Base + "/rempartpurchase/{partPurchaseId}";
            public const string RemPartPurchaseDetail = Base + "/rempartpurchase";



            public const string OwnRMS = Base + "/ownrms/{tenantId}";
            public const string SupplierRMS = Base + "/supplierrms/{supplierId}/{tenantId}";

            public const string GetMasterParts = Base + "/itemmasterparts/{tenantId}";
            public const string GetMasterPartById = Base + "/itemmasterpartsbyid/{partid}";
            public const string GetPartsUOMs = Base + "/partsuoms/{tenantId}";
            public const string GetSelectParts = Base + "/selectparts/{tenantId}";
            //itemmasterparts
        }

        public static class MasterParts
        {
            public const string CheckPartNo = Base + "/check-partno/{partNo}";
        }

       
        public static class BoughtOutFinishDetail
        {
            public const string PostBoughtOutFinishDetail = Base + "/boughtoutfinishdetail";
            // Added for Listing BoughtOutFinishDetails
            public const string GetBoughtOutFinishDetailList = Base + "/bofs/{tenantId}";
        }

        public static class DocumentManagement
        {
            public const string AllDocumentType = Base + "/alldocumenttype/{tenantId}";
            public const string AllCustRetnData = Base + "/allcustretndata/{tenantId}";
            public const string GetAllExtn = Base + "/getallextn/{tenantId}";
            public const string GetAllDocUpload = Base + "/getalldocupload/{tenantId}";
            public const string GetAllDocView = Base + "/getalldocview/{tenantId}";
            public const string GetAllDocCategory = Base + "/getalldoccategory";
            public const string GetAllDocList = Base + "/getalldoclist/{tenantId}";
            public const string GetOneDocList = Base + "/getonedoclist/{doclistId}/{tenantId}";
            public const string CheckPartNoInDocList = Base + "/checkpartnoindoclist/{partId}/{tenantId}";
            public const string GetAllUiName = Base + "/getalluiname/{tenantId}";
            public const string GetReasonList = Base + "/getallreasonlist/{tenantId}";
            public const string GetAllRefDoc = Base + "/getallrefdoc/{tenantId}";
            public const string GetDocStatus = Base + "/getdocstatus/{statusid}";
            public const string PostDocumentType = Base + "/postdocumenttype";
            public const string PostCustRetnData = Base + "/postcustretndata";
            public const string PostExtn = Base + "/postextn";
            public const string PostDocUpload = Base + "/postdocupload";
            public const string PostDocView = Base + "/postdocview";
            public const string PostDocList = Base + "/postdocList";
            public const string PostUiName = Base + "/postuiname";
            public const string PostDocReason = Base + "/postdocreason";
            public const string PostDocLog = Base + "/postrefdoc";
            public const string DeleteDocType = Base + "/deletedocumenttype/{doctypeId}/{tenantId}";
            public const string DeleteCustRetdata = Base + "/deletecustretdata/{custRetId}/{tenantId}";
            public const string DeleteExtndata = Base + "/deleteextndata/{extnId}/{tenantId}";
            public const string DeleteDocListdata = Base + "/deletedoclistdata/{docListId}/{tenantId}";
            public const string CheckDocTypeName = Base + "/checkdoctypename/{docTypeName}";
            public const string CheckExtnName = Base + "/checkextnname/{extnName}";
            public const string DocumentTypeInDoclist = Base + "/doctypeindoclist/{docTypeid}/{tenantId}";
        }
    }
}
