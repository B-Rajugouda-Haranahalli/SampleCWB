var noofWOCreation = [];

function loadWO() {
    api.getbulk("/WorkOrder/AllWorkOrders").then((data) => {
        //data = data.filter(item => item.active !== 2);
        var tablebody = $("#WorkOrderList tbody");
        $(tablebody).html("");//empty tbody


        //console.log(data);
        for (i = 0; i < data.length; i++) {
            //data[i].strStatus = WoOrdStatus[data[i].status];
            $(tablebody).append(AppUtil.ProcessTemplateData("WorkOrderListRow", data[i]));
        }
    }).catch((error) => {
    });
}

$(document).ready(function () {

    loadWO();

    $("#btnClear").on("click", function () {
        $("#searchWo").val('');
        $("#searchCustomer").val('');
        $("#searchPartNo").val('');
        $("#searchPartDesc").val('');
        $("#searchStatus").val('');
        $("#WoComplDtFrom").val('');
        $("#WoComplDtTo").val('');
        $("#WorkOrderList tbody tr").show();

    });

    $("#searchWo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#WorkOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
        });
    });

  

    $("#searchPartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#WorkOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchPartDesc").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#WorkOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchPartType").on("change", function () {
        var selectedValue = $(this).val();
        if (selectedValue == "0") {
            $("#WorkOrderList tbody tr").show();
        }
        else {

        $("#WorkOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[10]).text().toLowerCase().indexOf(selectedValue) > -1)
        });// show only the filtered rows
        }
    });

    $("#searchStatus").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#WorkOrderList tbody tr").filter(function () {
            $(this).toggle($(this.children[9]).text().toLowerCase().indexOf(value) > -1)
        });
    });

   

    $("#WoComplDtTo").on("change", function () {
        var fromDate = $("#WoComplDtFrom").val().split("/").reverse().join("-");
        var toDate = $("#WoComplDtTo").val().split("/").reverse().join("-");
        var fromDateTimestamp = new Date(fromDate).getTime();
        var toDateTimestamp = new Date(toDate).getTime();

        if (fromDateTimestamp > toDateTimestamp) {
            alert("Wo Compl Dt From Is Greater Than Wo Compl Dt To");
            $("#WoComplDtFrom").val('');
            $("#WoComplDtTo").val('');
            return false;
        }
        $("#WorkOrderList tbody tr").filter(function () {
            var dateText = $(this.children[8]).text(); // assuming the date is in the 3rd column
            var tableDate = dateText.split("-").reverse().join("-");

            $(this).toggle(tableDate >= fromDate && tableDate <= toDate);
        });
    });

    //popups code-------

    $('#routing').on('change', (e) => {
        const routeId = $(e.target).val();
        // make an API call to get data for select2 based on the selected studentId
        api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
            //console.log(data);
            const selectElement = $('#StartingOpNo');
            const selectEndOpNo = $('#EndingOpNo');
            $.each(data, (index, item) => {
                selectElement.html("");
                selectElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
            const reversedData = data.slice().reverse();
            selectEndOpNo.html('');
            $.each(reversedData, (index, item) => {
                selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
        }).catch((error) => {
            console.error(error);
        });
    });

    $("#updateWO").on("click", function () {
        var planwoqty = $('#PlanWoQty').val();
        var wonumber = $('#woNumber').text();
        var woid = $('#WOID').val();
        var soid = $('#SalesOrderId').val();
        var partid = $('#PartId').val();
        var WoComplDate = new Date(Date.parse($('#WoComplDate').val()));
        var formattedDate = WoComplDate.toISOString();
        var routingid = $("#routing").val();
        var startingOpNo = $("#StartingOpNo").val();
        var endingOpNo = $("#EndingOpNo").val();
        var soqty = $('#SoQty').val();
        var reqdate = $('#reqdate').val();
        var status = $('#woStatus').val();
        var selectedData = {};
        var rstDt = new Date(Date.parse($('#p1planComplDate').val()));
        const restrictDt = new Date(rstDt); // Get today's date

        // Check if WoComplDate is after today's date
        if (WoComplDate < restrictDt) {
            alert("Please Don't enter the previous completion date.");
            $('#WoComplDate').val(""); // Clear the invalid date
            return false; // Prevent further processing if date is invalid
        }


        if (planwoqty < soqty) {
            alert("Plan WO Qnty should be Greater than Or Equal To Sales Order Qnty");
            return false;
        }
        else {
            if (planwoqty > 0) {
                var rowData = {
                    parentWoId: parseInt(woid),
                    salesOrderId: parseInt(soid),
                    wonumber: " ",
                    partId: parseInt(partid),
                    partType: 0,
                    parentlevel: '',
                    calcWOQty: parseInt(planwoqty),
                    planCompletionDate: formattedDate,
                    routingId: parseInt(routingid),
                    startingOpNo: parseInt(startingOpNo),
                    endingOpNo: parseInt(endingOpNo),
                    status: parseInt(status)
                };

                selectedData = rowData;
                var resultData = [];

                api.post("/businessaquisition/WOpost", selectedData).then((data) => {
                    resultData.push(data);
                    var wosorel = [];
                    var wosomethod = {};
                    resultData.forEach(function (a, i) {
                        wosorel.push({
                            workOrderId: a.woid,
                            salesOrderId: a.salesOrderId,
                            active: 0
                        });
                    });
                    //requestInProgress = false;
                    api.getbulk("/WorkOrder/GetSoWo?workOrderId=" + woid).then((data) => {
                        var checkboxes = $('#multipleSO .rowMCheckbox:checked');
                        var salesOrderIds = [];
                        checkboxes.each(function () {
                            var row = $(this).closest('tr');
                            var salesOrderId = row.find('td:eq(0)').text(); // or row.find('td:eq(0)').text() if salesOrderId is in the first column
                            salesOrderIds.push(parseInt(salesOrderId));
                        });
                        const filteredData = data.filter((item) => salesOrderIds.includes(item.salesOrderId));
                        filteredData.forEach(function (a, i) {
                            wosorel.push({
                                wosoId: a.wosoId,
                                workOrderId: a.workOrderId,
                                salesOrderId: a.salesOrderId,
                                active: 2
                            });
                        });
                        wosomethod = Object.values(wosorel);
                        $.ajax({
                            type: "POST",
                            url: '/BusinessAquisition/PostWoSoRel',
                            contentType: "application/json; charset=utf-8",
                            headers: { 'Content-Type': 'application/json' },
                            data: JSON.stringify(wosomethod),
                            dataType: "json",
                            success: function (result) {
                            }
                        });

                        var bal = $('#BalQty').val();
                        var woinactive = {
                            woid: parseInt(woid),
                            salesOrderId: parseInt(soid),
                            wonumber: wonumber,
                            partId: parseInt(partid),
                            partType: 0,
                            parentlevel: '',
                            calcWOQty: parseInt(bal),
                            planCompletionDate: formattedDate,
                            routingId: parseInt(routingid),
                            startingOpNo: parseInt(startingOpNo),
                            endingOpNo: parseInt(endingOpNo),
                            status: parseInt(status),
                            active: 2
                        };
                        api.post("/businessaquisition/WOpost", woinactive).then((data) => {
                            loadWO();
                        }).catch((error) => {
                        });
                    });
                    //--

                    $("#btnPop1Close").click();
                }).catch((error) => {
                    AppUtil.HandleError("WOForm", error);
                    //console.log(error);
                });
            } else {
                alert("Please Select atleast one!");
            }
        }

    });

    $('#woc-partno').on('hidden.bs.modal', function (event) {
        loadWO();
        const selectElement = $('#StartingOpNo');
        const selectEndOpNo = $('#EndingOpNo');
        selectElement.html("");
        selectEndOpNo.html('');
        $('#routing').prop('readonly', false);
        $('#routing').css('pointer-events', '');
        $('#StartingOpNo').prop('readonly', false);
        $('#StartingOpNo').css('pointer-events', '');
        $('#EndingOpNo').prop('readonly', false);
        $('#EndingOpNo').css('pointer-events', '');
    });
    // Event listener for when the modal is shown
    $('#woc-partno').on('shown.bs.modal', function (event) {
        // Select all checkboxes in the table with the class 'rowMCheckbox'
        var soqty = $('#SoQty').val();
        var qntyonhand = $('#QtyOnHand').val();
        var balqnty = soqty - qntyonhand;
        $('#BalQty').val(balqnty);
        const checkboxes = document.querySelectorAll('#multipleSO .rowMCheckbox');

        // Define the function to update the total quantity
        function updateTotalQty() {
            let total = 0; // Initialize the total variable
            let soid = 0;
            var reqdate = "";
            // Iterate over all checkboxes
            checkboxes.forEach(checkbox => {
                if (checkbox.checked) { // Check if the checkbox is checked
                    // Traverse to the row and find the quantity in the 4th cell (index 3)
                    const row = checkbox.closest('tr'); // Using closest() for better readability
                    const qty = parseInt(row.querySelector('td:nth-child(4)').textContent, 10); // Get the quantity value from the 4th cell
                    soid = parseInt(row.querySelector('td:nth-child(1)').textContent, 10);
                    reqdate = row.querySelector('td:nth-child(7)').textContent
                    if (!isNaN(qty)) { // Ensure that the qty is a valid number
                        total += qty; // Add the quantity to the total
                    }
                }
            });

            // Update the values of the input fields with the calculated total
            if (total > 0) {
                $('#SoQty').val(total);
            }
            $('#SalesOrderId').val(soid);
            $('#reqdate').val(reqdate);
            $('#PlanWoQty').val(total);
        }
        // Calculate the initial total when the modal is first shown
        updateTotalQty();

        checkboxes.forEach(checkbox => {
            // Remove any existing event listeners to avoid duplication
            checkbox.removeEventListener('change', updateTotalQty);
            checkbox.addEventListener('change', updateTotalQty);
        });

    });

    $('#popup2').on('hidden.bs.modal', function (event) {
        var tablebody = $("#MulitpleWOs tbody");
        $(tablebody).html("");
        var $modal = $(this);
        //setTimeout(function () {
        $modal.find('input[type=radio][name=radioWO]').unbind('change');
        $modal.find('input[type=radio][name=equalwo]').unbind('change');
        $modal.find('button[id=MultipleWo]').unbind('click');
        $('input[type=radio][name=radioWO]').prop('checked', false);
        $('input[type=radio][name=equalwo]').prop('checked', false);
        $('#p2WOid').val('');
        $('#p2SalesOrderId').val('');
        $('#p2PartId').val('');
        $('#dispatchDate').val('');
        $('#equaldiv').hide();
        loadWO();
        noofWOCreation = [];
        //console.log(noofWOCreation);
        //}, 100);
    });

    $('#popup2').on('shown.bs.modal', function (event) {

        $("#MultipleWo").prop('disabled', false);
        var planwoqty = $('#p2totalSoQty').val();
        var wonumber = $('#p2WoNumber').text();
        var woid = $('#p2WOid').val();
        var soid = $('#p2SalesOrderId').val();
        var partid = $('#p2PartId').val();
        var parttype = $('#p2PartType').val();
        var wostatus = $('#p2Status').val();
        var reloadOption = $('#p2ReloadOption').val();
        var WoComplDate = new Date(Date.parse($('#p2PlanComplDate').text()));
        var Compldt = $('#p2PlanComplDate').text();
        var partNo = $('#p2PartNo').text();
        var partDesc = $('#P2partdesc').text();
        var formattedDate = WoComplDate.toISOString();
        var requestInProgress = false;
        let totalQuantity = 0;
        var balsoqnty = $('#p2BalQty').val();
        var qntyOnhand = $('#p2QtyOnHand').val();
        var balmanuf = planwoqty - qntyOnhand;
        $('#p2BaltoManuf').val(balmanuf);
        //document.querySelectorAll('#MulitpleWOs tbody tr').forEach(row => {
        //    const quantity = parseInt(row.cells[1].textContent);
        //    if (!isNaN(quantity)) {
        //        totalQuantity += quantity;
        //    }
        //});
        $('#popup2Sum').val(0);
        $('input[name="radioWO"]').prop('disabled', false);
        $('input[name="equalwo"]').prop('disabled', false);
        $("#MultipleWo").prop('disabled', false);
        $("#NewWoPopupBtn").prop('disabled', false);

        var sonumber = "";

        var parentchildwo = [];

        api.get("/WorkOrder/GetSoNumber?soid=" + soid).then(async (data) => {
            //console.log(data);
            sonumber = await data.soNumber;
            $('#p2soNumber').text(sonumber);
        });

        api.getbulk("/WorkOrder/AllParentChildWos?parentWoId=" + woid).then(async (data) => {
            //console.log(data);
            var rop = "";
            $.each(data, (index, item) => {
                parentchildwo.push(item);
                rop = item.reloadOption;
            });
            if (parentchildwo.length > 1) {
                if (rop == "Manual") {
                    $('input[name="equalwo"]').prop('disabled', true);
                    $('input[name="radioWO"]').prop('disabled', true);
                    $('input[type=radio][name="radioWO"]').eq(2).prop('checked', true);
                    $('#equaldiv').hide();
                    $("#MultipleWo").prop('disabled', true);
                    $("#NewWoPopupBtn").prop('disabled', true);
                    $("#dispatchDate").prop('disabled', true);
                    $('#popup2Sum').val(planwoqty);
                }
                else {

                    $('input[name="equalwo"]').prop('disabled', true);
                    $('input[name="radioWO"]').prop('disabled', true);
                    $('input[type=radio][name="radioWO"]').eq(1).prop('checked', true);
                    $('#equaldiv').show();
                    $("#MultipleWo").prop('disabled', true);
                    $("#NewWoPopupBtn").prop('disabled', true);
                    $("#dispatchDate").prop('disabled', true);
                    $('#popup2Sum').val(planwoqty);
                    if (rop == "EQD_D") {
                        $('input[type=radio][name="equalwo"]').eq(0).prop('checked', true);
                    } else if (rop == "EQD_W") {
                        $('input[type=radio][name="equalwo"]').eq(1).prop('checked', true);
                    } else if (rop == "EQD_M") {
                        $('input[type=radio][name="equalwo"]').eq(2).prop('checked', true);
                    }

                }
                let totalQuantity = parentchildwo.reduce((acc, current) => acc + current.calcWOQty, 0);
                $('#popup2Sum').val(totalQuantity);
                var tablebody = $("#MulitpleWOs tbody");
                $(tablebody).html("");//empty tbody

                for (i = 0; i < parentchildwo.length; i++) {
                    $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", parentchildwo[i]));
                }

            }
        });



        $('input[type=radio][name=radioWO]').change(function () {
            if (this.value == "1") {
                $('#equaldiv').hide();
            } else if (this.value == "2") {
                $('#equaldiv').show();
            } else if (this.value == "3") {
                $('#equaldiv').hide();
                $('#popup3ManualMultiple').modal('show');
                $('#p3MsoNumber').text(sonumber);
                $("#ManualpartNo").text(partNo);
                $("#P3Mpartdesc").text(partDesc);
                $("#ManualwoCompletedBy").text(Compldt);
                $("#ManualTotalSoQty").val(planwoqty);
                $("#ManualPlanWoQty").val(planwoqty);
                $("#ManualWoComplDt").val(Compldt.split("-").join("-"));
                $("#Manualwoid").val(woid);
                $("#Manualsoid").val(soid);
                $("#ManualpartId").val(partid);
                $("#ManualpartType").val(parttype);
                $("#ManualWoNumber").val(wonumber);
                $("#ManualStatus").val(wostatus);
                $('#ManualBalSoQty').val(balsoqnty);
                $('#ManualQtyOnHand').val(qntyOnhand);

            } else {
                $('#equaldiv').hide();
            }
        });

        $('input[type=radio][name=equalwo]').change(function () {
            //event.stopImmediatePropagation();
            event.stopPropagation();
            if (this.value == "1") {
                var dispatchDateInput = $('#dispatchDate');
                var dispatchDate = new Date(dispatchDateInput.val()); // assuming dispatchDate is an input field
                var completedDateElement = $('#p2PlanComplDate').text();
                var completedDate = new Date(completedDateElement.replace(/-/g, '/')); // assuming completedDate is an input field
                var maxDate = new Date(completedDate); // Set the particular date here
                var today = new Date();

                if (dispatchDate > maxDate || dispatchDate < today) {
                    alert("Please select the Dispatch Date between Todays Date and Completed By Date");
                    return false;
                }
                var dateDiff = Math.abs(completedDate - dispatchDate); // calculate the absolute difference in milliseconds
                var daysDiff = Math.ceil(dateDiff / (1000 * 3600 * 24)); // convert to days
                if (isNaN(dispatchDate.getTime())) {
                    alert("Please Select Date.");
                    $(this).prop('checked', false);
                }
                if (daysDiff > 22) {
                    alert("The number of days between dispatch date and completed date must be lesser than 22 days.");
                    $(this).prop('checked', false); // uncheck the radio button
                }
                else {
                    var dispatchDateIso = dispatchDate.toISOString();
                    var completedDateIso = completedDate.toISOString();
                    var disoption = "Daily";
                    api.get("/WorkOrder/CalculateWOQuantity?dispatchStartDate=" + dispatchDateIso + "&soCompletionDate=" + completedDateIso + "&balanceToManufacture=" + planwoqty + "&dispatchOption=" + disoption).then((data) => {
                        //console.log(data);
                        noofWOCreation.push(...data);
                    });
                }
            } else if (this.value == "2") {
                var dispatchDateInput = $('#dispatchDate');
                var dispatchDate = new Date(dispatchDateInput.val()); // assuming dispatchDate is an input field
                var completedDateElement = $('#p2PlanComplDate').text();
                var completedDate = new Date(completedDateElement.replace(/-/g, '/')); // assuming completedDate is an input field
                var maxDate = new Date(completedDate); // Set the particular date here
                var today = new Date();

                if (dispatchDate > maxDate || dispatchDate < today) {
                    alert("Please select the Dispatch Date between Todays Date and Completed By Date");
                    return false;
                }
                if (isNaN(dispatchDate.getTime())) {
                    alert("Please Select Date.");
                    $(this).prop('checked', false);
                }
                var dateDiff = Math.abs(completedDate - dispatchDate); // calculate the absolute difference in milliseconds
                var daysDiff = Math.ceil(dateDiff / (1000 * 3600 * 24)); // convert to days
                if (daysDiff <= 22) {
                    alert("The number of days between dispatch date and completed date must be greater than 22 days.");
                    $(this).prop('checked', false); // uncheck the radio button
                }
                else {
                    var dispatchDateIso = dispatchDate.toISOString();
                    var completedDateIso = completedDate.toISOString();
                    var disoption = "Weekly";
                    api.get("/WorkOrder/CalculateWOQuantity?dispatchStartDate=" + dispatchDateIso + "&soCompletionDate=" + completedDateIso + "&balanceToManufacture=" + planwoqty + "&dispatchOption=" + disoption).then((data) => {
                        //console.log(data);
                        noofWOCreation.push(...data);
                        var tablebody = $("#MulitpleWOs tbody");
                        $(tablebody).html("");//empty tbody

                        for (i = 0; i < noofWOCreation.length; i++) {
                            $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", noofWOCreation[i]));
                        }
                    });
                }
            } else if (this.value == "3") {
                var dispatchDateInput = $('#dispatchDate');
                var dispatchDate = new Date(dispatchDateInput.val()); // assuming dispatchDate is an input field
                var completedDateElement = $('#p2PlanComplDate').text();
                var completedDate = new Date(completedDateElement.replace(/-/g, '/')); // assuming completedDate is an input field
                var maxDate = new Date(completedDate); // Set the particular date here
                var today = new Date();

                if (dispatchDate > maxDate || dispatchDate < today) {
                    alert("Please select the Dispatch Date between Todays Date and Completed By Date");
                    return false;
                }
                if (isNaN(dispatchDate.getTime())) {
                    alert("Please Select Date.");
                    $(this).prop('checked', false);
                }
                var dateDiff = Math.abs(completedDate - dispatchDate); // calculate the absolute difference in milliseconds
                var daysDiff = Math.ceil(dateDiff / (1000 * 3600 * 24)); // convert to days
                if (daysDiff <= 95) {
                    alert("The number of days between dispatch date and completed date must be greater than 95days.");
                    $(this).prop('checked', false); // uncheck the radio button
                }
                else {
                    var dispatchDateIso = dispatchDate.toISOString();
                    var completedDateIso = completedDate.toISOString();
                    var disoption = "Monthly";
                    api.get("/WorkOrder/CalculateWOQuantity?dispatchStartDate=" + dispatchDateIso + "&soCompletionDate=" + completedDateIso + "&balanceToManufacture=" + planwoqty + "&dispatchOption=" + disoption).then((data) => {
                        //console.log(data);
                        noofWOCreation.push(...data);
                    });
                }

            } else {

            }
        });

        $("#MultipleWo").on("click", function () {
            const selectedValue = $('input[name="equalwo"]:checked').val();
            var eq = "";
            if (selectedValue == "1") {
                eq = "D";
            }
            else if (selectedValue == "2") {
                eq = "W";
            }
            else {
                eq = "M";
            }
            let result = confirm("Are You Sure You Want To Proceed?");
            if (result) {

            } else {
                return false;
            }
            //if (requestInProgress) return;
            //requestInProgress = true;
            noofWOCreation.forEach((wo) => {
                wo.partId = parseInt(partid);
                wo.salesOrderId = parseInt(soid);
                wo.parentWoId = parseInt(woid);
                wo.reloadOption = "EQD_" + eq;
            });
            var tdata = [];
            if (noofWOCreation.length > 1) {
                $.ajax({
                    type: "POST",
                    url: '/BusinessAquisition/MultipleWOPost',
                    contentType: "application/json; charset=utf-8",
                    headers: { 'Content-Type': 'application/json' },
                    data: JSON.stringify(noofWOCreation),
                    dataType: "json",
                    success: function (result) {
                        //console.log(result);
                        tdata.push(...result);
                        var tablebody = $("#MulitpleWOs tbody");
                        $(tablebody).html("");//empty tbody

                        for (i = 0; i < tdata.length; i++) {
                            $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", tdata[i]));
                        }
                        var wosorel = [];
                        var wosomethod = {};
                        tdata.forEach(function (a, i) {
                            wosorel.push({
                                workOrderId: a.woid,
                                salesOrderId: a.salesOrderId
                            });
                        });
                        requestInProgress = false;
                        wosomethod = Object.values(wosorel);
                        //--
                        $.ajax({
                            type: "POST",
                            url: '/BusinessAquisition/PostWoSoRel',
                            contentType: "application/json; charset=utf-8",
                            headers: { 'Content-Type': 'application/json' },
                            data: JSON.stringify(wosomethod),
                            dataType: "json",
                            success: function (result) {
                                window.locationre = result.url;
                                noofWOCreation = [];
                                $("#MultipleWo").prop('disabled', true);
                                $('input[name="equalwo"]').prop('disabled', true);
                                $('input[name="radioWO"]').prop('disabled', true);
                                $('input[type=radio][name="radioWO"]').eq(1).prop('checked', true);
                                $('#equaldiv').show();
                                $("#MultipleWo").prop('disabled', true);
                                $("#NewWoPopupBtn").prop('disabled', true);
                                $("#dispatchDate").prop('disabled', true);
                                requestInProgress = false;
                                $('#popup2Sum').val(planwoqty);
                            }
                        });
                        var woinactive = {
                            woid: parseInt(woid),
                            salesOrderId: parseInt(soid),
                            wonumber: wonumber,
                            partId: parseInt(partid),
                            partType: parseInt(parttype),
                            parentlevel: '',
                            calcWOQty: parseInt(planwoqty),
                            planCompletionDate: formattedDate,
                            routingId: parseInt(0),
                            startingOpNo: parseInt(0),
                            endingOpNo: parseInt(0),
                            status: parseInt(1),
                            active: 2
                        };
                        api.post("/businessaquisition/WOpost", woinactive).then((data) => {
                            //console.log(data);
                            //$('#popup3').modal('hide');
                            //reloadWO(reloadOption, partid);
                            loadWO();
                        }).catch((error) => {
                        });

                    }
                });
            }


        });

        /*
        //var tdata = [];
        //var wodt = $('#p2PlanComplDate').text();
        //if (reloadOption == "EQD_D") {
        //    $("#MultipleWo").prop('disabled', true);
        //    $("#NewWoPopupBtn").prop('disabled', true);
        //    $('input[type="radio"]').prop('disabled', true);
        //    $('input[name="equalwo"]').prop('disabled', true);
        //    // Then show the 1st radio btn checked
        //    $('input[type=radio][name="equalwo"]').eq(0).prop('checked', true);
        //    var trowdata = {
        //        active: 0,
        //        buildToStock: "\u0000",
        //        calcWOQty: planwoqty,
        //        comment: null,
        //        endingOpNo: 0,
        //        for_Ref: "\u0000",
        //        parentlevel: "N",
        //        partDesc: "",
        //        partId: parseInt(partid),
        //        partType: parseInt(parttype),
        //        partNo: "",
        //        planCompletionDateStr: wodt.split("-").reverse().join("-").join("-"),
        //        reloadOption: reloadOption,
        //        routingId: 0,
        //        salesOrderId: parseInt(soid),
        //        startingOpNo: 0,
        //        status: parseInt(wostatus),
        //        tenantId: 0,
        //        testData: "Y",
        //        woDate: null,
        //        woDateStr: "",
        //        woNumber: wonumber,
        //        woid: parseInt(woid)
        //    };

        //    tdata.push(trowdata);
        //    var tablebody = $("#MulitpleWOs tbody");
        //    $(tablebody).html("");//empty tbody

        //    for (i = 0; i < tdata.length; i++) {
        //        $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", tdata[i]));
        //    }

        //}
        //else if (reloadOption == "EQD_W") {
        //    $("#MultipleWo").prop('disabled', true);
        //    $("#NewWoPopupBtn").prop('disabled', true);
        //    $('input[type="radio"]').prop('disabled', true);
        //    $('input[name="equalwo"]').prop('disabled', true);
        //    // Then show the 1st radio btn checked
        //    $('input[type=radio][name="equalwo"]').eq(1).prop('checked', true);

        //    var trowdata = {
        //        active: 0,
        //        buildToStock: "\u0000",
        //        calcWOQty: planwoqty,
        //        comment: null,
        //        endingOpNo: 0,
        //        for_Ref: "\u0000",
        //        parentlevel: "N",
        //        partDesc: "",
        //        partId: parseInt(partid),
        //        partType: parseInt(parttype),
        //        partNo: "",
        //        planCompletionDateStr: wodt.split("-").reverse().join("-"),
        //        reloadOption: reloadOption,
        //        routingId: 0,
        //        salesOrderId: parseInt(soid),
        //        startingOpNo: 0,
        //        status: parseInt(wostatus),
        //        tenantId: 0,
        //        testData: "Y",
        //        woDate: null,
        //        woDateStr: "",
        //        woNumber: wonumber,
        //        woid: parseInt(woid)
        //    };

        //    tdata.push(trowdata);
        //    var tablebody = $("#MulitpleWOs tbody");
        //    $(tablebody).html("");//empty tbody

        //    for (i = 0; i < tdata.length; i++) {
        //        $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", tdata[i]));
        //    }

        //}
        //else if (reloadOption == "EQD_M") {
        //    $("#MultipleWo").prop('disabled', true);
        //    $("#NewWoPopupBtn").prop('disabled', true);
        //    $('input[type="radio"]').prop('disabled', true);
        //    $('input[name="equalwo"]').prop('disabled', true);
        //    // Then show the 1st radio btn checked
        //    $('input[type=radio][name="equalwo"]').eq(2).prop('checked', true);

        //    var trowdata = {
        //        active: 0,
        //        buildToStock: "\u0000",
        //        calcWOQty: planwoqty,
        //        comment: null,
        //        endingOpNo: 0,
        //        for_Ref: "\u0000",
        //        parentlevel: "N",
        //        partDesc: "",
        //        partId: parseInt(partid),
        //        partType: parseInt(parttype),
        //        partNo: "",
        //        planCompletionDateStr: wodt.split("-").reverse().join("-"),
        //        reloadOption: reloadOption,
        //        routingId: 0,
        //        salesOrderId: parseInt(soid),
        //        startingOpNo: 0,
        //        status: parseInt(wostatus),
        //        tenantId: 0,
        //        testData: "Y",
        //        woDate: null,
        //        woDateStr: "",
        //        woNumber: wonumber,
        //        woid: parseInt(woid)
        //    };

        //    tdata.push(trowdata);
        //    var tablebody = $("#MulitpleWOs tbody");
        //    $(tablebody).html("");//empty tbody

        //    for (i = 0; i < tdata.length; i++) {
        //        $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", tdata[i]));
        //    }

        //}
        */

    });

    $('#popup3').on('shown.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var workOrderId = relatedTarget.data("workorderid");
        var salesOrderId = relatedTarget.data("salesorderid");
        var woNumber = relatedTarget.data("wonumber");
        var partNo = relatedTarget.data("partno");
        var planCompletionDateStr = relatedTarget.data("plancompletiondatestr");
        var partId = relatedTarget.data("partid");
        var partType = relatedTarget.data("parttype");
        var planWOQty = relatedTarget.data("calwoqty");
        var wostatus = relatedTarget.data("ptstatus");
        var reloadopt = relatedTarget.data("nreloadoption");
        var partno = $("#p2PartNo").text();
        var partDesc = $("#p2PartNo").text();
        var sonumber = $("#p2soNumber").text();
        $("#p3soNumber").text(sonumber);
        $("#p3partNo").text(partno);
        $("#P3partdesc").text(partDesc);
        $("#woCompletedBy").text(planCompletionDateStr.split("-").reverse().join("-"));
        $("#singleTotalSoQty").val(planWOQty);
        $("#singlePlanWoQty").val(planWOQty);
        $("#woid").val(workOrderId);
        $("#soid").val(salesOrderId);
        $("#partId").val(partId);
        $("#partType").val(partType);
        $("#p3Status").val(wostatus);
        $("#singleWoNumber").val(woNumber);
        $("#p3ReloadOption").val(reloadopt);
        $("#singleWoComplDt").val(planCompletionDateStr.split("-").reverse().join("-"));
        $("#singleBalSoQty").val(0);
        $("#singleQtyOnHand").val(0);
        $("#singleNoofWoReleased").val(0);
        $("#singleSumWoQnty").val(0);
        $("#singleBalWoQty").val(0);

        var balmanufqnty = planWOQty - 0;

        $("#singleBalToManuf").val(balmanufqnty);

        if (partType === 1) {
            $("#popup3divRouting").show().addClass("row");
            api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + parseInt(partId)).then((data) => {
                //console.log(data);
                const selectElement = $('#singleRouting');
                selectElement.prop("disabled", false);
                selectElement.html("");
                if (data.length === 1) {
                    $('#singleRouting').prop('readonly', true);
                    $('#singleRouting').css('pointer-events', 'none');
                    $.each(data, (index, item) => {
                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                    });
                    var routeId = $('#singleRouting').val();
                    $('#singleStartOpNo').prop('readonly', true);
                    $('#singleStartOpNo').css('pointer-events', 'none');
                    $('#singleEndOpNo').prop('readonly', true);
                    $('#singleEndOpNo').css('pointer-events', 'none');
                    api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
                        //console.log(data);
                        const selectstartElement = $('#singleStartOpNo');
                        const selectEndOpNo = $('#singleEndOpNo');
                        selectstartElement.html("");
                        $.each(data, (index, item) => {
                            selectstartElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                        });
                        const reversedData = data.slice().reverse();
                        selectEndOpNo.html('');
                        $.each(reversedData, (index, item) => {
                            selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                        });
                    }).catch((error) => {
                        console.error(error);
                    });
                } else {

                    selectElement.append(`<option value="0">--Select--</option>`);
                    $.each(data, (index, item) => {
                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                    });
                }

            }).catch((error) => {
            });
            $('#singleStartOpNo').prop("disabled", false);
            $('#singleEndOpNo').prop("disabled", false);
        }
        else {

            $("#popup3divRouting").hide();
            const selectElement = $('#singleRouting');
            selectElement.html("");
            selectElement.prop("disabled", true);
            $('#singleStartOpNo').html("").prop("disabled", true);
            $('#singleEndOpNo').html("").prop("disabled", true);
        }



    });

    $('#popup3').on('hidden.bs.modal', function (event) {
        const selectElement = $('#singleStartOpNo');
        const selectEndOpNo = $('#singleEndOpNo');
        selectElement.html("");
        selectEndOpNo.html('');
        $('#singleRouting').prop('readonly', false);
        $('#singleRouting').css('pointer-events', '');
        $('#singleStartOpNo').prop('readonly', false);
        $('#singleStartOpNo').css('pointer-events', '');
        $('#singleEndOpNo').prop('readonly', false);
        $('#singleEndOpNo').css('pointer-events', '');

    });

    $('#singleRouting').on('change', (e) => {
        const routeId = $(e.target).val();
        // make an API call to get data for select2 based on the selected studentId

        api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
            //console.log(data);
            //const uniqueData = [...new Set(data.map(item => item.stepOperation))];
            const uniqueData = data.filter((item, index, self) =>
                self.findIndex((t) => t.stepOperation === item.stepOperation) === index
            );
            const selectElement = $('#singleStartOpNo');
            const selectEndOpNo = $('#singleEndOpNo');
            selectElement.html("");
            $.each(uniqueData, (index, item) => {
                selectElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
            const reversedData = uniqueData.slice().reverse();
            selectEndOpNo.html('');
            $.each(reversedData, (index, item) => {
                selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
        }).catch((error) => {
            console.error(error);
        });

    });

    $("#singleSaveWo").on("click", function () {
        var woid = parseInt($("#woid").val());
        var soid = parseInt($("#soid").val());
        var partid = parseInt($("#partId").val());
        var soqty = parseInt($("#singleTotalSoQty").val());
        var parttype = parseInt($("#partType").val());
        var planWoQty = parseInt($("#singlePlanWoQty").val());
        var wonumber = $("#singleWoNumber").val();
        var WoComplDate = new Date(Date.parse($('#singleWoComplDt').val()));
        var formattedDate = WoComplDate.toISOString();
        var routingid = $("#singleRouting").val();
        var startingOpNo = $("#singleStartOpNo").val();
        var endingOpNo = $("#singleEndOpNo").val();
        var status = parseInt($("#p3Status").val());
        var reloadOption = $("#p3ReloadOption").val();
        const woCompletedByText = $('#woCompletedBy').text();
        const rstDtParts = woCompletedByText.split('-');
        const rstDt = new Date(rstDtParts[2], rstDtParts[1] - 1, rstDtParts[0]);
        const restrictDt = new Date(rstDt);
        if (isNaN(WoComplDate.getTime())) {
            alert("Please Enter the WO Compl Date.");
            return false;
            // or display an error message to the user
        } else if (WoComplDate < restrictDt) {
            alert("Wo Complition Date Should Be Greater Than Current Complition Date");
            return false;
            // or display an error message to the user
        }
        else {
            formattedDate = WoComplDate.toISOString();
        }

        if (planWoQty < soqty) {
            alert("Plan Wo Qnty Should be Greater or Equal to Total So Qnty.");
            return false;
        }

        var rowData = {
            woid: parseInt(woid),
            salesOrderId: parseInt(soid),
            wonumber: wonumber,
            partId: parseInt(partid),
            partType: parseInt(parttype),
            parentlevel: '',
            calcWOQty: parseInt(planWoQty),
            planCompletionDate: formattedDate,
            routingId: parseInt(routingid),
            startingOpNo: parseInt(startingOpNo),
            endingOpNo: parseInt(endingOpNo),
            reloadOption: reloadOption,
            status: parseInt(status)
        };

        api.post("/businessaquisition/WOpost", rowData).then((data) => {
            //console.log(data);
            $('#popup3').modal('hide');
            reloadWO(reloadOption, partid);
        }).catch((error) => {
        });

    });

    $('#popup3ManualMultiple').on('shown.bs.modal', function (event) {
        var partType = $("#ManualpartType").val();
        var partId = $("#ManualpartId").val();
        var totalsoqnty = $('#ManualTotalSoQty').val();
        var balSoqnty = $('#ManualBalSoQty').val();
        var QntyOnhand = $('#ManualQtyOnHand').val();
        var balmanuf = totalsoqnty - QntyOnhand;
        $('#ManualBalToManuf').val(balmanuf);
        $('#ManualNoofWoReleased').val(0);
        $('#ManualSumWoQnty').val(0);
        $('#ManualBalWoQty').val(0);
        if (partType == "1") {
            $("#popup3ManualdivRouting").show().addClass("row");
            api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + parseInt(partId)).then((data) => {
                //console.log(data);
                const selectElement = $('#ManualRouting');
                selectElement.prop("disabled", false);
                selectElement.html("");
                if (data.length === 1) {
                    $('#ManualRouting').prop('readonly', true);
                    $('#ManualRouting').css('pointer-events', 'none');
                    $.each(data, (index, item) => {
                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                    });
                    var routeId = $('#ManualRouting').val();
                    $('#ManualStartOpNo').prop('readonly', true);
                    $('#ManualStartOpNo').css('pointer-events', 'none');
                    $('#ManualEndOpNo').prop('readonly', true);
                    $('#ManualEndOpNo').css('pointer-events', 'none');
                    api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
                        //console.log(data);
                        const selectstartElement = $('#ManualStartOpNo');
                        const selectEndOpNo = $('#ManualEndOpNo');
                        selectstartElement.html("");
                        $.each(data, (index, item) => {
                            selectstartElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                        });
                        const reversedData = data.slice().reverse();
                        selectEndOpNo.html('');
                        $.each(reversedData, (index, item) => {
                            selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                        });
                    }).catch((error) => {
                        console.error(error);
                    });
                } else {

                    selectElement.append(`<option value="0">--Select--</option>`);
                    $.each(data, (index, item) => {
                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                    });
                }

            }).catch((error) => {
            });
            $('#ManualStartOpNo').prop("disabled", false);
            $('#ManualEndOpNo').prop("disabled", false);
        }
        else {
            $("#popup3ManualdivRouting").hide()
            const selectElement = $('#routing');
            selectElement.html("");
            selectElement.prop("disabled", true);
            $('#ManualStartOpNo').html("").prop("disabled", true);
            $('#ManualEndOpNo').html("").prop("disabled", true);
        }
    });

    $('#ManualRouting').on('change', (e) => {
        const routeId = $(e.target).val();
        // make an API call to get data for select2 based on the selected studentId
        api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
            //console.log(data);
            //const uniqueData = [...new Set(data.map(item => item.stepOperation))];
            const uniqueData = data.filter((item, index, self) =>
                self.findIndex((t) => t.stepOperation === item.stepOperation) === index
            );
            const selectElement = $('#ManualStartOpNo');
            const selectEndOpNo = $('#ManualEndOpNo');
            selectElement.html("");
            $.each(uniqueData, (index, item) => {
                selectElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
            const reversedData = uniqueData.slice().reverse();
            selectEndOpNo.html('');
            $.each(reversedData, (index, item) => {
                selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
        }).catch((error) => {
            //console.error(error);
        });
    });

    $('#popup3ManualMultiple').on('hidden.bs.modal', function (event) {
        var $modal = $(this);
        //$modal.find('button[id=ManualSaveWo]').unbind('click');
        $('#ManualRouting').prop('readonly', false);
        $('#ManualRouting').css('pointer-events', '');
        $('#ManualStartOpNo').prop('readonly', false);
        $('#ManualStartOpNo').css('pointer-events', '');
        $('#ManualEndOpNo').prop('readonly', false);
        $('#ManualEndOpNo').css('pointer-events', '');
        const selectElement = $('#ManualStartOpNo');
        const selectEndOpNo = $('#ManualEndOpNo');
        selectElement.html("");
        selectEndOpNo.html('');
    });

    $("#ManualSaveWo").on("click", function () {
        var woid = parseInt($("#Manualwoid").val());
        var soid = parseInt($("#Manualsoid").val());
        var partid = parseInt($("#ManualpartId").val());
        var parttype = parseInt($("#ManualpartType").val());
        var wostatus = parseInt($("#ManualStatus").val());
        var soqty = parseInt($("#ManualTotalSoQty").val());
        var planWoQty = parseInt($("#ManualPlanWoQty").val());
        var wonumber = $("#ManualWoNumber").val();
        var WoComplDate = new Date(Date.parse($('#ManualWoComplDt').val()));
        var formattedDate;
        var routingid = $("#ManualRouting").val();
        var startingOpNo = $("#ManualStartOpNo").val();
        var endingOpNo = $("#ManualEndOpNo").val();
        var resultData = [];
        var rstDt = new Date(Date.parse($('#ManualwoCompletedBy').text()));
        const restrictDt = new Date(rstDt);
        if (isNaN(WoComplDate.getTime())) {
            alert("Please Enter the WO Compl Date.");
            return false;
            // or display an error message to the user
        }
        if (WoComplDate < restrictDt) {
            alert("Wo Complition Date Should Be Greater Than Current Complition Date");
            return false;
            // or display an error message to the user
        } else {
            formattedDate = WoComplDate.toISOString();
        }

        if (planWoQty < soqty) {
            alert("Plan Wo Qnty Should be Greater or Equal to So Total So Qnty.");
            return false;
        }

        var rowData = {
            parentWoId: parseInt(woid),
            salesOrderId: parseInt(soid),
            wonumber: "",
            partId: parseInt(partid),
            partType: parseInt(parttype),
            parentlevel: '',
            calcWOQty: parseInt(planWoQty),
            reloadOption: "Manual",
            planCompletionDate: formattedDate,
            routingId: parseInt(routingid),
            startingOpNo: parseInt(startingOpNo),
            endingOpNo: parseInt(endingOpNo),
            status: parseInt(wostatus)
        };

        api.post("/businessaquisition/WOpost", rowData).then((data) => {
            //console.log(data);
            resultData.push(data);
            $('#popup3ManualMultiple').modal('hide');
            //$('#popup2Sum').val(planwoqty);
            var tablebody = $("#MulitpleWOs tbody");
            $(tablebody).html("");//empty tbody

            for (i = 0; i < resultData.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", resultData[i]));
            }
            $('input[name="equalwo"]').prop('disabled', true);
            $('input[name="radioWO"]').prop('disabled', true);
            $('input[type=radio][name="radioWO"]').eq(2).prop('checked', true);
            $('#equaldiv').hide();
            $("#MultipleWo").prop('disabled', true);
            $("#NewWoPopupBtn").prop('disabled', true);
            $("#dispatchDate").prop('disabled', true);
            $('#popup2Sum').val(planwoqty);
            var wosorel = [];
            var wosomethod = {};
            resultData.forEach(function (a, i) {
                wosorel.push({
                    workOrderId: a.woid,
                    salesOrderId: a.salesOrderId
                });
            });
            //requestInProgress = false;
            wosomethod = Object.values(wosorel);
            //--
            $.ajax({
                type: "POST",
                url: '/BusinessAquisition/PostWoSoRel',
                contentType: "application/json; charset=utf-8",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(wosomethod),
                dataType: "json",
                success: function (result) {
                   
                }
            });
            var woinactive = {
                woid: parseInt(woid),
                salesOrderId: parseInt(soid),
                wonumber: wonumber,
                partId: parseInt(partid),
                partType: parseInt(parttype),
                parentlevel: '',
                calcWOQty: parseInt(planwoqty),
                planCompletionDate: formattedDate,
                routingId: parseInt(0),
                startingOpNo: parseInt(0),
                endingOpNo: parseInt(0),
                status: parseInt(1),
                active: 2
            };
            api.post("/businessaquisition/WOpost", woinactive).then((data) => {
               
                loadWO();
            }).catch((error) => {
            });
        }).catch((error) => {
        });
    });

    $("#NewWoPopupBtn").on("click", function () {
        var planwoqty = $('#p2totalSoQty').val();
        var wonumber = $('#p2WoNumber').text();
        var sonumber = $('#p2soNumber').text();
        var woid = $('#p2WOid').val();
        var soid = $('#p2SalesOrderId').val();
        var partid = $('#p2PartId').val();
        var parttype = $('#p2PartType').val();
        var wostatus = $('#p2Status').val();
        var WoComplDate = new Date(Date.parse($('#p2PlanComplDate').text()));
        var Compldt = $('#p2PlanComplDate').text();
        var partNo = $('#p2PartNo').text();
        var partDesc = $('#P2partdesc').text();
        var formattedDate = WoComplDate.toISOString();
        var balsoqnty = $('#p2BalQty').val();
        var qntyOnhand = $('#p2QtyOnHand').val();

        $('#popup3NewWo').modal('show');
        $("#NewpartNo").text(partNo);
        $("#P3Npartdesc").text(partDesc);
        $("#p3NsoNumber").text(sonumber);
        $("#NewwoCompletedBy").text(Compldt);
        $("#NewTotalSoQty").val(planwoqty);
        $("#NewPlanWoQty").val(planwoqty);
        $("#Newwoid").val(woid);
        $("#Newsoid").val(soid);
        $("#NewpartId").val(partid);
        $("#NewpartType").val(parttype);
        $("#NewWoStatus").val(wostatus);
        $("#NewWoNumber").val(wonumber);
        $("#NewBalSoQty").val(balsoqnty);
        $("#NewQtyOnHand").val(qntyOnhand);
        $("#NewWoComplDt").val(Compldt.split("-").join("-"));
        document.getElementById("NewSaveWo").textContent = "Generate WO";
    });

    $("#NewSaveWo").on("click", function () {
        var woid = parseInt($("#Newwoid").val());
        var soid = parseInt($("#Newsoid").val());
        var partid = parseInt($("#NewpartId").val());
        var parttype = parseInt($("#NewpartType").val());
        var wostatus = parseInt($("#NewWoStatus").val());
        var planWoQty = parseInt($("#NewPlanWoQty").val());
        var soqty = parseInt($("#NewTotalSoQty").val());
        var wonumber = $("#NewWoNumber").val();
        var WoComplDate = new Date(Date.parse($('#NewWoComplDt').val()));
        var formattedDate;
        var routingid = $("#NewRouting").val();
        var startingOpNo = $("#NewStartOpNo").val();
        var endingOpNo = $("#NewEndOpNo").val();
        var sreloadOption = $("#p3NReloadOption").val();
        var reloadOpt = "New";
        var resultData = [];
        var rstDt = new Date(Date.parse($('#NewwoCompletedBy').text()));
        const restrictDt = new Date(rstDt);
        if (planWoQty < soqty) {
            alert("Plan Wo Qnty Should be Greater or Equal to So Total So Qnty.");
            return false;
        }
        if (WoComplDate < restrictDt) {
            alert("Wo Complition Date Should Be Greater Than Current Complition Date");
            return false;
            // or display an error message to the user
        } else {
            formattedDate = WoComplDate.toISOString();
        }
        if (sreloadOption) {
            reloadOpt = sreloadOption;
        }
        var rowData = {
            woid: parseInt(woid),
            salesOrderId: parseInt(soid),
            wonumber: wonumber,
            partId: parseInt(partid),
            partType: parseInt(parttype),
            parentlevel: '',
            calcWOQty: parseInt(planWoQty),
            planCompletionDate: formattedDate,
            reloadOption: reloadOpt,
            routingId: parseInt(routingid),
            startingOpNo: parseInt(startingOpNo),
            endingOpNo: parseInt(endingOpNo),
            status: parseInt(wostatus)
        };

        api.post("/businessaquisition/WOpost", rowData).then((data) => {
            //console.log(data);
            resultData.push(data);
            $('#popup3NewWo').modal('hide');
            var tablebody = $("#MulitpleWOs tbody");
            $(tablebody).html("");//empty tbody

            for (i = 0; i < resultData.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", resultData[i]));
            }
            loadWO();
        }).catch((error) => {
        });

    });

    $('#NewRouting').on('change', (e) => {
        const routeId = $(e.target).val();

        api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
            //console.log(data);
            //const uniqueData = [...new Set(data.map(item => item.stepOperation))];
            const uniqueData = data.filter((item, index, self) =>
                self.findIndex((t) => t.stepOperation === item.stepOperation) === index
            );
            const selectElement = $('#NewStartOpNo');
            const selectEndOpNo = $('#NewEndOpNo');
            selectElement.html("");
            $.each(uniqueData, (index, item) => {
                selectElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
            const reversedData = uniqueData.slice().reverse();
            selectEndOpNo.html('');
            $.each(reversedData, (index, item) => {
                selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
        }).catch((error) => {
            //console.error(error);
        });
    });
    $('#popup3NewWo').on('hidden.bs.modal', function (event) {
        var $modal = $(this);
        //$modal.find('button[id=ManualSaveWo]').unbind('click');
        $('#NewRouting').prop('readonly', false);
        $('#NewRouting').css('pointer-events', '');
        $('#NewStartOpNo').prop('readonly', false);
        $('#NewStartOpNo').css('pointer-events', '');
        $('#NewEndOpNo').prop('readonly', false);
        $('#NewEndOpNo').css('pointer-events', '');
        const selectElement = $('#NewStartOpNo');
        const selectEndOpNo = $('#NewEndOpNo');
        selectElement.html("");
        selectEndOpNo.html('');
    });
    $('#popup3NewWo').on('shown.bs.modal', function (event) {
        var totalsoqnty = $("#NewTotalSoQty").val();
        var balsoq = $("#NewBalSoQty").val();
        var qntonhand = $("#NewQtyOnHand").val();
        var balmanuf = totalsoqnty - balsoq;
        $('#NewBalToManuf').val(balmanuf);
        $('#NewNoofWoReleased').val(0);
        $('#NewSumWoQnty').val(0);
        $('#NewBalWoQty').val(0);
        var partType = $("#NewpartType").val();
        var partId = $("#NewpartId").val();

        if (partType == "1") {
            $("#popup3NewdivRouting").show().addClass("row");
            api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + parseInt(partId)).then((data) => {
                //console.log(data);
                const selectElement = $('#NewRouting');
                selectElement.prop("disabled", false);
                selectElement.html("");
                if (data.length === 1) {
                    $('#NewRouting').prop('readonly', true);
                    $('#NewRouting').css('pointer-events', 'none');
                    $.each(data, (index, item) => {
                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                    });
                    var routeId = $('#NewRouting').val();
                    $('#NewStartOpNo').prop('readonly', true);
                    $('#NewStartOpNo').css('pointer-events', 'none');
                    $('#NewEndOpNo').prop('readonly', true);
                    $('#NewEndOpNo').css('pointer-events', 'none');
                    api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
                        //console.log(data);
                        const selectstartElement = $('#NewStartOpNo');
                        const selectEndOpNo = $('#NewEndOpNo');
                        selectstartElement.html("");
                        $.each(data, (index, item) => {
                            selectstartElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                        });
                        const reversedData = data.slice().reverse();
                        selectEndOpNo.html('');
                        $.each(reversedData, (index, item) => {
                            selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                        });
                    }).catch((error) => {
                        console.error(error);
                    });
                } else {

                    selectElement.append(`<option value="0">--Select--</option>`);
                    $.each(data, (index, item) => {
                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                    });
                }

            }).catch((error) => {
            });
            $('#NewStartOpNo').prop("disabled", false);
            $('#NewEndOpNo').prop("disabled", false);
        }
        else {
            $("#popup3NewdivRouting").hide();
            const selectElement = $('#routing');
            selectElement.html("");
            selectElement.prop("disabled", true);
            $('#ManualStartOpNo').html("").prop("disabled", true);
            $('#ManualEndOpNo').html("").prop("disabled", true);
        }
    });
    $('#popup7').on('hidden.bs.modal', function (event) {
        var $modal = $(this);
        //$modal.find('button[id=ManualSaveWo]').unbind('click');
        $('#popup7Routing').prop('readonly', false);
        $('#popup7Routing').css('pointer-events', '');
        $('#popup7StartingOpNo').prop('readonly', false);
        $('#popup7StartingOpNo').css('pointer-events', '');
        $('#popup7EndingOpNo').prop('readonly', false);
        $('#popup7EndingOpNo').css('pointer-events', '');
        const selectElement = $('#popup7StartingOpNo');
        const selectEndOpNo = $('#popup7EndingOpNo');
        selectElement.html("");
        selectEndOpNo.html('');
    });
    $('#popup7').on('shown.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var workOrderId = relatedTarget.data("workorderid");
        var salesOrderId = relatedTarget.data("salesorderid");
        var woNumber = relatedTarget.data("wonumber");
        var partNo = relatedTarget.data("partno");
        var partDesc = relatedTarget.data("partdesc");
        var planCompletionDateStr = relatedTarget.data("plancompletiondatestr");
        var partId = relatedTarget.data("partid");
        var partType = relatedTarget.data("parttype");
        var wostatus = relatedTarget.data("wostatus");
        var planWOQty = relatedTarget.data("calwoqty");
        var formattedDate = planCompletionDateStr.split("-").reverse().join("-");

        $("#popup7PartNo").text(partNo);
        $("#P7partdesc").text(partDesc);
        $("#popup7PartNoField").val(partNo);
        $("#popup7WoComplDt").val(formattedDate);
        $("#popup7WoQnty").val(planWOQty);
        $("#popup7SoQnty").val(planWOQty);
        $("#Popup7woid").val(workOrderId);
        $("#Popup7soid").val(salesOrderId);
        $("#Popup7partId").val(partId);
        $("#Popup7partType").val(partType);
        $("#Popup7WoStatus").val(wostatus);
        $("#popup7QtyOnHand").val(0);
        $("#popup7BuildToStockQnty").val(0);
        $("#popup7WoNumber").text(woNumber);


        if (partType === 1) {
            $("#popup7divRouting").show().addClass("row");
            api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + parseInt(partId)).then((data) => {
                //console.log(data);
                const selectElement = $('#popup7Routing');
                selectElement.prop("disabled", false);
                selectElement.html("");
                if (data.length === 1) {
                    $('#popup7Routing').prop('readonly', true);
                    $('#popup7Routing').css('pointer-events', 'none');
                    $.each(data, (index, item) => {
                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                    });
                    var routeId = $('#popup7Routing').val();
                    $('#popup7StartingOpNo').prop('readonly', true);
                    $('#popup7StartingOpNo').css('pointer-events', 'none');
                    $('#popup7EndingOpNo').prop('readonly', true);
                    $('#popup7EndingOpNo').css('pointer-events', 'none');
                    api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
                        //console.log(data);
                        const selectstartElement = $('#popup7StartingOpNo');
                        const selectEndOpNo = $('#popup7EndingOpNo');
                        selectstartElement.html("");
                        $.each(data, (index, item) => {
                            selectstartElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                        });
                        const reversedData = data.slice().reverse();
                        selectEndOpNo.html('');
                        $.each(reversedData, (index, item) => {
                            selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                        });
                    }).catch((error) => {
                        console.error(error);
                    });
                } else {

                    selectElement.append(`<option value="0">--Select--</option>`);
                    $.each(data, (index, item) => {
                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                    });
                }

            }).catch((error) => {
            });
            $('#popup7StartingOpNo').prop("disabled", false);
            $('#popup7EndingOpNo').prop("disabled", false);
        }
        else {
            $("#popup7divRouting").hide();
            const selectElement = $('#popup7Routing');
            selectElement.html("");
            selectElement.prop("disabled", true);
            $('#popup7StartingOpNo').html("").prop("disabled", true);
            $('#popup7EndingOpNo').html("").prop("disabled", true);
        }


    });

    $('#popup7Routing').on('change', (e) => {
        const routeId = $(e.target).val();
        api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
            //console.log(data);
            //const uniqueData = [...new Set(data.map(item => item.stepOperation))];
            const uniqueData = data.filter((item, index, self) =>
                self.findIndex((t) => t.stepOperation === item.stepOperation) === index
            );
            const selectElement = $('#popup7StartingOpNo');
            const selectEndOpNo = $('#popup7EndingOpNo');
            selectElement.html("");
            $.each(uniqueData, (index, item) => {
                selectElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
            const reversedData = uniqueData.slice().reverse();
            selectEndOpNo.html('');
            $.each(reversedData, (index, item) => {
                selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
            });
        }).catch((error) => {
            //console.error(error);
        });
    });

    $("#popup7SaveWo").on("click", function () {
        var woid = parseInt($("#Popup7woid").val());
        var soid = parseInt($("#Popup7soid").val());
        var partid = parseInt($("#Popup7partId").val());
        var parttype = parseInt($("#Popup7partType").val());
        var wostatus = parseInt($("#Popup7WoStatus").val());
        var planWoQty = parseInt($("#popup7WoQnty").val());
        //var soqty = parseInt($("#NewTotalSoQty").val());
        var wonumber = $("#popup7WoNumber").text();
        var WoComplDate = new Date(Date.parse($('#popup7WoComplDt').val()));
        var formattedDate = WoComplDate.toISOString();
        var routingid = $("#popup7Routing").val();
        var startingOpNo = $("#popup7StartingOpNo").val();
        var endingOpNo = $("#popup7EndingOpNo").val();
        var rowData = {
            woid: parseInt(woid),
            salesOrderId: parseInt(soid),
            wonumber: wonumber,
            partId: parseInt(partid),
            partType: parseInt(parttype),
            parentlevel: '',
            calcWOQty: parseInt(planWoQty),
            planCompletionDate: formattedDate,
            routingId: parseInt(routingid),
            startingOpNo: parseInt(startingOpNo),
            endingOpNo: parseInt(endingOpNo),
            status: parseInt(wostatus),
            buildToStock: 'Y'
        };

        api.post("/businessaquisition/WOpost", rowData).then((data) => {
            //console.log(data);
            $('#popup7').modal('hide');
            loadWO();
        }).catch((error) => {
        });
    });


    $('#wohold').on('shown.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var workOrderId = relatedTarget.data("workorderid");
        var salesOrderId = relatedTarget.data("salesorderid");
        var woNumber = relatedTarget.data("wonumber");
        var partNo = relatedTarget.data("partno");
        var partDesc = relatedTarget.data("partdesc");
        var planCompletionDateStr = relatedTarget.data("plancompletiondatestr");
        var partId = relatedTarget.data("partid");
        var partType = relatedTarget.data("parttype");
        var wostatus = relatedTarget.data("wostatus");
        var planWOQty = relatedTarget.data("calwoqty");
        var routingid = relatedTarget.data("routingid");
        var startingopno = relatedTarget.data("startingopno");
        var endingopno = relatedTarget.data("endingopno");
        var buildtostock = relatedTarget.data("buildtostock");
        var formattedDate = planCompletionDateStr.split("-").reverse().join("-");

        //$("#popup7PartNo").text(partNo);
        //$("#P7partdesc").text(partDesc);
        //$("#HoldPartNoField").val(partNo);
        $("#HolWoComlDt").val(formattedDate);
        $("#HoldPlanWoQnty").val(planWOQty);
        $("#HoldWorkOrderId").val(workOrderId);
        $("#HoldSalesOrderId").val(salesOrderId);
        $("#HoldPartId").val(partId);
        $("#HoldPartType").val(partType);
        $("#HoldWoNumber").val(woNumber);
        $("#HoldWoStatus").val(wostatus);
        $("#HoldRoutingId").val(routingid);
        $("#HoldStartingOpNo").val(startingopno);
        $("#HoldEndingOpNo").val(endingopno);
        $("#HoldBuildToStock").val(buildtostock);

    });


    $("#BtnWOHold").on("click", function () {
        var woid = parseInt($("#HoldWorkOrderId").val());
        var soid = parseInt($("#HoldSalesOrderId").val());
        var partid = parseInt($("#HoldPartId").val());
        var parttype = parseInt($("#HoldPartType").val());
        var wostatus = parseInt($("#HoldWoStatus").val());
        var planWoQty = parseInt($("#HoldPlanWoQnty").val());
        //var soqty = parseInt($("#NewTotalSoQty").val());
        var wonumber = $("#HoldWoNumber").val();
        var WoComplDate = new Date(Date.parse($('#HolWoComlDt').val()));
        var formattedDate = WoComplDate.toISOString();
        var routingid = $("#HoldRoutingId").val();
        var startingOpNo = $("#HoldStartingOpNo").val();
        var endingOpNo = $("#HoldEndingOpNo").val();
        var buildToStock = $("#HoldBuildToStock").val();
        if (buildToStock == 'Y') {

        } else {
            buildToStock = '';
        }
        if (wostatus === 8) {
            //wostatus = 1;
        } else {
            wostatus = 8;
        }
        var rowData = {
            woid: parseInt(woid),
            salesOrderId: parseInt(soid),
            wonumber: wonumber,
            partId: parseInt(partid),
            partType: parseInt(parttype),
            parentlevel: '',
            calcWOQty: parseInt(planWoQty),
            planCompletionDate: formattedDate,
            routingId: parseInt(routingid),
            startingOpNo: parseInt(startingOpNo),
            endingOpNo: parseInt(endingOpNo),
            status: parseInt(wostatus),
            buildToStock: buildToStock
        };

        api.post("/businessaquisition/WOpost", rowData).then((data) => {
            //console.log(data);
            $('#wohold').modal('hide');
            loadWO();
        }).catch((error) => {
        });
    });


});



function EditWo(element) {
    //console.log("--Edit--");
    var relatedTarget = $(element);
    var workOrderId = relatedTarget.data("workorderid");
    var salesOrderId = relatedTarget.data("salesorderid");
    var woNumber = relatedTarget.data("wonumber");
    var partNo = relatedTarget.data("partno");
    var partDesc = relatedTarget.data("partdesc");
    var planCompletionDateStr = relatedTarget.data("plancompletiondatestr");
    var partId = relatedTarget.data("partid");
    var partType = relatedTarget.data("parttype");
    var planWOQty = relatedTarget.data("calwoqty");
    var woNumber = relatedTarget.data("wonumber");
    var wostatus = relatedTarget.data("wostatus");
    var reloadOption = relatedTarget.data("reloadoption");
    document.getElementById('WoComplDate').value = planCompletionDateStr.split("-").reverse().join("-");
    document.getElementById('p1planComplDate').value = planCompletionDateStr.split("-").reverse().join("-");
    $('#PlanWoQty').val(0);
    $('#SoQty').val(planWOQty);
    $('#woNumber').text(woNumber);
    $('#partNo').text(partNo);
    $('#P1partdesc').text(partDesc);
    $('#WOID').val(workOrderId);
    $('#SalesOrderId').val(salesOrderId);
    $('#PartId').val(partId);
    $('#woStatus').val(wostatus);

    var WOSoTable = [];
    var temp = [];
    if (workOrderId > 0) {

        api.getbulk("/WorkOrder/GetSoWo?workOrderId=" + workOrderId).then((data) => {
            //console.log(data);
            $.each(data, (index, item) => {
                var rowdata = {
                    wosoId: parseInt(item.wosoId),
                    workOrderId: parseInt(item.workOrderId),
                    salesOrderId: parseInt(item.salesOrderId),

                };
                temp.push(rowdata);
            });


            $.ajax({
                type: "POST",
                url: '/WorkOrder/GetOneSO',
                contentType: "application/json; charset=utf-8",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(temp),
                dataType: "json",
                success: function (result) {
                    window.locationre = result.url;
                    //console.log(result);
                    $.each(result, (index, item) => {
                        WOSoTable.push(item);
                    });

                    //CheckNumberOfSo(WOSoTable);     
                    if (WOSoTable.length >= 2) {
                        $('#woc-partno').modal('show');
                        $('#popup2').modal('hide');
                        var tablebody = $("#multipleSO tbody");
                        $(tablebody).html("");//empty tbody

                        for (i = 0; i < WOSoTable.length; i++) {
                            $(tablebody).append(AppUtil.ProcessTemplateData("MultipleSoRow", WOSoTable[i]));
                        }


                        if (partType === 1) {
                            $("#popup1DivRouting").show().addClass("row");
                            api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + parseInt(partId)).then((data) => {
                                //console.log(data);
                                const selectElement = $('#routing');
                                selectElement.prop("disabled", false);
                                selectElement.html("");
                                if (data.length === 1) {
                                    $('#routing').prop('readonly', true);
                                    $('#routing').css('pointer-events', 'none');
                                    $.each(data, (index, item) => {
                                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                                    });
                                    var routeId = $('#routing').val();
                                    $('#StartingOpNo').prop('readonly', true);
                                    $('#StartingOpNo').css('pointer-events', 'none');
                                    $('#EndingOpNo').prop('readonly', true);
                                    $('#EndingOpNo').css('pointer-events', 'none');
                                    api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
                                        //console.log(data);
                                        const selectstartElement = $('#StartingOpNo');
                                        const selectEndOpNo = $('#EndingOpNo');
                                        selectstartElement.html("");
                                        $.each(data, (index, item) => {
                                            selectstartElement.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                                        });
                                        const reversedData = data.slice().reverse();
                                        selectEndOpNo.html('');
                                        $.each(reversedData, (index, item) => {
                                            selectEndOpNo.append(`<option value="${item.stepOperation}">${item.stepOperation}</option>`);
                                        });
                                    }).catch((error) => {
                                        console.error(error);
                                    });
                                } else {

                                    selectElement.append(`<option value="0">--Select--</option>`);
                                    $.each(data, (index, item) => {
                                        selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);

                                    });
                                }

                            }).catch((error) => {
                            });
                            $('#StartingOpNo').prop("disabled", false);
                            $('#EndingOpNo').prop("disabled", false);
                        }
                        else {
                            $("#popup1DivRouting").hide();
                            const selectElement = $('#routing');
                            selectElement.html("");
                            selectElement.prop("disabled", true);
                            $('#StartingOpNo').html("").prop("disabled", true);
                            $('#EndingOpNo').html("").prop("disabled", true);
                        }
                    }
                    else if (WOSoTable.length === 1) {
                        if (reloadOption) {
                            var sonumber = "";

                            api.get("/WorkOrder/GetSoNumber?soid=" + parseInt(salesOrderId)).then((data) => {
                                //console.log(data);
                                sonumber = data.soNumber;
                                $('#p3NsoNumber').text(sonumber);
                            });
                            $('#popup2').modal('hide');
                            $('#popup3NewWo').modal('show');
                            $("#NewpartNo").text(partNo);
                            $("#P3Npartdesc").text(partDesc);
                            $("#NewwoCompletedBy").text(planCompletionDateStr.split("-").reverse().join("-"));
                            $("#NewTotalSoQty").val(planWOQty);
                            $("#NewPlanWoQty").val(planWOQty);
                            $("#Newwoid").val(workOrderId);
                            $("#Newsoid").val(salesOrderId);
                            $("#NewpartId").val(partId);
                            $("#NewpartType").val(partType);
                            $("#NewWoStatus").val(wostatus);
                            $("#NewWoNumber").val(woNumber);
                            $("#p3NReloadOption").val(reloadOption);
                            $("#NewBalSoQty").val(0);
                            $("#NewQtyOnHand").val(0); //reloadOption
                            $("#NewWoComplDt").val(planCompletionDateStr.split("-").reverse().join("-"));
                            document.getElementById("NewSaveWo").textContent = "Save WO";
                        }
                        else {
                            $('#popup2').modal('show');
                            $('#p2totalSoQty').val(planWOQty);
                            $('#p2BalQty').val(0);
                            $('#p2WoNumber').text(woNumber);
                            $('#p2PartNo').text(partNo);
                            $('#P2partdesc').text(partDesc);
                            $('#p2WOid').val(workOrderId);
                            $('#p2SalesOrderId').val(salesOrderId);
                            $('#p2PartId').val(partId);
                            $('#p2PartType').val(partType);
                            $('#p2Status').val(wostatus);
                            $('#p2ReloadOption').val(reloadOption);
                            $('#p2PlanComplDate').text(planCompletionDateStr.split("-").reverse().join("-"));
                            //popup2show = true;
                        }
                    }


                }
            });


        }).catch((error) => {
        });
    }

}