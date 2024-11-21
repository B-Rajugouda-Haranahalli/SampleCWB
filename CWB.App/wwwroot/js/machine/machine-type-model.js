var MachineTypeFormUtil = {
    ClearForm: () => {
        $("#MachineTypeName").val("");
        $("#MachineTypeTypeId").val("0");
        $("#frmMachineType").resetValidation();
    },
    UpdateMachineType: (data) => {
        var machineTypeSelect = $("#MachineMachineTypeId");
        var machineTypeOptions = $(machineTypeSelect).find("option[value='" + data.machineTypeTypeId + "']");
        if (machineTypeOptions.length == 0) {
            $(machineTypeSelect).append('<option value="' + data.machineTypeTypeId + '">' + data.machineTypeName + '</option>');
        } else {
            $(machineTypeOptions).text(data.machineTypeName);
        }
        $("#MachineMachineTypeId").val(data.machineTypeTypeId);
        $("#MachineTypeTypeId").val(data.machineTypeTypeId);
        //MachineTypeFormUtil.ClearForm();
        //$('#machine-type-dialog').modal('hide');
    },
    LoadMachineTypeForm: (isNew) => {
        var machineTypeSelect = $("#MachineMachineTypeId");
        if ($(machineTypeSelect).val() == "" || isNew) {
            MachineTypeFormUtil.ClearForm();
            var tablebody = $("#McTypeAssocDocGrid tbody");
            $(tablebody).html("");//empty tbody
            return;
        }
        $("#MachineTypeName").val($(machineTypeSelect).find("option:selected").text());
        $("#MachineTypeTypeId").val($(machineTypeSelect).val());
        LoadMcTypeDocList($(machineTypeSelect).val());
    }
};
$(function () {
    $("#btnMcTypeDocSave").click(function () {
        var mctypeid = parseInt($("#MachineTypeTypeId").val());
        if (mctypeid == 0) {
            var newNamevalidate = document.getElementById('MachineTypeName');
            newNamevalidate.style.border = '2px solid red';
            alert("Please Save the Machine Type."); 
            return false;
        } else {
            var newNamevalidate = document.getElementById('MachineTypeName');
            newNamevalidate.style.border = '';
        }
        var docTypeid = parseInt($("#McTypeDocumentTypeId").val());
        if (docTypeid == 0) {
            var newNamevalidate = document.getElementById('McTypeDocumentTypeId');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('McTypeDocumentTypeId');
            newNamevalidate.style.border = '';
        }
        var checkbox = $("#McTypeMandatory");

        if (checkbox.prop("checked")) {
            $("#McTMandatory").val('Y');
        } else {
            $("#McTMandatory").val('N');
        }
        var mandatory = $("#McTMandatory").val();
        var today = new Date();
        var deletionDate = today.toISOString().split('T')[0];
        var mcTypeDocId = parseInt($("#McTMcTypeDocListId").val());
        if (isNaN(mcTypeDocId)) {
            mcTypeDocId = 0;
        }
        var rowData = {
            mcTypeDocListId: mcTypeDocId,
            mcTypeId: mctypeid,
            documentTypeId: docTypeid,
            mandatory: mandatory,
            updatedBy: 0,
            updatedOn: deletionDate
        };
        api.getbulk("/Machine/CheckDocumentTypeInItemMaster?documentTypeId=" + docTypeid + "&mcTypeId=" + mctypeid).then((data) => {
            if (data) {
                api.post("/Machine/PostMcTypeDoc", rowData).then((data) => {
                    // console.log(data);
                    LoadMcTypeDocList(mctypeid);
                    $("#McTypeMandatory").prop("checked", false);
                    $("#McTMcTypeDocListId").val(0);
                    $("#McTypeDocumentTypeId").val(0);
                }).catch((error) => {
                });

            } else {
                $("#Text-Error").text("This Entry Is Already in The List").css('color', 'red');
            }
        }).catch((error) => {
        });
    });
    $("#btnMachineTypeSubmit").click(function () {
        if ($("#frmMachineType").valid()) {
            var formData = AppUtil.GetFormData("frmMachineType");
            api.post("/Machine/MachineType", formData).then((data) => {
                MachineTypeFormUtil.UpdateMachineType(data);
            }).catch((error) => {
                AppUtil.HandleError("frmMachineType", error);
            });
        }
    });

    $('#machine-type-dialog').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        MachineTypeFormUtil.LoadMachineTypeForm($(relatedTarget).data("isnew"));
        LoadDocTypeName();
        var newNamevalidate = document.getElementById('MachineTypeName');
        newNamevalidate.style.border = '';
        var McTypeDocumentTypeId = document.getElementById('McTypeDocumentTypeId');
        McTypeDocumentTypeId.style.border = '';
    });

    $('#machine-type-dialog').on('hide.bs.modal', function (event) {
        $("#Text-Error").text(" ");
        MachineTypeFormUtil.ClearForm();
        $("#McTypeMandatory").prop("checked", false);
        $("#McTMcTypeDocListId").val(0);
        $("#McTypeDocumentTypeId").val(0);
    });
    $("#McTypeAssocDocGrid").on("click", "button.edit", function () {
        var docid = $(this).data("id");
        var doctype = $(this).data("doctype");
        var doctypeId = $(this).data("doctypeid");
        var docMandatory = $(this).data("mandatory");
        var docTypeSelect = $("#McTypeDocumentTypeId");
        //var docTypeoption = $(docTypeSelect).find("option[value='" + doctypeId + "'");
        //if (docTypeoption.length === 0) {
        //    $(docTypeSelect).append('<option value="' + doctypeId + '">' + doctype + '</option>');
        //}
        $(docTypeSelect).val(doctypeId);
        if (docMandatory == "Y") {
            $("#McTypeMandatory").prop("checked", true);
        } else {
            $("#McTypeMandatory").prop("checked", false);
        }
        $("#McTMcTypeDocListId").val(docid);
    });
});

function LoadDocTypeName() {
    api.get("/masters/DocTypes/").then((data) => {
        var docTypeSelect = $("#McTypeDocumentTypeId");
        $(docTypeSelect).html("");
        $(docTypeSelect).append('<option value="' + 0 + '">--Select Document Type--</option>');
        for (i = 0; i < data.length; i++) {
            $(docTypeSelect).append('<option value="' + data[i].documentTypeId + '">' + data[i].documentName + '</option>');
        }
    }).catch((error) => {

    });
}
function LoadMcTypeDocList(mcTypeId) {
    var tablebody = $("#McTypeAssocDocGrid tbody");
    $(tablebody).html("");//empty tbody

    api.get("/Machine/GetMcTypeDocList?mcTypeId=" + mcTypeId).then((data) => {
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("McTypeAssocRow", data[i]));
        }
    }).catch((error) => {

    });
}

function DeleteMcTypeDoc(element) {

    var relatedTarget = $(element);
    var doclistid = relatedTarget.data("id");
    var doctypeId = relatedTarget.data("doctypeid");
    api.get("/masters/CheckDocTypeInDocList?docTypeid=" + doctypeId).then((data) => {
        if (data) {
            if (doclistid != 0) {
                var confrimval = confirm("Do You Want This Document Type.");
                if (confrimval) {
                    api.get("/Machine/DeleteMcTypeDoc?mcTypeDocListId=" + doclistid).then((data) => {
                        //console.log(data);
                        var mctypeid = parseInt($("#MachineTypeTypeId").val());
                        LoadMcTypeDocList(mctypeid);
                    }).catch((error) => {
                        //console.log(error);
                    });
                }
            } else {
                alert("This Document Type Do Not Have Document.");
            }
        } else {
            alert("Deletion of This can be done after files with the Extn are deleted from the System");
        }
    }).catch((error) => {

    });
}