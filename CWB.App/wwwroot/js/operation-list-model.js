var OperationListFormUtil = {
    UpdateFormIDs: (operationId) => {
        $("#OperationId").val(operationId);
        $("#OperationListIdForDocType").val(operationId);
    },
    UpdateOperationForm: (data) => {
        $("#OperationId").val(data.operationId);
        $("#OperationListIdForDocType").val(data.operationId);
        $("#Operation").val(data.operation);
        $("#IsMultiplePartsOfBOMUsed").prop('checked', data.isMultiplePartsOfBOMUsed).change();
        $("#IsMultipleSubCon").val(data.isMultipleSubCon);
        if (data.inhouse == 0) {
            $("#ChkIsMultipleSubCon").prop('checked', false);
        } else {
            $("#ChkIsMultipleSubCon").prop('checked', true);
        }
        if (data.subcon == 0) {
            $("#ChkIsSubcon").prop('checked', false);
        } else {
            $("#ChkIsSubcon").prop('checked', true);
        }
    },
    ClearOperationForm: () => {
        $("#OperationId").val("0");
        $("#Operation").val("");
        $("#IsMultiplePartsOfBOMUsed").prop('checked', false).change();
        $("#ChkIsMultipleSubCon").prop('checked', false);
        $("#ChkIsSubcon").prop('checked', false);
    },
    ClearOperationListDocForm: () => {
        $("#OperationDocumentTypeId").val(0);
        $("#OperationListIdForDocType").val("0");
        $("#IsOperationDocumentMandatory").prop('checked', false).change();
    },
    ResetOperationListDocForm: () => {
        $("#OperationDocumentTypeId").val(0);
        $("#IsOperationDocumentMandatory").prop('checked', false).change();
    },
    ShowOperationListDocContainer: () => {

        $("#containerOperationListDocType").show();
    },
    HideOperationListDocContainer: () => {
        $("#containerOperationListDocType").hide();
    },
    LoadOperation: (operationId) => {
        api.get("/operationlist/Operation/" + operationId).then((data) => {
            OperationListFormUtil.UpdateOperationForm(data);
            OperationListFormUtil.LoadOperationListDocs(data.operationId);
        }).catch((error) => {

        });
    },
    LoadOperationListDocs: (operationId) => {
        api.get("/operationlist/GetOperationalDocuments/" + operationId).then((data) => {
            var tablebody = $("#tbl-operation-list-doc tbody");
            $(tablebody).html("");//empty tbody
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("operationlist-doc-template", data[i]));
            }
            OperationListFormUtil.ShowOperationListDocContainer();
        }).catch((error) => {

        });

    },
    LoadOperationDocTypes: (operationId) => {
        api.get("/masters/DocTypes/" + operationId).then((data) => {
            var docTypeSelect = $("#OperationDocumentTypeId");
            $(docTypeSelect).html("");
            $(docTypeSelect).append('<option value="'+0+'">--Select Document Type--</option>');
            for (i = 0; i < data.length; i++) {
                $(docTypeSelect).append('<option value="' + data[i].documentTypeId + '">' + data[i].documentName + '</option>');
            }


        }).catch((error) => {

        });
    },
    ProcessOperationList: (operationId, IsLoadOperation) => {
        OperationListFormUtil.UpdateFormIDs(operationId);
        if (IsLoadOperation) {
            OperationListFormUtil.LoadOperation(operationId);
        }
        OperationListFormUtil.LoadOperationDocTypes(operationId);
        OperationListFormUtil.ShowOperationListDocContainer();
    },
    ProcessOperationDocumentList: (operationId) => {
        OperationListFormUtil.ResetOperationListDocForm();
        OperationListFormUtil.LoadOperationListDocs(operationId);
        OperationListFormUtil.LoadOperationDocTypes(operationId);
    }

};
$(function () {
    //Hide operation document mapping initially
    //OperationListFormUtil.HideOperationListDocContainer();
    $('#dialog-operation').on('show.bs.modal', function (event) {
        OperationListFormUtil.ClearOperationForm();
        OperationListFormUtil.ClearOperationListDocForm();
        $("#frmOperationlist").resetValidation();
        $("#frmOperationlistdoc").resetValidation();
        var relatedTarget = $(event.relatedTarget)
        var operationId = relatedTarget.data("id");
        $("#Text-Error").text("");
        if (operationId == "0") {
            $("#PopHeadName").text("Add");
            $("#ChkIsMultipleSubCon").prop('checked', true);
            var tablebody = $("#tbl-operation-list-doc tbody");
            $(tablebody).html("");//empty tbody
            //OperationListFormUtil.HideOperationListDocContainer();
            OperationListFormUtil.ProcessOperationList(0, true);
            return;
        }
        $("#PopHeadName").text("Edit");
        OperationListFormUtil.ProcessOperationList(operationId, true);

    });
    $('#dialog-operation').on('hide.bs.modal', function (event) {
        var newNamevalidate = document.getElementById('OperationDocumentTypeId');
        newNamevalidate.style.border = '';
        var newNamOperationevalidate = document.getElementById('Operation');
        newNamOperationevalidate.style.border = '';
        OperationListFormUtil.ClearOperationForm();
        OperationListFormUtil.ClearOperationListDocForm();
        api.get("/operationlist/Operations").then((data) => {
            var tablebody = $("#tbl-operation-list tbody");
            $(tablebody).html("");//empty tbody
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("operationlist-template", data[i]));
            }
            //filter once loaded
            OperationListUtil.FilterOperationList();
        }).catch((error) => {

        });
        $("#OperationListDocumentId").val(0);
    });
    $("#btnOperationSubmit").click(function () {
        var did = $("#Operation").val();
        if (did.length == 0) {
            var newNamevalidate = document.getElementById('Operation');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('Operation');
            newNamevalidate.style.border = '';
        }
        var subcon = 0;
        var inhouse = 0;
        if ($("#ChkIsMultipleSubCon").prop('checked')) {
            inhouse = 1;
        }
        if ($("#ChkIsSubcon").prop('checked')) {
            document.forms["frmOperationlist"]["Subcon"].value = 1;
        } else {
            document.forms["frmOperationlist"]["Subcon"].value = 0;
        }
        $("#IsMultipleSubCon").val(inhouse);
        //$("IsSubCon").val(subcon);
        if ($("#frmOperationlist").valid()) {
            var formData = AppUtil.GetFormData("frmOperationlist");
            api.post("/operationlist/Operation", formData).then((data) => {
                OperationListFormUtil.ProcessOperationList(data.operationId, false);
            }).catch((error) => {
                AppUtil.HandleError("frmOperationlist", error);
            });
        }
    });
    $("#btnOperationDocTypeSubmit").click(function () {

        var opId = parseInt($("#OperationListIdForDocType").val());
        if (opId == 0) {
            alert("Please Save the Operation. ");
            return false;
        }
        var did = $("#OperationDocumentTypeId").val();
        if (parseInt(did) == 0) {
            var newNamevalidate = document.getElementById('OperationDocumentTypeId');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('OperationDocumentTypeId');
            newNamevalidate.style.border = '';
        }
        if ($("#frmOperationlistdoc").valid()) {
            var formData = AppUtil.GetFormData("frmOperationlistdoc");
            var doctypeid = formData["OperationDocumentTypeId"];  //// Use quotes around the key name
            var OperationListIdForDocType = formData["OperationListIdForDocType"];  // Use quotes around the key name
            var OperationListDocumentId = formData["OperationListDocumentId"];  // Use quotes around the key name

            api.get("/operationlist/GetOperationalDocuments/" + parseInt(OperationListIdForDocType)).then((data) => {
                data = data.filter(item => item.documentTypeId == doctypeid);
                if (parseInt(OperationListDocumentId) == 0 && data.length ==1) {
                    if (data.length > 0 && data[0].documentTypeId == parseInt(doctypeid)) {
                        $("#Text-Error").text("This Entry Is Already Present In the List");
                        return false;
                    }
                }
                $("#Text-Error").text("");
                api.post("/operationlist/OperationalDocuments", formData).then((data) => {
                    OperationListFormUtil.ProcessOperationDocumentList(data.operationListIdForDocType);
                    $("#OperationListDocumentId").val(0);
                }).catch((error) => {
                    AppUtil.HandleError("frmOperationlist", error);
                });
            }).catch((error) => {

            });
        }
    });
    $("#tbl-operation-list-doc").on("click", "button.edit", function () {
        var docid = $(this).data("id");
        var doctype = $(this).data("doctype");
        var doctypeId = $(this).data("doctypeid");
        var docMandatory = $(this).data("mandatory");
        var docTypeSelect = $("#OperationDocumentTypeId");
        var docTypeoption = $(docTypeSelect).find("option[value='" + doctypeId + "'");
        if (docTypeoption.length === 0) {
            $(docTypeSelect).append('<option value="' + doctypeId + '">' + doctype + '</option>');
        }       
        $(docTypeSelect).val(doctypeId);
        $("#IsOperationDocumentMandatory").prop('checked', docMandatory).change();
        $("#OperationListDocumentId").val(docid);
    });
});
function DeletOperationDoc(element) {
    var relatedTarget = $(element);
    var masterdocid = relatedTarget.data("id");
    var doctypeId = relatedTarget.data("doctypeid");
    api.get("/masters/CheckDocTypeInDocList?docTypeid=" + doctypeId).then((data) => {
        // loadDocType(); 
        if (data) {
            let confirmval = confirm("Are your sure you want to delete this ?", "Yes", "No");
            if (confirmval) {
                api.get("/operationlist/DeletOperationDoc?opDocId=" + masterdocid).then((data) => {
                    LoadOperationListDocs();
                }).catch((error) => {

                });
            }
        } else {
            alert("Deletion of This can be done after files with the Extn are deleted from the System");
        }
    }).catch((error) => {

    });
}