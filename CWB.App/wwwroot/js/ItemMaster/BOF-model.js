let archive = 0;
var BOFFormUtil = {

    UpdateFormIDs: (data) => {
        $("#BoughtOutFinishDetailId").val(data.boughtOutFinishDetailId);
        $("#PartId").val(data.partId);
    },
    ClearPurchaseDetailTab: () => {
        $('input[type=radio][name=PartisMadefrom]').prop('checked', false);
        $("#InputPartNo").val("");
        $("#MFDescription").val("");
        $("#InputWeight").val(null);
        $("#Scrapgenerated").val("");
        $("#QuantityperInput").val("");
        $("#YieldNotes").val("");
        $('#PreferedRawMaterial').prop('checked', false);
        $("#ddlStatus").val(0);
        $("#txtStatusChangeReason").val("");
        $("#txtCustomer").val("");
        $("#txtPartNo").val("");
        $("#txtRevNo").val("");
        $("#txtRevDate").val(null);
        $("#PartDescription").val("");
        $("#ddlUOM").val(0);
        $("#txtFinishedWeight").val("");
        $("#BoughtOutFinishDetailId").val("0");
        $("#PartId").val("0");

        
    },
    ConfirmDialog: (prevId,id, message) => {
        //////debugger;
        var result = confirm(message);
        if (result) {
            $("#BoughtOutFinishMadeType").val(id);
            if (Id === "1") {
                //$("#RadioStandard").prop("checked", true);
            }
            else if (Id === "2") {
                //$("#RadioCatalog").prop("checked", true);
            }
            else {
                //$("#RadioMadeToPrint").prop("checked", true);
            }
        }
        else {
            $("#BoughtOutFinishMadeType").val(prevId);
        //    alert(prevId)
            if (prevId === "1") {
                $("#RadioStandard").prop("checked", true);
            }
            else if (prevId === "2") {
                $("#RadioCatalog").prop("checked", true);
            }
            else {
                $("#RadioMadeToPrint").prop("checked", true);
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
        if ($("#SupplierPartNo").val().length == 0) {
            return false;
        }
        if ($("#BoughtOutFinishDetailId").val() == "0") {
            return false;
        }
        return true;
    },
    ValidateBOFDetails: (Mode) => {
        var Message = "";
        var ManufacturedPartDetail = true;
        if (Mode == 1) {
            alert("Please Fill The Basic Information.");
            //alert(Message);
        }
        if (Mode == 2) {
            if ($("#BoughtOutFinishMadeType").val() == 0) {
                ManufacturedPartDetail = false;
                var newNamevalidate = document.getElementById('BoughtOutFinishMadeType');
                newNamevalidate.style.border = '2px solid red';
            } else {
                var newNamevalidate = document.getElementById('BoughtOutFinishMadeType');
                newNamevalidate.style.border = '';
            }
            if ($("#PartNo").val().length == "") {
                ManufacturedPartDetail = false;
                var newNamevalidate = document.getElementById('PartNo');
                newNamevalidate.style.border = '2px solid red';
            } else {
                var newNamevalidate = document.getElementById('PartNo');
                newNamevalidate.style.border = '';
            }

            if ($("#PartDescription").val().length == "") {
                ManufacturedPartDetail = false;
                var newNamevalidate = document.getElementById('PartDescription');
                newNamevalidate.style.border = '2px solid red';
            } else {
                var newNamevalidate = document.getElementById('PartDescription');
                newNamevalidate.style.border = '';
            }
            if ($("#UOMId").val().length == 0) {
                ManufacturedPartDetail = false;
                var newNamevalidate = document.getElementById('UOMId');
                newNamevalidate.style.border = '2px solid red';
            } else {
                var newNamevalidate = document.getElementById('UOMId');
                newNamevalidate.style.border = '';
            }
            //alert("Please Fill The Basic Information.");
            //alert(Message);
        }
        return ManufacturedPartDetail;
    }
};
function DecodePartId() {
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
$(function () {
    var manufacturedPartType = 0;
    ////debugger;
    // Document is ready
    DecodePartId();

    CURRENT_TAB = "BOFMain";
    $('input[type=radio][name=BoughtOutFinishMade]').change(function () {
        var ShowMessage = "Please Save The Detail(s) or All The Data Will Erase";
        //var preVal = $("#BoughtOutFinishMadeType").val();
        //var val = $('input[name=BoughtOutFinishMade]:checked').val();
        //alert(preVal + " v:" + val);
        var inputs = document.getElementsByName('BoughtOutFinishMade');
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].checked) {
                if (confirm(ShowMessage)) {
                    inputs[i].checked = true;
                } 
                break;
            }
        }
        //BOFFormUtil.ConfirmDialog(preVal, val, ShowMessage)
    });

    $("#TabPartInfo").click(function (event) {
        if ($('.nav-tabs .active').text().trim() == "Part Info") {
            event.preventDefault();
            return;
        }
        else {
            if (modelObj.Edit) {
                $('.nav-pills a[href="#tab-1"]').tab('show');
                CURRENT_TAB = "BOFMain";
            }
            else {
                $('.nav-pills a[href="#tab-1"]').tab('show');
                CURRENT_TAB = "BOFMain";
                document.getElementById("BOFform").reset();
                $("#BoughtOutFinishDetailId").val("0");
                $("#PartId").val("0");
            }

        }
        //BOFform
    });

    $("#TabPurchaseDetails").click(function (event) {
        if ($('.nav-tabs .active').text().trim() == "Purchase Details") {
            event.preventDefault();
            return;
        }
        else {
            if (!BOFFormUtil.ValidateMainTabForTabChange()) {
                alert("Please Fill The Basic Information.");
                event.preventDefault();
            }
            else {
                document.getElementById("FormPurchaseDetails").reset();
                let spanPartNo = document.getElementById("Span_PartNo");
                let spanPartDesc = document.getElementById("Span_PartDescription");
                spanPartNo.innerText = $("#PartNo").val();
                spanPartDesc.innerText = $("#PartDescription").val();
                masterPartType = "2";//BOF
              


                $('.nav-pills a[href="#tab-2"]').tab('show');
                CURRENT_TAB = "TabPurchaseDetails";
                var tablebody = $("#TablePurchaseDetails tbody");
                tablebody.html("");
                if (modelObj.Edit) {
                    reloadPPDs(spanPartNo);
                }
                
            }
        }
    });

    $("#editUomPopup").click(function (event) {
        var selectedValue = $("#UOMId").val();  // Get the value of the selected option
        var selectedText = $("#UOMId").find("option:selected").text();
        document.forms["frmAddUOM"]["Name"].value = selectedText;
        document.forms["frmAddUOM"]["UOMId"].value = selectedValue;

    });
    $('#uom').on('hidden.bs.modal', function (event) {
        $("#frmAddUOM")[0].reset();  // Clears all input fields
    });
   

    $('input[type=radio][name=PartisMadefrom]').change(function () {
        $("#PartMadeFrom").val(this.value);
    });

    $("#Status").change(function () {
        if ($(this).val() === "Inactive") {
            $("#StatusChangeReason").attr("required", "required");
            $("#StatusChangeReason").attr("data-val-required", "StatusChangeReason is required when Status is Inactive.");
        } else {
            $("#StatusChangeReason").removeAttr("required");
            $("#StatusChangeReason").removeAttr("data-val-required");
        }
    });

    $("#btnBOFDetailSubmit").click(function (event) {
        if (!modelObj.Edit) {
            if ($("#BoughtOutFinishDetailId").val() != "0") {
                alert("Älready saved... BOF");
                event.preventDefault();
                return;
            }
        }
        if (BOFFormUtil.ValidateBOFDetails(2)) {
            if ($("#BOFform").valid()) {
                var formData = AppUtil.GetFormData("BOFform");
                api.post("/masters/boughtoutfinishdetail", formData).then((data) => {
                    ////debugger;
                    BOFFormUtil.UpdateFormIDs(data);
                   // document.getElementById("BOFform").reset();

                }).catch((error) => {
                    AppUtil.HandleError("BOFform", error);
                });
            }
        }
        event.preventDefault();
    });
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
        //var today = new Date();
        //today.setDate(today.getDate() + 10);  // Add 10 days to the current date

        //// Format the date as YYYY-MM-DD (you can modify this to your required format)
        //var deletionDate = today.toISOString().split('T')[0];

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
    $("#BoughtOutFinishMadeType").change(function () {
        loadDocUploadList();
    });

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
    }
});

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
    var content = parseInt($("#BoughtOutFinishMadeType").val());
    if (content == 1) {
        content = 6;
    } else if (content == 2) {
        content = 8;
    } else if (content == 3) {
        content = 7;
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
                            rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item">Delete<\/a>/, '');
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
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item">Delete<\/a>/, '');
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