var DesignationsConstants = {
    DivisionId: 0
};
var DesignationsFormUtil = {
    UpdateFormIDs: (data) => {
        //////debugger;
        $("#DesignationId").val(data.designationId);
    },
    PopulateForm: (data) => {
        //////debugger;
        $("#DesignationId").val(data.designationId);
        $("#DesignationName").val(data.designationName);
    },
    ClearForm: () => {
        $("#DesignationId").val("0");
        $("#DesignationName").val("");
    },
    HasFunction: (obj, methodName) => {
        return ((typeof obj[methodName]) == "function");
    }
};

function loadDesignation() {
    var tablebody = $("#tbl-Designations tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/designation/designations").then((data) => {
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateDataNew("designation-template", data[i], i));
        }
        //console.log(tablebody);
    }).catch((error) => { });
}

function DelDesignation(name, designationId) {
    let confirmVal = confirm("Are your sure you want to delete this Designation? : " + name, "Yes", "No");
    if (confirmVal) {
        api.get("/designation/delDesignation?designationId=" + designationId).then((data) => {
            loadDesignation();
        }).catch((error) => { });
    }
}

$(function () {

    $('#dialog-designation').on('show.bs.modal', function (event) {
        DesignationsFormUtil.ClearForm();
        var relatedTarget = $(event.relatedTarget);
        var DesignationId = relatedTarget.data("id");
        $("#Id").val(DesignationId);
        var name = relatedTarget.data("designationname");
        $("#Name").val(name);
    });

    //Search Designation --
    $("#searchDesignation").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbl-Designations tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#btnDesignationSubmit").click(function () {
        //   //debugger;
        if ($("#frmDesignation").valid()) {
            var formData = AppUtil.GetFormData("frmDesignation");
            api.post("/designation/designation", formData).then((data) => {
                DesignationsFormUtil.UpdateFormIDs(data);
                document.getElementById("frmDesignation").reset();
                document.getElementById("btnDesignationClose").click();

                loadDesignation();

            }).catch((error) => {
                AppUtil.HandleError("frmDesignation", error);
            });
        }
    });

});