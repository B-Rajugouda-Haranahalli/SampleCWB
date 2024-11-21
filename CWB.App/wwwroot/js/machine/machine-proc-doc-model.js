var MachineProcDocUtil = {
    LoadMachineProcDocList: (machineId) => {
        var newNamevalidate = document.getElementById('ProcDocTypeId');
        newNamevalidate.style.border = '';
        $("#ProcMcId").val(machineId);
        api.get("/Machine/GetMcProcDocList?mcId=" + machineId).then((data) => {
            var tablebody = $("#tbl-machine-proc-doc-list tbody");
            $(tablebody).html("");//empty tbody
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("template-machine-proc-doc", data[i]));
            }
        }).catch((error) => {

        });
    },
    LoadMachineDocType: (machineId) => {
        api.get("/masters/DocTypes/" + machineId).then((data) => {
            var docTypeSelect = $("#ProcDocTypeId");
            $(docTypeSelect).html("");
            $(docTypeSelect).append('<option value="' + 0 + '">--Select Document Type--</option>');
            for (i = 0; i < data.length; i++) {
                $(docTypeSelect).append('<option value="' + data[i].documentTypeId + '">' + data[i].documentName + '</option>');
            }
        }).catch((error) => {

        });
    },
    ResetMachineProcDocForm: () => {
        $("#MachineProcDocumentId").val(0);
        $("#MachineProcDocumentTypeId").val("");
        $("#IsMachineProcDocumentMandatory").prop('checked', false).change();
        $("#frmMachineProcsDoc").resetValidation();

    },
    ResetMachineProcDocType: () => {
        //reset table
        var tablebody = $("#tbl-machine-proc-doc-list tbody");
        $(tablebody).html("");//empty tbody
        var docTypeSelect = $("#MachineProcDocumentTypeId");
        $(docTypeSelect).html("");
    }
};

$(function () {
    $("#btnMachineProDocSubmit").click(function () {
        var mctypeid = parseInt($("#MachineMachineId").val());
        if (mctypeid == 0) {
            alert("Please Save the Machine Type.");
            return false;
        } else {
        }
        var docTypeid = parseInt($("#ProcDocTypeId").val());
        if (docTypeid == 0) {
            var newNamevalidate = document.getElementById('ProcDocTypeId');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('ProcDocTypeId');
            newNamevalidate.style.border = '';
        }
        var checkbox = $("#ProcManChK");

        if (checkbox.prop("checked")) {
            $("#ProcMan").val('Y');
        } else {
            $("#ProcMan").val('N');
        }
        var mandatory = $("#ProcMan").val();
        var today = new Date();
        var deletionDate = today.toISOString().split('T')[0];
        var mcTypeDocId = parseInt($("#ProcSlNoDocId").val());
        if (isNaN(mcTypeDocId)) {
            mcTypeDocId = 0;
        }
        var rowData = {
            mcSlNoDocListId: mcTypeDocId,
            mcId: mctypeid,
            documentTypeId: docTypeid,
            mandatory: mandatory,
            updatedBy: 0,
            updatedOn: deletionDate
        };
        api.post("/Machine/PostMcProcDoc", rowData).then((data) => {
            // console.log(data);
            MachineProcDocUtil.LoadMachineProcDocList(mctypeid);
            $("#ProcManChK").prop("checked", false);
            $("#ProcDocTypeId").val(0);
            $("#ProcSlNoDocId").val(0);
        }).catch((error) => {
        });
    });
    $("#tbl-machine-proc-doc-list").on("click", "button.edit", function () {
        var docid = $(this).data("id");
        var doctype = $(this).data("doctype");
        var doctypeId = $(this).data("doctypeid");
        var docMandatory = $(this).data("mandatory");
        var docTypeSelect = $("#ProcDocTypeId");
        //var docTypeoption = $(docTypeSelect).find("option[value='" + doctypeId + "'");
        //if (docTypeoption.length === 0) {
        //    $(docTypeSelect).append('<option value="' + doctypeId + '">' + doctype + '</option>');
        //}
        $(docTypeSelect).val(doctypeId);
        if (docMandatory == "Y") {
            $("#ProcManChK").prop("checked", true);
        } else {
            $("#ProcManChK").prop("checked", false);
        }
        $("#ProcSlNoDocId").val(docid);
    });

});
function DeleteMcProcDoc(element) {

    var relatedTarget = $(element);
    var doclistid = relatedTarget.data("id");
    var doctypeId = relatedTarget.data("doctypeid");
    api.get("/masters/CheckDocTypeInDocList?docTypeid=" + doctypeId).then((data) => {
        if (data) {
            if (doclistid != 0) {
                var confrimval = confirm("Do You Want This Document Type.");
                if (confrimval) {
                    api.get("/Machine/DeleteMcProcDoc?mcSlNoDocListId=" + doclistid).then((data) => {
                        //console.log(data);
                        var mctypeid = parseInt($("#MachineMachineId").val());
                        MachineProcDocUtil.LoadMachineProcDocList(mctypeid);
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