
var editSalesOrder = false;
var holdsalesorder = false;

function loadSO() {
    api.getbulk("/WorkOrder/AllSalesOrders").then((data) => {
        const soPendingCount = data.filter((salesOrder) => salesOrder.status === 1).length;
        $("#noOfUnplannedSO").text(soPendingCount)
        var tablebody = $("#SalesOrderList tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("SalesOrderListRow", data[i]));
        }
    }).catch((error) => {
    });
}

function PostDeliverySchedule() {

    //console.log("....PostDeliverySchedule....");
    var formName = "DeliveryScheduleForm";
    if (editSalesOrder) {
        formName = "EditSOForm";
    }
    var formData = AppUtil.GetFormData(formName);
    api.post("/businessaquisition/deliveryschedule", formData).then((data) => {

        $('#Edit-SalesOrder').modal('hide');
        loadSO();
    }).catch((error) => {
        AppUtil.HandleError("DeliveryScheduleForm", error);
    });
}

function PostSOHold() {
    //DeliveryScheduleForm
    //CustomerOrderForm
    //console.log("....PostSOHold....");
    var formData = AppUtil.GetFormData("poholdform");
    //  console.log(formData);
    api.post("/businessaquisition/solog", formData).then((data) => {
        //console.log("****PostSOHold****");
        //console.log(data);
        //console.log("****End-PostSOHold****");
        var customerOrderId = $('#AggregateCustomerOrderId').val();
        //console.log(customerOrderId);
        document.getElementById("poholdform").reset();
        holdsalesorder = false;
        loadSO();
        document.getElementById("btnholdclose").click();
    }).catch((error) => {
        AppUtil.HandleError("poholdform", error);
    });
}

function PostPOHold() {
    //DeliveryScheduleForm
    //CustomerOrderForm
    //console.log("....PostPOHold....");
    var formData = AppUtil.GetFormData("poholdform");
    //  console.log(formData);
    api.post("/businessaquisition/polog", formData).then((data) => {
        //console.log("****PostPOHold****");
        //console.log(data);
        //console.log("****End-PostPOHold****");
        document.getElementById("poholdform").reset();
       
        document.getElementById("btnholdclose").click();
    }).catch((error) => {
        AppUtil.HandleError("poholdform", error);
    });
}

$(document).ready(function () {

    loadSO();
    $("#btnClear").on("click", function () {
        $("#searchSo").val('');
        $("#searchCustomer").val('');
        $("#searchPartNo").val('');
        $("#searchPartDesc").val('');
        $("#searchStatus").val('');
        $("#searchPoNo").val('');
        $("#SocomplDtFrom").val('');
        $("#SocomplDtTo").val('');
        //loadSO();
        $("#SalesOrderList tbody tr").show();
    });
    $("#searchSo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#SalesOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchCustomer").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#SalesOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchPartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#SalesOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchPartDesc").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#SalesOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchStatus").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#SalesOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[7]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchPoNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#SalesOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#SocomplDtTo").on("change", function () {
        var fromDate = $("#SocomplDtFrom").val().split("/").reverse().join("-");
        var toDate = $("#SocomplDtTo").val().split("/").reverse().join("-");
        var fromDateTimestamp = new Date(fromDate).getTime();
        var toDateTimestamp = new Date(toDate).getTime();

        if (fromDateTimestamp > toDateTimestamp) {
            alert("So Compl Dt From Is Greater Than So Compl Dt To");
            $("#SocomplDtFrom").val('');
            $("#SocomplDtTo").val('');
            return false;
        }
        $("#SalesOrderList tbody tr").filter(function () {
            var dateText = $(this.children[11]).text(); // assuming the date is in the 3rd column
            var tableDate = dateText.split("-").reverse().join("-");

            $(this).toggle(tableDate >= fromDate && tableDate <= toDate);
        });
    });
    $('#Edit-SalesOrder').on('hidden.bs.modal', function (event) {
        editSalesOrder = false;
        $('#RequiredQuantity').val("");
        $('#DSComment').val("");
        $('#RequiredByDate').val("");
        $('#ScheduleId').val("0");
    });
    $('#Edit-SalesOrder').on('shown.bs.modal', function (event) {
        editSalesOrder = false;
        document.getElementById("EditSOForm").reset();
        var relatedTarget = $(event.relatedTarget);
        var customerOrderId = relatedTarget.data("customerorderid");
        var requiredbydatestr = relatedTarget.data("requiredbydatestr");
        var requiredquantity = relatedTarget.data("requiredquantity");
        var comment = relatedTarget.data("comment");
        var partId = relatedTarget.data("partid");
        var salesorder = relatedTarget.data("salesorder");
        var scheduleid = relatedTarget.data("salesorderid");
        document.getElementById('EditSORequiredByDate').value = requiredbydatestr.split("-").reverse().join("-");
        $('#EditSORequiredQuantity').val(requiredquantity);
        $('#EditSOComment').val(comment);
        $('#EditSOPartId').val(partId);
        $('#EditSOScheduleId').val(scheduleid);
        $('#EditSOCustomerOrderId').val(customerOrderId);
        //console.log("EditSOScheduleId: " + scheduleid);
        //console.log("EditSOComment: " + comment);
        //console.log("EditSOCustomerOrderId: " + customerOrderId);
        //console.log("EditSOPartId: " + partId);
        if (salesorder == "Y") {
            editSalesOrder = true;
        }
    });

    $("#BtnEditSO").on("click", function () {
        //alert("Add Schedule clicked");
        //if ($("#SalesCustomerOrderId").val() == "0") {
        //    alert("Please create a customer oder first.");
        //    return;
        //}
        PostDeliverySchedule();
    });

    $('#po-hold').on('hidden.bs.modal', function (event) {
        //if (holdsalesorder) {
        //    LoadSalesOrders(salesCustOrderId);
        //}
    });

    $('#po-hold').on('shown.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var customerorderid = relatedTarget.data("customerorderid");
        var salesorderid = relatedTarget.data("salesorderid");
        var salesorder = relatedTarget.data("salesorder");
        holdsalesorder = false;
        salesCustOrderId = 0;
        if (salesorder == "Y") {
            $('#POHSalesOrderId').val(salesorderid);
            holdsalesorder = true;
            salesCustOrderId = customerorderid;
        }
        else {
            $('#POHSalesOrderId').val("0");
        }
        //console.log("POHold salesorderid " + salesorderid);

        //console.log("POHold "+salesorderid);
        //POHCustomerOrderId
        $('#POHCustomerOrderId').val(customerorderid);
    });

    $("#BtnPOHold").on("click", function () {
        // alert("Add CustomerOrder clicked");
        if (holdsalesorder) {
            PostSOHold();
        }
        else {
            PostPOHold();
        }

    });
});
