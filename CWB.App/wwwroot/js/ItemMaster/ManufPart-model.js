let existingpartdata = [] ;
let custRM = [];
let ownRM = [];
let ownRMSelected = false;
let tableHasOwnRMParts = false;
let dataWritten = false;

let makeFroms = new Array();
let boms = new Array();

let archive = 0;

var ContactsConstants = {
    MPMakeFromId: 0,
    DivisionId: 0
    
};

var MPRawMaterialUtil = {
    UpdateFormIDs: (data) => {
        $("#MPMakeFromId").val(data.mPMakeFromId);
    },
    UpdateMakeFromTableNew: (keyVals) => {
        //////debugger;
        var tablebody = $("#tbl-MakeFromRM tbody");
        var templateElement = $("#MakeFrom-template").html();
        //$(tablebody).html("");//empty tbody
        for (const pair of keyVals) {
            let key = pair[0];
            let val = pair[1];
            templateElement = templateElement.replaceAll("{" + key + "}", val);
        }
        $(tablebody).append(templateElement);
    },
    ClearMakefromTab: () => {
        $("#InputWeight").val("");
        $("#ScrapGenerated").val("");
        $("#QuantityPerInput").val("");
        $("#YieldNotes").val("");
        $("#MPMakeFromId").val("0");
        $('#PreferedRawMaterial').prop('checked', false);
    }
};
var MPBOMUtil = {
    UpdateFormIDs: (data) => {
        $("#MPBOMId").val(data.mPBOMId);
    },
    UpdateBOMTableNew: (keyVals) => {
        //////debugger;
        var tablebody = $("#tbl-BOM tbody");
        var templateElement = $("#BOM-template").html();
        //$(tablebody).html("");//empty tbody
        for (const pair of keyVals) {
            let key = pair[0];
            let val = pair[1];
            templateElement = templateElement.replaceAll("{" + key + "}", val);
            //$(tablebody).append(AppUtil.ProcessTemplateData("BOM-template", pair);
            //if (ContactsConstants.MPRawMeterialId == formData[i].mpMakeFromId) {
            // ManufPartFormUtil.PopulateForm(data[i]);
            //}
        }
        $(tablebody).append(templateElement);
    },
    ClearBOMTab: () => {
        $("#Quantity").val("");
        $("#MPBOMId").val("0");
    }
};

var ManufPartFormUtil = {

    UpdateFormIDs: (data) => {
        $("#ManufacturedPartNoDetailId").val(data.manufacturedPartNoDetailId);
        $("#PartId").val(data.partId);
    },
    PopulateForm: (data) => {
        $("#DivisionId").val(data.divisionId);
        $("#CompanyId").val(data.companyId);
        $("#CompanyType").val(data.companyType);
        $("#CompanyName").val(data.companyName);
        $("#DivisionName").val(data.divisionName);
        $("#Location").val(data.location);
        $("#Notes").val(data.notes);
    },
    ClearConstants: () => {
        ContactsConstants.MPRawMeterialId = 0;
    },
    
    ClearMainTab: () => {
        $("#ManufacturedPartNoDetailId").val("0");
        $("#CompanyId").val($("#CompanyId option:first").val()).trigger('change');
        $("#CompanyName").val("");
        $("#PartNo").val("");
        $("#RevNo").val("0")
        $("#RevDate").val(null);
        $("#PartDescription").val("");
        $("#UOMId").val("1");
        $("#FinishedWeight").val("");
        $("#StatusChangeReason").val("");
        $("PartId").val("0");
    },
    ConfirmDialog: (id, message) => {
        var result = confirm(message);
        //console.log("result " + result);
        if (result) {
            //console.log("clearing main tab ");
            ManufPartFormUtil.ClearMainTab();
            if (id == 1) {
                MPBOMUtil.ClearBOMTab();
                $("#TabHeadBOM").hide();
                $("#TabHeadMakefrom").show();
                //$('.nav-pills a[href="#tab-2"]').tab('show')
              //  $("#InputPartNo").val($("#PartNumber").val);
             //   $("#MFDescription").val($("#PartDescription").val);
            }
            else if (id == 2) {
                MPRawMaterialUtil.ClearMakefromTab();
                $("#TabHeadMakefrom").hide();
                $("#TabHeadBOM").show();
                //$('.nav-pills a[href="#tab-3"]').tab('show')
              //  $("#BOMPartNo").val($("#PartNumber").val);
              //  $("#BOMPartDesc").val($("#PartDescription").val);
            }
        }
        else {
            if (id == 1) {
                $("#ManufChildPart").prop("checked", false);
                $("#Assembly").prop("checked", true);
            }
            else //if (id == 2) 
            {
                $("#Assembly").prop("checked", false);
                $("#ManufChildPart").prop("checked", true);
            }
        }
    },
    ValidateMainTabForRadio: () => {
        if ($("#CompanyName").val().length != "") {
            return true;
        }
        if ($("#PartNo").val().length != "") {
            return true;
        }
        if ($("#PartDescription").val().length != "") {
            return true;
        }
        if ($("#RevNo").val().length != "") {
            return true;
        }
        return false;
    },
    ValidateManufPartDetails: (Mode) => { 
        var Message = "";
        var ManufacturedPartDetail = true;
        if ($("#PartNo").val().length == "") {
            ManufacturedPartDetail = false;
            Message += "Part No\n"
        }

        if ($("#PartDescription").val().length == "") {
            ManufacturedPartDetail = false;
            Message += "Part Desc\n"
        }
        if ($("#RevNo").val().length == "") {
            ManufacturedPartDetail = false;
            Message += "Rev No\n"
        }
       
        if (ManufacturedPartDetail == false && Mode == 1) {
            alert("Please Fill The Basic Information.");
            //alert(Message);
        }
        if (ManufacturedPartDetail == false && Mode == 2) {
            if ($("#PartNo").val().length == "") {
                ManufacturedPartDetail = false;
                var newNamevalidate = document.getElementById('PartNo');
                newNamevalidate.style.border = '2px solid red';
            }

            if ($("#PartDescription").val().length == "") {
                ManufacturedPartDetail = false;
                var newNamevalidate = document.getElementById('PartDescription');
                newNamevalidate.style.border = '2px solid red';
            }
            if ($("#FinishedWeight").val().length == "") {
                ManufacturedPartDetail = false;
                var newNamevalidate = document.getElementById('FinishedWeight');
                newNamevalidate.style.border = '2px solid red';
            }

            if ($("#FinalPartNosoldtoCustomer").prop("checked")) {

                var newNamevalidate = document.getElementById('PriceSettledwithCustomer_INR');
                newNamevalidate.style.border = '2px solid red';
            } else {
                // Optionally reset the border if unchecked
                var newNamevalidate = document.getElementById('PriceSettledwithCustomer_INR');
                newNamevalidate.style.border = '';
            }

            //alert("Please Fill The Basic Information.");
            //alert(Message);
        }
        return ManufacturedPartDetail;
        //return true;
    },
    ValidateMPMakeFrom: () => {
        var Message = "";
         var RawMeterial = true;
        if ($("#MFDescription").val().length == "") {
            RawMeterial = false;
            var newNamevalidate = document.getElementById('MFDescription');
            newNamevalidate.style.border = '2px solid red';
        }
        else {
            var newNamevalidate = document.getElementById('MFDescription');
            newNamevalidate.style.border = '';
        }
         if ($("#InputWeight").val().length == "") {
             RawMeterial = false;
             var newNamevalidate = document.getElementById('InputWeight');
             newNamevalidate.style.border = '2px solid red';
         }
         else {
             var newNamevalidate = document.getElementById('InputWeight');
             newNamevalidate.style.border = '';
         }
        if ($("#ScrapGenerated").val().length == "") {
            RawMeterial = false;
            var newNamevalidate = document.getElementById('ScrapGenerated');
            newNamevalidate.style.border = '2px solid red';
        }
        else {
            var newNamevalidate = document.getElementById('ScrapGenerated');
            newNamevalidate.style.border = '';
        }
        if ($("#QuantityPerInput").val().length == "") {
            RawMeterial = false;
            var newNamevalidate = document.getElementById('QuantityPerInput');
            newNamevalidate.style.border = '2px solid red';
        }
        else {
            var newNamevalidate = document.getElementById('QuantityPerInput');
            newNamevalidate.style.border = '';
        }
        if ($("#YieldNotes").val().length == "") {
            RawMeterial = false;
            var newNamevalidate = document.getElementById('YieldNotes');
            newNamevalidate.style.border = '2px solid red';
         }
        else {
            var newNamevalidate = document.getElementById('YieldNotes');
            newNamevalidate.style.border = '';
        }
         if (RawMeterial == false) {
            //alert("Field(s) cannot be left blank.");
        }
        return RawMeterial;
        //return true;
    },
    ValidateBOM: () => {
        var Message = "";
        var Bom = true;

        if ($("#BOMPartNo").val().length == "") {
            Bom = false;
            var newNamevalidate = document.getElementById('BOMPartNo');
            newNamevalidate.style.border = '2px solid red';
        }
        else {
            var newNamevalidate = document.getElementById('BOMPartNo');
            newNamevalidate.style.border = '';
        }
        if ($("#BOMPartDesc").val().length == "") {
            Bom = false;
            var newNamevalidate = document.getElementById('BOMPartDesc');
            newNamevalidate.style.border = '2px solid red';
        }
        else {
            var newNamevalidate = document.getElementById('BOMPartDesc');
            newNamevalidate.style.border = '';
        }
        if ($("#Quantity").val().length == "" || $("#Quantity").val() == "0") {
            Bom = false;
            var newNamevalidate = document.getElementById('Quantity');
            newNamevalidate.style.border = '2px solid red';
        }
        else {
            var newNamevalidate = document.getElementById('Quantity');
            newNamevalidate.style.border = '';
        }
        
        //if (Bom == false) {
        //    alert("Field(s) cannot be left blank.");
        //}
        return Bom;
        //return true;
    }
};
function DecodeManufPartId() {
    var partid = $("#PartId").val();
    $.ajax({
        type: "POST",
        url: "/masters/DecodePartId",
        data: { partId: partid },
        success: function (decodepartid) {
            $("#PartId").val(decodepartid);
            var closeatag = document.getElementById("closeatag");
            if (decodepartid != 0) {
                $("#headingN").text("Edit");
                closeatag.href = "/Masters/MasterDetails";
            } else {
                $("#headingN").text("New");
                closeatag.href = "/Masters/Index";
            }
        }
    });
}

//function viewFile(fileName) {
    
//    $('#fileModal').modal('show');

//}
function loadReasonDropDown() {
    var selElem = $('#DocReasonName');
    selElem.html('');
    api.getbulk("/DocumentManagement/GetAllRefReson").then((data) => {

        var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        selElem.append(rdiv_data);
        for (i = 0; i < data.length; i++) {
            rdiv_data = "<option value='" + data[i].refDocReasonListId + "'>" + data[i].docReason + "</option>";
            selElem.append(rdiv_data);
        }
    });

}
function displayFileName() {
    var docid = parseInt($("#doclistidFile").val());
    if (docid != 0) {
        var doctypeid = parseInt($("#docTypeIdFile").val());
        api.get("/DocumentManagement/GetAllDocumentType").then((data) => {
            const fdoc = data.find(item => item.documentTypeId === doctypeid);
            if (fdoc.docuCategory == 2) {
                var confrimval = confirm("Do You Want Move the Exsisting File To Archive.");
                if (confrimval) {
                    var fileInput = document.getElementById('fileUploadInput');
                    var fileName = fileInput.files[0].name; // Get the uploaded file name
                    document.getElementById('fileNameDisplay').value = fileName; // Display the file name in the text input
                    var docid = parseInt($("#doclistidFile").val());
                    archive = 1;
                    $("#Resonbtn").click();
                } else {
                    var fileInput = document.getElementById("fileUploadInput");
                    fileInput.value = "";
                }
            } else {
                var fileInput = document.getElementById('fileUploadInput');
                var fileName = fileInput.files[0].name; // Get the uploaded file name
                document.getElementById('fileNameDisplay').value = fileName;
            }
        }).catch((error) => {
        });
    } else {
        var fileInput = document.getElementById('fileUploadInput');
        var fileName = fileInput.files[0].name; // Get the uploaded file name
        document.getElementById('fileNameDisplay').value = fileName; 
    }
}

$(document).ready(function () {
    loadDocUploadList();
    $("#ManufacturedPartType").change(function () {
        loadDocUploadList();
    });
    $('#RefLogPopup').on('shown.bs.modal', function (event) {

        var relatedTarget = $(event.relatedTarget);
        var doclistid = relatedTarget.data("doclistid");
        var doctypename = relatedTarget.data("doctypename");
        var documenttypeid = relatedTarget.data("documenttypeid");
        var partnos = $("#PartNo").val();
        api.get("/DocumentManagement/GetAllDocumentType").then((data) => {
            const fdoc = data.find(item => item.documentTypeId === documenttypeid);
            if (fdoc.docuCategory == 2) {
                $("#SpanDocType").text(doctypename);
                $("#SpanDocPart").text(partnos);
                api.getbulk("/masters/GetRefDocLogOfDoclistId?doclistid=" + doclistid).then((data) => {
                    //data = data.filter(item => item.status == 1 || item.status==0);\
                    var tablebody = $("#RefLogGrid tbody");
                    $(tablebody).html("");//empty tbody
                    for (i = 0; i < data.length; i++) {
                        $(tablebody).append(AppUtil.ProcessTemplateDataNew("RefLogGridRow", data[i], i));
                    }
                }).catch((error) => {
                    console.log(error);
                });
            } else {
                $("#RefLogPopup").modal("hide");
            }
        }).catch((error) => {
        });
    });
    $('#RefDocReason').on('shown.bs.modal', function (event) {
        loadReasonDropDown();
    });

    $("#RefDocSave").on("click", function () {
        var PartIds = parseInt($("#PartId").val());
        var RefDocId = parseInt($("#RefDocId").val());
        var DocReasonName = parseInt($("#DocReasonName").val());
        var doclistidFile = parseInt($("#doclistidFile").val());
        var comments = $("#RefDoc").val();
        var today = new Date();
        var deletionDate = today.toISOString().split('T')[0];
        if (isNaN(DocReasonName) || DocReasonName === 0) {
            var newNamevalidate = document.getElementById('DocReasonName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('DocReasonName');
            newNamevalidate.style.border = '';
        }

        var rowData = {
            refDocLogId: 0,
            partId: PartIds,
            docListId: doclistidFile,
            docReasonId: DocReasonName,
            comments: comments,
            uploadedOn: deletionDate,
            action: "Replacement"
        };
        $.ajax({
            type: "POST",
            url: '/Masters/PostRefDocLog',
            contentType: "application/json; charset=utf-8",
            headers: { 'Content-Type': 'application/json' },
            data: JSON.stringify(rowData),
            dataType: "json",
            success: function (result) {
                var DocReasonName = document.getElementById('DocReasonName');
                var RefDoc = document.getElementById('RefDoc');
                DocReasonName.style.border = '';
                RefDoc.style.border = '';
                $('#RefDocReason').modal('hide');
            }
        });

    });


    $('#doc-item').on('shown.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var doctypeid = relatedTarget.data("doctypeid");
        var doctypename = relatedTarget.data("doctypename");
        var docdatareqdcust = relatedTarget.data("docdatareqdcust");
        var doccatid = relatedTarget.data("doccatid");
        var fileextnid = relatedTarget.data("fileextnid");
        var retperyear = relatedTarget.data("retperyear");
        var retpermon = relatedTarget.data("retpermon");
    });
    $('#fileModal').on('shown.bs.modal', function () {
        $(this).find('.modal-content').css({
            'width': '1000px',
            'height': '750px'
        });
        var file = "kier-in-sight-archives-BSPG-wXR7zM-unsplash.jpg";

        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/masters/ViewFile?fileName=' + file, true);
        xhr.responseType = 'arraybuffer';
        xhr.onload = function (e) {
            if (this.status == 200) {
                var blob = new Blob([this.response], { type: "image/jpeg" });

                const objectElement = document.getElementById('fileViewer');
                const url = URL.createObjectURL(blob);
                objectElement.src = url;
                //objectElement.width = '1000px';
                //objectElement.height = '1000px';
                //objectElement.type = 'text/plain';
                //var link = document.createElement('a');
                //link.href = window.URL.createObjectURL(blob);
                //link.download = "Report_" + new Date() + ".pdf";
                //link.click();
            }
        };
        xhr.send();
    }).on('hidden.bs.modal', function () {
       
    });
    DecodeManufPartId();
    if (!document.body) {
        return setTimeout(jquery.ready,1);
    };
    // Initialize select2
    $("#CompanyId").select2();
    // Read selected option
    //loadCustomers("CompanyId");
    //$("#CompanyId").trigger("change");

    $("#TabMPMain").on('click',function (event) {
        if ($('.nav-tabs .active').text().trim().indexOf("Basic") >= 0) {
            event.preventDefault();
            return;
        }
        else {
            $('.nav-pills a[href="#tab-1"]').tab('show');
            CURRENT_TAB = "TabMPMain";
            const selectedValue = $("#ManufacturedPartType").val();
            if (selectedValue === "1") {
                $("#TabHeadBOM").hide();
                $("#TabHeadMakefrom").show();
            } else {
                $("#TabHeadBOM").show();
                $("#TabHeadMakefrom").hide();
            }
            if (modelObj.Edit) {
                //do nothing 
            }
            else {
                document.getElementById("ManufPartForm").reset();
                //Reset hidden fields
                $("#CompanyName").val("");
                $("#PartId").val("0");
                $("#ManufacturedPartNoDetailId").val("0");
                ManufPartFormUtil.ClearMainTab();
                $("#CompanyId").val("").trigger('change');
            }
        }

    });
    $("#TabHeadMakefrom").on('click',function (event) {
       /* debugger;
        var relatedTarget = $(event.relatedTarget);
        var makefromid = relatedTarget.data("makefromid");
        api.get("/masters/getmakefrom?Id=" + makefromid).then((data) => {
            ownRM = data;
            var tablebody = $("#tbl-MakeFromRM tbody");
            $(tablebody).html("");//empty tbody
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateDataNew("MakeFromRMTemplate", data[i], i));
            }
        });*/ 

        //  //////debugger;
        if (!ManufPartFormUtil.ValidateManufPartDetails(1)) {
        }
        else {
            if ($("#ManufacturedPartNoDetailId").val() == "0") {
                alert("Save info to proceed further.");
                event.preventDefault();
                return;
            }
            CURRENT_TAB = "TabHeadMakefrom";
            $('.nav-pills a[href="#tab-2"]').tab('show');
            document.getElementById("MPRawMaterial").reset();

            $("#CompanyName").val($("#CompanyId option:selected").text());

            var coName = $("#CompanyName").val();
            var partDesc = $("#PartDescription").val();
            var partNo = $("#PartNo").val();
            
            $('#lblCompanyName').text(coName);
          //  $("#InputPartNo").val(partNo);
            $("#DelManufPartId").val($("#ManufacturedPartNoDetailId").val());
            $("#EditManufPartId").val($("#ManufacturedPartNoDetailId").val());
            var manufPartId = $("#ManufacturedPartNoDetailId").val();
            $("#ManufPartId").val($("#ManufacturedPartNoDetailId").val());
            $("#lblPartNumber").text(partNo);
        //    $("#MFDescription").val(partDesc);
            $("#lblPartDescription").text(partDesc);
            var tablebody = $("#tbl-MakeFromRM tbody");
            tablebody.html("");
            makeFroms = new Array();
            if (modelObj.Edit)
            {
                reloadMakeFroms(manufPartId);
            }
        }
    });

    $("#TabHeadBOM").on('click',function (event) {

        if (!ManufPartFormUtil.ValidateManufPartDetails(1)) {
        }
        else {
            if ($("#ManufacturedPartNoDetailId").val() == "0") {
                alert("Save info to proceed further.");
                event.preventDefault();
                return;
            }
            CURRENT_TAB = "TabHeadBOM";
            $('.nav-pills a[href="#tab-3"]').tab('show');
            document.getElementById("MPBOM").reset();
            $("#CompanyName").val($("#CompanyId option:selected").text());
            var coName = $("#CompanyName").val();
            var partDesc = $("#PartDescription").val();
            var partNo = $("#PartNo").val();
            var partId = $("#PartId").val();
            $('#lblBOMCompanyName').text(coName);
            //$("#BOMPartNo").val(partNo);
            $("#DelBOMManufPartId").val($("#ManufacturedPartNoDetailId").val());
            $("#EditBOMManufPartId").val($("#ManufacturedPartNoDetailId").val());
            $("#BOMManufPartId").val($("#ManufacturedPartNoDetailId").val());
            $("#lblBOMPartNumber").text(partNo);
            var manufPartId = $("#ManufacturedPartNoDetailId").val();
            //$("#BOMPartDesc").val(partDesc);
            $("#lblBOMPartDescription").text(partDesc);
            var tablebody = $("#tbl-BOM tbody");
            tablebody.html("");
            boms = new Array();
            if (modelObj.Edit) {
                reloadBOMs(manufPartId);
            }
        }
    });

    $('#rm-select').on('show.bs.modal', function (event) {
        //debugger;
        ownRM = {};
        loadBaseRMs("OBaseRawMaterialId");
        loadRMTypes("ORawMaterialTypeId");
        loadSuppliers("OSupplierId");
        ownRM = "";
        //   $("OPartNo").val($("#InputPartNo").val());
        //  $("OPartDescription").val($("#MFDescription").val());
        //OPartNo
        //OPartDescription
        api.get("/masters/ownrms").then((data) => {
            //console.log(data);
            ownRM = data;
            var tablebody = $("#OwnRMTable tbody");
            $(tablebody).html("");//empty tbody
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateDataNew("OwnRMSelectionTemplate", data[i],i));
            }

        }).catch((error) => {

        });

    });

    $('#rm-select-cust').on('show.bs.modal', function (event) {
        custRM = {};
        loadBaseRMs("CBaseRawMaterialId");
        loadRMTypes("CRawMaterialTypeId");
        let spansupplier = document.getElementById("RMSupplier");
        spansupplier.innerText = $("#CompanyName").val();
        //   $("CPartNo").val($("#InputPartNo").val());
        //      $("CPartDescription").val($("#MFDescription").val());
        //CPartNo
        //CPartDescription
        var tablebody = $("#CustRMTable tbody");
        var coId = $("#CompanyId option:selected").val();
        $(tablebody).html("");//empty tbody
        api.get("/masters/supplierrms?supplierId=" + 1).then((data) => {
            custRM = data;
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateDataNew("CustomerSuppliedRMTemplate", data[i],i));
            }

        }).catch((error) => {
        });
    });

    //rm-select
    $("#EditMakeFrom").click(function (event) {
        ////debugger;
        EditMakeFrom(event);
    });

    $("#DelMakeFrom").click(function (event) {
        ////debugger;
        DeleteMakdeFrom(event);
    });

    $("#EditBOM").click(function (event) {
        ////debugger;
        EditBOM(event);
    });

    $("#DelBOM").click(function (event) {
        ////debugger;
        DeleteBOM(event);
       
    });

    $("#CancelDelMakeFrom").click(function (event) {
        ////debugger;
        document.getElementById("btnDelMakeFromClose").click();

    });

    $("#CancelDelBOM").click(function (event) {
        ////debugger;
        document.getElementById("btnDelBOMClose").click();

    });

    $('#dialog-DeleteBOM').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var bomid = relatedTarget.data("bomid");
        api.get("/masters/getbom?Id=" + bomid).then((eData) => {
           /* for (var key in eData) {
                console.log(key);
                console.log(eData[key]);
            }*/
            //console.log("++++++++++++++++");
            key = "bomPartNo";
            $("#DelBOMPartNo").val(eData[key]);
            key = "bomPartDesc";
            $("#DelBOMPartDesc").val(eData[key]);
            key = "quantity";
            $("#DelQuantity").val(eData[key]);
            key = "mpbomId";
            $("#DelMPBOMId").val(eData[key]);
        }).catch((error) => {
        });
        //alert(bomid);
    });

    $('#dialog-DeleteMakeFrom').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var makefromid = relatedTarget.data("makefromsid");
        api.get("/masters/getmakefrom?Id=" + makefromid).then((eData) => {
         /*   for (var key in eData) {
                console.log(key);
                console.log(eData[key]);
            }
            console.log("++++++++++++++++");*/
            key = "inputWeight";
            $("#DelInputWeight").val(eData[key]);
            key = "mfDescription";
            $("#DelMFDescription").val(eData[key]);
            key = "quantityPerInput";
            $("#DelQuantityPerInput").val(eData[key]);

            key = "yieldNotes";
            $("#DelYieldNotes").val(eData[key]);
            masterPartNo = eData[key];
            key = "preferedRawMaterial";
            $("#DelPreferedRawMaterial").val(eData[key]);
            key = "inputPartNo";
            $("#DelInputPartNo").val(eData[key]);

            key = "partMadeFrom";
            $("#DelPartMadeFrom").val(eData[key]);
            key = "scrapGenerated";
            $("#DelScrapgenerated").val(eData[key]);
            key = "mpMakeFromId";
            $("#DelMPMakeFromId").val(eData[key]);

            key = "mpPartId";
            $("#DelMPPartId").val(eData[key]);
            key = "manufPartId";
            $("#DelManufPartId").val(eData[key]);

        }).catch((error) => {
        });
        //alert(makefromid);
    });

    $('#dialog-EditBOM').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var bomid = relatedTarget.data("bomid");
        //bomPartNo
        //bomPartDesc
        //quantity
        api.get("/masters/getbom?Id=" + bomid).then((eData) => {
           /* for (var key in eData) {
                console.log(key);
                console.log(eData[key]);
            }
            console.log("++++++++++++++++");*/
            key = "bomPartNo";
            $("#EditBOMPartNo").val(eData[key]);
            key = "bomPartDesc";
            $("#EditBOMPartDesc").val(eData[key]);
            key = "quantity";
            $("#EditQuantity").val(eData[key]);
            key = "mpbomId";
            $("#EditMPBOMId").val(eData[key]);
            key = "bomPartId";
            $("#EditMPBOMPartId").val(eData[key]);

        }).catch((error) => {
        });
        //alert(bomid);
    });
    $('#dialog-EditMakeFrom').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var makefromid = relatedTarget.data("makefromid");
        api.get("/masters/getmakefrom?Id=" + makefromid).then((eData) => {
            //console.log(eData);
            key = "inputPartNo";
            $("#EditInputPartNo").val(eData[key]);

            key = "inputWeight";
            $("#EditInputWeight").val(eData[key]);

            key = "mfDescription";
            $("#EditMFDescription").val(eData[key]);
            key = "quantityPerInput";
            $("#EditQuantityPerInput").val(eData[key]);

            key = "yieldNotes";
            $("#EditYieldNotes").val(eData[key]);
            masterPartNo = eData[key];
            key = "preferedRawMaterial";
            $("#EditPreferedRawMaterial").val(eData[key]);
            
            key = "mpPartMadeFrom";
            $("#EditPartMadeFrom").val(eData[key]);
            key = "scrapGenerated";
            $("#EditScrapgenerated").val(eData[key]);
            key = "mpMakeFromId";
            $("#EditMPMakeFromId").val(eData[key]);

            key = "mpPartId";
            $("#EditMPPartId").val(eData[key]);
            key = "manufPartId";
            $("#EditManufPartId").val(eData[key]);

            key = "preferedRawMaterial";
            doument.getElementById("EditPreferedRawMaterial").checked = false;
            if (eData[key] == 1) {
                doument.getElementById("EditPreferedRawMaterial").checked = true;
            }
            
        }).catch((error) => {
        });
     //   alert(makefromid);
    });

    $("#checkboxFinalPart").change(function () {
        if ($("#checkboxFinalPart").prop("checked")) {
            $("#FinalPartNosoldtoCustomer").val('1');
        } else {
            $("#FinalPartNosoldtoCustomer").val('0');
        }
        checkboxFinalPart();
    });
    checkboxFinalPart();
    //TCheckBoc();
    $("#UomEdit").click(function (event) {
        var selectedValue = $("#UOMId").val();  // Get the value of the selected option
        var selectedText = $("#UOMId").find("option:selected").text();
        document.forms["frmAddUOM"]["Name"].value = selectedText;
        document.forms["frmAddUOM"]["UOMId"].value = selectedValue;

    });
    $('#uom').on('hidden.bs.modal', function (event) {
        $("#frmAddUOM")[0].reset();  // Clears all input fields
    });

    $('#doc-item').on('hidden.bs.modal', function (event) {
        var InfoComments = document.getElementById('InfoComments');
        InfoComments.style.border = '';
        var newNamevalidate = document.getElementById('fileNameDisplay');
        newNamevalidate.style.border = '';
        $("#doclistidFile").val(0);
        $("#fileNameDisplay").val('');
        $("#InfoComments").val('');
        $("#DocTypeName").val('');
        $("#FileExtnName").val('');
        $("#docTypeIdFile").val(0);
        var fileInput = document.getElementById("fileUploadInput");
        fileInput.value = "";

    });
    $('#doc-item').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var filename = relatedTarget.data("filename");
        var doctypename = relatedTarget.data("doctypename");
        var comments = relatedTarget.data("comments");
        var doclistid = relatedTarget.data("doclistid");
        var documenttypeid = relatedTarget.data("documenttypeid");
        var upload = relatedTarget.data("upload");
        var fileextnname = relatedTarget.data("fileextnname");
        var deletiondate = relatedTarget.data("deletiondate");
        if (doclistid == 0 && upload == 1) {
            $("#doclistidFile").val(0);
            $("#fileNameDisplay").val('');
            $("#InfoComments").val(comments);
            $("#DocTypeName").val(doctypename);
            $("#FileExtnName").val(fileextnname);
            $("#docTypeIdFile").val(documenttypeid);
            var dateOnly = deletiondate.split('T')[0];

            // Set the value of the input field with the formatted date
            $("#deletiondate").val(dateOnly);
        } else if (doclistid > 0 && upload == 2) {
            $("#doclistidFile").val(doclistid);
            $("#fileNameDisplay").val(filename);
            $("#InfoComments").val(comments);
            $("#DocTypeName").val(doctypename);
            $("#FileExtnName").val(fileextnname);
            $("#docTypeIdFile").val(documenttypeid);
            var dateOnly = deletiondate.split('T')[0];

            // Set the value of the input field with the formatted date
            $("#deletiondate").val(dateOnly);
        }
        else {
            $("#DocTypeName").val("Others");
            $("#docTypeIdFile").val(3);
            $("#doclistidFile").val(0);
            $("#FileExtnName").val("Any Extn");
            var today = new Date();
            today.setDate(today.getDate() + 10);  
            var deletionDate = today.toISOString().split('T')[0];
            $("#deletiondate").val(deletionDate);

        }

    });

    document.getElementById("UploadFileSave").addEventListener("click", function (event) {
        event.preventDefault();  // Prevent the default form submission
        var partids = parseInt($("#PartId").val());
        if (partids <= 0) {
            alert("Please Save Basic Information.");
            return false;
        }
        var com = document.getElementById("InfoComments").value;
        if (com.trim() === "" || com.length === 1) {
            var newNamevalidate = document.getElementById('InfoComments');
            newNamevalidate.style.border = '2px solid red'; // Set border to red for invalid input
            return false; // Prevent form submission or further processing
        } else {
            var newNamevalidate = document.getElementById('InfoComments');
            newNamevalidate.style.border = ''; // Clear the border for valid input
        }
        // Create a FormData object to hold file and form data
        var formData = new FormData();

        // Add the file to the FormData object
        var fileInput = document.getElementById("fileUploadInput");
        var file = fileInput.files[0];
        var allowedExtensions = $("#FileExtnName").val().split(',').map(function (ext) {
            return ext.trim().toLowerCase(); // Create an array of allowed extensions (e.g., ['.pdf'])
        });
        if (!isNaN(file) || file) {
            var fileName = file.name;
            var fileExtension = '.' + fileName.split('.').pop().toLowerCase(); // Get the file extension and add a dot (e.g., '.pdf')

            // Validate that the file's extension is in the allowedExtensions array
            if (allowedExtensions.includes(fileExtension) || allowedExtensions.includes("any extn")) {
                formData.append("uploadedFile", file);
            } else {
                var newNamevalidate = document.getElementById('fileNameDisplay');
                newNamevalidate.style.border = '2px solid red';
                $("#fileNameDisplay").val("Invalid file type. Please upload a valid file."); // Show error message for invalid file type
                return false;
            }
        } else {
            var newNamevalidate = document.getElementById('fileNameDisplay');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }

        // Add the other form inputs to the FormData object
        formData.append("DocumentTypeName", document.getElementById("DocTypeName").value);
        formData.append("FileExtnName", document.getElementById("FileExtnName").value);
        formData.append("FileName", document.getElementById("fileNameDisplay").value);
        formData.append("StorageLocation", "/Active");
        formData.append("Comments", document.getElementById("InfoComments").value);
        formData.append("DocListId", parseInt(document.getElementById("doclistidFile").value));
        formData.append("DocumentTypeId", parseInt(document.getElementById("docTypeIdFile").value));
        formData.append("PartId", parseInt(document.getElementById("PartId").value));
        formData.append("UploadUiId", parseInt(1));
        //var today = new Date();
        //today.setDate(today.getDate() + 10);  // Add 10 days to the current date

        //// Format the date as YYYY-MM-DD (you can modify this to your required format)
        //var deletionDate = today.toISOString().split('T')[0];

        // Append the DeletionDate to the FormData
        var deletionDateValue = document.getElementById("deletiondate").value;
        var deletionDate = new Date(deletionDateValue);

        var formattedDate = deletionDate.toISOString().split('T')[0];

        formData.append("DeletionDate", formattedDate);
        // Post the form data to the server
        if (archive == 0) {
            $.ajax({
                type: "POST",
                url: "/masters/PostDocList",
                data: formData,
                contentType: false,  // Important: Let the browser set the Content-Type header automatically
                processData: false,  // Important: Don't process the form data, let it be as FormData
                success: function (response) {
                    // Handle the success response here
                    //alert("File uploaded and data saved successfully!");
                    $("#doc-item").modal("hide");
                    loadDocUploadList();
                    archive = 0;
                },
                error: function (xhr, status, error) {
                    // Handle any errors here
                    console.error("An error occurred:", error);
                    alert("There was an error saving the file and data.");
                }
            });
        } else {
            $.ajax({
                type: "POST",
                url: "/masters/MoveFileToArchive",
                data: formData,
                contentType: false,  // Important: Let the browser set the Content-Type header automatically
                processData: false,  // Important: Don't process the form data, let it be as FormData
                success: function (response) {
                    // Handle the success response here
                    //alert("File uploaded and data saved successfully!");
                    $("#doc-item").modal("hide");
                    loadDocUploadList();
                    archive = 0;
                },
                error: function (xhr, status, error) {
                    // Handle any errors here
                    console.error("An error occurred:", error);
                    alert("There was an error saving the file and data.");
                }
            });
        }
    });

    $('#status-info').on('show.bs.modal', function (event) {
        var currentStatus = $("#Status").val();
        var currentrReason = $("#StatusChangeReason").val();
        $("#CurrentStatus").val(currentStatus);
        $("#statusResasonopup").val(currentrReason);
    });
    $("#BtnstatusSave").click(function (event) {
        var streason = $("#statusResasonopup").val();
        var statusPopup = $("#statusPopup").val();
        $("#StatusChangeReason").val(streason);
        $("#Status").val(statusPopup);
        if (statusPopup == "Inactive") {
            if (streason.length == 0) {
                var newNamevalidate = document.getElementById('statusResasonopup');
                newNamevalidate.style.border = '2px solid red';
                //return false;
            } else {
                var newNamevalidate = document.getElementById('statusResasonopup');
                newNamevalidate.style.border = '';
                $("#status-info").modal("hide");
            }
        } else {
            $("#status-info").modal("hide");
        }
    });
    $('#setPreferredPopUp').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var makefromid = relatedTarget.data("makefromid");
        var inputpartdesc = relatedTarget.data("inputpartdesc");
        var partNumber = $("#lblPartNumber").text();
        var lblPartDescription = $("#lblPartDescription").text();
        $("#SetPreferedMKId").val(makefromid);
        $("#InputDesc").text(inputpartdesc);
        $("#MainPartSpan").text(partNumber);
        $("#MainDescSpan").text(lblPartDescription);

    });
    $('#setPreferredPopUp').on('hidden.bs.modal', function (event) {
        $("#SetPreferedMKId").val(0);
        $("#SetPreferedChkBox").prop("checked", false);
    });
    $("#btnSetPreferedSave").click(function (event) {
        var mkID = parseInt($("#SetPreferedMKId").val());
        var prefred = false;
        if ($("#SetPreferedChkBox").prop("checked") == true) {
            prefred = true;
        }
        var rowData = {
            mpMakeFromId: mkID,
            preferedRawMaterial: prefred
        };
        api.post("/masters/MPPreferredMK", rowData).then((data) => {
            var partId = data.manufPartId;
            reloadMakeFroms(partId);
            event.preventDefault();
            //$("#TabHeadMakefrom").click();
            document.getElementById("btnPreferredInputClose").click();
        }).catch((error) => {
            AppUtil.HandleError("FormEditMakeFrom", error);
        });
    });
    //$("#inputpartCloseBtn").click(function (event) {
    //    var modalElement = document.getElementById("inputpart");
    //    var modal = bootstrap.Modal.getInstance(modalElement);
    //    modal.hide();
    //});
    document.addEventListener("DOMContentLoaded", function () {
    var myModal = new bootstrap.Modal(document.getElementById('inputpart'));
    document.getElementById("inputpartCloseBtn").addEventListener("click", function () {
        myModal.hide();
    });
});

    $("#AddInputPart").click(function (event) {
        const selectedValue = document.getElementById('MPPartMadeFrom').value;
        //event.preventDefault();
        if (selectedValue === '0') { // value 1
            var newNamevalidate = document.getElementById('MPPartMadeFrom');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
            var newNamevalidate = document.getElementById('MPPartMadeFrom');
            newNamevalidate.style.border = '';
            var myModal = new bootstrap.Modal(document.getElementById('inputpart'));
            myModal.show();
            $("#MPMKMadefrom").val(newNamevalidate.value);
    });
    const selectElement = document.querySelector('select[name="MPPartMadeFrom"]');
    selectElement.addEventListener('change', handleRadioClick);
    //debugger;
    var manufacturedPartType = modelObj.manufacturedPartType;
    $("#TabHeadBalloon").hide();
   // console.log(manufacturedPartType);
    if (manufacturedPartType == 1) {
        $("#TabHeadMakefrom").show();
        $("#TabHeadBOM").hide();
    }
    else {
        $("#TabHeadMakefrom").hide();
        $("#TabHeadBOM").show();
    }


    $("#CompanyId").on('change',function () {
        setCo();
    });

    
    //$("#btnAddMPMakeFrom").hide();
    // Document is ready
    $('#ManufacturedPartType').change(function () {
        var ShowMessage = "Please Save The Detail(s) or All The Data Will Erase";
       // $("#ManufacturedPartType").val(this.value);

        if (ManufPartFormUtil.ValidateMainTabForRadio()) {
           // alert("calling clearMainTab...")
            ManufPartFormUtil.ConfirmDialog(this.value, ShowMessage)
        }
        else {
            if (this.value == 1) {
                $("#MasterPartType").val("0");
                MPBOMUtil.ClearBOMTab();
                    $("#TabHeadBOM").hide();
                    $("#TabHeadMakefrom").show();
                 //   $('.nav-pills a[href="#tab-2"]').tab('show');
                }
            else if (this.value == 2) {
                $("#MasterPartType").val("1");
                MPRawMaterialUtil.ClearMakefromTab();
                    $("#TabHeadMakefrom").hide();
                    $("#TabHeadBOM").show();
                   // $('.nav-pills a[href="#tab-3"]').tab('show');
                }
        }
    });

    $("#CompanyNamebak").change(function () {
        if ($("#ManufacturedPartType").val() == 1) {
            $('#lblCompanyName').text($(this).val());
        }
        else {
            $('#lblBOMCompanyName').text($(this).val());
        }
    });
    $("#PartNumberbak").change(function () {
        if ($("#ManufacturedPartType").val() == 1) {
            $("#InputPartNo").val($(this).val());
            $("#lblPartNumber").text($(this).val());
        }
        else {
            $("#BOMPartNo").val($(this).val());
            $("#lblBOMPartNumber").text($(this).val());
        }
    });

   
    document.getElementById('addUomPopup').addEventListener('click', function () {
        var myModal = new bootstrap.Modal(document.getElementById('dialog-AddUOM'));
        myModal.show();
    });

    $("#PartDescriptionbak").change(function () {
        if ($("#ManufacturedPartType").val() == 1) {
            $("#MFDescription").val($(this).val());
            $("#lblPartDescription").text($(this).val());
        }
        else {
            $("#BOMPartDesc").val($(this).val());
            $("#lblBOMPartDescription").text($(this).val());
        }
    });

    /*if ($("#ManufacturedPartNoDetailId").val() != "0") {
        return true;
    }*/
   
    $("#Status").change(function () {
        if ($(this).val() === "Inactive") {
            $("#StatusChangeReason").attr("required", "required");
            $("#StatusChangeReason").attr("data-val-required", "StatusChangeReason is required when Status is Inactive.");
        } else {
            $("#StatusChangeReason").removeAttr("required");
            $("#StatusChangeReason").removeAttr("data-val-required");
        }
    });
   

    $("#btnManufPartDetailSubmit").click(function (event) {
        if (!modelObj.Edit) {
            //alert($("#ManufacturedPartNoDetailId").val());
            if ($("#ManufacturedPartNoDetailId").val() != "0") {
                event.preventDefault();
                alert("Älready saved...");
                return;
            }
        }
        if (ManufPartFormUtil.ValidateManufPartDetails(2)) {
            if ($("#ManufPartForm").valid()) {
                //////debugger;
                var formData = AppUtil.GetFormData("ManufPartForm");
                api.post("/Masters/ManufacturedPartNoDetail", formData).then((data) => {
                    ManufPartFormUtil.UpdateFormIDs(data);
                }).catch((error) => {
                    AppUtil.HandleError("ManufPartForm", error);
                });
            }
        }
        event.preventDefault();
    });

    $("#AddDeptClose").click(function (event) {
        window.location.reload();
    });
    $("#btnAddMPMakeFrom").click(function (event) {
        if (ManufPartFormUtil.ValidateMPMakeFrom()) {
            if ($("#MPRawMaterial").valid()) {

                var keyval = AppUtil.GetFormDataNew("MPRawMaterial");
                var formData = AppUtil.GetFormData("MPRawMaterial");
                var tablebody = $("#tbl-MakeFromRM tbody");
                var inputwieght = parseInt($("#InputWeight").val());
                var FinishedWeight = $("#FinishedWeight").val();
                var QuantityPerInput = $("#QuantityPerInput").val();
                totalinputweight = FinishedWeight * QuantityPerInput;
                if (totalinputweight <= inputwieght) {
                    $("#Scrap-Error").text("Check Scrap Weight ...");
                    return false;
                }
                api.post("/masters/mpmakefrom", formData).then((data) => {
                    //   //debugger;
                    data['deleted'] = false;
                    makeFroms.push(data);
                    MPRawMaterialUtil.UpdateFormIDs(data);
                    $(tablebody).append(AppUtil.ProcessTemplateData("MakeFrom-template", data));
                    //MPRawMaterialUtil.UpdateMakeFromTableNew(keyval);
                    var coName = $("#CompanyName").val();
                    var partDesc = $("#MFDescription").val();
                    var partNo = $("#InputPartNo").val();
                    var manufPartId = $("#ManufPartId").val();
                    var mpPartId = $("#MPPartId").val();
                    MPRawMaterialUtil.ClearMakefromTab();
                    //document.getElementById("MPRawMaterial").reset();
                    $("#InputPartNo").val(partNo);
                    $("#MFDescription").val(partDesc);
                    $("#lblPartDescription").text(partDesc);
                    $("#ManufPartId").val(manufPartId);
                    $("#MPPartId").val(mpPartId);
                    window.location.reload();
                    $("#Scrap-Error").text("");
                }).catch((error) => {
                    AppUtil.HandleError("MPRawMaterial", error);
                });
            }
        }
    });
    $("#btnAddRawMeterial").click(function (event) {
            if ($("#PartNumber").val() != "") {
                $("#InputWeight").val(null);
                $("#Scrapgenerated").val("");
                $("#QuantityperInput").val("");
                $("#YieldNotes").val("");
                $('#PreferedRawMaterial').prop('checked', false);
            }    
    });

    $("#AddBOMBACK").click(function (event) {
        if (ManufPartFormUtil.ValidateBOM()) {
            if ($("#ManufPartForm").valid()) {
                var formData = AppUtil.GetFormData("ManufPartForm");
                ManufPartFormUtil.UpdateMPMakeFromTable(formData);
            }
            event.preventDefault();
        }
    });
    return;
});

$("#OSupplierId").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#OwnRMTable tbody tr").filter(function () {
        $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
    });
});

$("#OSupplierPartNo").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#OwnRMTable tbody tr").filter(function () {
        $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
    });
});

$("#ORawMaterialMadeSubType").change(function () {
    debugger;
    var value = parseFloat($("#ORawMaterialMadeSubType option:selected").val());
    $("#OwnRMTable tbody tr").filter(function () {
        $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
    });
});

$("#ORawMaterialTypeId").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#OwnRMTable tbody tr").filter(function () {
        $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
    });
});

$("#OBaseRawMaterialId").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#OwnRMTable tbody tr").filter(function () {
        $(this).toggle($(this.children[7]).text().toLowerCase().indexOf(value) > -1)
    });
});

$("#CRawMaterialMadeSubType").change(function () {
    debugger;
    var value = parseFloat($("#CRawMaterialMadeSubType option:selected").val());
    $("#CustRMTable tbody tr").filter(function () {
        $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
    });
});

$("#CRawMaterialTypeId").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#CustRMTable tbody tr").filter(function () {
        $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
    });
});

$("#CBaseRawMaterialId").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#CustRMTable tbody tr").filter(function () {
        $(this).toggle($(this.children[7]).text().toLowerCase().indexOf(value) > -1)
    });
});

function copyBOFData() {
    var data = bofParts;
    var radiochkd = $('input[name=radiopartsbof]:checked');
    var selval = radiochkd.val();
    //alert("selVal "+selval);
    let i = 0;
    var fndObj = false;
    for (i = 0; i < data.length; i++) {
        //console.log("************");
        if (i == selval) {
            //console.log(i);
            //console.log(data[i]);
            $('#BOMPartId').val(data[i]['partId']);
            $('#BOMPartNo').val(data[i]['partNo']);
            $('#BOMPartDesc').val(data[i]['description']);
            fndObj = true;
            break;
        }
    }
    document.getElementById("btn-close-ExistingPartsBOF").click();
}


function AddToBOM() {
    if (ManufPartFormUtil.ValidateBOM()) {
        if ($("#MPBOM").valid()) {
            //////debugger;
            var keyval = AppUtil.GetFormDataNew("MPBOM");
            var formData = AppUtil.GetFormData("MPBOM");
            var tablebody = $("#tbl-BOM tbody");
            var templateElement = $("#BOM-template").html();
            api.post("/masters/mpbom", formData).then((data) => {
                ////debugger;
                data['deleted'] = false;
                boms.push(data);
                MPBOMUtil.UpdateFormIDs(data);
                //MPBOMUtil.UpdateBOMTableNew(keyval);
                $(tablebody).append(AppUtil.ProcessTemplateData("BOM-template", data));
                MPBOMUtil.ClearBOMTab();
                //$("#BOMManufPartId").val($("#ManufacturedPartNoDetailId").val());
                //document.getElementById("MPBOM").reset();
              //  ManufPartFormUtil.ClearBOMTabAfterAddBOM();
                //                ManufPartFormUtil.UpdateMPMakeFromTable(data.inputPartNo, data.mpMakeFromId);
                preventDefault();
            }).catch((error) => {
                AppUtil.HandleError("MPBOM", error);
            });
        }
    }
}
//document.addEventListener("DOMContentLoaded", function () {
//    // Check if the FinalPartNosoldtoCustomer hidden field has a value of "true"
//    var isFinalPartChecked = document.getElementById("FinalPartNosoldtoCustomer").value === "true";
//    document.getElementById("checkboxFinalPart").checked = isFinalPartChecked;
//});
function checkboxFinalPart() {
    var getv = $("#FinalPartNosoldtoCustomer").val();
    if ($("#checkboxFinalPart").prop("checked") || getv == "1") {
        $("#checkboxFinalPart").prop("checked", true);
        $("#FinalPartNosoldtoCustomer").val('1');
        $("#ReorderFields").show(); // Hide the price div if checkbox is checked
        $("#priceDiv").show(); // Hide the price div if checkbox is checked
    } else {
        $("#FinalPartNosoldtoCustomer").val('0');
        $("#ReorderFields").hide(); // Show the price div if checkbox is unchecked
        $("#priceDiv").hide(); // Show the price div if checkbox is unchecked
    }

}

function EditMakeFrom(event) {
    var formData = AppUtil.GetFormData("FormEditMakeFrom");
    var partId = $("#EditManufPartId").val();
    api.post("/masters/mpmakefrom", formData).then((data) => {
        //debugger;
        //var partId = data['manufPartId'];
        //reloadMakeFroms(partId);
        //modifyMakeFromListForEdit(data);
        reloadMakeFroms(partId);
        event.preventDefault();
        document.getElementById("btnEditMakeFromClose").click();
    }).catch((error) => {
        AppUtil.HandleError("FormEditMakeFrom", error);
    });
}
function DeleteMakdeFrom(event) {
    var formData = AppUtil.GetFormData("FormDelMakeFrom");
    var partId = $("#DelManufPartId").val();
    
    //debugger;
    api.post("/masters/remmakefrom", formData).then((data) => {
        //modifyMakeFromListForDel(data);
        reloadMakeFroms(partId);
        document.getElementById("btnDelMakeFromClose").click();
        if (dataWritten && ownRMSelected) {
            disableOtherCustRadios();
        }
        else {
            enableOtherCustRadios();
        }
        event.preventDefault();
    }).catch((error) => {
        AppUtil.HandleError("FormEditBOM", error);
    });
}

function EditBOM(event){
    var formData = AppUtil.GetFormData("FormEditBOM");
    var partId = $("#EditBOMManufPartId").val();
    api.post("/masters/mpbom", formData).then((data) => {
        //var partNo = data['bomPartNo'];
        //console.log("====1")
        //console.log(data);
        //console.log("====2")
        //modifyBOMListForEdit(data);
        reloadBOMs(partId);
        event.preventDefault();
        document.getElementById("btnEditBOMClose").click();
    }).catch((error) => {
        AppUtil.HandleError("FormEditBOM", error);
    });
}

function DeleteBOM(event){
    var formData = AppUtil.GetFormData("FormDeleteBOM");
    var partId = $("#DelBOMManufPartId").val();
    api.post("/masters/rembom", formData).then((data) => {
        //console.log("====3")
        //console.log(data);
        //console.log("====4")
        //modifyBOMListForDel(data);
        reloadBOMs(partId);
        event.preventDefault();
        document.getElementById("btnDelBOMClose").click();
    }).catch((error) => {
        AppUtil.HandleError("FormEditBOM", error);
    });
}

function reloadMakeFroms(partId) {
    var tablebody = $("#tbl-MakeFromRM tbody");
    tablebody.html("");
    let rData = {};
    let i = 0;
    dataWritten = false;
    api.get("/masters/mpmakefromlist?partId=" + partId).then((rData) => {
        for (i = 0; i < rData.length; i++) {
            let rowData = rData[i];
            rowData.checked = rowData.preferedRawMaterial ? 'checked' : '';
            $(tablebody).append(AppUtil.ProcessTemplateData("MakeFrom-template", rData[i]));
            //let rowTemplate = $('#MakeFrom-template').html()
            //    .replace('{checked}', isChecked);

            dataWritten = true;
        }
    }).catch((error) => {
    });
    /*for (i = 0; i < makeFroms.length; i++) {
        if (makeFroms[i]['deleted']) {
            continue;
        }
        console.log(makeFroms[i]);
        dataWritten = true;
        $(tablebody).append(AppUtil.ProcessTemplateData("MakeFrom-template", makeFroms[i]));
    }*/
}

function reloadBOMs(partId) {
    var tablebody = $("#tbl-BOM tbody");
    tablebody.html("");
    let rData = {};
    let i = 0;
    api.get("/masters/boms?partId=" + partId).then((rData) => {
        for (i = 0; i < rData.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("BOM-template", rData[i]));
        }
    }).catch((error) => {
    });
    /*for (i = 0; i < boms.length; i++) {
        if (boms[i]['deleted']) {
            continue;
        }
        console.log(boms[i]);
        $(tablebody).append(AppUtil.ProcessTemplateData("BOM-template", boms[i]));
    }*/
}
let modOpN = "";
function modifyMakeFromListForEdit(newData) {
    modOpN = "Edit";
    modifyMakeFromList(newData);
}
function modifyMakeFromListForDel(newData) {
    modOpN = "Delete";
    modifyMakeFromList(newData);
}
function modifyMakeFromList(newData) {
    let foundObj = false;
    let i = 0;
    let newObj = {};
    let idxToDel = -1;
    for (i = 0; i < makeFroms.length; i++) {
        if (newData['mpMakeFromId'] == makeFroms[i]['mpMakeFromId']) {
            idxToDel = i;
            foundObj = true;
            break;
        }
    }
    if (foundObj) {
        if (idxToDel > -1) {
            makeFroms.splice(idxToDel, 1);
            //console.log("Spliced one element " + idxToDel)
        }
        if (modOpN == "Edit") {
            newObj = JSON.parse(JSON.stringify(newData));
            newObj['deleted'] = false;
            //console.log("Adding new Obj makefroms..");
            //console.log(newObj);
            makeFroms.push(newObj);
        }
    }
}

function modifyBOMListForEdit(newData) {
    modOpN = "Edit";
    modifyBOMList(newData);
}
function modifyBOMListForDel(newData) {
    modOpN = "Delete";
    modifyBOMList(newData);
}
function modifyBOMList(newData) {
    let foundObj = false;
    let i = 0;
    let newObj = {};
    let idxToDel = -1;
    for (i = 0; i < boms.length; i++) {
        if (newData['mpbomId'] == boms[i]['mpbomId']) {
            idxToDel = i;
            foundObj = true;
            break;
        }
    }
    if (foundObj) {
        if (idxToDel > -1) {
            boms.splice(idxToDel, 1);
            //console.log("Spliced one element " + idxToDel)
        }
        if (modOpN == "Edit") {
            newObj = JSON.parse(JSON.stringify(newData));
            newObj['deleted'] = false;
            //console.log("Adding new Obj..");
            //console.log(newObj);
            boms.push(newObj);
        }
    }
}

function AddAnotherRM() {
   
}

//const box = document.getElementById('box');

function handleRadioClick() {
    const searchbtn = $("#searchbtn");
    searchbtn.empty();

    const selectedValue = document.getElementById('MPPartMadeFrom').value;

    if (selectedValue === '1') { // value 1
        var templateElement = $("#csrmsearch").html();
        searchbtn.append(templateElement);
    }
    if (selectedValue === '2') { // value 2
        var templateElement = $("#owrmsearch").html();
        searchbtn.append(templateElement);
    }
    if (selectedValue === '3') { // value 3
        var templateElement = $("#otmpsearch").html();
        searchbtn.append(templateElement);
    }
}

function downloadNLoadExistingParts() {
    var ManufPartType = $("#ManufacturedPartType").val();
    var coName = $('#CompanyName').val();
    var coId = $("#CompanyId option:selected").val();

    $('#epco').val("");
    $('#eppn').val("");
    $('#eppd').val("");

    epco = "";
    eppn = "";
    eppd = "";
    existingpartdata = "";

    api.get("/masters/ManufacturedPartNoDetailList?ManufPartType=" + ManufPartType + "&CompanyName=" + coId).then((data) => {
        existingpartdata = data;
        loadExistingParts(existingpartdata)//,"child");
        //console.log(existingpartdata);
    }).catch((error) => {
        //console.log(error);
    });

}

function loadExistingParts(data) {
    var compSelect = $('#tbl-existingparts tbody');
    if (compSelect) {
        compSelect.empty();
        var strVal = $("#CompanyName").val();
        $('#epco').val(strVal);
    }
    var coId = $("#CompanyId option:selected").val();


    for (i = 0; i < data.length; i++) {
        var chld = "Child";
        if (data[i].manufacturedPartType == "2") //{ 
            chld = "Assembly";
        //    if(soption == "child")
        //      continue;
        //}
        //else
        //{
        //  if (soption == "assembly")
        //    continue;
        //}*
        if (epco.length > 0) {
            if (!(data[i].companyId.startsWith(coId))) {
                continue;
            }
        }
        if (eppn.length > 0) {
            if (!(data[i].partNo.startsWith(eppn))) {
                continue;
            }
        }
        if (eppd.length > 0) {
            if (!(data[i].partDescription.startsWith(eppd))) {
                continue;
            }
        }
        //todo:soption is a search string
        var val = "<tr><td><input type='radio' name='caselect' value='" + i + "'></td>" +
            "<td>" + data[i].partNo + "</td>" +
            "<td>" + data[i].partDescription + "</td>" +
            "<td>" + chld + "</td>" +
            "<td style='visibility:collapse'>" + data[i].manufacturedPartNoDetailId + "</td>";
        compSelect.append(val);
    }

}

function disableOtherCustRadios() {
    document.getElementById("otmp").disabled = true;
    document.getElementById("csrm").disabled = true;
}
function enableOtherCustRadios() {
    document.getElementById("otmp").disabled = false;
    document.getElementById("csrm").disabled = false;
}

function copyCustData() {
    var data = custRM;
    var radiochkd = $('input[name=CSRM]:checked');
    var selval = radiochkd.val();

    $('#InputPartNo').val(data[selval].partNo);
    $('#MFDescription').val(data[selval].partNo+" / "+data[selval].partDescription);
    $('#MPPartId').val(data[selval].partId);
    if (data[selval].multiplePartsMadeFrom1InputRM == 'N') {
        $("#InputWeight").val(data[selval].rawMaterialWeight);
        $("#InputWeight").prop('readonly', true);
    } else {
        $("#InputWeight").val(data[selval].rawMaterialWeight);
        $("#InputWeight").prop('readonly', false);
    }
    document.getElementById("btn-close-CustRM").click();
    ownRMSelected = false;
}
function copyOwnData() {
    var data = ownRM;
    var radiochkd = $('input[name=OSRM]:checked');
    var selval = radiochkd.val();
    $('#InputPartNo').val(data[selval].partNo);
    $('#MFDescription').val(data[selval].partDescription);
    $('#MPPartId').val(data[selval].partId);
    if (data[selval].multiplePartsMadeFrom1InputRM == 'N') {
        $("#InputWeight").val(data[selval].rawMaterialWeight);
        $("#InputWeight").prop('readonly', true);
    } else {
        $("#InputWeight").val(data[selval].rawMaterialWeight);
        $("#InputWeight").prop('readonly', false);
    }
    document.getElementById("btn-close-RMSelect").click();
    ownRMSelected = true;
}


function copyData() {

    if (existingpartdata.length == 0) {
        alert("Please load parts again...");
        return;
    }
    else {
        //alert("calling copyData");
    }
    var data = existingpartdata;
    var radiochkd = $('input[name=caselect]:checked');
    var selval = radiochkd.val();
    if (CURRENT_TAB == "TabMPMain") {
        $('#PartId').val("0");
        $('#PartNo').val("");
        $('#ManufacturedPartNoDetailId').val("0");
        $('#RevNo').val(data[selval].revNo);
        $('#PartDescription').val(data[selval].partDescription);
        $('#StatusChangeReason').val(data[selval].statusChangeReason);
        $('#FinishedWeight').val(data[selval].finishedWeight);
        $('#RevDate').val(data[selval].revDate);
    }
    else if (CURRENT_TAB == "TabHeadMakefrom") {
        $('#InputPartNo').val(data[selval].partNo);
        $('#MPPartId').val(data[selval].partId);
        $('#MFDescription').val(data[selval].partDescription);
    }
    else {
        $('#BOMPartNo').val(data[selval].partNo);
        $('#MPPartId').val(data[selval].partId);
        $('#BOMPartDesc').val(data[selval].partDescription);
    }
    document.getElementById("btn-close-ExistingParts").click();
    ownRMSelected = false;
}

function doChldAssyRadioJob() {
    //var radiochkd = $('input[name=grchldassy]:checked');
    //var selval = radiochkd.val();
    loadExistingParts(existingpartdata)//, selval);
}

$(document).on('click', '#btnAddUOMClose', function () {
    var dlgComp = $("#dialog-AddUOM");
    document.getElementById("frmAddUOM").reset();
    dlgComp.dialog('close');
});

$(document).on('click', '#btnEditUOMClose', function () {
    var dlgComp = $("#dialog-EditUOM");
    document.getElementById("frmEditUOM").reset();
    dlgComp.dialog('close');
});


$(document).on('click', '#SaveUOM', function () {
    var valid = $("#frmAddUOM").valid();
    var name = $("#Name").val();
    if (name.length == 0) {
        var newNamevalidate = document.getElementById('Name');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('Name');
        newNamevalidate.style.border = '';
    }
    var formData = AppUtil.GetFormData("frmAddUOM");
    if (valid) {

        api.getbulk("/masters/CheckUOM?uomName=" + name).then((data) => {
            if (!data) {
                api.post("/masters/adduom", formData).then((data) => {
                    var newopt = {
                        id: data.uomId,
                        text: data.name
                    };
                    var newOption = new Option(newopt.text, newopt.id, true, true);
                    $('#UOMId').append(newOption).trigger('change');
                    //  $('#UOMId').val(newCo);
                    //loadUOMs("UOMId");
                    document.getElementById("btn_adduom_close").click();
                }).catch((error) => {
                });
            } else {
                var newNamevalidate = document.getElementById('Name');
                newNamevalidate.style.border = '2px solid red';
            }
        }).catch((error) => {
        });
    }
});

$(document).on('click', '#btnEditUOMSubmit', function () {
    var valid = $("#frmEditUOM").valid();
    if (valid) {
        alert("Todo: Edit UOM...");
    }
});


function setUOMId() {
    var strVal = $("#UOMs").children(":selected").attr("id");
    if (strVal == null || strVal == "")
        strVal = "1";
    $('#UOMId').val(strVal);
    //alert(strVal + " UOMId");
}

function setCo() {
    //  var strVal = $("#CompanyId").text();
    $("#CompanyName").val($("#CompanyId option:selected").text());
    let strVal = $("#CompanyName").val();
    //     alert("Setting val" + strVal);
    if (strVal != "") {
        $('#createfep').prop('disabled', false);
    }
    else {
        $('#createfep').prop('disabled', true);
    }
    /*if ($("#ManufacturedPartType").val() == 1) {
        $('#lblCompanyName').text($(this).val());
    }
    else {
        $('#lblBOMCompanyName').text($(this).val());
    }*/
}

function DeleteDocList(element) {
    var relatedTarget = $(element);
    var doclistid = relatedTarget.data("doclistid");
    if (doclistid != 0) {
        var confrimval = confirm("Do You Want This Document.");
        if (confrimval) {
            api.get("/masters/DeleteDocListAndFile?doclistid=" + doclistid).then((data) => {
                //console.log(data);
                loadDocUploadList();
            }).catch((error) => {
                //console.log(error);
            });
        }
    } else {
        alert("This Document Type Do Not Have Document.");
    }
}
function viewFile(element) {
    var relatedTarget = $(element);
    var file = relatedTarget.data("filename");
    var doctypename = relatedTarget.data("doctypename");
    var customername = relatedTarget.data("customername");
    var partno = relatedTarget.data("partno");
    var partdesc = relatedTarget.data("partdesc");
    var routingname = relatedTarget.data("routingname");
    var oprno = relatedTarget.data("oprno");
    var retdate = relatedTarget.data("retdate");
    if (file == null) {
        $('#viewDoc').modal('hide');
        return false;
    }
    if (doctypename == "Other") {
        $('#viewDoc').modal('hide');
        return false;
    } else {
        $('#viewDoc').modal('show');
        $("#DocTypenameText").text(doctypename);
        $("#PartNoText").text(partno);
        $("#PartDescText").text(partdesc);
        $("#CustomerText").text(customername);
        $("#RetentionDateText").text(retdate);
        $("#RoutingNameText").text(routingname);
        $("#OprNoText").text(oprno);


        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/masters/ViewFile?fileName=' + file, true);
        xhr.responseType = 'arraybuffer';
        xhr.onload = function (e) {
            if (this.status == 200) {
                var blob = new Blob([this.response], { type: "application/pdf" });

                const objectElement = document.getElementById('fileViewer');
                const url = URL.createObjectURL(blob);
                objectElement.src = url;
                //objectElement.width = '1000px';
                //objectElement.height = '1000px';
                //objectElement.type = 'text/plain';
                //var link = document.createElement('a');
                //link.href = window.URL.createObjectURL(blob);
                //link.download = "Report_" + new Date() + ".pdf";
                //link.click();
            }
        };
        xhr.send();
    }
}

function downloadFile(element) {
    var relatedTarget = $(element);
    var file = relatedTarget.data("filename");

    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/masters/ViewFile?fileName=' + file, true);
    xhr.responseType = 'arraybuffer';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: "application/pdf" });

            //const objectElement = document.getElementById('fileViewer');
            //const url = URL.createObjectURL(blob);
            //objectElement.src = url;
            //objectElement.width = '1000px';
            //objectElement.height = '1000px';
            //objectElement.type = 'text/plain';
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = file;
            link.click();
        }
    };
    xhr.send();
}
function loadDocUploadList() {

    var content = parseInt($("#ManufacturedPartType").val());
    var partid = $("#PartId").val();
    if (isNaN(partid)) {
        $.ajax({
            type: "POST",
            url: "/masters/DecodePartId",
            data: { partId: partid },
            success: function (decodepartid) {
                $("#PartId").val(decodepartid);

                var pid = decodepartid;

                api.getbulk("/masters/BOFDoclist?contentid=" + content + "&partid=" + pid).then((data) => {
                    //data = data.filter(item => item.status == 1 || item.status==0);
                    var tablebody = $("#ManufPartDocgrid tbody");
                    $(tablebody).html("");//empty tbody
                    //console.log(data);
                    for (i = 0; i < data.length; i++) {
                        var rowHtml = AppUtil.ProcessTemplateData("maufDocUploadRow", data[i]);

                        // Check if mandatory is 'Y', if so, hide the Delete option
                        if (data[i].docListId === 0) {
                            // Simplified regex to match the Upload link
                            rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Edit *<\/a>/i, '');
                            rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
                        }
                        if (data[i].docListId != 0) {
                            // Remove the Edit link from the generated row
                            rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Upload *<\/a>/i, '');
                        }

                        if (data[i].mandatory === 'Y') {
                            // Remove the Delete link from the generated row
                            rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
                        }

                        // Append the processed row to the table body
                        $(tablebody).append(rowHtml);
                    }
                }).catch((error) => {
                    console.log(error);
                });
            }
        });
    } else {
        api.getbulk("/masters/BOFDoclist?contentid=" + content + "&partid=" + partid).then((data) => {
            //data = data.filter(item => item.status == 1 || item.status == 0);
            var tablebody = $("#ManufPartDocgrid tbody");
            $(tablebody).html("");//empty tbody
            //console.log(data);
            for (i = 0; i < data.length; i++) {
                var rowHtml = AppUtil.ProcessTemplateData("maufDocUploadRow", data[i]);

                // Check if mandatory is 'Y', if so, hide the Delete option
                if (data[i].docListId === 0) {
                    // Simplified regex to match the Upload link
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Edit *<\/a>/i, '');
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
                }
                if (data[i].docListId != 0) {
                    // Remove the Edit link from the generated row
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Upload *<\/a>/i, '');
                }

                if (data[i].mandatory === 'Y') {
                    // Remove the Delete link from the generated row
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
                }

                // Append the processed row to the table body
                $(tablebody).append(rowHtml);
            }
        }).catch((error) => {
            console.log(error);
        });

    }
}