var noofWOCreation = [];
function loadWO() {
    api.getbulk("/WorkOrder/AllProductionWo").then((data) => {
        data = data.filter(item => item.active !== 2);
        var tablebody = $("#detailedPlanWo tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            //data[i].strStatus = WoOrdStatus[data[i].status];
            $(tablebody).append(AppUtil.ProcessTemplateData("detailedPlanWoRow", data[i]));
        }
        const customerChildParts = data.filter((workOrder) => workOrder.partType === 1);
        const count = customerChildParts.length;
        $('#noOfChildPart').text(count);
        const workOrdersWithStatus1 = data.filter((workOrder) => workOrder.status === 1);
        const totalcount = workOrdersWithStatus1.length;
        $('#noOfWo').text(totalcount);
    }).catch((error) => {
    });
}

function landingPage() {
    //api.getbulk("/WorkOrder/AllWorkOrders").then((data) => {
    //    const workOrdersWithStatus1 = data.filter((workOrder) => workOrder.status === 1);
    //    const count = workOrdersWithStatus1.length;
    //    $('#noOfWo').text(count);
    //}).catch((error) => {
    //});
    //api.getbulk("/WorkOrder/AllProductionWo").then((data) => {
    //    const customerChildParts = data.filter((workOrder) => workOrder.partType === 1);
    //    const count = customerChildParts.length;
    //    $('#noOfChildPart').text(count);
    //}).catch((error) => {
    //});
    //api.getbulk("/WorkOrder/GetAllBomlist").then((data) => {
    //    const count = data.length;
    //    $('#noOfAssyBomExploded').text(count);
    //    const manfParts = data.map((bom) => bom.child_Part_No_Type == "ManufacturedPart");
    //    const bofparts = data.map((bom) => bom.child_Part_No_Type == "BOF");

    //    // Get the unique manufacturer parts
    //    const uniqueManfParts = [...new Set(manfParts)];
    //    const uniqueBofParts = [...new Set(bofparts)];

    //    // Get the common manufacturer parts (i.e., parts that appear more than once)
    //    const commonManfParts = manfParts.filter((part, index, array) => array.indexOf(part) !== index);
    //    const CommonBofParts = bofparts.filter((part, index, array) => array.indexOf(part) !== index);

    //    // Get the count of common manufacturer parts
    //    const commonManfPartsCount = commonManfParts.length;
    //    const commonBofPartsCount = CommonBofParts.length;

    //    // Get the count of unique manufacturer parts
    //    const uniqueManfPartsCount = uniqueManfParts.length;
    //    const uniqueBofCount = uniqueBofParts.length;

    //    // Update the UI
    //    $('#noOfCommonManfParts').text(commonManfPartsCount);
    //    $('#totalNoOfUniqueManfParts').text(uniqueManfPartsCount);
    //    $('#noOfCommonBOFParts').text(commonBofPartsCount);
    //    $('#totalNoOfUniqueBOFParts').text(uniqueBofCount);
    //}).catch((error) => {
    //});
    //api.getbulk("/WorkOrder/GetAllProcPlan").then((data) => {
    //    const count = data.map((raw) => raw.partType == "RawMaterial");
    //    $('#noOfRMPart').text(count.length);
    //    const rmparts = data.filter((raw) => raw.partType === "RawMaterial").map((raw) => raw.partNo);
    //    const uniqueRMParts = [...new Set(rmparts)];

    //    const commonRMParts = rmparts.filter((part, index, array) => array.indexOf(part) !== index);
    //    const commonRMPartsCount = commonRMParts.length;
    //    const uniqueRMPartsCount = uniqueRMParts.length;

    //    // Get the count of unique manufacturer parts
    //    //const uniqueRMPartsCount = uniqueRMParts.length;
    //    $('#noOfCommonRMPart').text(commonRMPartsCount);
    //    $('#totalNoOfUniqueRMPart').text(uniqueRMPartsCount);

    //}).catch((error) => {
    //});
}

function getWorkOrderStatus(productionWoData, workOrderId) {
    for (var i = 0; i < productionWoData.length; i++) {
        if (productionWoData[i].woId === workOrderId) {
            return productionWoData[i].status;
        }
    }
    return null;
}

function loadProcPlan() {
    api.getbulk("/WorkOrder/GetAllProcPlan").then((data) => {
        var tablebody = $("#ProcPlanGrid tbody");
        $(tablebody).html("");//empty tbody
        api.getbulk("/WorkOrder/AllProductionWo").then((productionWoData) => {
            var tablebody = $("#ProcPlanGrid tbody");
            $(tablebody).html("");//empty tbody
            for (i = 0; i < data.length; i++) {
                var rowData = data[i];
                var workOrderStatus = getWorkOrderStatus(productionWoData, rowData.workOrderId);
                if (workOrderStatus !== 3 && workOrderStatus !== 4) {
                    rowData.checkboxDisabled = true;
                } else {
                    rowData.checkboxDisabled = false;
                }
               // $(tablebody).append(AppUtil.ProcessTemplateData("ProcPlanRow", rowData));
                var rowHtml = AppUtil.ProcessTemplateData("ProcPlanRow", rowData);
                $(tablebody).append(rowHtml);

                // Apply the disabled attribute based on checkboxDisabled
                //if (rowData.checkboxDisabled) {
                //    $(tablebody).find('tr').last().find('input[type="checkbox"]').prop('disabled', true);
                //} else {
                //    $(tablebody).find('tr').last().find('input[type="checkbox"]').prop('disabled', false);
                //}
            }
            const count = data.map((raw) => raw.partType == "RawMaterial");
            $('#noOfRMPart').text(count.length);
            const rmparts = data.filter((raw) => raw.partType === "RawMaterial").map((raw) => raw.partNo);
            const uniqueRMParts = [...new Set(rmparts)];

            const commonRMParts = rmparts.filter((part, index, array) => array.indexOf(part) !== index);
            const commonRMPartsCount = commonRMParts.length;
            const uniqueRMPartsCount = uniqueRMParts.length;

            // Get the count of unique manufacturer parts
            //const uniqueRMPartsCount = uniqueRMParts.length;
            $('#noOfCommonRMPart').text(commonRMPartsCount);
            $('#totalNoOfUniqueRMPart').text(uniqueRMPartsCount);
        }).catch((error) => { });
    }).catch((error) => {
    });
}

function loadBomList() {
    api.getbulk("/WorkOrder/GetAllBomlist").then((data) => {
        var tablebody = $("#BomListGrid tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("BomListRow", data[i]));
        }
        const count = data.length;
        $('#noOfAssyBomExploded').text(count);
        const manfParts = data.map((bom) => bom.child_Part_No_Type == "ManufacturedPart");
        const bofparts = data.map((bom) => bom.child_Part_No_Type == "BOF");

        // Get the unique manufacturer parts
        const uniqueManfParts = [...new Set(manfParts)];
        const uniqueBofParts = [...new Set(bofparts)];

        // Get the common manufacturer parts (i.e., parts that appear more than once)
        const commonManfParts = manfParts.filter((part, index, array) => array.indexOf(part) !== index);
        const CommonBofParts = bofparts.filter((part, index, array) => array.indexOf(part) !== index);

        // Get the count of common manufacturer parts
        const commonManfPartsCount = commonManfParts.length;
        const commonBofPartsCount = CommonBofParts.length;

        // Get the count of unique manufacturer parts
        const uniqueManfPartsCount = uniqueManfParts.length;
        const uniqueBofCount = uniqueBofParts.length;

        // Update the UI
        $('#noOfCommonManfParts').text(commonManfPartsCount);
        $('#totalNoOfUniqueManfParts').text(uniqueManfPartsCount);
        $('#noOfCommonBOFParts').text(commonBofPartsCount);
        $('#totalNoOfUniqueBOFParts').text(uniqueBofCount);
    }).catch((error) => {
    });
}

function loadMcTimeListDetail() {
    api.getbulk("/WorkOrder/GetAllMcTimeList").then((data) => {
        var tablebody = $("#MachineTimeListDetail tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("MachineTimeListDetailRow", data[i]));
        }
        var tablebodysummary = $("#McTimeListSummary tbody");
        $(tablebodysummary).html("");//empty tbody
        //console.log(data);
        var result = [];
        const combinedData = {};
       
        data.forEach((item) => {
            if (!combinedData[item.machineTypeName]) {
                combinedData[item.machineTypeName] = { ...item }; // copy all properties from item
            } else {
                // Combine the data if machineTypeName is the same
                combinedData[item.machineTypeName].totalPlanTime += item.totalPlanTime;
            }
        });
        result = Object.values(combinedData);
        for (i = 0; i < result.length; i++) {
            $(tablebodysummary).append(AppUtil.ProcessTemplateData("McTimeListSummaryRow", result[i]));
        }
    }).catch((error) => {
    });
}

function reloadWO(reloadOption, partid) {
    api.getbulk("/WorkOrder/ReloadProductionWo?reloadoption=" + reloadOption + "&partid=" + partid).then((data) => {
        var tablebody = $("#Popup4Grid tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            //data[i].strStatus = WoOrdStatus[data[i].status];
            $(tablebody).append(AppUtil.ProcessTemplateData("Popup4GridRow", data[i]));
        }
    }).catch((error) => {
    });
}

function p2reloadWO(reloadOption, partid) {
    api.getbulk("/WorkOrder/ReloadProductionWo?reloadoption=" + reloadOption + "&partid=" + partid).then((data) => {
        var tablebody = $("#MulitpleWOs tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            //data[i].strStatus = WoOrdStatus[data[i].status];
            $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", data[i]));
        }
    }).catch((error) => {
    });
}
$(window).on('load', function () {
    loadWO();
    loadProcPlan();
    loadBomList();
    loadMcTimeListDetail();
});

$(document).ready(function () {
    //loadWO();
    //loadProcPlan();
    //loadBomList();
    //landingPage();
    //loadMcTimeListDetail();

    $('#select-all-checkbox-in-wo').change(function () {
        if ($(this).is(":checked")) {
            $('#detailedPlanWo tbody').find('input[type="checkbox"]').prop('checked', true);
            $('#ReleaseWo').prop('disabled', false);
        } else {
            $('#detailedPlanWo tbody').find('input[type="checkbox"]').prop('checked', false);
            $('#ReleaseWo').prop('disabled', true);
        }
    });
    function handleCheckboxChange() {
        var checkboxes = $("#detailedPlanWo tbody input[type='checkbox']:checked");

        if (checkboxes.length === 0) {
            $('#ReleaseWo').prop('disabled', true);
        } else {
            $('#ReleaseWo').prop('disabled', false);
        }
    }
    $('#detailedPlanWo tbody').on('change', 'input[type="checkbox"]', handleCheckboxChange);
    handleCheckboxChange();


    $('#select-all-checkbox-in-procplan').change(function () {
        if ($(this).is(":checked")) {
            $('#ProcPlanGrid tbody').find('input[type="checkbox"]').each(function () {
                if (!$(this).prop('disabled')) {
                    $(this).prop('checked', true);
                }
            });
            $('#ReleasePo').prop('disabled', false);
            $('#UpdateMOQ').prop('disabled', false);
        } else {
            $('#ProcPlanGrid tbody').find('input[type="checkbox"]').prop('checked', false);
            $('#ReleasePo').prop('disabled', true);
            $('#UpdateMOQ').prop('disabled', true);
        }
    });

    function handleCheckboxChangePP() {
        var checkboxes = $("#ProcPlanGrid tbody input[type='checkbox']:checked");

        if (checkboxes.length === 0) {
            $('#ReleasePo').prop('disabled', true);
            $('#UpdateMOQ').prop('disabled', true);
        } else {
            $('#ReleasePo').prop('disabled', false);
            $('#UpdateMOQ').prop('disabled', false);
        }
    }
    $('#ProcPlanGrid tbody').on('change', 'input[type="checkbox"]', handleCheckboxChangePP);
    handleCheckboxChangePP();

    $('#popup1').on('shown.bs.modal', function (event) {
        var soqty = $('#SoQty').val();
        var qntyonhand = $('#QtyOnHand').val();
        var balqnty = soqty - qntyonhand;
        $('#BalQty').val(balqnty);
        // Select all checkboxes in the table with the class 'rowMCheckbox'
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
    $('#popup1').on('hidden.bs.modal', function (event) {
        loadWO();
    });
    $("#updateWO").on("click", function () {
        var planwoqty = $('#PlanWoQty').val();
        var wonumber = $('#woNumber').text();
        var ppid = $('#p1ppid').val();
        var woid = $('#WOID').val();
        var soid = $('#SalesOrderId').val();
        var partid = $('#PartId').val();
        var WoComplDate = new Date(Date.parse($('#WoComplDate').val()));
        var formattedDate = WoComplDate.toISOString();
        var routingid = $("#routing").val();
        var startingOpNo = $("#StartingOpNo").val();
        var endingOpNo = $("#EndingOpNo").val();
        var soqty = $('#SoQty').val();
        //var partid = $('#PartId').val();
        var parttype = $('#PartType').val();
        var reqdate = $('#reqdate').val();
        var status = $('#woStatus').val();
        var selectedData = {};
        var rstDt = new Date(Date.parse($('#p1planComplDate').val()));
        const restrictDt = new Date(rstDt);
        if (WoComplDate < restrictDt) {
            alert("Please Don't Enter The Previous Completion Date.");
            $('#WoComplDate').val(""); // Clear the invalid date
            return false; // Prevent further processing if date is invalid
        }
        if (planwoqty < soqty) {
            alert("Plan WO Qnty Should Be Greater Than Or Equal To Sales Order Qnty");
            return false;
        }
        //else {
            if (planwoqty > 0) {
                var rowData = {
                    productionPlanId: parseInt(ppid),
                    woid: parseInt(woid),
                    salesOrderId: parseInt(soid),
                    wonumber: wonumber,
                    partId: parseInt(partid),
                    partType: parseInt(parttype),
                    parentlevel: '',
                    calcWOQty: parseInt(planwoqty),
                    planCompletionDate: formattedDate,
                    routingId: parseInt(routingid),
                    startingOpNo: parseInt(startingOpNo),
                    endingOpNo: parseInt(endingOpNo),
                    reloadOption: "p1",
                    status: parseInt(status)
                };

                selectedData = rowData;


                api.post("/WorkOrder/ProductionPlanPost", rowData).then((data) => {
                    // Handle success if needed
                    //if (planwoqty < soqty) {
                    //    var balqty = soqty - planwoqty;

                    //    var selectedRowsData = [];
                    //    var checkboxes = $("#multipleSO tbody input[type='checkbox']:checked");
                    //    checkboxes.each(function (index, checkbox) {
                    //        var row = checkbox.parentNode.parentNode;
                    //        var salesOrderId = parseInt($(row).find("td:eq(0)").text()); // get the text from the 3rd column (index 2)
                    //        //var balanceSOQty = parseInt($(row).find("td:eq(4)").text()); // get the text from the 5th column (index 4)
                    //        selectedRowsData.push({ salesOrderId });
                    //    });
                    //    console.log(selectedRowsData);
                    //    var norow = selectedRowsData.length;
                    //    for (var i = 0; i < selectedRowsData.length; i++) {
                    //        var edata = selectedRowsData[i];
                    //        if (norow > 1) {
                    //            var fBalqty = balqty / norow
                    //            edata = {
                    //                ...selectedRowsData[i],
                    //                balanceSOQty: fBalqty
                    //            };
                    //        }
                    //        api.post("/businessaquisition/SalesOrder", edata)
                    //            .then((data) => {
                    //                console.log("Data posted successfully!", data);
                    //            })
                    //            .catch((error) => {
                    //                console.error("Error posting data:", error);
                    //            });
                    //    }
                    //}
                    //else {
                    //    var balqty = soqty - planwoqty
                    //    var soupdate = {
                    //        salesOrderId: parseInt(soid),
                    //        requiredByDate: reqdate,
                    //        balanceSOQty: parseInt(balqty)
                    //    }
                    //    api.post("/businessaquisition/SalesOrder", soupdate).then((data) => {

                    //    }).catch((error) => { });
                    //}
                    $("#btnPop1Close").click();
                    loadWO();
                }).catch((error) => {
                    AppUtil.HandleError("WOForm", error);
                    //console.log(error);
                });
            } else {
                alert("Please Select atleast one!");
            }
        //}

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
        var ppid = $('#p2ppid').val();
        var woid = $('#p2WOid').val();
        var soid = $('#p2SalesOrderId').val();
        var partid = $('#p2PartId').val();
        var parttype = $('#p2PartType').val();
        var wostatus = $('#p2Status').val();
        var WoComplDate = new Date(Date.parse($('#p2PlanComplDate').text()));
        var Compldt = $('#p2PlanComplDate').text();
        var partNo = $('#p2PartNo').text();
        var formattedDate = WoComplDate.toISOString();
        var requestInProgress = false;
        let totalQuantity = 0;
        
        var balsoqnty = $('#p2BalQty').val();
        var qntyOnhand = $('#p2QtyOnHand').val();
        var balmanuf = planwoqty - qntyOnhand;
        $('#p2BaltoManuf').val(balmanuf);
        var sonumber = "";

        api.get("/WorkOrder/GetSoNumber?soid=" + soid).then(async (data) => {
            //console.log(data);
            sonumber = await data.soNumber;
            $('#p2soNumber').text(sonumber);
        });

        //document.querySelectorAll('#MulitpleWOs tbody tr').forEach(row => {
        //    const quantity = parseInt(row.cells[1].textContent);
        //    if (!isNaN(quantity)) {
        //        totalQuantity += quantity;
        //    }
        //});
        $('#popup2Sum').val(planwoqty);



        $('input[type=radio][name=radioWO]').change(function () {
            if (this.value == "1") {
                $('#equaldiv').hide();
            } else if (this.value == "2") {
                $('#equaldiv').show();
            } else if (this.value == "3") {
                $('#equaldiv').hide();
                $('#popup3ManualMultiple').modal('show');
                $("#p3MsoNumber").text(sonumber);
                $("#ManualpartNo").text(partNo);
                $("#ManualwoCompletedBy").text(Compldt);
                $("#ManualTotalSoQty").val(planwoqty);
                $("#ManualPlanWoQty").val(planwoqty);
                $("#Manualppid").val(ppid);
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
            //if (requestInProgress) return;
            //requestInProgress = true;
            noofWOCreation.forEach((wo) => {
                wo.parentWoId = parseInt(woid);
                wo.partId = parseInt(partid);
                wo.salesOrderId = parseInt(soid);
                wo.reloadOption = "EQD";
            });
            var tdata = [];
            if (noofWOCreation.length > 1) {
                $.ajax({
                    type: "POST",
                    url: '/WorkOrder/MultipleProductionWOPost',
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
                                requestInProgress = false;
                            }
                        });

                    }
                });
            }


        });
    });

    $('#popup3').on('shown.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var ppid = relatedTarget.data("ppid");
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
        var sonumber = $("#p2soNumber").text();
        $("#p3soNumber").text(sonumber);
        $("#p3partNo").text(partno);
        $("#woCompletedBy").text(planCompletionDateStr.split("-").reverse().join("-"));
        $("#singleTotalSoQty").val(planWOQty);
        $("#singlePlanWoQty").val(planWOQty);
        $("#p3ppid").val(ppid);
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
            api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + partId).then((data) => {
                //console.log(data);
                const selectElement = $('#singleRouting');
                selectElement.prop("disabled", false);
                $.each(data, (index, item) => {
                    selectElement.html("");
                    selectElement.append(`<option value="0">--Select--</option>`);
                    selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);
                });
            }).catch((error) => {
            });
            $('#singleStartOpNo').prop("disabled", false);
            $('#singleEndOpNo').prop("disabled", false);
        }
        else {
            const selectElement = $('#singleRouting');
            selectElement.html("");
            selectElement.prop("disabled", true);
            $('#singleStartOpNo').html("").prop("disabled", true);
            $('#singleEndOpNo').html("").prop("disabled", true);
        }



    });

    $('#singleRouting').on('change', (e) => {
        const routeId = $(e.target).val();
        // make an API call to get data for select2 based on the selected studentId
        api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
            //console.log(data);
            const selectElement = $('#singleStartOpNo');
            const selectEndOpNo = $('#singleEndOpNo');
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

    $("#singleSaveWo").on("click", function () {
        var ppid = parseInt($("#p3ppid").val());
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
        var rstDt = new Date(Date.parse($('#woCompletedBy').text()));
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
            productionPlanId: parseInt(ppid),
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

        api.post("/WorkOrder/ProductionPlanPost", rowData).then((data) => {
            //console.log(data);
            $('#popup3').modal('hide');
            p2reloadWO(reloadOption, partid);
        }).catch((error) => {
        });

    });


    $('#popup3ManualMultiple').on('shown.bs.modal', function (event) {
        var totalsoqnty = $('#ManualTotalSoQty').val();
        var balSoqnty = $('#ManualBalSoQty').val();
        var QntyOnhand = $('#ManualQtyOnHand').val();
        var balmanuf = totalsoqnty - QntyOnhand;
        $('#ManualBalToManuf').val(balmanuf);
        $('#ManualNoofWoReleased').val(0);
        $('#ManualSumWoQnty').val(0);
        $('#ManualBalWoQty').val(0);
    });

    $('#popup3ManualMultiple').on('hidden.bs.modal', function (event) {
        var $modal = $(this);
        //$modal.find('button[id=ManualSaveWo]').unbind('click');
    });

    $("#ManualSaveWo").on("click", function () {
        var ppid = parseInt($("#Manualppid").val());
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
            alert("Wo Completion Date Should Be Greater Than Current Completion Date");
            return false;
            // or display an error message to the user
        } else {
            formattedDate = WoComplDate.toISOString();
        }

        if (planWoQty < soqty) {
            alert("Plan Wo Qnty Should Be Greater Or Equal To So Total So Qnty.");
            return false;
        }
        
        var rowData = {
            productionPlanId: parseInt(ppid),
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
            reloadOption: "Manual"
        };

        api.post("/WorkOrder/ProductionPlanPost", rowData).then((data) => {
            //console.log(data);
            resultData.push(...data);
            $('#popup3ManualMultiple').modal('hide');
            var tablebody = $("#MulitpleWOs tbody");
            $(tablebody).html("");//empty tbody

            for (i = 0; i < resultData.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", resultData[i]));
            }
        }).catch((error) => {
        });

    });

    $("#NewWoPopupBtn").on("click", function () {
        var planwoqty = $('#p2totalSoQty').val();
        var sonumber = $('#p2soNumber').text();
        var wonumber = $('#p2WoNumber').text();
        var ppid = $('#p2ppid').val();
        var woid = $('#p2WOid').val();
        var soid = $('#p2SalesOrderId').val();
        var partid = $('#p2PartId').val();
        var parttype = $('#p2PartType').val();
        var wostatus = $('#p2Status').val();
        var WoComplDate = new Date(Date.parse($('#p2PlanComplDate').text()));
        var Compldt = $('#p2PlanComplDate').text();
        var partNo = $('#p2PartNo').text();
        var formattedDate = WoComplDate.toISOString();
        var balsoqnty = $('#p2BalQty').val();
        var qntyOnhand = $('#p2QtyOnHand').val();

        $('#popup3NewWo').modal('show');
        $("#p2NsoNumber").text(sonumber);
        $("#NewpartNo").text(partNo);
        $("#NewwoCompletedBy").text(Compldt);
        $("#NewTotalSoQty").val(planwoqty);
        $("#NewPlanWoQty").val(planwoqty);
        $("#Newppid").val(ppid);
        $("#Newwoid").val(woid);
        $("#Newsoid").val(soid);
        $("#NewpartId").val(partid);
        $("#NewpartType").val(parttype);
        $("#NewWoStatus").val(wostatus);
        $("#NewWoNumber").val(wonumber);
        $("#NewBalSoQty").val(balsoqnty);
        $("#NewQtyOnHand").val(qntyOnhand);
        $("#NewWoComplDt").val(Compldt.split("-").join("-"));

    });

    $("#NewSaveWo").on("click", function () {
        var ppid = parseInt($("#Newppid").val());
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
        var resultData = [];
        var rstDt = new Date(Date.parse($('#NewwoCompletedBy').text()));
        const restrictDt = new Date(rstDt);
        if (planWoQty < soqty) {
            alert("Plan Wo Qnty Should Be Greater Or Equal To So Total So Qnty.");
            return false;
        }
        if (WoComplDate < restrictDt) {
            alert("Wo Completion Date Should Be Greater Than Current Completion Date");
            return false;
            // or display an error message to the user
        } else {
            formattedDate = WoComplDate.toISOString();
        }
        var rowData = {
            productionPlanId: parseInt(ppid),
            woid: parseInt(woid),
            salesOrderId: parseInt(soid),
            wonumber: wonumber,
            partId: parseInt(partid),
            partType: parseInt(parttype),
            parentlevel: '',
            calcWOQty: parseInt(planWoQty),
            planCompletionDate: formattedDate,
            reloadOption: "New",
            routingId: parseInt(routingid),
            startingOpNo: parseInt(startingOpNo),
            endingOpNo: parseInt(endingOpNo),
            status: parseInt(wostatus)
        };

        api.post("/WorkOrder/ProductionPlanPost", rowData).then((data) => {
            //console.log(data);
            resultData.push(...data);
            $('#popup3NewWo').modal('hide');
            var tablebody = $("#MulitpleWOs tbody");
            $(tablebody).html("");//empty tbody

            for (i = 0; i < resultData.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("MultipleWoRow", resultData[i]));
            }
        }).catch((error) => {
        });

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

    });

    $('#popup4').on('hidden.bs.modal', function (event) {
        var $modal = $(this);
        $modal.find('input[type=radio][name=popup4Radiobtn]').unbind('change');
        $modal.find('button[id=popup4SaveWo]').unbind('click');
        $('input[type=radio][name=popup4Radiobtn]').prop('checked', false);
        var tablebody = $("#Popup4Grid tbody");
        $(tablebody).html("");
        loadWO();
    });

    $('#popup4').on('shown.bs.modal', function (event) {
        var showpopup5 = false;
        var planwoqty = $('#popup4CalcWoQnty').val();
        var qntyOnhand = $('#popup4QntyOnHanf').val();
        var wonumber = $('#popup4WoNumber').text();
        var ppid = $('#popup4ppid').val();
        var woid = $('#popup4woid').val();
        var soid = $('#popup4soid').val();
        var partid = $('#popup4partId').val();
        var parttype = $('#popup4partType').val();
        var wostatus = $('#popup4Status').val();
        var WoComplDate = $('#popup4WoComplDt').val();
        //var Compldt = $('#p2PlanComplDate').text();
        var partNo = $('#popup4PartNo').text();
        //var formattedDate = WoComplDate.toISOString();  popup4BalQnty
        var balmaf = planwoqty - qntyOnhand;
        $('#popup4BalQnty').val(balmaf);
        $('#popup4PlanWoQnty').val(balmaf);
        var splitwos = [];
        $('input[type=radio][name=popup4Radiobtn]').change(function () {
            if (this.value == "1") {
                showpopup5 = true;
            } else if (this.value == "2") {
                showpopup5 = false;
            } else {
                showpopup5 = false;
            }
        });
        $("#popup4SaveWo").on("click", function () {
            if (showpopup5) {
                $('#popup5').modal('show');
                $("#pup5PatNo").text(partNo);
                //$("#popup5WoComplDtField").val(WoComplDate);
                $("#popup5CalcWoQnty").val(planwoqty);
                $("#popup5PlanWoQntyField").val(planwoqty);
                var formattedDate = WoComplDate.split("-").reverse().join("/");
                document.getElementById('popup5WoComplDtField').value = formattedDate;
                //$("#ManualPlanWoQty").val(planwoqty);
                $("#popup5ppid").val(ppid);
                $("#popup5woid").val(woid);
                $("#popup5soid").val(soid);
                $("#popup5partId").val(partid);
                $("#popup5partType").val(parttype);
                $("#popup5WoNumber").text(wonumber);
                $("#popup5WoNumberField").val(wonumber);
                $("#popup5WoComplDt").val(WoComplDate);
                $("#popup5Status").val(wostatus);
                $("#popup5QntyOnHand").val(qntyOnhand);
                $("#popup5BalQnty").val(balmaf);
                $("#popup5PlanWoQnty").val(balmaf);
                if (parttype === "1") {
                    api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + partid).then((data) => {
                        //console.log(data);
                        const selectElement = $('#popup5Routing');
                        selectElement.prop("disabled", false);
                        $.each(data, (index, item) => {
                            selectElement.html("");
                            selectElement.append(`<option value="0">--Select--</option>`);
                            selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);
                        });
                    }).catch((error) => {
                    });
                    $('#popup5StartingOp').prop("disabled", false);
                    $('#popup5EndingOp').prop("disabled", false);
                }
                else {
                    const selectElement = $('#popup5Routing');
                    selectElement.html("");
                    selectElement.prop("disabled", true);
                    $('#popup5StartingOp').html("").prop("disabled", true);
                    $('#popup5EndingOp').html("").prop("disabled", true);
                }
            }
            else {
                var qnty = parseInt(planwoqty);
                var splitnumber = parseInt(document.getElementById("numbersplitwo").value);
                var initialDt = WoComplDate.split("-").reverse().join("-")
                api.get("/WorkOrder/SplitWo?woid=" + woid + "&initialDate=" + initialDt + "&numDays=" + splitnumber + "&quantity=" + qnty + "&salersorderId=" + soid + "&partId=" + partid + "&partType=" + parttype).then((data) => {
                    //console.log(data);
                    splitwos.push(...data);
                    if (splitwos.length > 0) {
                        var formattedDt = new Date(Date.parse(WoComplDate.split("-").reverse().join("/")));
                        var inactiveppwo = {
                            productionPlanId: parseInt(ppid),
                            //woId: parseInt(woid),
                            salesOrderId: parseInt(soid),
                            wonumber: wonumber,
                            partId: parseInt(partid),
                            partType: parseInt(parttype),
                            parentlevel: '',
                            calcWOQty: parseInt(qnty),
                            planCompletionDate: formattedDt.toISOString(),
                            routingId: parseInt(0),
                            startingOpNo: parseInt(0),
                            endingOpNo: parseInt(0),
                            reloadOption: "inactive",
                            status: parseInt(0),
                            active: 2
                        };

                        api.post("/WorkOrder/ProductionPlanPost", inactiveppwo).then((data) => {
                            //console.log(data);
                        }).catch((error) => {
                        });
                    }
                    loadWO();
                    var tablebody = $("#Popup4Grid tbody");
                    $(tablebody).html("");//empty tbody

                    for (i = 0; i < splitwos.length; i++) {
                        $(tablebody).append(AppUtil.ProcessTemplateData("Popup4GridRow", splitwos[i]));
                    }
                });
               
            }
        });

    });

    //$('#popup5').on('hidden.bs.modal', function (event) {
    //    var $modal = $(this);
    //    $modal.find('button[id=popup5SaveWo]').unbind('click');
    //    $("#pup5PatNo").text();
    //    $("#popup5WoComplDtField").val();
    //    $("#popup5CalcWoQnty").val();
    //    $("#popup5PlanWoQntyField").val();
    //    //var formattedDate = WoComplDate.split("-").reverse().join("/");
    //    //document.getElementById('popup5WoComplDtField').value = formattedDate;
    //    //$("#ManualPlanWoQty").val(planwoqty);
    //    $("#popup5woid").val();
    //    $("#popup5soid").val();
    //    $("#popup5partId").val();
    //    $("#popup5partType").val();
    //    $("#popup5WoNumber").text();
    //    $("#popup5WoNumberField").val();
    //    $("#popup5WoComplDt").val();
    //    $("#popup5Status").val();
    //});

    $('#popup5Routing').on('change', (e) => {
        const routeId = $(e.target).val();
        // make an API call to get data for select2 based on the selected studentId
        api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
            //console.log(data);
            const selectElement = $('#popup5StartingOp');
            const selectEndOpNo = $('#popup5EndingOp');
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
    $('#popup5').on('hidden.bs.modal', function (event) {

        const selectElement = $('#popup5StartingOp');
        const selectEndOpNo = $('#popup5EndingOp');
        selectElement.html("");
        selectEndOpNo.html('');

    });

    $("#popup5SaveWo").on("click", function () {
        var ppid = parseInt($("#popup5ppid").val());
        var woid = parseInt($("#popup5woid").val());
        var soid = parseInt($("#popup5soid").val());
        var partid = parseInt($("#popup5partId").val());
        var parttype = parseInt($("#popup5partType").val());
        var wostatus = parseInt($("#popup5Status").val());
        var soqty = parseInt($("#popup5CalcWoQnty").val());
        var planWoQty = parseInt($("#popup5PlanWoQntyField").val());
        var wonumber = $("#popup5WoNumberField").val();
        var WoComplDate = new Date(Date.parse($('#popup5WoComplDtField').val()));
        var formattedDate;
        var routingid = $("#popup5Routing").val();
        var startingOpNo = $("#popup5StartingOp").val();
        var endingOpNo = $("#popup5EndingOp").val();
        var compldt = $("#popup5WoComplDt").val();
        const restrictDt = new Date(compldt.split("-").reverse().join("-"));
        var resultData = [];
        if (isNaN(WoComplDate.getTime())) {
            alert("Please Enter the WO Compl Date.");
            return false;
            // or display an error message to the user
        } else if (WoComplDate < restrictDt) {
            alert("Wo Completion Date Should Be Greater Than Current Completion Date");
            return false;
            // or display an error message to the user
        }
        else {
            //formattedDate = WoComplDate.toISOString();   popup5WoComplDt
            //var maxAllowedDate = new Date(Date.parse($('#popup5WoComplDt').val()));
            //if (WoComplDate > maxAllowedDate) {
            //    alert("WO Compl Date is greater.");
            //    return false;
            //} else {
                formattedDate = WoComplDate.toISOString();
            //}
        }

        if (planWoQty < soqty) {
            alert("Plan Wo Qnty Should Be Greater Or Equal To Calc. WO Qnty.");
            return false;
        }

        var rowData = {
            productionPlanId: parseInt(ppid),
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
            reloadOption: "Edit",
            status: parseInt(wostatus)
        };

        api.post("/WorkOrder/ProductionPlanPost", rowData).then((data) => {
            //console.log(data);
            resultData.push(...data);
            $('#popup5').modal('hide');
            loadWO();
            var tablebody = $("#Popup4Grid tbody");
            $(tablebody).html("");//empty tbody

            for (i = 0; i < resultData.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("Popup4GridRow", resultData[i]));
            }
        }).catch((error) => {
        });

    });

    $('#popup5Edit').on('shown.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var ppid = relatedTarget.data("productionplanid");
        var workOrderId = relatedTarget.data("workorderid");
        var salesOrderId = relatedTarget.data("salesorderid");
        var woNumber = relatedTarget.data("wonumber");
        var partNo = relatedTarget.data("partno");
        var planCompletionDateStr = relatedTarget.data("plancompletiondatestr");
        var partId = relatedTarget.data("partid");
        var partType = relatedTarget.data("parttype");
        var parentLevel = relatedTarget.data("parentlevel");
        var planWOQty = relatedTarget.data("calwoqty");
        var woNumber = relatedTarget.data("wonumber");
        var wostatus = relatedTarget.data("wostatus");
        var reloadoption = relatedTarget.data("pfreloadoption");
        $("#popup5EditPatNo").text(partNo);
        $("#popup5EditWoNumber").text(woNumber);
        $("#popup5WoComplDtField").val(planCompletionDateStr);
        $("#popup5EditCalcWoQnty").val(planWOQty);
        $("#popup5EditPlanWoQntyField").val(planWOQty);
        $("#popup5Editwoid").val(workOrderId);
        $("#popup5Editppid").val(ppid);
        $("#popup5Editsoid").val(salesOrderId);
        $("#popup5EditpartId").val(partId);
        $("#popup5EditpartType").val(partType);
        $("#popup5EditWoNumberField").val(woNumber);
        $("#popup5EditWoComplDt").val(planCompletionDateStr);
        $("#popup5EditStatus").val(wostatus);
        $("#popup5Reloadoption").val(reloadoption);
        $("#popup5EditQntyOnHand").val(0);

        var baltomanuf = planWOQty - 0;
        $("#popup5EditBalQnty").val(baltomanuf);
        $("#popup5EditPlanWoQnty").val(baltomanuf);


        if (partType === 1) {
            api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + partId).then((data) => {
                //console.log(data);
                const selectElement = $('#popup5EditRouting');
                selectElement.prop("disabled", false);
                $.each(data, (index, item) => {
                    selectElement.html("");
                    selectElement.append(`<option value="0">--Select--</option>`);
                    selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);
                });
            }).catch((error) => {
            });
            $('#popup5EditStartingOp').prop("disabled", false);
            $('#popup5EditEndingOp').prop("disabled", false);
        }
        else {
            const selectElement = $('#popup5EditRouting');
            selectElement.html("");
            selectElement.prop("disabled", true);
            $('#popup5EditStartingOp').html("").prop("disabled", true);
            $('#popup5EditEndingOp').html("").prop("disabled", true);
        }

    });

    $('#popup5EditRouting').on('change', (e) => {
        const routeId = $(e.target).val();
        // make an API call to get data for select2 based on the selected studentId
        api.getbulk("/WorkOrder/RoutingSteps?routingId=" + routeId).then((data) => {
            //console.log(data);
            const selectElement = $('#popup5EditStartingOp');
            const selectEndOpNo = $('#popup5EditEndingOp');
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
            //console.error(error);
        });
    });

    $("#popup5EditSaveWo").on("click", function () {
        var ppid = parseInt($("#popup5Editppid").val());
        var woid = parseInt($("#popup5Editwoid").val());
        var soid = parseInt($("#popup5Editsoid").val());
        var partid = parseInt($("#popup5EditpartId").val());
        var parttype = parseInt($("#popup5EditpartType").val());
        var wostatus = parseInt($("#popup5EditStatus").val());
        var soqty = parseInt($("#popup5EditCalcWoQnty").val());
        var planWoQty = parseInt($("#popup5EditPlanWoQntyField").val());
        var wonumber = $("#popup5EditWoNumberField").val();
        var WoComplDate = new Date(Date.parse($('#popup5EditWoComplDtField').val()));
        var formattedDate;
        var routingid = $("#popup5EditRouting").val();
        var startingOpNo = $("#popup5EditStartingOp").val();
        var endingOpNo = $("#popup5EditEndingOp").val();
        var reloadoption = $("#popup5Reloadoption").val();
        var maxAllowedDate = new Date(Date.parse($('#popup5EditWoComplDtField').text()));
        var resultData = [];
        if (isNaN(WoComplDate.getTime())) {
            alert("Please Enter the WO Compl Date.");
            return false;
            // or display an error message to the user
        } else {
            //formattedDate = WoComplDate.toISOString();   popup5WoComplDt
            if (WoComplDate < maxAllowedDate) {
                alert("Wo Completion Date Should Be Greater Than Current Completion Date");
                return false;
            } else {
                formattedDate = WoComplDate.toISOString();
            }
        }

        if (planWoQty < soqty) {
            alert("Plan Wo Qnty Should Be Greater Or Equal To Calc. WO Qnty.");
            return false;
        }

        var rowData = {
            productionPlanId: parseInt(ppid),
            //woId: parseInt(woid),
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
            reloadOption: reloadoption,
            status: parseInt(wostatus),
        };

        api.post("/WorkOrder/ProductionPlanPost", rowData).then((data) => {
            //console.log(data);
            resultData.push(data);
            $('#popup5Edit').modal('hide');
            loadWO();
            reloadWO(reloadoption, partid);
        }).catch((error) => {
        });

    });

    /*----proc update moq 
    var checkboxes = $("#SalesOrders1 tbody input[type='checkbox']:checked");
      checkboxes.each(function (index, checkbox) {
            var row = checkbox.parentNode.parentNode;
            var rowData = {
                salesOrderId: parseInt($(row).find("td:eq(1)").text()),
                wonumber: "",
                partId: parseInt($(row).find("td:eq(3)").text()),
                partType: 0,
                partlevel: ' ',
                calcWOQty: parseInt($(row).find("td:eq(8)").text()),
                planCompletionDate: $(row).find("td:eq(12)").text()
            };
            var balanceSoQty = parseInt($(row).find("td:eq(9)").text())
            if (balanceSoQty > 0) {
                rowData.calcWOQty = balanceSoQty;
            }
            // Group by PartId
            if (partIdMap[rowData.partId]) {
                partIdMap[rowData.partId].calcWOQty += rowData.calcWOQty;
                SalesorderId.push([rowData.partId, rowData.salesOrderId]);
                let currentDate = new Date(rowData.planCompletionDate);
                let storedDate = new Date(partIdMap[rowData.partId].planCompletionDate);
                if (currentDate < storedDate) {
                    partIdMap[rowData.partId].planCompletionDate = rowData.planCompletionDate;
                    partIdMap[rowData.partId].salesOrderId = rowData.salesOrderId;
                }
            } else {
                partIdMap[rowData.partId] = rowData;
                SalesorderId.push([rowData.partId, rowData.salesOrderId]);
            }
        });
*/

    $("#UpdateMOQ").on("click", function () {
        var selectedRowsData = {};
        var temprowdata = {};
        var checkboxes = $("#ProcPlanGrid tbody input[type='checkbox']:checked");
        checkboxes.each(function (index, checkbox) {
            var row = checkbox.parentNode.parentNode;
                var rowData = {
                    procPlanId: parseInt($(row).find("td:eq(1)").text()),
                    partId: parseInt($(row).find("td:eq(3)").text()),
                    partType: $(row).find("td:eq(5)").text(),
                    calc_Proc_Qnty: parseInt($(row).find("td:eq(8)").text()),
                    plan_Proc_Qnty: parseInt($(row).find("td:eq(11)").text()),
                    moq: parseInt($(row).find("td:eq(12)").text()),
                    uomid: 0,
                    workOrderId: parseInt($(row).find("td:eq(2)").text())
            };
            if (rowData.plan_Proc_Qnty < rowData.moq) {
                rowData.plan_Proc_Qnty = rowData.moq;
            }
            temprowdata[rowData.partId] = rowData;
        });

        selectedRowsData = Object.values(temprowdata);
        if (selectedRowsData.length > 0) {
        $.ajax({
            type: "POST",
            url: '/WorkOrder/PostProcPlan',
            contentType: "application/json; charset=utf-8",
            headers: { 'Content-Type': 'application/json' },
            data: JSON.stringify(selectedRowsData),
            dataType: "json",
            success: function (result) {
                loadProcPlan();
            }
        });
        } else {
            alert("Please select at least one material");
        }
    });

    $("#ReleaseWo").on("click", function () {
       
        //var checkboxes = $("#detailedPlanWo tbody input[type='checkbox']:checked"); // Select only checked checkboxes
        //var selectedRowsData = {};
        //var partIdMap = {};
        //var SalesorderId = [];
        //var WoSoRel = [];
        //var WoSOMethod = {};

        //checkboxes.each(function (index, checkbox) {
        //    var row = checkbox.parentNode.parentNode;
        //    var rowData = {
        //        productionPlanId: parseInt($(row).find("td:eq(15)").text()),
        //        salesOrderId: parseInt($(row).find("td:eq(2)").text()),
        //        wonumber: "",
        //        partId: parseInt($(row).find("td:eq(3)").text()),
        //        partType: 0,
        //        partlevel: ' ',
        //        calcWOQty: parseInt($(row).find("td:eq(7)").text()),
        //        planCompletionDate: $(row).find("td:eq(16)").text(),
        //        status: 3,
        //        reloadOption:""
        //    };
        //    var balanceSoQty = parseInt($(row).find("td:eq(9)").text())
        //    if (balanceSoQty > 0) {
        //        rowData.calcWOQty = balanceSoQty;
        //    }
        //    // Group by PartId
        //    if (partIdMap[rowData.partId]) {
        //        partIdMap[rowData.partId].calcWOQty += rowData.calcWOQty;
        //        SalesorderId.push([rowData.partId, rowData.salesOrderId]);
        //        let currentDate = new Date(rowData.planCompletionDate);
        //        let storedDate = new Date(partIdMap[rowData.partId].planCompletionDate);
        //        if (currentDate < storedDate) {
        //            partIdMap[rowData.partId].planCompletionDate = rowData.planCompletionDate;
        //            partIdMap[rowData.partId].salesOrderId = rowData.salesOrderId;
        //        }
        //    } else {
        //        partIdMap[rowData.partId] = rowData;
        //        SalesorderId.push([rowData.partId, rowData.salesOrderId]);
        //    }
        //});

        //selectedRowsData = Object.values(partIdMap);
       
        //    $.ajax({
        //        type: "POST",
        //        url: '/WorkOrder/MultipleProductionUpdateWOPost',
        //        contentType: "application/json; charset=utf-8",
        //        headers: { 'Content-Type': 'application/json' },
        //        data: JSON.stringify(selectedRowsData),
        //        dataType: "json",
        //        success: function (result) {
        //            //alert(result);
                   
        //            console.log(result);
                   
        //            window.locationre = result.url;
        //            loadWO();
        //            loadProcPlan();
        //        }
        //    });
        //}
    });

    $("#ReleasePo").on("click", function () {
        //var selectedRowsData = {};
        //var temprowdata = {};
        //var checkboxes = $("#ProcPlanGrid tbody input[type='checkbox']:checked");
        //checkboxes.each(function (index, checkbox) {
        //    var row = checkbox.parentNode.parentNode;
        //    var rowData = {
        //        procPlanId: parseInt($(row).find("td:eq(1)").text()),
        //        partId: parseInt($(row).find("td:eq(3)").text()),
        //        poQnty: parseInt($(row).find("td:eq(8)").text()),
        //        planPoReceiptDate: $(row).find("td:eq(16)").text(),
        //        companyId: parseInt($(row).find("td:eq(17)").text()),
        //    };
        //    //if (rowData.plan_Proc_Qnty < rowData.moq) {
        //    //    rowData.plan_Proc_Qnty = rowData.moq;
        //    //}
        //    temprowdata[rowData.partId] = rowData;
        //});

        //selectedRowsData = Object.values(temprowdata);
        //if (selectedRowsData.length > 0) {
        //    $.ajax({
        //        type: "POST",
        //        url: '/WorkOrder/MulitplePOdetails',
        //        contentType: "application/json; charset=utf-8",
        //        headers: { 'Content-Type': 'application/json' },
        //        data: JSON.stringify(selectedRowsData),
        //        dataType: "json",
        //        success: function (result) {
        //            loadProcPlan();
        //        }
        //    });
        //} else {
        //    alert("Please select at least one material");
        //}
    });



    //---Filtering---
    $("#searchWoPartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#detailedPlanWo tbody tr").filter(function () {
            $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $('#checkparentlevel').on('click', function () {
        if ($(this).is(':checked')) {
            // Show only rows where data-parentlevel="Y"
            $("#detailedPlanWo tbody tr").filter(function () {
                $(this).toggle($(this.children[17]).text().toLowerCase().indexOf(Y) > -1);
            });
        } else {
            // Show all rows when the checkbox is unchecked
            $("#detailedPlanWo tbody tr").show();
        }
    });

    $("#searchWoPartDesc").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#detailedPlanWo tbody tr").filter(function () {
            $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchWoNumber").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#detailedPlanWo tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchWoPartType").on("change", function () {
        var selectedValue = $(this).val();
        if (selectedValue == "0") {
            $("#detailedPlanWo tbody tr").show();
            return;
        }
        $("#detailedPlanWo tbody tr").filter(function () {
            $(this).toggle($(this.children[14]).text().toLowerCase().indexOf(selectedValue) > -1)
        });// show only the filtered rows
    });

    $("#searchMatProcPartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#ProcPlanGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchMatProcPartDesc").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#ProcPlanGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchMatProcPartType").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#ProcPlanGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchBomProcPartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#BomListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchBomProcPartDesc").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#BomListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchBomProcPartType").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#BomListGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchMCSummaryLocation").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#McTimeListSummary tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchMCSummarMCType").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#McTimeListSummary tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#searchMCSummaryMachine").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#McTimeListSummary tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#searchMCDetailParttNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#MachineTimeListDetail tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#searchMCDetailPartDesc").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#MachineTimeListDetail tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#searchMCDetailWoRef").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#MachineTimeListDetail tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#searchMCDetailLocation").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#MachineTimeListDetail tbody tr").filter(function () {
            $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#searchMCDetailMCType").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#MachineTimeListDetail tbody tr").filter(function () {
            $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#searchMCDetailMachine").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#MachineTimeListDetail tbody tr").filter(function () {
            $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
        });
    });
});


function EditWo(element) {
    //console.log("--Edit--");
    var relatedTarget = $(element);
    var workOrderId = relatedTarget.data("workorderid");
    if (workOrderId === 0) {
        workOrderId = relatedTarget.data("parentwoid");
    }
    var salesOrderId = relatedTarget.data("salesorderid");
    var woNumber = relatedTarget.data("wonumber");
    var partNo = relatedTarget.data("partno");
    var planCompletionDateStr = relatedTarget.data("plancompletiondatestr");
    var partId = relatedTarget.data("partid");
    var partType = relatedTarget.data("parttype");
    var parentLevel = relatedTarget.data("parentlevel");
    var planWOQty = relatedTarget.data("calwoqty");
    var woNumber = relatedTarget.data("wonumber");
    var wostatus = relatedTarget.data("wostatus");
    var ppid = relatedTarget.data("ppid");

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
                    if (WOSoTable.length >= 2 && partType === 2 && parentLevel == 'Y') {
                        $('#popup1').modal('show');
                        $('#popup2').modal('hide');
                        document.getElementById('WoComplDate').value = planCompletionDateStr.split("-").reverse().join("-");
                        document.getElementById('p1planComplDate').value = planCompletionDateStr.split("-").reverse().join("-");
                        $('#PlanWoQty').val(0);
                        $('#SoQty').val(planWOQty);
                        $('#woNumber').text(woNumber);
                        $('#partNo').text(partNo);
                        $('#p1ppid').val(ppid);
                        $('#WOID').val(workOrderId);
                        $('#SalesOrderId').val(salesOrderId);
                        $('#PartId').val(partId);
                        $('#PartType').val(partType);
                        $('#woStatus').val(wostatus);
                        var tablebody = $("#multipleSO tbody");
                        $(tablebody).html("");//empty tbody

                        for (i = 0; i < WOSoTable.length; i++) {
                            $(tablebody).append(AppUtil.ProcessTemplateData("MultipleSoRow", WOSoTable[i]));
                        }


                        if (partType === 1) {
                            api.getbulk("/WorkOrder/GetRoutings?manufPartId=" + partId).then((data) => {
                                //console.log(data);
                                const selectElement = $('#routing');
                                selectElement.prop("disabled", false);
                                $.each(data, (index, item) => {
                                    selectElement.html("");
                                    selectElement.append(`<option value="0">--Select--</option>`);
                                    selectElement.append(`<option value="${item.routingId}">${item.routingName}</option>`);
                                });
                            }).catch((error) => {
                            });
                            $('#StartingOpNo').prop("disabled", false);
                            $('#EndingOpNo').prop("disabled", false);
                        }
                        else {
                            const selectElement = $('#routing');
                            selectElement.html("");
                            selectElement.prop("disabled", true);
                            $('#StartingOpNo').html("").prop("disabled", true);
                            $('#EndingOpNo').html("").prop("disabled", true);
                        }
                    }
                    else if (WOSoTable.length === 1 && partType === 2 && parentLevel == 'Y') {
                        $('#popup2').modal('show');
                        $('#p2totalSoQty').val(planWOQty);
                        $('#p2BalQty').val(0);
                        $('#p2WoNumber').text(woNumber);
                        $('#p2PartNo').text(partNo);
                        $('#p2ppid').val(ppid);
                        $('#p2WOid').val(workOrderId);
                        $('#p2SalesOrderId').val(salesOrderId);
                        $('#p2PartId').val(partId);
                        $('#p2PartType').val(partType);
                        $('#p2Status').val(wostatus);
                        $('#p2QtyOnHand').val(0);
                        $('#p2PlanComplDate').text(planCompletionDateStr.split("-").reverse().join("-"));
                        //popup2show = true;
                    }
                    else {
                        $('#popup4').modal('show');
                        $('#popup4CalcWoQnty').val(planWOQty);
                        $('#popup4ppid').val(ppid);
                        $('#popup4QntyOnHanf').val(0);
                        $('#popup4BalQnty').val(0);
                        $('#popup4WoNumber').text(woNumber);
                        $('#popup4StWoNumber').text(woNumber);
                        $('#popup4PartNo').text(partNo);
                        $('#popup4woid').val(workOrderId);
                        $('#popup4soid').val(salesOrderId);
                        $('#popup4partId').val(partId);
                        $('#popup4partType').val(partType);
                        $('#popup4Status').val(wostatus);
                        $('#popup4WoComplDt').val(planCompletionDateStr);
                    }


                }
            });


        }).catch((error) => {
        });
    }

}