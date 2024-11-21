var MachieListFormUtil = {
    ProcessTabs: (machineId, relaodTabs) => {
        $('a[href="#machine-general"]').tab('show');
        if (machineId == "0") {
            $(".machine-tab-withdata").addClass("disabled");
            return;
        }
        $(".machine-tab-withdata").removeClass("disabled");
        MachineProcDocUtil.LoadMachineDocType(machineId);
        if (relaodTabs) {
            //General Tab
            MachineGeneralFormUtil.GetMachineDetails(machineId);
            //Process Document Tab            
            MachineProcDocUtil.LoadMachineProcDocList(machineId);
        }
    },
    ProcessModelClose: () => {
        //General Tab
        MachineGeneralFormUtil.ResetGeneralForm();
        //Process Tab
        MachineProcDocUtil.ResetMachineProcDocForm();
        MachineProcDocUtil.ResetMachineProcDocType();
        //reload the list
        MachieListUtil.LoadMachineList();
    },

};
$(function () {

    $('#dialog-machine').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        MachieListFormUtil.ProcessTabs($(relatedTarget).data("id"), true);
    });

    $('#dialog-machine').on('hide.bs.modal', function (event) {
        MachieListFormUtil.ProcessModelClose();
    });

    $(".btn-machine-tab-nav").click(function () {
        var targetTab = $(this).data("targettab");
        $('a[href="#' + targetTab + '"]').tab('show');
    });
    $('.machine-tabs a[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href");
        if (target == "#machine-general") {
            $(".machine-header-info").removeClass("visible");
            $(".machine-header-info").addClass("invisible");
        } else {
            $(".machine-header-info").removeClass("invisible");
            $(".machine-header-info").addClass("visible");
        }
    });
});