'use strict';
//
class AssociativeArray extends Array {
    constructor() {
        super();
        this.data = {};
    }
    push(key, value) {
        super.push(key);
        this.data[key] = value
    }
    *[Symbol.iterator]() {
        let nextIndex = 0;
        while (nextIndex < this.length) {
            yield this[nextIndex];
            nextIndex++;
        }
    }
}

let addedId = "";
let addedValue = "";
let archive = 0;


var RawMaterialDetailFormUtil = {

    UpdateFormIDs: (data) => {
        $("#RawMaterialDetailId").val(data.rawMaterialDetailId);
        $("#PartId").val(data.partId);
    },
    ClearMakefromTab: () => {
        $("#Supplier").val("");
        $("#SupplierId").val("");
        $("#SupplierId").trigger('change');
        $("#PartNo").val("");
        $("#PartDescription").val("");
        $("#RawMaterialWeight").val("");
        $("#RawMaterialNotes").val("");
        $("#RevDate").val("");
        $("#RevNo").val("");

        $("#BaseRawMaterialId").val(1);
        $("#RawMaterialTypeId").val(1);
        $("#BaseRawMaterialId").trigger("change");
        $("#RawMaterialTypeId").trigger("change");


        //$("#OPRM").prop("checked", true).trigger("click");
        $("#MPRM").prop("checked", true).trigger("click");
        $("#Status").val("Active");
        //$("#Status").trigger("change");
        $("#StatusChangeReason").val("");
        $("#Standard").val(1);
        $("#Standard").trigger("change");
        $("#MaterialSpecId").val(1);
        $("#MaterialSpecId").trigger("change");
        $("#RawMaterialDetailId").val("0");
        $("#PartId").val("0");
    },
    ConfirmDialog: (id, message) => {
        var result = confirm(message);
        if (result) {
            $("#TabPurchaseDetails").hide();
            if (id == "1") {
                RawMaterialDetailFormUtil.ClearMakefromTab();
                $("#TabPurchaseDetails").show();
            } else {
                RawMaterialDetailFormUtil.ClearMakefromTab();
            }
        }
        else {
            $("#TabPurchaseDetails").hide();
            if (id == "1") {
                $("#OPRM").prop("checked", false);
                $("#CSRM").prop("checked", true);
            } else {
                $("#OPRM").prop("checked", true);
                $("#CSRM").prop("checked", false);
                $("#TabPurchaseDetails").show();
            }
        }
    },
    ValidateMainTabForTabChange: () => {
        if ($("#PartNo").val().length == 0) {
            return false;
        }
        if ($("#PartDescription").val().length == 0) {
            return false;
        }
        //if ($("#RevNo").val().length == 0) {
        //    return false;
        //}
        if ($("#RawMaterialWeight").val().length == 0) {
            return false;
        }
        if ($("#RawMaterialDetailId").val() == "0") {
            return false;
        }
       return true;
    },
    ValidateMainTabForRadio: () => {
        if ($("#PartNo").val().length) {
            return true;
        }
        if ($("#PartDescription").val().length) {
            return true;
        }
        if ($("#RevNo").val().length) {
            return true;
        }
        if ($("#RawMaterialWeight").val().length) {
            return true;
        }
        //if ($("#RawMaterialNotes").val().length) {
        //    return true;
        //}
        return false;
    },
    ValidateRawMaterialDetails: () => {
        var Message = "";
        var RawMaterialDetail = true;
        //////debugger;
        /*if ($("#RawMaterialMadeType").val().length == "") {
            RawMaterialDetail = false;
            Message += "RawMaterial Made Type\n"
        }
        if ($("#RawMaterialTypeId").val() == 0) {
            RawMaterialDetail = false;
            Message += "Raw Material Type\n"
        }*/
        
        if ($("#PartDescription").val().length == "") {
            RawMaterialDetail = false;
            Message += "Part Description\n"
        }
        /*if ($("#BaseRawMaterialId").val() == 0) {
            RawMaterialDetail = false;
            Message += "Base Raw Material\n"
        }*/
        if ($("#RawMaterialMadeType").val() == 0) {
            RawMaterialDetail = false;
            var newNamevalidate = document.getElementById('RawMaterialMadeType');
            newNamevalidate.style.border = '2px solid red';
        } else {
            var newNamevalidate = document.getElementById('RawMaterialMadeType');
            newNamevalidate.style.border = '';
        }
        if ($("#RawMaterialMadeSubType").val() == 0) {
            RawMaterialDetail = false;
            var newNamevalidate = document.getElementById('RawMaterialMadeSubType');
            newNamevalidate.style.border = '2px solid red';
        } else {
            var newNamevalidate = document.getElementById('RawMaterialMadeSubType');
            newNamevalidate.style.border = '';
        }
        if ($("#RawMaterialTypeId").val() == 0) {
            RawMaterialDetail = false;
            var newNamevalidate = $('#RawMaterialTypeId').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
        } else {
            var newNamevalidate = $('#RawMaterialTypeId').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        if ($("#PartNo").val().length == "") {
            RawMaterialDetail = false;
            var newNamevalidate = document.getElementById('PartNo');
            newNamevalidate.style.border = '2px solid red';
        } else {
            var newNamevalidate = document.getElementById('PartNo');
            newNamevalidate.style.border = '';
        }

        if ($("#PartDescription").val().length == "") {
            RawMaterialDetail = false;
            var newNamevalidate = document.getElementById('PartDescription');
            newNamevalidate.style.border = '2px solid red';
        } else {
            var newNamevalidate = document.getElementById('PartDescription');
            newNamevalidate.style.border = '';
        }
        if ($("#UOMId").val().length == 0) {
            RawMaterialDetail = false;
            var newNamevalidate = document.getElementById('UOMId');
            newNamevalidate.style.border = '2px solid red';
        } else {
            var newNamevalidate = document.getElementById('UOMId');
            newNamevalidate.style.border = '';
        }
        if ($("#RawMaterialWeight").val().length == 0) {
            RawMaterialDetail = false;
            var newNamevalidate = document.getElementById('RawMaterialWeight');
            newNamevalidate.style.border = '2px solid red';
        } else {
            var newNamevalidate = document.getElementById('RawMaterialWeight');
            newNamevalidate.style.border = '';
        }
        if ($("#BaseRawMaterialId").val() == 0) {
            RawMaterialDetail = false;
            var newNamevalidate = $('#BaseRawMaterialId').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
        } else {
            var newNamevalidate = $('#BaseRawMaterialId').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        if ($("#Standard").val() == 0) {
            RawMaterialDetail = false;
            var newNamevalidate = $('#Standard').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
        } else {
            var newNamevalidate = $('#Standard').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        if ($("#MaterialSpecId").val() == 0) {
            RawMaterialDetail = false;
            var newNamevalidate = $('#MaterialSpecId').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
        } else {
            var newNamevalidate = $('#MaterialSpecId').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        //if (RawMaterialDetail == false) {
        //    alert("Following field(s) cannot be left blank.\n" + Message);
        //}
        return RawMaterialDetail;
    },
    ClearTable: () => {
        var tablebody = $("#TablePurchaseDetails tbody");
        $(tablebody).empty();
    },
    UpdatePurchaseDetailsTable: (keyVals) => {
        //////debugger;
        var tablebody = $("#TablePurchaseDetails tbody");
        var templateElement = $("#PurchaseDetailTemplate").html();
        for (const pair of keyVals) {
            let key = pair[0];
            let val = pair[1];

            templateElement = templateElement.replaceAll("{" + key + "}", val);
        }
        $(tablebody).append(templateElement);
    },
    UpdatePurchaseDetailsTableFromPostData: (keyVals) => {
        //////debugger;
        var tablebody = $("#TablePurchaseDetails tbody");
        var templateElement = $("#PurchaseDetailTemplate").html();
        for (const key of keyVals) {
            let val = keyVals.data[key];
            templateElement = templateElement.replaceAll("{" + key + "}", val);
        }
        $(tablebody).append(templateElement);
    }
};

function ToggleSupplierFields(showhideflag) {
    //SupplierLabel
    //SupplierGroup
    $("#SupplierLabel").hide();
    $("#SupplierGroup").hide();
    $("#SupplierGroupCol").hide();
    $("#TabPurchaseDetails").show();

    if (showhideflag == 1) {
        $("#SupplierLabel").show();
        $("#SupplierGroup").show();
        $("#SupplierGroupCol").show();
        $("#TabPurchaseDetails").hide();
    }
};
function DecodeRawPartId() {
    var partid = $("#PartId").val();
    $.ajax({
        type: "POST",
        url: "/masters/DecodePartId",
        data: { partId: partid },
        success: function (decodepartid) {
            $("#PartId").val(decodepartid);
            var closeatag = document.getElementById("closeatag");
            if (decodepartid != 0) {
                closeatag.href = "/Masters/MasterDetails";
            } else {
                closeatag.href = "/Masters/Index";
            }
        }
    });
}
$(function () {
    //var RawMaterialMadeType = 0;
    //debugger;
    // Document is ready
    DecodeRawPartId();
    $("#RawMaterialTypeId").select2();
    $("#UOMId").select2();
  //  loadRMTypes("RawMaterialTypeId");

    $("#BaseRawMaterialId").select2();
 //   loadBaseRMs("BaseRawMaterialId");

    $("#Standard").select2();
 //   loadRMStandards("Standard");

    $("#MaterialSpecId").select2();
  //  loadRMSpecs("MaterialSpecId");

    $("#SupplierId").select2();
 //   loadSuppliers("SupplierId");

    CURRENT_TAB = "TabRawMaterial";
    ToggleSupplierFields(0);

    $("#SupplierId").on('click', function () {
        $("#Supplier").val($("#SupplierId option:selected").text());
    });


    $("#TabRawMaterial").on('click', function (evemt) {
        if ($('.nav-tabs .active').text().trim() == "Part Info") {
            event.preventDefault();
            return;
        }
        else {
            if (modelObj.Edit) {
                $('.nav-pills a[href="#tab-1"]').tab('show');
                CURRENT_TAB = "TabRawMaterial";
            }
            else {
                $('.nav-pills a[href="#tab-1"]').tab('show');
                CURRENT_TAB = "TabRawMaterial";
                document.getElementById("RawMetform").reset();
                $("#Supplier").val("");
                $("#PartId").val("0");
                $("#RawMaterialDetailId").val("0");
                //alert("After Reset..." + $('#PartId').val());
                $("#RawMaterialTypeId").val("").trigger('change');
                $("#SupplierId").val("").trigger('change');
                $("#BaseRawMaterialId").val("").trigger('change');
                $("#Standard").val("").trigger('change');
                $("#MaterialSpecId").val("").trigger('change');
            }
        }
    });

    $("#TabPurchaseDetails").on('click',function (event) {
        if ($('.nav-tabs .active').text().trim() == "Purchase Details") {
            event.preventDefault();
            return;
        }
        else {
            if (!RawMaterialDetailFormUtil.ValidateMainTabForTabChange()) {
                alert("Please Fill The Basic Information.");
                event.preventDefault();
            }
            else {
                document.getElementById("FormPurchaseDetails").reset();
                let spanPartNo = document.getElementById("Span_PartNo");
                let spanPartDesc = document.getElementById("Span_PartDescription");
                spanPartNo.innerText = $("#PartNo").val();
                spanPartDesc.innerText = $("#PartDescription").val();
                masterPartType = "3";//Raw Material
                $("#PPartId").val($("#PartId").val());
                $("#PPartNo").val($("#PartNo").val());
            
                
                $('.nav-pills a[href="#tab-2"]').tab('show');
                CURRENT_TAB = "TabPurchaseDetails";
                var tablebody = $("#TablePurchaseDetails tbody");
                tablebody.html("");
                if (modelObj.Edit) {
                    reloadPPDs(spanPartNo);
                }
            }
        }
        //
        // loadParts($("#PartNo").val());
    });

    //1=show 0=hide
    
    //$('#RawMaterialMadeType').change(function () {
    //$('select[name="RawMaterialMadeType"]').change(function () {
       
    //    var ShowMessage = "Please Save The Detail(s) or All The Data Will Erase";
    //    var val = this.value;  // Get the selected value

    //    if (RawMaterialDetailFormUtil.ValidateMainTabForRadio()) {
    //        // If the form is valid, show confirmation dialog
    //        RawMaterialDetailFormUtil.ConfirmDialog(this.value, ShowMessage);
    //    } else {
    //        // Handle logic based on the selected value (1 or 2)
    //        if (val === "1") {
    //            ToggleSupplierFields(0); // Handle "Own Purchased RM"
    //            RawMaterialDetailFormUtil.ClearMakefromTab();
    //        } else if (val === "2") {
    //            ToggleSupplierFields(1); // Handle "Customer Supplied RM"
    //            RawMaterialDetailFormUtil.ClearMakefromTab();
    //        } else {
    //            // Handle other values if necessary
    //        }
    //    }
    //});

    $('#status-info').on('hidden.bs.modal', function (event) {
        var newNamevalidate = document.getElementById('statusResasonopup');
        newNamevalidate.style.border = '';
    });
    $('#status-info').on('show.bs.modal', function (event) {
        var currentStatus = $("#Status").val();
        $("#CurrentStatus").val(currentStatus);
        $("#statusResasonopup").val("");
    });
    $("#BtnstatusSave").click(function (event) {
        var streason = $("#statusResasonopup").val();
        var statusPopup = $("#statusPopup").val();
        if (statusPopup == "Inactive") {
            if (streason.length == 0) {
                var newNamevalidate = document.getElementById('statusResasonopup');
                newNamevalidate.style.border = '2px solid red';
                return false;
            } else {
                var newNamevalidate = document.getElementById('statusResasonopup');
                newNamevalidate.style.border = '';
                $("#status-info").modal("hide");
            }
            $("#StatusChangeReason").val(streason);
            $("#Status").val(statusPopup);
        } else {
            $("#status-info").modal("hide");
        }
    });


    $("#StEdit").click(function (event) {
        var selectedValue = $("#Standard").val();  // Get the value of the selected option
        var selectedText = $("#Standard").find("option:selected").text();
        document.forms["StandardForm"]["Name"].value = selectedText;
        document.forms["StandardForm"]["Standard"].value = selectedValue;

    });
    $('#dialog-AddRMStandard').on('hidden.bs.modal', function (event) {
        $("#SpecForm")[0].reset();  // Clears all input fields inside the form

    });

    $("#MSpecEdit").click(function (event) {
        var selectedValue = $("#MaterialSpecId").val();  // Get the value of the selected option
        var selectedText = $("#MaterialSpecId").find("option:selected").text();
        document.forms["SpecForm"]["Name"].value = selectedText;
        document.forms["SpecForm"]["MaterialSpecId"].value = selectedValue;

    });
    $('#dialog-AddRMSpec').on('hidden.bs.modal', function (event) {
        $("#SpecForm")[0].reset();  // Clears all input fields
    });
    $("#baseMtEdit").click(function (event) {
        var selectedValue = $("#BaseRawMaterialId").val();  // Get the value of the selected option
        var selectedText = $("#BaseRawMaterialId").find("option:selected").text();
        document.forms["BaseForm"]["Name"].value = selectedText;
        document.forms["BaseForm"]["BaseRawMaterialId"].value = selectedValue;

    });
    $('#dialog-AddBaseRM').on('hidden.bs.modal', function (event) {
        $("#BaseForm")[0].reset();  // Clears all input fields inside the
    });

    $("#editRMpart").click(function (event) {
        var selectedValue = $("#RawMaterialTypeId").val();  // Get the value of the selected option
        var selectedText = $("#RawMaterialTypeId").find("option:selected").text();
        document.forms["TypeForm"]["Name"].value = selectedText;
        document.forms["TypeForm"]["RawMaterialTypeId"].value = selectedValue;
        $("#ENameSP").text("Edit");

    });
    $('#dialog-AddRMType').on('hidden.bs.modal', function (event) {
        $("#ENameSP").text("Add");
        $("#TypeForm")[0].reset();  // Clears all input fields
    });
    $("#UomEdit").click(function (event) {
        var selectedValue = $("#UOMId").val();  // Get the value of the selected option
        var selectedText = $("#UOMId").find("option:selected").text();
        document.forms["frmAddUOM"]["Name"].value = selectedText;
        document.forms["frmAddUOM"]["UOMId"].value = selectedValue;

    });
    $('#uom').on('hidden.bs.modal', function (event) {
        $("#frmAddUOM")[0].reset();  // Clears all input fields
    });
    
    $("#btnRawMaterialDetailSubmit").click(function (event) {
        ////debugger;
        if (!modelObj.Edit) {
            if ($("#RawMaterialDetailId").val() != "0") {
                alert("Älready saved... RM");
                event.preventDefault();
                return;
            }
        }
        
        if (RawMaterialDetailFormUtil.ValidateRawMaterialDetails()) {
            //if ($("#RawMetform").valid()) {
                //       ////debugger;
                var formData = AppUtil.GetFormData("RawMetform");
                api.post("/masters/rawmaterialdetail", formData).then((data) => {
                    RawMaterialDetailFormUtil.UpdateFormIDs(data);
                    //  document.getElementById("RawMetform").reset();
                }).catch((error) => {
                    AppUtil.HandleError("RawMetform", error);
                });
            //} else {
            //    alert("Invalid form...")
            //}
        }
        event.preventDefault();
    });

    $('#dialog-AddUOM').on('show.bs.modal', function (e) {
        //alert("Show...");
    });

    $('#dialog-AddUOM').on('shown.bs.collapse', function (e) {
        //alert("Collapse...")
    });



    $('#dialog-AddRMType').on('show.bs.modal', function (e) {
        CURDLG = $(this);
        let txt = $("#RawMaterialTypeId option:selected").text();
        let id = $("#RawMaterialTypeId option:selected").val();
        $('#TypeName').val(txt);
        $('#DRawMaterialTypeId').val(id);
        document.getElementById("TypeForm").reset();
        //alert("Show...RMType "+id+":"+id);

    });

    $('#dialog-AddRMStandard').on('show.bs.modal', function (e) {
        CURDLG = $(this);
        let txt = $("#Standard option:selected").text();
        let id = $("#Standard option:selected").val();
        $('#StandardName').val(txt);
        $('#DStandard').val(id);
        document.getElementById("StandardForm").reset();
    });

    $('#dialog-AddBaseRM').on('show.bs.modal', function (e) {
        let txt = $("#BaseRawMaterialId option:selected").text();
        let id = $("#BaseRawMaterialId option:selected").val();
        $('#BaseRMName').val(txt);
        $('#DBaseRawMaterialId').val(id);
        //SpecForm
        document.getElementById("BaseForm").reset();
    });

    $('#dialog-AddRMSpec').on('show.bs.modal', function (e) {
        CURDLG = $(this);
        let txt = $("#MaterialSpecId option:selected").text();
        let id = $("#MaterialSpecId option:selected").val();
        $('#SpecName').val(txt);
        $('#DMaterialSpecId').val(id);
        document.getElementById("SpecForm").reset();
    });

    $('#dialog-AddRMType').on('hide.bs.modal', function (e) {
        //if ((addedId.length) > 0)
        {
            if ((addedValue.length) > 0) {
                var data = {
                    id: addedId,
                    text: addedValue
                };
               var newOption = new Option(data.text, data.id, true, true);
               $('#RawMaterialTypeId').append(newOption).trigger('change');
            }
        }
        addedId = "";
        addedValue = "";
    });
    $('#dialog-AddRMSpec').on('hide.bs.modal', function (e) {
        //if ((addedId.length) > 0)
        {
            if ((addedValue.length) > 0) {
                var data = {
                    id: addedId,
                    text: addedValue
                };
                var newOption = new Option(data.text, data.id, true, true);
                $('#MaterialSpecId').append(newOption).trigger('change');
            }
        }
        addedId = "";
        addedValue = "";
    });

    $('#dialog-AddRMStandard').on('hide.bs.modal', function (e) {
     
        //if ((addedId.length) > 0)
        {
            if ((addedValue.length) > 0) {
                var data = {
                    id: addedId,
                    text: addedValue
                };
                var newOption = new Option(data.text, data.id, true, true);
                $('#Standard').append(newOption).trigger('change');

            }
        }
        addedId = "";
        addedValue = "";
    });

    $('#dialog-AddBaseRM').on('hide.bs.modal', function (e) {
        //if ((addedId.length) > 0)
        {
            if ((addedValue.length) > 0) {
                var data = {
                    id: addedId,
                    text: addedValue
                };
                var newOption = new Option(data.text, data.id, true, true);
                $('#BaseRawMaterialId').append(newOption).trigger('change');
             //   $('#BaseRawMaterialId').trigger('change');
            }
        }
        addedId = "";
        addedValue = "";
    });
    if (modelObj["Edit"]) {
        //console.log("***************");
        //console.log(modelObj);
        //console.log("***************")
        if (modelObj["rawMaterialMadeType"] == 1) {
            //$("#RawMaterialMadeType").val("1").trigger('change');
            ToggleSupplierFields(0);
        }
        else {
            ToggleSupplierFields(1);
            //$("#RawMaterialMadeType").val("2").trigger('change');
        }
    };

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
        //today.setDate(today.getDate() + 10);
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

    loadDocUploadList();
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
        if (com.trim() === "" || com.length === 0) {
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
        var today = new Date();
        today.setDate(today.getDate() + 10);  // Add 10 days to the current date

        // Format the date as YYYY-MM-DD (you can modify this to your required format)
        var deletionDate = today.toISOString().split('T')[0];

        // Append the DeletionDate to the FormData
        formData.append("DeletionDate", deletionDate);
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
    $("#RawMaterialMadeSubType").change(function () {
        loadDocUploadList();
    });

});
$(document).on('change', '#RawMaterialMadeType', function () {
    var ShowMessage = "Please Save The Detail(s) or All The Data Will Erase";
        var val = this.value;  // Get the selected value

    if (RawMaterialDetailFormUtil.ValidateMainTabForTabChange()) {
            // If the form is valid, show confirmation dialog
            RawMaterialDetailFormUtil.ConfirmDialog(this.value, ShowMessage);
        } else {
            // Handle logic based on the selected value (1 or 2)
            if (val === "1") {
                ToggleSupplierFields(0); // Handle "Own Purchased RM"
                RawMaterialDetailFormUtil.ClearMakefromTab();
            } else if (val === "2") {
                ToggleSupplierFields(1); // Handle "Customer Supplied RM"
                RawMaterialDetailFormUtil.ClearMakefromTab();
            } else {
                // Handle other values if necessary
            }
        }
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

function AddRMType() {
    var name = $("#TypeName").val();
    if (name.length == 0) {
        var newNamevalidate = document.getElementById('TypeName');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('TypeName');
        newNamevalidate.style.border = '';
    }
    if ($("#TypeForm").valid()) {
        ////debugger;
        var formData = AppUtil.GetFormData("TypeForm");
        formData.MultiplePartsMadeFrom1InputRM = $('#TypeMulitpleInputRM').prop('checked') ? 'Y' : 'N';
        
        api.getbulk("/masters/CheckRmType?uomName=" + name).then((data) => {
            if (!data) {
                api.post("/masters/rmtype", formData).then((data) => {
                    // RawMaterialDetailFormUtil.UpdateFormIDs(data);
                    //  document.getElementById("RawMetform").reset();
                    addedId = "" + data.rawMaterialTypeId;
                    addedValue = data.name;

                    // CURDLG.modal('hide');
                    // $('#RawMaterialTypeId').val(addedId);
                    // $('#RawMaterialTypeId').trigger('change');
                    document.getElementById("btn-close-AddRMType").click();
                    //btn - close - AddRMType
                    //    CURDLG.modal('hide');
                    ////debugger;
                }).catch((error) => {
                    AppUtil.HandleError("RawMetform", error);
                });
            } else {
                var newNamevalidate = document.getElementById('TypeName');
                newNamevalidate.style.border = '2px solid red';
            }
        }).catch((error) => {
        });
    } else {
        alert("Invalid form...")
    }
    //event.preventDefault();
}

function AddRMStandard() {
    var name = $("#StandardName").val();
    if (name.length == 0) {
        var newNamevalidate = document.getElementById('StandardName');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('StandardName');
        newNamevalidate.style.border = '';
    }
    if ($("#StandardForm").valid()) {
        //       ////debugger;
        var formData = AppUtil.GetFormData("StandardForm");
        api.getbulk("/masters/CheckRmStandard?uomName=" + name).then((data) => {
            if (!data) {
                api.post("/masters/rmstandard", formData).then((data) => {
                    // RawMaterialDetailFormUtil.UpdateFormIDs(data);
                    //  document.getElementById("RawMetform").reset();
                    addedId = data.standard;
                    addedValue = data.name;
                    document.getElementById("btn-close-AddStandard").click();

                }).catch((error) => {
                    AppUtil.HandleError("RawMetform", error);
                });
            } else {
                var newNamevalidate = document.getElementById('StandardName');
                newNamevalidate.style.border = '2px solid red';
            }
        }).catch((error) => {
        });
    } else {
        alert("Invalid form...")
    }
}

function AddRMSpec() {
    var name = $("#SpecName").val();
    if (name.length == 0) {
        var newNamevalidate = document.getElementById('SpecName');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('SpecName');
        newNamevalidate.style.border = '';
    }
    if ($("#SpecForm").valid()) {
        //       ////debugger;
        var formData = AppUtil.GetFormData("SpecForm");
        api.getbulk("/masters/CheckRmSpec?uomName=" + name).then((data) => {
            if (!data) {
                api.post("/masters/rmspec", formData).then((data) => {
                    // RawMaterialDetailFormUtil.UpdateFormIDs(data);
                    //  document.getElementById("RawMetform").reset();
                    addedId = data.materialSpecId;
                    addedValue = data.name;
                    document.getElementById("btn-close-AddSpec").click();
                }).catch((error) => {
                    AppUtil.HandleError("RawMetform", error);
                });
            } else {
                var newNamevalidate = document.getElementById('SpecName');
                newNamevalidate.style.border = '2px solid red';
            }
        }).catch((error) => {
        });
    } else {
        alert("Invalid form...")
    }
}

function AddBaseRM() {
    var name = $("#BaseRMName").val();
    if (name.length == 0) {
        var newNamevalidate = document.getElementById('BaseRMName');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('BaseRMName');
        newNamevalidate.style.border = '';
    }
    if ($("#BaseForm").valid()) {
        //       ////debugger;
        var formData = AppUtil.GetFormData("BaseForm");
        api.getbulk("/masters/CheckBaseRm?uomName=" + name).then((data) => {
            if (!data) {
                api.post("/masters/baserm", formData).then((data) => {
                    // RawMaterialDetailFormUtil.UpdateFormIDs(data);
                    //  document.getElementById("RawMetform").reset();
                    addedId = data.baseRawMaterialId;
                    addedValue = data.name;
                    document.getElementById("btn-close-AddBaseRM").click();
                }).catch((error) => {
                    AppUtil.HandleError("RawMetform", error);
                });
            } else {
                var newNamevalidate = document.getElementById('BaseRMName');
                newNamevalidate.style.border = '2px solid red';
            }
        }).catch((error) => {
        });
    } else {
        alert("Invalid form...")
    }
}

function loadParts(partNo) {//pass the element name
    api.get("/masters/partpurchases").then((data) => {
        //var div_data = "<option value=""></option>";
        for (i = 0; i < data.length; i++) {
            var arr = new AssociativeArray();
            arr.push("ID", data[i].partPurchaseId);
            arr.push("SupplierId", data[i].supplier);
            arr.push("LeadTimeInDays", data[i].leadTimeInDays);
            arr.push("MinimumOrderQuantity", data[i].minimumOrderQuantity);
            arr.push("Price", data[i].price);
            //var keyVals = data[i].entries();
            //console.log(arr);
            RawMaterialDetailFormUtil.ClearTable();
            RawMaterialDetailFormUtil.UpdatePurchaseDetailsTableFromPostData(arr);
            //   //debugger;
            //compSelect.append(div_data);
            //RawMaterialDetailFormUtil.ClearTable();
            //RawMaterialDetailFormUtil.UpdatePurchaseDetailsTable();
        }
        //compSelect.html(div_data);
        //filter once loaded

    }).catch((error) => {
        //console.log(error);
    });
}

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

            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = file;
            link.click();
        }
    };
    xhr.send();
}
function loadDocUploadList() {
    var content = parseInt($("#RawMaterialMadeSubType").val());
    var main = parseInt($("#RawMaterialMadeType").val());
    if (main==0) {
            var newNamevalidate = document.getElementById('RawMaterialMadeType');
        newNamevalidate.style.border = '2px solid red';
        $("#RawMaterialMadeSubType").val(0);
        return false;
    }
    else if (main == 2) {
        content = 3;
        var newNamevalidate = document.getElementById('RawMaterialMadeType');
        newNamevalidate.style.border = '';
    } else {
        var newNamevalidate = document.getElementById('RawMaterialMadeType');
        newNamevalidate.style.border = '';
        if (content == 1) {
            content = 5;
        } else if (content == 2) {
            content = 4;
        } else if (content == 3) {
            content = 7;
        }
    }
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
                    //data = data.filter(item => item.status !== 6);
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
            //data = data.filter(item => item.status !== 6);
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