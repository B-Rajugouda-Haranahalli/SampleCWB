var ba_masterparts = {};
var schedules = new Array();
var holdsalesorder = false;
var deletesalesorder = false;
var salesCustOrderId = 0;
var editSalesOrder = false;

const OrdStatus = {
    1: "Not Planned",
    2: "Planned",
    3: "Matl Recd",
    4: "WIP",
    5: "Complete",
    6: "On Hold",
    7: "Deleted"
};

function LoadPOLines(customerOrderId) {
    api.get("/businessaquisition/getpolines?customerOrderId=" + customerOrderId).then((data) => {
        var tablebody = $("#POLinesTable tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            data[i].strStatus = OrdStatus[data[i].status];
            if (data[i].hold) {
                data[i].strHold = "Y";
            }
            else
                data[i].strHold = "N";
            if (data[i].done) {
                data[i].strDone = "Y";
            }
            else
                data[i].strDone = "N";
            //data[i].customerName = GetNameForCustomer(data[i].customerId);
            $(tablebody).append(AppUtil.ProcessTemplateData("POLineRow", data[i]));
        }
        //  console.log($(tablebody).html());
    }).catch((error) => {
    });
}

function LoadCustomerOrders() {
    
    api.get("/businessaquisition/getcustorders").then((data) => {
        var tablebody = $("#CustomerOrders tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            data[i].strStatus = OrdStatus[data[i].status];
            if (data[i].hold) {
                data[i].strHold = "Y";
            }
            else
                data[i].strHold = "N";
            if (data[i].done) {
                data[i].strDone = "Y";
            }
            else
                data[i].strDone = "N";
            //data[i].customerName = GetNameForCustomer(data[i].customerId);
            $(tablebody).append(AppUtil.ProcessTemplateData("CustomerOrdersRow", data[i]));
        }
      //  console.log($(tablebody).html());
    }).catch((error) => {
    });
}

function LoadTempForm() {
    var totalQty = 0;
    //console.log(schedules);
    for (var i = 0; i < schedules.length; i++) {
        totalQty += schedules[i].requiredQuantity;
        var partId = schedules[i].dsPartId;
        var partNo = schedules[i].partNo;
        $("#PartNo").val(partNo);
        $("#PartId").val(partId);
        //$("#PartId").trigger('change');
        $('#DSPartId').val(partId);
        $('#DSPartNo').val(partNo);
        //console.log(partId + "/" + partNo);
        document.getElementById("LaunchDeliverySchedule").disabled = false;
    }
    $('#TotalQty').val(totalQty);
}

function showSalesOrdersForPart(partId,partNo) {
    if (ba_masterparts.length == 0) {
        alert("Please load parts again...");
        return;
    }
    else {
        //alert("calling copyData");
    }
    var customerorderid = $('#AggregateCustomerOrderId').val();
    //$('#SalesCustomerOrderId').val();
    document.getElementById("DeliveryScheduleForm").reset();
    document.getElementById("AggregateObjForm").reset();
    $('#PartId').val(partId);
    $('#PartNo').val(partNo);
    $('#DSPartId').val(partId);
    $('#DSPartNo').val(partNo);
    $('#AggregateCustomerOrderId').val(customerorderid);
    $('#SalesCustomerOrderId').val(customerorderid);
    //console.log(partId + "/" + partNo);
    LoadSalesOrders(customerorderid);
    LoadDeliverySchedules($('#SalesCustomerOrderId').val());
    document.getElementById("LaunchDeliverySchedule").disabled = false;
    //document.getElementById("btnPONoDetails").click();
    $("#PONoDetailsPopup").modal('show');
}
function copyPartData() {

    if (ba_masterparts.length == 0) {
        alert("Please load parts again...");
        return;
    }
    else {
        //alert("calling copyData");
    }
    var radiochkd = $('input[name=radiopartba]:checked');
    var selval = radiochkd.val();
    var data = ba_masterparts;
    var customerorderid = $('#AggregateCustomerOrderId').val();
    //$('#SalesCustomerOrderId').val();
    
    document.getElementById("DeliveryScheduleForm").reset();
    document.getElementById("AggregateObjForm").reset();
    var partId = data[selval].partId;
     $.ajax({
            type: "GET",
            url: "/masters/CheckPartNoInDocList",
            data: { partId: partId },
            success: function (response) {
                if (!response) {
                    alert("This Part Doesnot Have Required Document.");
                    return;
                }
                else {
                    $('#PartId').val(data[selval].partId);
                    $('#PartNo').val(data[selval].partNo);
                    $('#DSPartId').val(data[selval].partId);
                    $('#DSPartNo').val(data[selval].partNo);
                    document.getElementById("LaunchDeliverySchedule").disabled = false;
                    document.getElementById("btn-close-ba-ExistingParts").click();

                    $('#AggregateCustomerOrderId').val(customerorderid);
                    $('#SalesCustomerOrderId').val(customerorderid);
                    //console.log(data[selval].partId + "/" + data[selval].partNo);
                    LoadSalesOrders(customerorderid);
                    LoadDeliverySchedules($('#SalesCustomerOrderId').val());
                }
        //PostDeliverySchedule();

               }
     });



}

function LoadSalesOrders(customerOrderId) {
    //
    GetMasterParts();
    var partId = $('#PartId').val();
    api.get("/businessaquisition/getsalesorders?customerOrderId=" + customerOrderId).then((data) => {
        var tablebody = $("#SalesOrders tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            if (data[i].partId == partId) { }
            else { continue; }

            /*if (data[i].status == 40) {
                continue;
            }*/
            //data[i].partNo = GetPartNo(data[i].partId);
            //data[i].strStatus = OrdStatus[data[i].status];
            data[i].strHold = "N";
            if (data[i].hold) {
                data[i].strHold = "Y";
            }
            data[i].strDone = "N";
            if (data[i].done) {
                data[i].strDone = "Y";
            }
            //if (data[i].status == 35)
            {
                $(tablebody).append(AppUtil.ProcessTemplateData("SalesOrderRow", data[i]));
            }
        }
        //LoadCustomerOrders();
    }).catch((error) => {
    });
}

function LoadPartsForSearching() {
    GetMasterParts();
    var tablebody = $("#tbl-ba-existingparts tbody");
    $(tablebody).html("");//empty tbody
    let i = 0;
    if (ba_masterparts.length > 0) {
        for (i = 0; i < ba_masterparts.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateDataNew("BAParts", ba_masterparts[i], i));
        }
    }
}

function LoadPOLogs(customerOderId, salesOrderId) {
    //GetMasterParts();
    api.get("/businessaquisition/getpologs?customerOrderId=" + customerOderId).then((data) => {
        //console.log(data);
        var tablebody = $("#POLogTable tbody");
        $(tablebody).html("");//empty tbody
        for (i = 0; i < data.length; i++) {
            //data[i].partNo = GetPartNo(data[i].partId);
            if (data[i].newValue == "NOTPlanned") {
                data[i].newValue = "Not Planned";
            }
            if (data[i].newValue == "OnHold") {
                data[i].newValue = "On Hold";
            }
            if (data[i].oldValue == "NOTPlanned") {
                data[i].oldValue = "Not Planned";
            }
            if (data[i].oldValue == "OnHold") {
                data[i].oldValue = "On Hold";
            }
            if (salesOrderId > 0) {
                if (data[i].partId == 0) { continue; }
            }
            $(tablebody).append(AppUtil.ProcessTemplateData("PORow", data[i]));
        }
    }).catch((error) => {
    });
}

function LoadSOAggregate(customerOrderId) {
    api.get("/businessaquisition/getsoaggregate?customerOrderId=" + customerOrderId).then((data) => {
        //console.log(data);
        $('#TotalQty').val(data.totalQty);
        $('#PartId').val(data.partId);
        $('#PartId').trigger('change');
        $('#SOComment').val(data.comment);
        $('#SOAggregateId').val(data.soAggregateId);
        $('#AggregateCustomerOrderId').val(data.customerOrderId);
    }).catch((error) => {
    });
}


function LoadDeliverySchedules(customerOrderId) {
    api.get("/businessaquisition/getschedules?customerOrderId=" + customerOrderId).then((data) => {
        //console.log(data);
        schedules = new Array();
        var tablebody = $("#DeliverySchedules tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        var partId = $('#DSPartId').val();
        for (i = 0; i < data.length; i++) {
            /*if (data[i].status == 40) {
                continue;
            }*/

            if (partId == data[i].dsPartId) { }
            else { continue; }

            schedules.push(data[i]);
            $(tablebody).append(AppUtil.ProcessTemplateData("DeliverScheduleRow", data[i]));
        }

        LoadTempForm();
    }).catch((error) => {
    });
}

function RemoveCustomerOrder(customerOderId) {
    let confirmval = confirm("Are your sure you want to delete this Order?", "Yes", "No");
    if (confirmval) {
        api.get("/businessaquisition/removecustomerorder?cutomerOrderId=" + customerOderId).then((data) => {
            //console.log(data);
            LoadCustomerOrders();
        }).catch((error) => {
        });
    }
}

function RemoveSalesOrder(salesOrderId) {
    let confirmval = confirm("Are your sure you want to delete this Order?", "Yes", "No");
    if (confirmval) {
        api.get("/businessaquisition/removesalesorder?salesOrderId=" + salesOrderId).then((data) => {
            //console.log(data);
            LoadSalesOrders(data.customerOderId);
        }).catch((error) => {
        });
    }
}

function RemoveDeliverySchedule(scheduleId) {
    let confirmval = confirm("Are your sure you want to delete this Delivery Schedule item?", "Yes", "No");
    if (confirmval) {
        api.get("/businessaquisition/removeschedule?scheduleId=" + scheduleId).then((data) => {
            //console.log(data);
            LoadDeliverySchedules($('#SalesCustomerOrderId').val());
        }).catch((error) => {
        });
    }
}

function PostSalesOrder() {
    //SalesOrderForm
    //console.log("....PostSalesOrder....");
    var formData = AppUtil.GetFormData("SalesOrderForm");
    //  console.log(formData);
    api.post("/businessaquisition/salesorder", formData).then((data) => {
        //console.log("****SalesOrder****");
        //console.log(data);
        //console.log("****End-SalesOrder****");
        //document.getElementById("Btn").click();
    }).catch((error) => {
        AppUtil.HandleError("SalesOrderForm", error);
    });
}

function PostAggregateObj() {

    /*//SalesOrderForm
    console.log("....PostAggregateObj....");
    var formData = AppUtil.GetFormData("AggregateObjForm");
    //  console.log(formData);
    api.post("/businessaquisition/soaggregate", formData).then((data) => {
        console.log("****SOAggregate****");
        console.log(data);
        console.log("****End-SOAggregate****");
        //document.getElementById("Btn").click();
    }).catch((error) => {
        AppUtil.HandleError("AggregateObjForm", error);
    });*/
    var cuoid = $('#AggregateCustomerOrderId').val();
    api.get("/businessaquisition/addsalesorders?customerOrderId=" + cuoid).then((data) => {
        //console.log(data);
        LoadSalesOrders(cuoid);
    }).catch((error) => {
    });
}

function PostCustomerOder() {
    //CustomerOrderForm
    //console.log("....PostCustomerOrder....");
    var formData = AppUtil.GetFormData("CustomerOrderForm");
    //  console.log(formData);
    api.post("/businessaquisition/customerorder", formData).then((data) => {
        //console.log("****CustomerOrder****");
        //console.log(data);
        //console.log("****End-CustomerOrder****");
        $('#AggregateCustomerOrderId').val(data.customerOrderId);
        $('#SalesCustomerOrderId').val(data.customerOrderId);
        //console.log(data.customerOrderId);
        alert("Customer Order Created");
        $('#CustomerOrderForm')[0].reset();
        $("#searchpart").prop("disabled", false);
        $("#PONoDetailsPopup").modal('show');
        //  $('#').val("");
        //$('#').val("");
        //document.getElementById("Btn").click();
    }).catch((error) => {
        AppUtil.HandleError("CustomerOrderForm", error);
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

        if (editSalesOrder) {
            var cuoid = $('#SalesCustomerOrderId').val();
            LoadSalesOrders(cuoid);
            document.getElementById("BtnEditSalesOrderClose").click();
            LoadDeliverySchedules(cuoid);
        }
        else {
            //console.log("****DeliverySchedule****");
            //console.log(data);
            //console.log("****End-DeliverySchedule****");
            var cuoid = $('#SalesCustomerOrderId').val();
            var partId = $('#DSPartId').val();
            $('#RequiredQuantity').val("");
            $('#DSComment').val("");
            $('#RequiredByDate').val("");
            $('#ScheduleId').val("0");
            //console.log("/" + cuoid);
            LoadDeliverySchedules(cuoid);
        }
    }).catch((error) => {
        AppUtil.HandleError("DeliveryScheduleForm", error);
    });
}

function PostPODelete() {
    //DeliveryScheduleForm
    //CustomerOrderForm
    //console.log("....PostPODelete....");
    var formData = AppUtil.GetFormData("podeleteform");
    //  console.log(formData);
    var endpoint = "/businessaquisition/polog";
    if (deletesalesorder) {
        endpoint = "/businessaquisition/solog";
    }

    api.post(endpoint, formData).then((data) => {
        //console.log("****PostPODelete****");
        //console.log(data);
        //console.log("****End-PostPODelete****");
        LoadCustomerOrders();
        document.getElementById("btnpodeleteclose").click();
    }).catch((error) => {
        AppUtil.HandleError("podeleteform", error);
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
        LoadCustomerOrders();
        document.getElementById("btnholdclose").click();
    }).catch((error) => {
        AppUtil.HandleError("poholdform", error);
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
        LoadSalesOrders(customerOrderId);
        holdsalesorder = false;
        document.getElementById("btnholdclose").click();
    }).catch((error) => {
        AppUtil.HandleError("poholdform", error);
    });
}
function PostPOResume() {
    //DeliveryScheduleForm
    //CustomerOrderForm
    //console.log("....PostPOResume....");
    var formData = AppUtil.GetFormData("poholdform");
    //  console.log(formData);
    api.post("/businessaquisition/polog", formData).then((data) => {
        //console.log("****PostPOResume****");
        //console.log(data);
        //console.log("****End-PostPOResume****");
        LoadCustomerOrders();
        //document.getElementById("Btn").click();
    }).catch((error) => {
        AppUtil.HandleError("poholdform", error);
    });
}

//Schedules

function SetScheduleEditVals(scheduleId,requiredByDate
    , requiredQuantity, customerOrderId, comment,dsPartId)
{
    //$('#RequiredByDate').val(requiredByDate);
    //console.log("Comment: " + comment);
    document.getElementById('RequiredByDate').value = requiredByDate.split("-").reverse().join("-");
    $('#RequiredQuantity').val(requiredQuantity);
    $('#DSComment').val(comment);
    $('#DSPartId').val(dsPartId);
    $('#ScheduleId').val(scheduleId);
    $('#SalesCustomerOrderId').val(customerOrderId);
    //RequiredByDate
    //RequiredQuantity
    //Comment
    //SalesPartId
    //ScheduleId
    //SalesCustomerOrderId
}
function DeleteSchedule(scheduleId, customerOrderId) {

}

//SalesOrders
function SetSalesOrderEditValues(customerOrderId, partId,comment)
{

}

//CustomerOrders
function AddParts() {
    //debugger;
    if (ba_masterparts.length > 0) {
        var compSelect = $('#PartId');//should be a select2 dropdown
        if (!compSelect.length)
            return;
        compSelect.empty();
        var div_data = "<option value=''></option>";
        compSelect.append(div_data);
        let i = 0;
        for (i = 0; i < ba_masterparts.length; i++) {
            div_data = "<option value='" +
                ba_masterparts[i].partId + "'>" +
                ba_masterparts[i].partNo +
                "</option>";
            compSelect.append(div_data);
        }
    }
}

function GetMasterParts() {
    if (ba_masterparts.length > 0) { }
    else {
        api.get("/masters/masterparts").then((data) => {
            ba_masterparts = data;
        }).catch((error) => {
        });
    }
}

function LoadMasterParts() {
    GetMasterParts();
    //console.log(ba_masterparts);
    if (ba_masterparts.length > 0) {
        //AddParts();
    }
}

function GetPartNo(partId) {
    for (i = 0; i < ba_masterparts.length; i++) {
        if (ba_masterparts[i].partId == partId) {
            return ba_masterparts[i].partNo;
        }
    }
    return "";
}


$(function () {
    //console.log("CustomerOrders Ready");
    var tablebody = $("#CustomerOrders tbody");
    $("#CustomerId").select2();
    loadCustomers("CustomerId");

    $(tablebody).html("");//empty tbody
    //$("#PartId").select2();
    LoadMasterParts();
    LoadCustomerOrders();

    document.getElementById("LaunchDeliverySchedule").disabled = true;

    //  LoadPOLogs();

    /* $("input[type=date]").datepicker({
         dateFormat: 'dd-MM-yyyy',
         onSelect: function (dateText, inst) {
             $(inst).val(dateText); // Write the value in the input
         }
     });
 
     // Code below to avoid the classic date-picker
     $("input[type=date]").on('click', function () {
         return false;
     });*/

    /*$("#PartId").on('change', function () {
        console.log($("#PartId option:selected").val());
        $('#DSPartId').val($("#PartId option:selected").val());
        $('#DSPartNo').val($("#PartId option:selected").text());
        if ($('#DSPartId').val().trim().length > 0) {
            document.getElementById("LaunchDeliverySchedule").disabled = false;
        }
    });*/
    $("#CustomerId").on('change', function () {
        //console.log($("#CustomerId option:selected").val());
        //CustomerName
        $('#CustomerName').val($("#CustomerId option:selected").text());
        // $('#SalesPartId').val($("#PartId option:selected").val());

    });
    $('#ba_existing-part').on('hidden.bs.modal', function (e) {
        document.getElementById('PONoDetailsPopup').style.filter = 'none';
    });
    $('#ba_existing-part').on('shown.bs.modal', function (event) {
        LoadPartsForSearching();
        document.getElementById('PONoDetailsPopup').style.filter = 'blur(5px)';
    });
    
    $('#po-Delete').on('shown.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var customerorderid = relatedTarget.data("customerorderid");
        var salesorderid = relatedTarget.data("salesorderid");
        var salesorder = relatedTarget.data("salesorder");
        deletesalesorder = false;
        salesCustOrderId = 0;
        if (salesorder == "Y") {
            $('#PODSalesOrderId').val(salesorderid);
            deletesalesorder = true;
            salesCustOrderId = customerorderid;
        }
        else {
            $('#PODSalesOrderId').val("0");
        }
        //console.log("POHold salesorderid " + salesorderid);

        //console.log("POHold " + salesorderid);
        //PODCustomerOrderId
        $('#PODCustomerOrderId').val(customerorderid);
    });

    $('#po-Delete').on('hidden.bs.modal', function (event) {
        if (deletesalesorder) {
            LoadSalesOrders(salesCustOrderId);
        }
        $('#podeleteform')[0].reset();
    });

    $('#po-hold').on('hidden.bs.modal', function (event) {
        if (holdsalesorder) {
            LoadSalesOrders(salesCustOrderId);
        }
    });

    
    //;
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

    $('#po-log').on('shown.bs.modal', function (event) {
        //LoadPOLogs
        var relatedTarget = $(event.relatedTarget);
        var customerorderid = relatedTarget.data("customerorderid");
        var salesorderid = relatedTarget.data("salesorderid");
        var salesorder = relatedTarget.data("salesorder");

        var relatedTarget = $(event.relatedTarget);
        var customerorderid = relatedTarget.data("customerorderid");
        //POHCustomerOrderId
        LoadPOLogs(customerorderid, salesorderid);
    });
    $('#Edit-SalesOrder').on('hidden.bs.modal', function (event) {
        editSalesOrder = false;
        document.getElementById('PONoDetailsPopup').style.filter = 'none';
    });
    $('#Edit-SalesOrder').on('shown.bs.modal', function (event) {
        document.getElementById('PONoDetailsPopup').style.filter = 'blur(5px)';
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
    
    $('#new-order-entry').on('shown.bs.modal', function (event) {
        loadCusomtersFromMem("CustomerId");
        LoadMasterParts();
        var tablebody = $("#SalesOrders tbody");
        $(tablebody).html("");//empty tbody
        var tablebody = $("#POLinesTable tbody");
        $(tablebody).html("");//empty tbody

        $(LaunchDeliverySchedule).prop("disabled", true);
        $("#searchpart").prop("disabled", true);
        document.getElementById("CustomerOrderForm").reset();
        document.getElementById("DeliveryScheduleForm").reset();
        document.getElementById("AggregateObjForm").reset();
        if (IsAddOpCalled()) {
            schedules = new Array();
            return;
        }
        else {
            
            var relatedTarget = $(event.relatedTarget);
            var customerorderid = relatedTarget.data("customerorderid");
            var customerId = relatedTarget.data("customerid");
            var customerName = relatedTarget.data("customername");
            var comment = relatedTarget.data("comment");
            var ordertype = relatedTarget.data("ordertype");
            var ponumber = relatedTarget.data("ponumber");
            var podate = relatedTarget.data("podate");
            var directentrydetails = relatedTarget.data("directentrydetails");
            $('#CustomerOrderId').val(customerorderid);
            $(LaunchDeliverySchedule).prop("disabled", false);
            $("#searchpart").prop("disabled", false);
            //console.log(customerorderid+"/"+customerId);
            //$("#CustomerId").select2();
            $('#CustomerId').val(customerId);
            $('#CustomerId').trigger('change');
            //console.log("comment: " + comment);
            $('#Comment').val(comment);
            if (ordertype == 1) {
                $("#POEntry").prop('checked', true);
            }
            else {
                $("#DirectEntry").prop('checked', true);
            }
            $('#PONumber').val(ponumber);
            document.getElementById('PODate').value = podate.split("-").reverse().join("-");
            $('#DirectEntryDetails').val(directentrydetails);
            $('#AggregateCustomerOrderId').val(customerorderid);
            $('#SalesCustomerOrderId').val(customerorderid);
            LoadDeliverySchedules(customerorderid);
            
            //LoadSOAggregate(customerorderid);
            $('#AggregateCustomerOrderId').val(customerorderid);
            $('#SalesCustomerOrderId').val(customerorderid);
            LoadSalesOrders(customerorderid);
            LoadPOLines(customerorderid);
        }
    });
    $('#new-order-entry').on('hidden.bs.modal', function (e) {
        LoadCustomerOrders();
    });

    $('#Delivery-Schedule').on('shown.bs.modal', function (e) {
        document.getElementById('PONoDetailsPopup').style.filter = 'blur(5px)';
        var val = $('#DSPartId').val();
        if (val == "0" || val.trim()=="") {
            alert("Please select a part.");
            document.getElementById("BtnAddScheduleClose").click();
            return;
        }

       
        schedules = new Array();
        var dspartNo = $('#DSPartNo').val();
        
        var elm = document.getElementById("PartNoToSchedule");
        elm.innerText = dspartNo;

        var tablebody = $("#DeliverySchedules tbody");
        $(tablebody).html("");//empty tbody
        if (IsAddOpCalled()) {
            return;
        }
        LoadDeliverySchedules($('#SalesCustomerOrderId').val());
    });
    $('#Delivery-Schedule').on('hidden.bs.modal', function (e) {
        document.getElementById('PONoDetailsPopup').style.filter = 'none';
        //schedules = new Array();
        LoadTempForm();
        var customerOrderId = $('#AggregateCustomerOrderId').val();
        //console.log(customerOrderId);
        LoadSalesOrders(customerOrderId);
        LoadPOLines(customerOrderId);
    });
    $("#btnWo").on("click", function () {

    });
    $("#btnlistSO").on("click", function () {
        LoadSalesOrders(1);
    });

    $("#BtnAddCustomerOrder").on("click", function () {
        // alert("Add CustomerOrder clicked");
        const podate = $("#PODate").val();
        const currentDate = new Date();
        const userDate = new Date(podate);
        if (userDate < currentDate) {
            alert('Please Enter A Date Greater Than Or Equal To Today\'s Date');
            $("#PODate").val('');
            return;
        }
       PostCustomerOder();
    });

    $("#BtnPODelete").on("click", function () {
        // alert("Add CustomerOrder clicked");
        PostPODelete();
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

    $("#BtnAddSalesOrder").on("click", function () {
      //  alert("Add Sales Order clicked");
        if ($("#AggregateCustomerOrderId").val() == "0") {
            alert("Please create a customer oder first.");
            return;
        }
        if (schedules.length == 0) {
            alert("No schedules added.");
            return;
        }
        PostAggregateObj();
        var customerOrderId = $('#AggregateCustomerOrderId').val();
        //console.log(customerOrderId);
        LoadSalesOrders(customerOrderId);
    });
    $("#BtnAddSchedule").on("click", function () {
        //alert("Add Schedule clicked");
        if ($("#SalesCustomerOrderId").val() == "0") {
            alert("Please create a customer oder first.");
            return;
        }
        var partId = $("#DSPartId").val(); // assuming DSPartId is the input field for part number
        //$.ajax({
        //    type: "GET",
        //    url: "/BusinessAquisition/CheckPartNo",
        //    data: { partId: partId },
        //    success: function (response) {
        //        if (!response) {
        //            alert("This part number already exists.");
        //            return;
        //        }
                PostDeliverySchedule();
        //    }
        //});
    });

    $("#BtnEditSO").on("click", function () {
        //alert("Add Schedule clicked");
        if ($("#SalesCustomerOrderId").val() == "0") {
            alert("Please create a customer oder first.");
            return;
        }
        PostDeliverySchedule();
    });


    $("#baeppn").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbl-ba-existingparts tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#baeppd").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#tbl-ba-existingparts tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    }); 
    $("#Search-BA-PONumber").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#CustomerOrders tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#Search-BA-Customer").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#CustomerOrders tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    }); 
    
    //Search-BA-PONumber
    //Search-BA-Customer
    
    $('#PONoDetailsPopup').on('shown.bs.modal', () => {
        document.getElementById('new-order-entry').style.filter = 'blur(5px)'; // adjust the blur value as needed
    });

    $('#PONoDetailsPopup').on('hidden.bs.modal', () => {
        document.getElementById('new-order-entry').style.filter = 'none';
    });

});