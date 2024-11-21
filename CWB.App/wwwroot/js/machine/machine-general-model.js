var MachineGeneralConstants = {
    DepartmentId: 0
};
var MachineGeneralFormUtil = {
    LoadShop: (plantId) => {
        if (plantId == "") {
            var departmentSelect = $("#MachineDepartmentId");
            $(departmentSelect).html("");
            $(departmentSelect).append('<option value="">--Select Shop--</option>');
            return;
        }
        api.get("/department/getdepartments/" + plantId).then((data) => {
            var departmentSelect = $("#MachineDepartmentId");
            $(departmentSelect).html("");
            $(departmentSelect).append('<option value="">--Select Shop--</option>');
            for (i = 0; i < data.length; i++) {
                $(departmentSelect).append('<option value="' + data[i].departmentId + '">' + data[i].name + '</option>');
            }
            if (MachineGeneralConstants.DepartmentId != 0) {
                $(departmentSelect).val(MachineGeneralConstants.DepartmentId);
                MachineGeneralFormUtil.SetMachineInfoHeader();
            }
        }).catch((error) => {

        });
    },
    LoadMachineForm: (data) => {
        MachineGeneralFormUtil.LoadShop(data.machinePlantId);
        $("#MachinePlantId").val(data.machinePlantId);
        $("#MachineMachineName").val(data.machineMachineName);
        $("#MachineMachineSlNo").val(data.machineMachineSlNo);
        $("#MachineMachineManufacturer").val(data.machineMachineManufacturer);
        $("#MachineMachineTypeId").val(data.machineMachineTypeId);
        $("#MachineOperationListId").val(data.machineOperationListId);
        $("#MachineMachineId").val(data.machineMachineId);
        //set for other tabs
        $("#MachineProcDocumentMachineId").val(data.machineMachineId);
        MachineGeneralConstants.DepartmentId = data.machineDepartmentId;

    },
    GetMachineDetails: (machineId) => {
        api.get("/Machine/Machine/" + machineId).then((data) => {
            MachineGeneralFormUtil.LoadMachineForm(data);

        }).catch((error) => {

        });
    },
    SetMachineInfoHeader: () => {
        $("#machine-info-plant").text($("#MachinePlantId option:selected").text());
        $("#machine-info-department").text($("#MachineDepartmentId option:selected").text());
        $("#machine-info-machine").text($("#MachineMachineName").val());
        $("#machine-info-slno").text($("#MachineMachineSlNo").val());
    },
    ResetGeneralForm: () => {
        $("#MachinePlantId").val("");
        $("#MachineDepartmentId").val("");
        $("#MachineMachineName").val("");
        $("#MachineMachineSlNo").val("");
        $("#MachineMachineManufacturer").val("");
        $("#MachineMachineTypeId").val("");
        $("#MachineOperationListId").val("");
        $("#MachineMachineId").val(0);
        MachineGeneralConstants.DepartmentId = 0;
        $("#frmMachineGeneral").resetValidation();
        //header reset..
        $("#machine-info-plant").text("");
        $("#machine-info-department").text("");
        $("#machine-info-machine").text("");
        $("#machine-info-slno").text("");
        //reset machine Id for other tabs..
        $("#MachineProcDocumentMachineId").val(0);
    }
};
$(function () {
    $("#MachinePlantId").change(function () {
        MachineGeneralFormUtil.LoadShop($(this).val());
    });

    $("#search-machine-plant").change(function () {
        var value = $(this).find("option:selected").text().toLowerCase();
        $("#tbl-machine-list tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#search-machine-shop").change(function () {
        var value = $(this).find("option:selected").text().toLowerCase();
        $("#tbl-machine-list tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#machineName").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbl-machine-list tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#manufacturer").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbl-machine-list tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#clearMachineBtn").click(function () {
        $("#search-machine-plant").val('');
        $("#search-machine-shop").val('');
        $("#machineName").val('');
        $("#manufacturer").val('');
        $("#tbl-machine-list tbody tr").show();
    });


    $("#btnMachineGeneralSave").click(function () {
        if ($("#frmMachineGeneral").valid()) {
            var formData = AppUtil.GetFormData("frmMachineGeneral");
            api.post("/Machine/Machine", formData).then((data) => {
                $("#MachineMachineId").val(data.machineMachineId);
                //set for other Tabs..
                $("#MachineProcDocumentMachineId").val(data.machineMachineId);
                MachieListFormUtil.ProcessTabs(data.machineMachineId, false);
                MachineGeneralFormUtil.SetMachineInfoHeader();

            }).catch((error) => {
                AppUtil.HandleError("frmMachineGeneral", error);
            });
        }
    });
});