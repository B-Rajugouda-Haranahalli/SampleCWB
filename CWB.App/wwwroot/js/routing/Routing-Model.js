let RoutingDetails = {};//contains page model


let dataPartsRoutings = {};
let partType = "ManufacturedPart";
let spartType = "ManufacturedPart";
let withRouting = true;
let selectedManuPartId = "";
let selectedBomId = "";
let qtyAvailable = 0;
let hasRouting = true;
let stepModel = {};
let routingModel = {};
let routingSteps = {};
let operations = {};
let locations = {};
let stepMachines = {};
let maclist = {};
var noOfRoutePart = 0;
var noOfWithoutDoc = 0;
//let editSubCon = false;
let archive = 0;
let mcTypeId = 0;
let Mcid = 0;

function RoutingPerformance() {
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let manufId = params.manufPartId;
    var batch = $("#BacthSize").val();
    api.getbulk("/routings/RoutingPerformance?manufPartId=" +manufId +"&batchSize=" + batch).then((data) => {
        //console.log(data);
        var tablebody = $("#PerformanceGrid tbody");
        tablebody.html('');
        for (i = 0; i < data.length; i++) {
            let rowData = data[i];
            if (rowData.deleted == 1)
                continue;
            rowData.checked = rowData.preferredRouting === 1 ? 'checked' : '';
            $(tablebody).append(AppUtil.ProcessTemplateData("PerformanceRow", rowData));
        }
    }).catch((error) => {
    });
}


//SubCon functions-start
//SubCons / SubConWSS / DeleteSubConDetails / DeleteSubConWS / AddSubCon//AddSubConWS

////SubConWSSubConDetailsId//SubConWSRoutingStepId//SubConWSDetailsId
//WorkStepDesc//MachineType//FloorToFloorTime//SetupTime//NoOfPartsPerLoading
function AssignWorkStepVals(workStepDesc, machineType, floorToFloorTime,
    setupTime, noOfPartsPerLoading, routingStepId , subConDetailsId, subConWSDetailsId) {
    $("#WorkStepDesc").val(workStepDesc);
    $("#SubConWSSubConDetailsId").val(subConDetailsId);
    $("#SubConWSRoutingStepId").val(routingStepId);
    $("#SubConWSDetailsId").val(subConWSDetailsId);
    $("#SubConFloorToFloorTime").val(floorToFloorTime);
    $("#SubConSetupTime").val(setupTime);
    $("#SubConNoOfPartsPerLoading").val(noOfPartsPerLoading);
    $("#MachineType").val(machineType).trigger('change');
}

function DeleteWS(subConWSDetailsId)
{
    //alert(subConWSDetailsId);
    let confirmval = confirm("Are your sure you want to delete this work step?", "Yes", "No");
    if (confirmval) {
        api.get("/routings/deletews?subConWSId=" + subConWSDetailsId).then((data) => {
            //console.log(data);
            LoadSubConWSS();
        }).catch((error) => {
            AppUtil.HandleError("FormDelRoutingName", error);
        });
    }

}

function AddMacOptions() {
    api.get("/machine/getmachines").then((data) => {
        maclist = data;
        var macplants = $("#MAC_Plants");
        macplants.empty();
        var div_data = "<option value=''>--Select--</option>";
        macplants.append(div_data);
        var macshops = $("#MAC_Shops");
        macshops.empty();
        macshops.append(div_data);
        //debugger;
        if (maclist.length > 0) {
            const uniquePlants = new Set();
            const uniqueShops = new Set();

            for (let i = 0; i < maclist.length; i++) {
                const plant = maclist[i].plant;
                const shop = maclist[i].shop;

                if (!uniquePlants.has(plant)) {
                    uniquePlants.add(plant);
                    macplants.append("<option id='" + plant + "'>" + plant + "</option>");
                }

                if (!uniqueShops.has(shop)) {
                    uniqueShops.add(shop);
                    macshops.append("<option id='" + shop + "'>" + shop + "</option>");
                }
            }
        }

        loadMachineTypes("MAC_Type");
    }).catch((error) => {
        //console.log(error);
    });
}
/*function SetOpType(optype) {
    alert(optype);
    //add-edit
    if (optype == "add") {
        editSubCon = false;
    }
    else
        editSubCon = true;
}*/
function ShowAddSubConJob(event) {
    ClearWSTable();
    if ($("#StepId").val() == "0") {
        alert("Save routing step before adding subcon.");
        document.getElementById("Add-SubCon-Close").click();
        //document.getElementById("Add-Machine-Close").click();
    }
    document.getElementById("FormSubCon").reset();
    document.getElementById("FormSubConWS").reset();
    loadMachineTypes("MachineType");
    let stepId = RoutingDetails["stepId"];
    var tablebody = $("#TitleTableSubCon tbody");
    tablebody.html("");
    $(tablebody).append(AppUtil.ProcessTemplateData("TitleRow", RoutingDetails));
    ////WorkDone    //TransportTime//CostPerPart//PreferredSubCon//SubConSupplierId//SubConDetailsId//SubConRoutingStepId
    var rTgt = $(event.relatedTarget);
    var addEdit = rTgt.data("addedit");
    var selectedSupplierId = -1;
    if (addEdit == "Add") {
        $('#SubConRoutingStepId').val(stepId);
        //loadSuppliersToTable("SubConNamesTable", "SubConNameRow", 0);

        var selElem = $('#subconname');
        selElem.html('');
        api.get("/masters/suppliers").then((data) => {
            var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
            selElem.append(rdiv_data);
            for (i = 0; i < data.length; i++) {
                rdiv_data = "<option value='" + data[i].companyId + "'>" + data[i].companyName + "</option>";
                selElem.append(rdiv_data);
            }
        }).catch((error) => {
            //console.log(error);
        });
    }
    else {
        $("#subconname").text(rTgt.data("company"));
        $("#WorkDone").val(rTgt.data("workdone"));
        $("#TransportTime").val(rTgt.data("transporttime"));
        $("#CostPerPart").val(rTgt.data("costperpart"));
        $("#PreferredSubCon").val(rTgt.data("preferredsubcon"));
        $("#SubConSupplierId").val(rTgt.data("subconsupplierid"));
        selectedSupplierId = rTgt.data("subconsupplierid");
        $("#SubConDetailsId").val(rTgt.data("subcondetailsid"));
        $("#SubConRoutingStepId").val(rTgt.data("subconroutingstepid"));
        RoutingDetails["subConDetailsId"] = rTgt.data("subcondetailsid");

        $("#SubConWSSubConDetailsId").val(rTgt.data("subcondetailsid"));
        $("#SubConWSRoutingStepId").val(rTgt.data("subconroutingstepid"));

        var selElem = $('#subconname');
        selElem.html('');
        api.get("/masters/suppliers").then((data) => {
            var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
            selElem.append(rdiv_data);
            for (i = 0; i < data.length; i++) {
                rdiv_data = "<option value='" + data[i].companyId + "'>" + data[i].companyName + "</option>";
                selElem.append(rdiv_data);
            }
            $("#subconname").val(selectedSupplierId);
        }).catch((error) => {
            //console.log(error);
        });
        let subcons = document.getElementsByName("RadioSubConName");
        //console.log(subcons.length);
        for (let i = 0; i < subcons.length; i++) {
            //console.log(subcons[i].value);
            if (selectedSupplierId == subcons[i].value) {
                subcons[i].checked = true;
            }
        }
        LoadSubConWSS();
     //   console.log("stepsubcon_" + selectedSupplierId);
     //   document.getElementById("stepsubcon_" + selectedSupplierId).checked = true;
        //stepsubcon_selectedSupplierId
    }
    
}
function LoadSubCons() {
    //debugger;
    var tablebody = $("#SubConsTable tbody");
    $(tablebody).html("");//empty tbody
    let stepId = RoutingDetails["stepId"];
    //console.log("=====stepId" + stepId);
    api.get("/routings/subcons?stepId=" + stepId).then((data) => {

        for (i = 0; i < data.length; i++) {
            if (data[i].deleted == 1)
                continue;
            data[i].checked = data[i].preferredSubcon === 1 ? 'checked' : '';
            $(tablebody).append(AppUtil.ProcessTemplateData("RouteSubConsTemplate", data[i]));
        }
        //console.log(data);
        //console.log(tablebody.html());
        //RouteSuppliersTable
        //RouteSupplierTemplate
    }).catch((error) => {
    });
};

function ClearWSTable()
{
    var tablebody = $("#WSTable tbody");
    $(tablebody).html("");//empty tbody
}

function LoadSubConWSS() {
    var tablebody = $("#WSTable tbody");
    $(tablebody).html("");//empty tbody
    
    let subConDetailsId = RoutingDetails["subConDetailsId"];
    let stepId = RoutingDetails["stepId"];
  //  alert(subConDetailsId + "/" + stepId);
    //masters/subconwss
    api.get("/routings/subconwss?stepId=" + stepId +"&subConDetailsId="+subConDetailsId).then((data) => {
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateDataNew("WSRow", data[i], i));
        }
        var content = data[0].machineType;
        mcTypeId = content;
        McTypeUploadDocList(content);
        //console.log($(tablebody).html());
    }).catch((error) => {
        //console.log(error);
    });

};
function McTypeUploadDocList(content) {
        //var content = parseInt($("#StepOperation").val());
        var StepRoutingId = $("#StepRoutingId").val();
        var partid = $("#StepId").val();

    api.getbulk("/Routings/GetMcTypeDocList?mcTypeId=" + content + "&routingId=" + StepRoutingId + "&stepId=" + partid).then((data) => {
            //data = data.filter(item => item.status == 1 || item.status == 0);
        var tablebody = $("#SubConDocGrid tbody");
            $(tablebody).html("");//empty tbody
            //console.log(data);
            for (i = 0; i < data.length; i++) {
                var rowHtml = AppUtil.ProcessTemplateData("maufDocUploadRow", data[i]);

                // Check if mandatory is 'Y', if so, hide the Delete option
                if (data[i].docListId === 0) {
                    // Simplified regex to match the Upload link
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Edit *<\/a>/i, '');
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
                }
                if (data[i].docListId != 0) {
                    // Remove the Edit link from the generated row
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Upload *<\/a>/i, '');
                }

                if (data[i].mandatory === 'Y') {
                    // Remove the Delete link from the generated row
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
                }

                // Append the processed row to the table body
                $(tablebody).append(rowHtml);
            }
        }).catch((error) => {
            console.log(error);
        });

}
function McIdUploadDocList(content) {
    //var content = parseInt($("#StepOperation").val());
    var StepRoutingId = $("#StepRoutingId").val();
    var partid = $("#StepId").val();

    api.getbulk("/Routings/GetMcIdDocList?mcId=" + content + "&routingId=" + StepRoutingId + "&stepId=" + partid).then((data) => {
        //data = data.filter(item => item.status == 1 || item.status == 0);
        var tablebody = $("#MachineDocGrid tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            var rowHtml = AppUtil.ProcessTemplateData("machineDocGridRow", data[i]);

            // Check if mandatory is 'Y', if so, hide the Delete option
            if (data[i].docListId === 0) {
                // Simplified regex to match the Upload link
                rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Edit *<\/a>/i, '');
                rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
            }
            if (data[i].docListId != 0) {
                // Remove the Edit link from the generated row
                rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Upload *<\/a>/i, '');
            }

            if (data[i].mandatory === 'Y') {
                // Remove the Delete link from the generated row
                rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
            }

            // Append the processed row to the table body
            $(tablebody).append(rowHtml);
        }
    }).catch((error) => {
        console.log(error);
    });

}
function DeleteSubCon(stepId,subConDetailsId) {
    //routings/deletesubcondetails
    var routingId = $("#DelRoutingId").val();
    alert(routingId);
    api.get("/routings/deletesubcondetails?routingId=" + routingId).then((data) => {
        //console.log(data);
        routdeleted = true;
        document.getElementById("BtnDelRoutingClose").click();
    }).catch((error) => {
        AppUtil.HandleError("FormDelRoutingName", error);
    });
}

function DeleteSubConWS(stepId, subConWSDetailsId) {
    //nasters/deletesubconws
}

function AddSubCon() {
    //masters/addsubcon
    //console.log("....AddSubCon....");
    var formData = AppUtil.GetFormData("FormSubCon");
  //  console.log(formData);
    api.post("/routings/addsubcon", formData).then((data) => {
        //console.log("****AddSubCon****");
        //console.log(data);
        //console.log("****End-AddSubCon****");
        RoutingDetails["subConDetailsId"] = data.subConDetailsId;
        $("#SubConWSSubConDetailsId").val(data.subConDetailsId);
        $("#SubConWSRoutingStepId").val(RoutingDetails["stepId"]);
        $("#SaveSubCon").prop('disabled', true);

        //document.getElementById("Btn").click();  SaveSubCon
    }).catch((error) => {
        AppUtil.HandleError("FormSubCon", error);
    });
    
};

function AddSubConWS() {
    //masters/addsubconws
    var formData = AppUtil.GetFormData("FormSubConWS");
    //console.log(formData);
    api.post("/routings/addsubconws", formData).then((data) => {
        //console.log(data);
        let wsid = $("#SubConWSSubConDetailsId").val();
        let stepid = $("#SubConWSRoutingStepId").val();
        LoadSubConWSS();
        document.getElementById("FormSubConWS").reset();
        $("#SubConWSSubConDetailsId").val(wsid);
        $("#SubConWSRoutingStepId").val(stepid);
        //document.getElementById("Btn").click();
    }).catch((error) => {
        AppUtil.HandleError("FormSubConWS", error);
    });
};

function EditSubCon() {
    //masters/addsubcon
    //editSubCon = true;
    //ShowAddSubConJob();
};

function EditSubConWS() {
    //masters/addsubconws
    AddSubConWS();
};

function SetPreferredSubCon(subConDetailsId) {
    //alert("Preferred machine" + ":" + routingStepId + "/" + routingStepMachineId);
    api.get("/routings/preferredsubcon?subConDetailsId=" + subConDetailsId).then((data) => {
        LoadSubCons();
    }).catch((error) => {
    });
}



//SubCon functions-end


function LoadLocations() {
    var selElem = $('#StepLocation');//should be a select2 dropdown
    if (!selElem.length)
        return;
    selElem.empty();
    var div_data = "<option value=''></option>";
    selElem.append(div_data);
    if (locations.length > 0) {
        let data = locations;
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].id + "'>" +
                data[i].name +
                "</option>";
            //console.log(div_data);
            selElem.append(div_data);
        }
        return;
    }
    
    api.get("/routings/locations").then((data) => {
        locations = data;
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].id + "'>" +
                data[i].name +
                "</option>";
            //console.log(div_data);
            selElem.append(div_data);
        }
    }).catch((error) => {
        //console.log(error);
    });
}

function LoadOperations() {
    var selElem = $('#StepOperation');//should be a select2 dropdown
    if (!selElem.length)
        return;
    selElem.empty();
    var div_data = "<option value=''></option>";
    selElem.append(div_data);

    /*if (operations.length > 0) {
        for (i = 0; i < operations.length; i++) {
            div_data = "<option value='" +
                operations[i].operationId + "'>" +
                operations[i].operation +
                "</option>";
            selElem.append(div_data);
        }
        return;
    }*/
    if (operations.length > 0) {
        let data = operations;
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].operationId + "'>" +
                data[i].operation +
                "</option>";
            //console.log(div_data);
            selElem.append(div_data);
        }
        return;
    }

    api.get("/operationlist/operations").then((data) => {
        operations = data;
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].operationId + "'>" +
                data[i].operation +
                "</option>";
            //console.log(div_data);
            selElem.append(div_data);
        }
    }).catch((error) => {
        //console.log(error);
    });
}


function ProcessTemplateDataNew(templateId, dataObj) {
    var templateElement = $("#" + templateId).html();
    ////console.log(templateId);
    templateElement = templateElement.replaceAll("{partType}", partType)
    for (var key in dataObj) {
        ////console.log(key + " " + dataObj[key]);
        templateElement = templateElement.replaceAll("{" + key + "}", dataObj[key])
    }
    //console.log(templateElement);
    return templateElement;
}

// JavaScript source code
function DowlonadPartsRoutings() {
    //RoutingListItems
    var tablebody = $("#PartsRoutingsTable tbody");
    $(tablebody).html("");//empty tbody
    //UpdatePurchaseDetailsTableFromPostData
    let i = 0;
    if (dataPartsRoutings.length > 2) {
        noOfRoutePart = 0;
        noOfWithoutDoc = 0;
        let data = dataPartsRoutings;
        for (i = 0; i < data.length; i++) {
            if (!(data[i]['masterPartType'] == partType))
                continue;

            if ((data[i]['mandocAvl'] == "")) {
                data[i]['mandocAvl'] = "N/A"
                noOfWithoutDoc = noOfWithoutDoc + 1;
            } else if ((data[i]['mandocAvl'] == "N/A")) {
                noOfWithoutDoc = noOfWithoutDoc + 1;
            }
            if ((data[i]['mandocAvl'] == "No")) {
                noOfWithoutDoc = noOfWithoutDoc + 1;
            }
            if ((data[i]['noOfRoutes'] == 0)) {
                noOfRoutePart = noOfRoutePart + 1;
            }
            var rowHtml = AppUtil.ProcessTemplateData("Parts-Routing-Template", data[i]);
            if (data[i].noOfRoutes === 0) {
                // Remove the "Edit" link if noOfRoutes is 0
                rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" onclick="EditRoutes\(event,[^,]+,[^,]+\);" class="dropdown-item">Edit<\/a>/, '');
            } else {
                // Remove the "Create New Routing" link if noOfRoutes is greater than 0
                rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" onclick="EditRoutes\(event,[^,]+,[^,]+\);" class="dropdown-item">Create New Routing<\/a>/, '');
            }
            $(tablebody).append(rowHtml);
            $("#prWithOutRoute").val(noOfRoutePart);
            $("#prWithOutDoc").val(noOfWithoutDoc);
        }
    }
    else {
        api.getbulk("/routings/routinglistitems").then((data) => {
            //console.log(data);
            noOfRoutePart = 0;
            noOfWithoutDoc = 0;
            dataPartsRoutings = data;
            for (i = 0; i < data.length; i++) {
                if (!(data[i]['masterPartType'] == partType))
                    continue;

                if ((data[i]['mandocAvl'] == "")) {
                    data[i]['mandocAvl'] = "N/A"
                    noOfWithoutDoc = noOfWithoutDoc + 1;
                }
                if ((data[i]['mandocAvl'] == "No")) {
                    noOfWithoutDoc = noOfWithoutDoc + 1;
                }
               // $(tablebody).append(AppUtil.ProcessTemplateDataNew("Parts-Routing-Template", data[i], i));
                var rowHtml = AppUtil.ProcessTemplateData("Parts-Routing-Template", data[i]);
                if ((data[i]['noOfRoutes'] == 0)) {
                    noOfRoutePart = noOfRoutePart + 1;
                }
                if (data[i].noOfRoutes === 0) {
                    // Remove the "Edit" link if noOfRoutes is 0
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" onclick="EditRoutes\(event,[^,]+,[^,]+\);" class="dropdown-item">Edit<\/a>/, '');
                } else {
                    // Remove the "Create New Routing" link if noOfRoutes is greater than 0
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" onclick="EditRoutes\(event,[^,]+,[^,]+\);" class="dropdown-item">Create New Routing<\/a>/, '');
                }
                $(tablebody).append(rowHtml);
                $("#prWithOutRoute").val(noOfRoutePart);
                $("#prWithOutDoc").val(noOfWithoutDoc);

            }
        }).catch((error) => {
        });
    }
}

function showElement(elem) {
    elem.style.display = "block";
};
function hideElem(elem) {
    elem.style.display = "none";
};
function hideMachinesSuppliersTable() {
    let machs = document.getElementById("Div_RouteMachines");
    let suplrs = document.getElementById("Div_RouteSubCons");
    hideElem(machs);
    hideElem(suplrs);
};
function getRoutingInfoFromTable() {
    //stepsupplierselect
    var chkdelm = $('input[name=RoutingChk]:checked');
    var currentrow = chkdelm.closest('tr');
    var routingId = $('input[name=RoutingChk]:checked').val();
    var routingName = currentrow.find("td:eq(1)").html();
    RoutingDetails["routingName"] = routingName;
    RoutingDetails["routingId"] = routingId;
    RoutingDetails["stepNumber"] = "";
};
function getRoutingInfoFromTarget(event) {
    //stepsupplierselect
    var relatedTarget = $(event.relatedTarget);
    var routingId = relatedTarget.data("routingid");
    var routingName = relatedTarget.data("routingname");
    RoutingDetails["routingName"] = routingName;
    RoutingDetails["routingId"] = routingId;
    RoutingDetails["stepNumber"] = "";
};

function emptyTables() {
    var tablebody = $("#RouteMachinesTable tbody");
    $(tablebody).html("");//empty tbody
    var tablebody = $("#SubConsTable tbody");
    $(tablebody).html("");//empty tbody
};



function loadSetSuppliers() {
    //debugger;
    var tablebody = $("#RouteSuppliersTable tbody");
    $(tablebody).html("");//empty tbody
    let stepId = RoutingDetails["stepId"];
    //console.log("=====stepId" + stepId);
    api.get("/routings/stepsuppliers?stepId=" + stepId).then((data) => {

        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("RouteSupplierTemplate", data[i]));
        }
        //console.log(data);
        //console.log(tablebody.html());
        //RouteSuppliersTable
        //RouteSupplierTemplate
    }).catch((error) => {
    });
};

function DoRoutingDetailsJob() {
    //getRoutingInfoFromTarget(event);
    $("#DivRoutingName").html("Routing " + RoutingDetails.routingName + " ");
    $("#DivRoutingName1").html("Routing Selected : " + RoutingDetails.routingName + " ");
    var apartName = $("#SpanPartName").text();
    var apDesc = $("#SpanPartDesc").text();
    var aComp = $("#SpanComp").text();
    $("#RDSpanPartName").text(apartName);
    $("#RSDPartName").text(apartName);
    $("#RDSpanPartDesc").text(apDesc);
    $("#RSDPartDesc").text(apDesc);
    //$("#RDSpanPartName").text(apartName);
    RoutingDetails["partNo"] = apartName;
    RoutingDetails["partDescription"] = apDesc;
    RoutingDetails["companyName"] = aComp;
    $("#RSDOpNor").text($("#StepNumber").val());
    $("#RDSpanComp").text(aComp);
    $("#RSDComp").text(aComp);
    $('#StepRoutingId').val(RoutingDetails.routingId);
    LoadRoutingSteps(RoutingDetails.routingId);

   // DisplayBomMessage();
}

function DisplayBomMessage() {
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let pvalue = params.manufPartId;
    let manufId = pvalue;
    let parttypeurl = params.partType;
    if (parttypeurl == "Assembly") {
        for (i = 0; i < routingSteps.length; i++) {

            api.get("/routings/boms?manufId=" + manufId + "&stepId=" + routingSteps[i].stepId).then((rData) => {
                if (rData.every(item => item.balanceQuantity === 0)) {
                    //console.log("All Qnty Used");
                    $("#BOMMessageDisplay").text('All BOM Part Nos & BOM Qnty are used in Assembly').css('color', 'black');
                } else {
                    $("#BOMMessageDisplay").text('All BOM Part Nos & BOM Qnty are not used in Assembly').css('color', 'red');
                }
            }).catch((error) => {
                // Handle error
            });
        }
    }
}

function loadStepMachinesForAdd() {
    var selElem = $('#StepMachine');//should be a select2 dropdown
    if (!selElem.length)
        return;
    selElem.empty();
    var div_data = "<option value=''></option>";
    selElem.append(div_data);
    let stepId = RoutingDetails["stepId"];
    //console.log("stepId" + stepId);
    api.get("/routings/stepmachines?stepId=" + stepId).then((data) => {
        stepMachines = data;
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" +
                data[i].routingStepMachineId + "'>" +
                data[i].name +
                "</option>";
           // console.log(div_data);
            selElem.append(div_data);
        }
    }).catch((error) => {
    });
};

function SetAssemblyIds() {
    $("#BOMManufacturedPartId").val(RoutingDetails.manufacturedPartId);
    var stepId = $("#StepId").val();
    $("#BOMRoutingStepId").val(stepId);
}
function LoadStepPartsFromData(rData) {
    var tablebody = $("#StepPart-BOMQTY-Table tbody");
    tablebody.html("");
    let i = 0;
    let lastSelectedBOMId = "";
    for (i = 0; i < rData.length; i++) {
        if (rData[i].quantityUsed != "0") {
            $(tablebody).append(AppUtil.ProcessTemplateData("RT_BOMQTY-template", rData[i]));
            if (lastSelectedBOMId == "") {
                lastSelectedBOMId = rData[i]['mpbomId'];
                $("#" + lastSelectedBOMId).attr('checked', true);
            }
        }
    }
   // console.log(rData);
}

function LoadStepParts(partId, stepId) {
    var tablebody = $("#StepPart-BOMQTY-Table tbody");
    tablebody.html("");
    let rData = {};
    let i = 0;
    api.get("/routings/boms?partId=" + partId + "&stepId=" + stepId).then((rData) => {
        for (i = 0; i < rData.length; i++) {
            if (rData[i].quantityUsed != "0") {
                $(tablebody).append(AppUtil.ProcessTemplateData("RT_BOMQTY-template", rData[i]));
            }
        }
        //console.log(rData);
        //  LoadStepPartsFromData(rData);
    }).catch((error) => {
    });
}

function SetBOMEditVals(partId, stepId, partNo, bomId, partDesc, qty) {
    //console.log(partId + "/" + stepId + "/" + partNo + "/" + bomId + "/" + partDesc + "/" + qty);
    SetOp("edit");
    $("#BOMId").val(bomId);
    $("#BOMPartNo").val(partNo);
    $("#BOMPartDescription").val(partDesc);
    $("#QuantityAssembly").val(qty);
    $("#RoutingStepPartId").val(partId);
    //BOMPartNo,BOMPartDescription,QuantityAssembly
    //BOMId
    //RoutingStepPartId
    //BOMRoutingStepId,BOMManufacturedPartId
    SetAssemblyIds();
    document.getElementById("BOMChk_" + bomId).checked = true;
    var chkdelm = $('input[name=BOMChk]:checked');
    var currentrow = chkdelm.closest('tr');
    var qtySelected = currentrow.find("td:eq(4)").html();
    qtyAvailable = parseInt(qtySelected);
    qtyAvailable += qty;
}

function DeleteStepPart(partId, stepId) {
    //int stepId,int stepPartId
    //alert(partId + "/" + stepId);
    let confirmval = confirm("Are your sure you want to delete this BOM?", "Yes", "No");
    if (confirmval) {
        api.get("/routings/deletesteppart?stepId=" + stepId+"&stepPartId="+partId).then((data) => {
            //console.log(data);

            var tablebody = $("#BomUsedGridDisplay tbody");
            tablebody.html("");
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });
            let pvalue = params.manufPartId;
            let manufId = pvalue;
            let stepId = $("#StepId").val();
            api.get("/routings/boms?manufId=" + manufId + "&stepId=" + stepId).then((rData) => {
                for (i = 0; i < rData.length; i++) {
                    if (rData[i].quantityUsed != "0") {
                        $(tablebody).append(AppUtil.ProcessTemplateData("BomGridLandingRow", rData[i]));

                    }
                }
            }).catch((error) => {
            });
            LoadBOMList(stepId);
        }).catch((error) => {
        });
    }
}

function loadChangelog(routingId) {
    var tablebody = $("#CSLogGrid tbody");
    tablebody.html("");
    api.get("/routings/GetRoutingStatusLog?routingId=" + routingId).then((rData) => {
        for (i = 0; i < rData.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("CSLogRow", rData[i]));
        }
    }).catch((error) => {
    });
}

function LoadRoutingSteps(routingId) {
    var tablebody = $("#StepTable tbody");
    tablebody.html("");
    let rData = {};
    let i = 0;
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let pvalue = params.manufPartId;
    let manufId = pvalue;
    let rdpartType = params.partType;
    if (rdpartType == "ManufacturedPart") {
        $("RdMakeName").text("made from");
    }
    api.get("/routings/routingsteps?routingId=" + routingId ).then((rData) => {
        rData.sort((a, b) => a.stepSequence - b.stepSequence);
        for (i = 0; i < rData.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("RoutingStepTemplate", rData[i]));
        }
        routingSteps = rData;
        //console.log(rData);
        DisplayBomMessage();
    }).catch((error) => {
    });
}

function LoadBOMList(stepId) {
    //debugger;
    var tablebody = $("#StepPart-BOM-Table tbody");
    tablebody.html("");
    let rData = {};
    let i = 0;
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let pvalue = params.manufPartId;
    let manufId = pvalue;
    api.get("/routings/boms?manufId=" + manufId + "&stepId=" + stepId).then((rData) => {
        //console.log(rData);
        for (i = 0; i < rData.length; i++) {
            if (rData[i].quantityUsed == "0") {
                $(tablebody).append(AppUtil.ProcessTemplateData("RT_BOM-template", rData[i]));
            }
        }
        // Get the first radio button element
        var firstRadioBtn = $('input[type="radio"][name="BOMChk"]:first');

        // Check the first radio button
        firstRadioBtn.prop('checked', true);
        LoadStepPartsFromData(rData);
    }).catch((error) => {
    });

}

function onlyNum() {
    let qa = document.getElementById("QuantityAssembly");
    let qaval = qa.value;

    if (qaval != '') {
        if (isNaN(qaval)) {
            // Set input value empty
            qa.value = "";
            return false;
        } else {
            let iQaVal = parseInt(qaval);
            if (iQaVal < 0) {
                qa.value = "";
                return false;
            }

            return true
        }
    }
}

/**
 * 
 * 
 * @param {any} stepId
 * @param {any} stepNumber
 * @returns
 */

function getAndShowStep(stepId, stepNumber) {
    /*var relatedTarget = $(event.relatedTarget);
    var stepId = relatedTarget.data("stepId");
    var stepNumber = relatedTarget.data("stepNumber");*/
    let data = routingSteps;
    let step = {};
    for (i = 0; i < data.length; i++) {
        if (data[i]["stepId"] == stepId) {
            //console.log("******Found Data*******");
            step = data[i];
            //console.log(data[i]);
            //console.log("*************");
        }
    }
    document.getElementById("FormRoutingStep").reset();
    $('a[href="#rsd"]').tab("show");
   
    LoadOperations();
    LoadLocations();
    $("#Status").val(step.status);
    $("#StepId").val(step.stepId);
    $("#StepRoutingId").val(step.routingId);
    $("#StepSequence").val(step.stepSequence);
    $("#NumberOfSimMachines").val(step.numberOfSimMachines);
    RoutingDetails["stepId"] = step.stepId;
    RoutingDetails["stepNumber"] = step.stepNumber;
    
    $("#RSDOpNor").text(step.stepNumber);
    $("#StepNumber").val(step.stepNumber);
    $("#StepDescription").val(step.stepDescription);
    let id = step.stepOperation;
    //console.log("sso " + step.stepOperation + "/" + id);
    $("#StepOperation").val(id).trigger('change');
    id = step.stepLocation;
    //console.log("ssl " + step.stepLocation + "/" + id);
    $("#StepLocation").val(id).trigger('change');
    if (stepId > 0) {
        var selectStepLoc = document.getElementById('StepLocation');
        selectStepLoc.style.pointerEvents = 'none';
        $("#addSubCon").prop('disabled', false);
        $("#addMachine").prop('disabled', false);
    } else {
        selectStepLoc.style.pointerEvents = 'auto';
        $("#addSubCon").prop('disabled', true);
        $("#addMachine").prop('disabled', true);
    }
    const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
    });
    let pvalue = params.partType; // "some_value"
    const $tabStepParts = $('#tab-step-parts');
    if (pvalue == "ManufacturedPart") {
        $tabStepParts.hide();
        $("#Div_BomGrid").hide();
    }
    else {
        $tabStepParts.show();
        $("#Div_BomGrid").show();
        var tablebody = $("#BomUsedGridDisplay tbody");
        tablebody.html("");
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        let pvalue = params.manufPartId;
        let manufId = pvalue;
        let stepId = $("#StepId").val();
        api.get("/routings/boms?manufId=" + manufId + "&stepId=" + stepId).then((rData) => {
            for (i = 0; i < rData.length; i++) {
                if (rData[i].quantityUsed != "0") {
                    $(tablebody).append(AppUtil.ProcessTemplateData("BomGridLandingRow", rData[i]));
                    
                }
            }
        }).catch((error) => {
        });
    }
    return false;
}

function getLocationId(val) {
    let data = locations;
    //console.log("Comparing..." + val);
    for (i = 0; i < data.length; i++) {
        //console.log("With..." + data[i].name + "/" + data[i].id);
        if (data[i].name == val) {
            return data[i].id;
        }
    }
    return 'NA';
}

function getOperationId(val) {
    let data = operations;
    //console.log("Comparing..." + val);
    for (i = 0; i < data.length; i++) {
        //console.log("With..." + data[i].operation + "/" + data[i].operationId);
        if (data[i].operation == val) {
            return data[i].operationId;
        }
    }
    return 'NA';
}

function loadStepMachines() {
    //debugger;
    var tablebody = $("#RouteMachinesTable tbody");
    $(tablebody).html("");//empty tbody
    let stepId = RoutingDetails["stepId"];
    //console.log("****stepId" + stepId);
    api.get("/routings/stepmachines?stepId=" + stepId).then((data) => {
        for (i = 0; i < data.length; i++) {
            data[i].bgColor = "white"
            data[i].strPreferedMachine = ""
            if (data[i].preferredMachine == 1) {
                data[i].bgColor = "#eee"
                data[i].strPreferedMachine = "Yes"
            }
            data[i].checked = data[i].preferredMachine === 1 ? 'checked' : '';
            $(tablebody).append(AppUtil.ProcessTemplateData("RouteMachinesRowTemplate", data[i]));
        }
        var noofmc = parseInt($("#NumberOfSimMachines").val());
        if (noofmc == data.length) {
            $("#addMachine").prop('disabled', true);
        } else {
            $("#addMachine").prop('disabled', false);
        }
        //console.log("****loadStepMachines*****");
        //console.log(data);
        //console.log("*************");
        //console.log(tablebody.html());
        //RouteSuppliersTable
        //RouteSupplierTemplate
    }).catch((error) => {
    });
};

function SetPreferredMachine(event, routingStepId, routingStepMachineId) {
    //alert("Preferred machine" + ":" + routingStepId + "/" + routingStepMachineId);
    var maxMachineCount = $("#NumberOfSimMachines").val();
    api.get("/routings/preferredstepmachine?routingStepId=" + routingStepId + "&routingStepMachineId=" + routingStepMachineId + "&maxMachineCount=" + maxMachineCount).then((data) => {
        //console.log(data);
        //console.log("loadStepMachines..")
        loadStepMachines();
    }).catch((error) => {
    });
}
function EditRoute(routingId,routingName,manufPartId) {
    //alert("Todo..");
    RoutingDetails["routingName"] = routingName;
    RoutingDetails["manufacturedPartId"] = manufPartId;
    RoutingDetails["routingId"] = routingId;
    RoutingDetails["stepNumber"] = "";
    DoRoutingDetailsJob();
    $('a[href="#rou-det"]').tab("show");
    //"#rou-det"
}
function ViewRoute() {
    //alert("Todo..");
}

function EditRoutes(event, noOfRoutes, manufacturedPartId) {
    if (noOfRoutes <= 0) {
        $("#BtnCreateAlRouting").hide();
    } else {
        $("#BtnCreateNewRouting").hide();
    }
       // return;
    selectedManuPartId = manufacturedPartId;
    $.ajax({
        type: "POST",
        url: "/routings/EncodeManufacturedPartId",
        data: { manufacturedPartId: manufacturedPartId },
        success: function (encodedManufPartId) {
            window.location.href = "/routings/routingdetails?manufPartId=" + encodedManufPartId + "&partType=" + partType;
        }
    });
    //window.location.href = "/routings/routingdetails?manufPartId=" + selectedManuPartId + "&partType=" + partType;
}


function updateSequenceNumbers() {
    $('#StepTable tbody tr').each(function (index) {
        //$(this).find('td:eq(1)').text(index + 1);
        $(this).find('td:eq(13)').text(index + 1); // update stepSequence
    });
}

function updateSequenceInDB() {
    var sequence = [];
    $('#StepTable tbody tr').each(function (index) {
        sequence.push({
            stepId: parseInt($(this).find('td:eq(11)').text()),
            routingId: parseInt($(this).find('td:eq(12)').text()),
            stepSequence: parseInt($(this).find('td:eq(13)').text())
        });
    });
    $.ajax({
        type: "POST",
        url: '/routings/ChangeRoutingStepSequence',
        contentType: "application/json; charset=utf-8",
        headers: { 'Content-Type': 'application/json' },
        data: JSON.stringify(sequence),
        dataType: "json",
        success: function (result) {
            console.log(result);
            if (result) {
                //loadDocList();
            }
        }
    });
}
function RouteloadMachinesToTable(tableName, rowTemplate, addEdit,machineid) {
    var machinelist = {};
    var tablebody = $("#" + tableName + " tbody");
    $(tablebody).html("");//empty tbody
    api.get("/machine/getmachines").then((data) => {
        //console.log(data);
        machinelist = data;
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateDataNew(rowTemplate, data[i], i));
        }
        if (addEdit != "Add") {
            // Corrected parentheses
            $("input[name='stepmachineselect'][value='" + machineid + "']").prop('checked', true);
        }
        return machinelist;
        //console.log($(tablebody).html());
    }).catch((error) => {
        //console.log(error);
    });
    return {};
    ;
}



$(function () {
    $('#StepTable').on('click', '.move-ups', function () {
        var row = $(this).closest('td').parent('tr');
        console.log('Row Up:', row);
        var prevRow = row.prev('tr');
        console.log('Prev Row:', prevRow);
        if (prevRow.length) {
            row.insertBefore(prevRow);
            updateSequenceNumbers();
            updateSequenceInDB();
        }
    });
    $("#StepTable tbody").sortable({
        start: function (e, ui) {
            var elements = ui.item.siblings('.selected.hidden').not('.ui-sortable-placeholder');
            ui.item.data('items', elements);
        },
        update: function (e, ui) {
            ui.item.after(ui.item.data("items"));
        },
        stop: function (e, ui) {
            ui.item.siblings('.selected').removeClass('hidden');
            $('tr.selected').removeClass('selected');
            updateSequenceNumbers();
            // Call function to update the database after sorting stops
            updateSequenceInDB();
        }
    }).disableSelection();
    $('#StepTable').on('click', '.move-downs', function () {
        var row = $(this).closest('td').parent('tr');
        console.log('Row Down:', row);
        var nextRow = row.next('tr');
        console.log('Next Row:', nextRow);
        if (nextRow.length) {
            row.insertAfter(nextRow);
            updateSequenceNumbers();
            updateSequenceInDB();
        }
    });

   // console.log("Ready");
    const table = document.querySelector('#RoutingGrid');
    if (table != null) {
        const rows = table.tBodies[0].rows;
        if (rows.length <= 0) {
            $("#BtnCreateAlRouting").hide();
        } else {
            $("#BtnCreateNewRouting").hide();
        }
        RoutingPerformance();
    }
    if (RoutingDetails) {
        partType = RoutingDetails['masterPartType'];
        if (partType == undefined) {
            partType = "ManufacturedPart";
        }
        if (partType == "") {
            partType = "ManufacturedPart";
        }
        //console.log("partType" + partType);
    }
    DowlonadPartsRoutings();
    //LoadBOMList(16);
    LoadOperations();
    LoadLocations();
    //======


   

    $("#CancelDelSubCon").click(function (event) {
        //routings/addnewrouting
        document.getElementById("BtnDelSubConClose").click();
    });

    $('#delete-subcon').on('hide.bs.modal', function (event) {
        if (!subcondeleted)
            return;
        LoadSubCons();
    });
    let subcondeleted = false;
    $("#BtnDelSubConSave").on("click", function (event) {
        //routings/addnewrouting
        var subcondetailsid = $("#DelSubConDetailsId").val();
        var stepid = $("#DelSubConStepId").val();
        api.get("/routings/deletesubcondetails?stepId=" + stepid + "&subConDetailsId=" + subcondetailsid).then((data) => {
           // console.log(data);
            subcondeleted = true;
            document.getElementById("BtnDelSubConClose").click();
        }).catch((error) => {
        });
    });
    $('#delete-subcon').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var subcondetailsid = relatedTarget.data("subcondetailsid");
        var stepId = relatedTarget.data("stepid");
        var company = relatedTarget.data("company");
        $("#DelSubConDetailsId").val(subcondetailsid);
        $("#DelSubConStepId").val(stepId);
        var elm = document.getElementById("SubConToDelete");
        elm.innerText = company;
    });
    
    $('#add-subcon').on('hide.bs.modal', function (event) {
        //editSubCon = false;
        LoadSubCons();
        $("#SaveSubCon").prop('disabled', false);
    });
    $('#AssociateBOMParts').on('shown.bs.modal', function (event) {
        LoadBOMList($("#StepId").val());
    });
    $('#add-subcon').on('shown.bs.modal', function (event) {
        ShowAddSubConJob(event);
    });
 
    $('#WithoutRouting').change(function () {
        if (this.checked) {
            hasRouting = false;
        }
        hasRouting = true;
     //   DowlonadPartsRoutings();
    });

    $('#StepMachine').change(function () {
        //debugger;
        let selVal = $(this).val();
        var val = $('input[name=mod-12]:checked').val();
        alert(selVal + "/" + val);
        if (val == "2") {
            let data = stepMachines;
            //console.log(data);
            for (i = 0; i < data.length; i++) {
                if (selVal == data[i].routingStepMachineId) {
                    $("#SetupTime").val(data[i].setupTime);
                    $("#FloorToFloorTime").val(data[i].floorToFloorTime);
                    $("#FirstPieceProcessingTime").val(data[i].firstPieceProcessingTime);
                    $("#NoOfPartsPerLoading").val(data[i].noOfPartsPerLoading);
                    break;
                }
            }
        }
    });

    $('#StepLocation').change(function () {
        //debugger;
        let selVal = $(this).val();
        let selText = $(this).text();
        let machs = document.getElementById("Div_RouteMachines");
        let suplrs = document.getElementById("Div_RouteSubCons");
        $("#NumberOfSimMachines").hide();
        $("#lblNumberOfSimMachines").hide();
        //if (partType == "ManufacturedPart") {
        //    $("#tab-step-parts").hide();
        //}
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        nspartType = params.partType;
        if (nspartType == "ManufacturedPart") {
            $("#tab-step-parts").hide();
            $("#Div_BomGrid").hide();
        } else {
            $("#tab-step-parts").show();
            $("#Div_BomGrid").show();
            var tablebody = $("#BomUsedGridDisplay tbody");
            tablebody.html("");
        }
        
        if (selVal == "1")//Inhouse
        {
            showElement(machs);
            hideElem(suplrs);
            //Div_RouteMachines show
            var stepid = $("#StepId").val();
            if (isNaN(stepid) || stepid === "0") {
                $("#RouteMachinesTable tbody").html("");
            } else {
                loadStepMachines()
            }
            $("#NumberOfSimMachines").show();
            $("#lblNumberOfSimMachines").show();
        }
        else if (selVal == "2")//SubCon
        {
            //Div_RouteSuppliers show
            hideElem(machs);
            showElement(suplrs);
            //loadSetSuppliers();
            LoadSubCons();
        }
        else {
            hideElem(machs);
            hideElem(suplrs);
        }
        $("#addSubCon").prop("disabled", true);
        $("#addMachine").prop("disabled", true); 
    });

    /*const checkbox = document.getElementById('WithoutRouting')

    checkbox.addEventListener('change', (event) => {
        if (event.currentTarget.checked) {
            hasRouting = false;
            DowlonadPartsRoutings();
        } else {
            hasRouting = true;
            DowlonadPartsRoutings();
        }
    });*/

    $('#BtnSelectBOMForRouting').click(function (event) {
        SetOp("add");
        //if (IsAddOpCalled())
        {
            var bomId = $('input[name=BOMChk]:checked').val();
            var chkdelm = $('input[name=BOMChk]:checked');
            var currentrow = chkdelm.closest('tr');
            var partNo = currentrow.find("td:eq(1)").html();
            var partDesc = currentrow.find("td:eq(2)").html();
            var qty = currentrow.find("td:eq(4)").html();
            qtyAvailable = parseInt(qty);
            if (qtyAvailable <= 0) {
                alert("Quantity not available.");
                return;
            }
            $("#BOMId").val(bomId);
            $("#BOMPartNo").val(partNo);
            $("#BOMPartDescription").val(partDesc);
            $("#QuantityAssembly").val(qtyAvailable);
            SetAssemblyIds();
            //console.log("manufacturedPartId / BOMRoutingStepId / RoutingStepPartId: " + RoutingDetails.manufacturedPartId + "/" + $("#BOMRoutingStepId").val() + "/" + $("#RoutingStepPartId").val());
            //console.log(bomId + ":" + partNo + ":" + partDesc + ":" + qty);
        }
    });

    $('input[type=radio][name=RoutingChk]').change(function () {
        getRoutingInfoFromTable();
        $("#DivRoutingName").html("<h5>Routing Name : " + RoutingDetails.routingName + "</h5>");
        $("#DivRoutingName1").html("<h5>Routing Selected : " + RoutingDetails.routingName + "</h5>");
    });
    $("#RoutingAvailable").click(function (event) {

    });

   

    $("#RoutingDetails").click(function (event) {
        //DoRoutingDetailsJob(event);
    });

    $("#RoutingStepDetails").click(function (event) {
        if (partType == "ManufacturedPart") {
            $("#tab-step-parts").hide();
        }
        $("#tab-step-info").show();
        document.getElementById("FormRoutingStep").reset();
        hideMachinesSuppliersTable();
        //document.getElementById("FormRoutingStep").reset();
        /* RoutingDetails["stepId"] = -1;
         RoutingDetails["stepNumber"] = -1;*/
        emptyTables();
      //  getRoutingInfoFromTable();
        $("#BOMManufacturedPartId").val(RoutingDetails.manufacturedPartId);
        $('#StepRoutingId').val(RoutingDetails.routingId);
    });

   

    function setDialogTitles() {
        //stepModel
        //RoutingDetails
    };
    
    $("#tab-step-info").click(function (event) {
       /* document.getElementById("FormRoutingStep").reset();
        emptyTables();
        var routingid = $('input[name=RoutingChk]:checked').val();
        $('#StepRoutingId').val(routingid);
        hideMachinesSuppliersTable();*/
    });

    $("#BtnBomPartsClose").click(function (event) {
        $('#AssociateBOMParts').modal('hide');
    });
    $("#tab-step-parts").click(function (event) {

        if ($("#StepId").val() == "0") {
            alert("Please save step info to proceed further.");
            event.preventDefault();
            return;
        }
        else {
            $('#AssociateBOMParts').modal('show');
            LoadBOMList($("#StepId").val());
        }
    });
    
    
    
    $("#SearchSubConName").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#SubConNamesTable tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#master_partno").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#PartsRoutingsTable tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#master_description").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#PartsRoutingsTable tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    }); 
    
    $("#master_co").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#PartsRoutingsTable tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $('#ChkprWithOutDoc').on('click', function () {
        if ($(this).is(':checked')) {
            var v = "N/A";
            var value = v.toLowerCase();
            $("#PartsRoutingsTable tbody tr").filter(function () {
                var temp = $(this.children[4]).text().toLowerCase();
                $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#PartsRoutingsTable tbody tr").show(); // show all rows when checkbox is unchecked
        }
    });
    $('#ChkprWithOutRoute').on('click', function () {
        if ($(this).is(':checked')) {
            var v = "0";
            var value = 0;
            $("#PartsRoutingsTable tbody tr").filter(function () {
                var temp = $(this.children[3]).text().toLowerCase();
                $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#PartsRoutingsTable tbody tr").show(); // show all rows when checkbox is unchecked
        }
    });
    
    $("#CalcBatchSize").on("click", function () {
        var bs = $("#BacthSize").val();
        if (isNaN(bs)|| bs==0) {
            var newNamevalidate = document.getElementById('BacthSize');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('BacthSize');
            newNamevalidate.style.border = '';
            RoutingPerformance();
        }
    });
    $("#ClearSearchFields").on("click", function () {
        $("#PartsRoutingsTable tbody tr").show();
        $("#master_partno").val('');
        $("#master_co").val('');
        $("#ChkprWithOutRoute").prop('checked', false);
        $("#ChkprWithOutDoc").prop('checked', false);
        $("#ChkprUpdate").prop('checked', false);
    });

    $('input[type=radio][name=MasterPartType]').change(function () {
        if (this.value == "1") {
            // alert("one clicked");
            partType = "ManufacturedPart";
            spartType = "ManufacturedPart";
            DowlonadPartsRoutings();
            $("#MakefromDiv").show();
        } else {
            partType = "Assembly";
            spartType = "Assembly";
            DowlonadPartsRoutings();
            $("#MakefromDiv").hide();
        }

    });
    $('input[type=radio][name=WithRouting]').change(function () {
        if (this.value == "1") {
            //  alert("three clicked");
            //      loadBOFs();
            withRouting = true;
            DowlonadPartsRoutings();
        } else {
            //  alert("four clicked");
            //     loadSupplierRMS();
            withRouting = false;
            DowlonadPartsRoutings();
        }

    });
    

    $('#preferred-rout').on('show.bs.modal', function (event) {   // Propdsdosd
        var relatedTarget = $(event.relatedTarget);
        var routingid = relatedTarget.data("routingid");
        var origroutingid = relatedTarget.data("origroutingid");
        var manufacturedPartId = relatedTarget.data("manufid");
        var routingName = relatedTarget.data("routingname");
        var preferredrouting = relatedTarget.data("preferredrouting");
        var mkpartid = relatedTarget.data("mkpartid");
        selectedManuPartId = manufacturedPartId;
        $("#PManufacturedPartId").val(manufacturedPartId);
        $("#PRoutingName").val(routingName);
        $("#RoutingNameText").text(routingName);
        $("#PRoutingId").val(routingid);
        $("#POrigRoutingId").val(origroutingid);
        $("#PMKPartId").val(mkpartid);
        var checkbox = $("#CheckPPreferredRouting");
        if (preferredrouting === 1) {
            checkbox.prop("checked", true);
        }
        else {
            checkbox.prop("checked", false);
        }
    });
    $('#preferred-rout').on('hide.bs.modal', function (event) {
        //window.location.href = "/routings/routingdetails?manufPartId=" + selectedManuPartId;
        location.reload();
    });

    //
    //<a href="javascript:void(0);"
    //data - routingName="@routing.RoutingName"
    //data - manufid="@routing.ManufacturedPartId"
    //data - origroutingid="@routing.OrigRoutingId"
    //data - routingid="@routing.RoutingId"
    //data - preferredrouting="@routing.PreferredRouting"
    //data - status="@routing.Status" data - toggle="modal"
    //data - target="#delete-rout" class="dropdown-item" > Delete</a >
    //<input hidden class="form-control form-control-sm" id="DelRoutingName" name="RoutingName" type="text" title="Enter the Routing Name ... It has to be Unique" data-plugin="tippy" data-tippy-placement="top">
        //<input type="text" hidden id="DelManufacturedPartId" name="ManufacturedPartId" value="0" />
        //<input type="text" hidden id="DelOrigRoutingId" name="OrigRoutingId" value="0" />
        //<input type="text" hidden id="DelStatus" name="Status" value="Active" />
        //<input type="text" hidden id="DelPreferredRouting" name="PreferredRouting" value="0" />
        //<input type="text" hidden id="DelRoutingId" name="RoutingId" value="0" />
    $('#delete-rout').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var routingName = relatedTarget.data("routingname");
        var manufid = relatedTarget.data("manufid");
        var origroutingid = relatedTarget.data("origroutingid");
        var routingid = relatedTarget.data("routingid");
        var preferredrouting = relatedTarget.data("preferredrouting");
        var status = relatedTarget.data("status");
        var elm = document.getElementById("RoutingToDelete");
        elm.innerText = routingName;
        //OrigRoutingId
        //PreferredRouting
        //Status
        selectedManuPartId = manufid;
        $("#DelRoutingName").val(routingName);
        $("#DelManufacturedPartId").val(manufid);
        $("#DelOrigRoutingId").val(origroutingid);

        $("#DelStatus").val(status);
        $("#DelRoutingId").val(routingid);
        $("#DelPreferredRouting").val(preferredrouting);
    });
    $('#delete-rout').on('hide.bs.modal', function (event) {
        if (!routdeleted)
            return;
        //window.location.href = "/routings/routingdetails?manufPartId=" + selectedManuPartId;
        location.reload();
        //$.ajax({
        //    type: "POST",
        //    url: "/routings/EncodeManufacturedPartId",
        //    data: { manufacturedPartId: selectedManuPartId },
        //    success: function (encodedManufPartId) {
        //        window.location.href = "/routings/routingdetails?manufPartId=" + encodedManufPartId + "&partType=" + partType;
        //    }
        //});
    });
    let routdeleted = false;
    $("#BtnDelRoutingSave").on("click",function (event) {
        //routings/addnewrouting
        var formData = AppUtil.GetFormData("FormDelRoutingName");
        var routingId = $("#DelRoutingId").val();
        //alert(routingId);
        api.get("/routings/deleterouting?routingId="+routingId).then((data) => {
           // console.log(data);
            routdeleted = true;
            document.getElementById("BtnDelRoutingClose").click();
            location.reload();
        }).catch((error) => {
            AppUtil.HandleError("FormDelRoutingName", error);
        });
    });
    $("#CancelDelRouting").click(function (event) {
        //routings/addnewrouting
        document.getElementById("BtnDelRoutingClose").click();
    });
    

    $('#delete-step').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var stepId = relatedTarget.data("stepid");
        var stepName = relatedTarget.data("stepnumber");
        var elm = document.getElementById("StepToDelete");
        elm.innerText = stepName + "/" + stepId;

        $("#DeleteStepId").val(stepId);
    });
    $('#delete-step').on('hide.bs.modal', function (event) {
        if (!stepdeleted)
            return;
        selectedManuPartId = RoutingDetails.manufacturedPartId;
        //window.location.href = "/routings/routingdetails?manufPartId=" + selectedManuPartId;
        location.reload();

    });

    let stepdeleted = false;
    $("#BtnDeleteStep").click(function (event) {
        //routings/addnewrouting
        var stepId = document.getElementById("DeleteStepId").value;
        var formData = AppUtil.GetFormData("FormDeleteStep");
        api.get("/routings/deletestep?stepId=" + stepId).then((data) => {
            stepdeleted = true;
            document.getElementById("BtnDeleteStepClose").click();
        }).catch((error) => {
        });
    });

    $("#CancelDeleteStep").click(function (event) {
        //routings/addnewrouting
        document.getElementById("BtnDeleteStepClose").click();
    });

    $('#edit-routingname').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var routingName = relatedTarget.data("routingname");
        var origroutingid = relatedTarget.data("origroutingid");
        var routingid = relatedTarget.data("routingid");
        var preferredrouting = relatedTarget.data("preferredrouting");
        var status = relatedTarget.data("status");
        var mkpartid = relatedTarget.data("mkpartid");
        var elm = document.getElementById("RoutingToEdit");
        elm.innerText = "*";
        var manufid = relatedTarget.data("manufid");
        selectedManuPartId = manufid;
        $("#CuurentRoutingName").val(routingName);
        $("#EdManufacturedPartId").val(manufid);
        $("#EdOrigRoutingId").val(origroutingid);

        $("#EdStatus").val(status);
        $("#EdMKPartId").val(mkpartid);
        $("#EdPreferredRouting").val(preferredrouting);
        $("#EdRoutingId").val(routingid);
    });
    $('#edit-routingname').on('hide.bs.modal', function (event) {
        var newNamevalidate = document.getElementById('EdRoutingName');
        newNamevalidate.style.border = '';
        $("#EdRoutingName").val('');
        if (!routeEdited)
            return;
        //window.location.href = "/routings/routingdetails?manufPartId=" + selectedManuPartId;
        location.reload();
    });
    let routeEdited = false;
    $("#BtnEdRoutingNameSave").click(function (event) {
        //routings/addnewrouting
        var currentName = $("#CuurentRoutingName").val();
        var newName = $("#EdRoutingName").val();
        if (newName.length === 0) {
            var newNamevalidate = document.getElementById('EdRoutingName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        if (currentName === newName) {
            var newNamevalidate = document.getElementById('EdRoutingName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        var formData = AppUtil.GetFormData("FormEdRoutingName");
        api.post("/routings/addnewrouting", formData).then((data) => {
           // console.log(data);
            routeEdited = true;
            document.getElementById("BtnEdRoutingNameClose").click();
        }).catch((error) => {
            AppUtil.HandleError("FormEdRoutingName", error);
        });
    });
    $("#CancelEdRoutingName").click(function (event) {
        //routings/addnewrouting
        document.getElementById("BtnEdRoutingNameClose").click();
    });

    $('#alt-rout').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var routingid = relatedTarget.data("routingid");
        var manufacturedPartId = relatedTarget.data("manufid");
        selectedManuPartId = manufacturedPartId;
        $("#AltManufacturedPartId").val(manufacturedPartId);
        $("#AltOrigRoutingId").val(routingid);
        if (routingid <= 0 || isNaN(routingid)) {
            var checkboxes = $("#RoutingGrid tbody input[type='checkbox']:checked");
            checkboxes.each(function (index, checkbox) {
                var row = checkbox.parentNode.parentNode;
                var rowData = {
                    selectroutingid: parseInt($(row).find("td:eq(0)").text()),
                    selectmanufid: parseInt($(row).find("td:eq(1)").text())
                };
                $("#AltManufacturedPartId").val(rowData.selectmanufid);
                $("#AltOrigRoutingId").val(rowData.selectroutingid);
            });

        }
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        spartType = params.partType;
        //makefrom --
        if (spartType === "ManufacturedPart") {
            var altmanufpartid = $("#AltManufacturedPartId").val();
            api.get("/masters/SortedMPMakeFromList?partId=" + altmanufpartid).then((data) => {
                var sortedMPF = data;
                //console.log(sortedMPF);
                const uniqueMPF = [...new Set(sortedMPF.map(mpm => mpm.mpPartId))].map(mpPartId => {
                    return sortedMPF.find(mpm => mpm.mpPartId === mpPartId);
                });
                $("#AltMKPartId").empty();
                uniqueMPF.forEach(function (mpm) {
                    $("#AltMKPartId").append("<option value='" + mpm.mpPartId + "'  name='MKPartId'>" + mpm.inputPartNo + " / " + mpm.mfDescription + "</option>");
                });
            }).catch((error) => {
                AppUtil.HandleError("", error);
            });
        }
        else {
            $("#AltMakefromDiv").hide();
        }
    });
    //$('#alt-rout').on('hide.bs.modal', function (event) {
    //    window.location.href = "/routings/routingdetails?manufPartId=" + selectedManuPartId;
    //});
    
    //$('#routing-new').on('hide.bs.modal', function (event) {
    //    document.getElementById("BtnNewRoutingClose").click();
    //});

    $('#changeMakeFromPopup').on('show.bs.modal', function (event) {   // Propdsdosd
        var relatedTarget = $(event.relatedTarget);
        var routingid = relatedTarget.data("routingid");
        var origroutingid = relatedTarget.data("origroutingid");
        var manufacturedPartId = relatedTarget.data("manufid");
        var routingName = relatedTarget.data("routingname");
        var preferredrouting = relatedTarget.data("preferredrouting");
        var mkpartid = relatedTarget.data("mkpartid");
        var mkpartname = relatedTarget.data("mkpartname");
        selectedManuPartId = manufacturedPartId;
        $("#CManufacturedPartId").val(manufacturedPartId);
        $("#CRoutingName").val(routingName);
        $("#CoutingNameText").text(routingName);
        $("#CRoutingId").val(routingid);
        $("#COrigRoutingId").val(origroutingid);
        $("#CMakefromName").val(mkpartname);
        $("#CPreferredRouting").val(preferredrouting);
        //var checkbox = $("#CheckPPreferredRouting");
        if (spartType === "ManufacturedPart") {
            api.get("/masters/SortedMPMakeFromList?partId=" + selectedManuPartId).then((data) => {
                var sortedMPF = data;
                //console.log(sortedMPF);
                const uniqueMPF = [...new Set(sortedMPF.map(mpm => mpm.mpPartId))].map(mpPartId => {
                    return sortedMPF.find(mpm => mpm.mpPartId === mpPartId);
                });
                $("#CMKPartId").empty();
                uniqueMPF.forEach(function (mpm) {
                    $("#CMKPartId").append("<option value='" + mpm.mpPartId + "'  name='MKPartId'>" + mpm.inputPartNo + " / " + mpm.mfDescription + "</option>");
                });
            }).catch((error) => {
                AppUtil.HandleError("", error);
            });
        }
    });

    $("#BtnCRoutingSave").click(function (event) {
        
        //routings/addnewrouting
        var formData = AppUtil.GetFormData("FormChangeMakeFrom");
        //PreferredRouting  preferred-rout
        api.post("/routings/addnewrouting", formData).then((data) => {
            //console.log(data);
            location.reload();
            //document.getElementById("BtnPreferredRoutingClose").click();
        }).catch((error) => {
            AppUtil.HandleError("FormPreferredRouting", error);
        });
    });


    $("#changelogClose").click(function (event) {
        //event.preventDefault();
        location.reload();
        $("#changelog").modal("hide");
    });

    $('#changelog').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var routingName = relatedTarget.data("routingname");
        var routingid = relatedTarget.data("routingid");
        var apartName = $("#SpanPartName").text();
        var apDesc = $("#SpanPartDesc").text();
        $("#SCSPartName").text(apartName);
        $("#SCSPartDesc").text(apDesc);
        $("#SCSRoutingName").text(routingName);
        loadChangelog(routingid);
    });
    $('#StatusChangePopup').on('show.bs.modal', function (event) {   // Propdsdosd
        var relatedTarget = $(event.relatedTarget);
        var routingid = relatedTarget.data("routingid");
        var origroutingid = relatedTarget.data("origroutingid");
        var manufacturedPartId = relatedTarget.data("manufid");
        var routingName = relatedTarget.data("routingname");
        var preferredrouting = relatedTarget.data("preferredrouting");
        var mkpartid = relatedTarget.data("mkpartid");
        var mkpartname = relatedTarget.data("mkpartname");
        var status = relatedTarget.data("status");
        selectedManuPartId = manufacturedPartId;
        $("#SManufacturedPartId").val(manufacturedPartId);
        $("#SRoutingName").val(routingName);
        //$("#CoutingNameText").text(routingName);
        $("#SRoutingId").val(routingid);
        $("#SOrigRoutingId").val(origroutingid);
        $("#SMKPartId").val(mkpartid);
        $("#SPreferredRouting").val(preferredrouting);
        $("#SViewStatus").val(status);
        var newNamevalidate = document.getElementById('SStatus');
        newNamevalidate.style.border = '';
        var SStatusChangeReasonv = document.getElementById('SStatusChangeReason');
        SStatusChangeReasonv.style.border = '';
        //var checkbox = $("#CheckPPreferredRouting");
        //if (spartType === "ManufacturedPart") {
        //    api.get("/masters/SortedMPMakeFromList?partId=" + selectedManuPartId).then((data) => {
        //        var sortedMPF = data;
        //        //console.log(sortedMPF);
        //        const uniqueMPF = [...new Set(sortedMPF.map(mpm => mpm.mpPartId))].map(mpPartId => {
        //            return sortedMPF.find(mpm => mpm.mpPartId === mpPartId);
        //        });
        //        $("#CMKPartId").empty();
        //        uniqueMPF.forEach(function (mpm) {
        //            $("#CMKPartId").append("<option value='" + mpm.mpPartId + "'  name='MKPartId'>" + mpm.inputPartNo + " / " + mpm.mfDescription + "</option>");
        //        });
        //    }).catch((error) => {
        //        AppUtil.HandleError("", error);
        //    });
        //}
    });

    $("#BtnSRoutingSave").click(function (event) {
        var selectedValue = $('#SStatus').val();
        var currentval = $("#SViewStatus").val();
        if (selectedValue == currentval) {
            var newNamevalidate = document.getElementById('SStatus');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        else {
            var newNamevalidate = document.getElementById('SStatus');
            newNamevalidate.style.border = '';
        }
        var reason = $('#SStatusChangeReason').val();
        if (reason.length === 0) {
            var newNamevalidate = document.getElementById('SStatusChangeReason');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        //routings/addnewrouting
        var formData = AppUtil.GetFormData("FormStatusChange");
        //PreferredRouting  preferred-rout
        api.post("/routings/addnewrouting", formData).then((data) => {
            //console.log(data);
            location.reload();
            //document.getElementById("BtnPreferredRoutingClose").click();
        }).catch((error) => {
            AppUtil.HandleError("FormStatusChange", error);
        });
    });


    $('#routing-new').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var manufacturedPartId = relatedTarget.data("manufacturedpartid");
        $("#ManufacturedPartId").val(manufacturedPartId);
        selectedManuPartId = manufacturedPartId;
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        spartType = params.partType;
        //makefrom --
        if (spartType === "ManufacturedPart") {
            api.get("/masters/SortedMPMakeFromList?partId=" + selectedManuPartId).then((data) => {
                var sortedMPF = data;
                //console.log(sortedMPF);
                const uniqueMPF = [...new Set(sortedMPF.map(mpm => mpm.mpPartId))].map(mpPartId => {
                    return sortedMPF.find(mpm => mpm.mpPartId === mpPartId);
                });
                $("#MKPartId").empty();
                uniqueMPF.forEach(function (mpm) {
                    $("#MKPartId").append("<option value='" + mpm.mpPartId + "'  name='MKPartId'>" + mpm.inputPartNo + " / " + mpm.mfDescription + "</option>");
                });
            }).catch((error) => {
                AppUtil.HandleError("", error);
            });
        }
        else {
            $("#MakefromDiv").hide();
        }
     /* var partNo = relatedTarget.data("partno");
        var coName = relatedTarget.data("companyname");
        var partDesc = var partNo = relatedTarget.data("partdescription");
        $("#NRPartNo").val(partNo);
        $("#NRCompanyName").val(coName);
        $("#NRPartDescription").val(partDesc);*/
        //<a href="javascript:void(0);" data-manufacturedPartId="{manufacturedPartId}" 
        //data - partno="{partNo}" data - companyname="{companyName}" data - partdescription="{partDescription}" data - toggle="modal" 
        //data - target="#routing-new" class="dropdown-item" > Create New Routing</a >
    });

    $("#BtnNewRouting").click(function (event) {
        //FormNewRoutingName
        //FormAltRoutingName
        //FormRoutingStep
        //FormStepPart
        var formData = AppUtil.GetFormData("FormNewRoutingName");
        api.post("/routings/addnewrouting", formData).then((data) => {
            //console.log(data);
            $.ajax({
                type: "POST",
                url: "/routings/EncodeManufacturedPartId",
                data: { manufacturedPartId: selectedManuPartId },
                success: function (encodedManufPartId) {
                    //window.location.href = "/routings/routingdetails?manufPartId=" + encodedManufPartId + "&partType=" + partType;
                    //$('a[href="#rsd"]').tab("show");
                    $("#BtnNewRoutingClose").trigger("click");
                    $("#BtnAddNextStep").trigger("click");
                    $('#StepRoutingId').val(data.routingId);
                }
            });
            //window.location.href = "/routings/routingdetails?manufPartId=" + selectedManuPartId;
        }).catch((error) => {
            AppUtil.HandleError("FormNewRoutingName", error);
        });
    });
    
    $("#BtnPRoutingSave").click(function (event) {
        var checkbox = $("#CheckPPreferredRouting");

        if (checkbox.prop("checked")) {
            $("#PPreferredRouting").val(1);
        } else {
            $("#PPreferredRouting").val(0);
        }
        //routings/addnewrouting
        var formData = AppUtil.GetFormData("FormPreferredRouting");
        //PreferredRouting  preferred-rout
        api.post("/routings/preferredrouting", formData).then((data) => {
            //console.log(data);
            document.getElementById("BtnPreferredRoutingClose").click();
        }).catch((error) => {
            AppUtil.HandleError("FormPreferredRouting", error);
        });
    });

    function handleCheckboxChange() {
        var checkboxes = $("#RoutingGrid tbody input[type='checkbox']:checked"); // Select checked checkboxes
        
        if (checkboxes.length === 1) {
            $('#BtnCreateAlRouting').prop('disabled', false);
        }
        else {
            $('#BtnCreateAlRouting').prop('disabled', true); // Disable the button
        }
    }

     //Attach the handleCheckboxChange function to the change event of the checkboxes using event delegation
    $('#RoutingGrid tbody').on('change', 'input[type="checkbox"]', handleCheckboxChange);

    // Optionally, trigger the event handler once to set the initial state of the button
    handleCheckboxChange();

    $("#BtnAltRoutingSave").click(function (event) {
        //routings/addnewrouting
        var newName = $("#AltRoutingName").val();
        if (newName.length === 0) {
            var newNamevalidate = document.getElementById('AltRoutingName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('AltRoutingName');
            newNamevalidate.style.border = '';
        }
        var formData = AppUtil.GetFormData("FormAltRoutingName");
        api.post("/routings/altrouting", formData).then((data) => {
            //console.log(data);
            document.getElementById("BtnAltRoutingClose").click();
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });
            var encodedManufPartId = params.manufPartId;
            var parttypeurl = params.partType;
            window.location.href = "/routings/routingdetails?manufPartId=" + encodedManufPartId + "&partType=" + parttypeurl;
        }).catch((error) => {
            AppUtil.HandleError("FormAltRoutingName", error);
        });
    });

    
    
    ////SubConWSSubConDetailsId//SubConWSRoutingStepId//SubConWSDetailsId //WorkStepDesc//MachineType//FloorToFloorTime//SetupTime//NoOfPartsPerLoading
    $('#add-machine').on('shown.bs.modal', function (event) {
        if ($("#StepId").val() == "0") {
            alert("Save routing step before adding machine.");
            document.getElementById("Add-Machine-Close").click();
        }
        $.ajax({           
            success: function (data) {
                AddMacOptions();
            }
        });
        document.getElementById("FormRoutingMachine").reset();
        var rTgt = $(event.relatedTarget);
        var addEdit = rTgt.data("addedit");

        var tablebody = $("#TitleTableMachine tbody");
        tablebody.html("");
        $(tablebody).append(AppUtil.ProcessTemplateData("TitleRowMachine", RoutingDetails));
        if (addEdit == "Add") {
            $("#MachineRoutingStepId").val(RoutingDetails["stepId"]);
            var chkdelm = $('input[name=stepmachineselect]:checked');
            var currentrow = chkdelm.closest('tr');
            var machineId = $('input[name=stepmachineselect]:checked').val();
            //McIdUploadDocList(machineId);
            var tablebody = $("#MachineDocGrid tbody");
            $(tablebody).html("");//empty tbody
            RouteloadMachinesToTable("AddMachineListTable", "AddMachineListRow", addEdit, 0);
        }
        else {
            //console.log("-------------");
            //console.log(rTgt.data("setuptime"));
            $("#SetupTime").val(rTgt.data("setuptime"));
            //console.log(rTgt.data("floortofloortime"));
            $("#FloorToFloorTime").val(rTgt.data("floortofloortime"));
            //console.log(rTgt.data("firstpieceprocessingtime"));
            //console.log("-------------");
            $("#FirstPieceProcessingTime").val(rTgt.data("firstpieceprocessingtime"));
            $("#NoOfPartsPerLoading").val(rTgt.data("noofpartsperloading"));
            $("#PreferredMachine").val(rTgt.data("preferredmachine"));
            $("#MachineId").val(rTgt.data("machineid"));
            $("#MachineRoutingStepId").val(rTgt.data("machineroutingstepid"));//maps to StepId
            $("#RoutingStepMachineId").val(rTgt.data("routingstepmachineid"));//maps to Id
            Mcid = rTgt.data("machineid");
            McIdUploadDocList(rTgt.data("machineid"));
            RouteloadMachinesToTable("AddMachineListTable", "AddMachineListRow", addEdit, Mcid);
        }
        //debugger;
        //RoutingStepMachineId
        //SetupTime//FloorToFloorTime//FirstPieceProcessingTime//
        //NoOfPartsPerLoading//PreferredMachine//MachineId
        //MachineRoutingStepId
        //RoutingStepMachineId
        loadStepMachinesForAdd();

        if ($("#stepmachine_1").length) {
            $("#stepmachine_1").prop('checked', true);
        }

    });
    //Routing Step Details Filter/Search
    $("#MAC_Plants").change(function () {
        var value = $(this).find("option:selected").text().toLowerCase();
        if (value == "--select--") {
            $("#AddMachineListTable tbody").show();
            return;
        }
        $("#AddMachineListTable tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#MAC_Shops").change(function () {
        var value = $(this).find("option:selected").text().toLowerCase();
        $("#AddMachineListTable tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#MAC_Name").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#AddMachineListTable tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    //$("#MAC_Type").change(function () {
    //    var value = $(this).find("option:selected").text().toLowerCase();
    //    $("#AddMachineListTable tbody tr").filter(function () {
    //        $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
    //    });
    //});
    //end---

    $('#delete-machine').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var machineId = relatedTarget.data("routingstepmachineid");
        var stepId = relatedTarget.data("stepid");
        $("#DelMachineStepId").val(stepId);
        $("#DelMachineId").val(machineId);
        var machinename = relatedTarget.data("machinename");
        var elm = document.getElementById("MachineToDelete");
        elm.innerText = machinename;
        //$("#DelRoutingId").val();
    });
    $("#BtnDelStepMachine").on("click",function (event) {
        var machineId = $("#DelMachineId").val();
        var stepId = $("#DelMachineStepId").val();
        //alert(machineId + "/" + stepId);
        api.get("/routings/deletemachine?stepId=" + stepId + "&machineId=" + machineId).then((data) => {
            //console.log(data);
            //console.log(tablebody.html());
            //RouteSuppliersTable
            //RouteSupplierTemplate
            loadStepMachines();
            document.getElementById("BtnDelMachineClose").click();
        }).catch((error) => {
        });
    });
    $("#CancelDelStepMachine").on("click",function (event) {
        //BtnDelMachineClose
        document.getElementById("BtnDelMachineClose").click();
    });

    $("#RoutingAvailableClose").on("click", function (event) {
        //BtnDelMachineClose
        //$('a[href="#rs2"]').tab("show");
        location.reload();
    });
    $("#RoutingDetailsClose").on("click", function (event) {
        //BtnDelMachineClose
        $('a[href="#rou-det"]').tab("show");
        $("#StepId").val("0");
        var routingName =RoutingDetails["routingName"];
        var manufPartId =RoutingDetails["manufacturedPartId"];
        var routingId =RoutingDetails["routingId"];
        EditRoute(routingId, routingName, manufPartId);
        DisplayBomMessage();
    });
    $("#RoutingStepDetails").on("click", function (event) {
        if ($('.nav-tabs .active').text().trim() == "RoutingStepDetails") {
            event.preventDefault();
            return;
        }
        $('a[href="#rs2"]').tab("show");
    });

    $('#delete-supplier').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var machineId = relatedTarget.data("routingstepsupplierid");
        var stepId = relatedTarget.data("stepid");
        var suppliername = "NA";
        var elm = document.getElementById("SupplierToDelete");
        elm.innerText = suppliername;
        //OrigRoutingId
        //PreferredRouting
        //Status
    });

    

    $("#BtnDelStepSupplier").click(function (event) {

    });

    $("#CancelDelStepSupplier").click(function (event) {
        //BtnDelMachineClose
        document.getElementById("BtnDeleteSupplierClose").click();
    });

    $("#subconname").on("change", function () {
        var selectedOption = $(this).find("option:selected");

        // Get the value of the selected option (supplier ID)
        var supplierId = selectedOption.val();
        $("#SubConSupplierId").val(supplierId);
        
        //$("#SubConSupplier").val(supplierName);
    });
    
    $('input[type=radio][name=stepsupplierselect]').change(function () {
        var chkdelm = $('input[name=stepsupplierselect]:checked');
        var currentrow = chkdelm.closest('tr');
        var supplierId = $('input[name=stepsupplierselect]:checked').val();
        var supplierName = currentrow.find("td:eq(1)").html();
        $("#SupplierId").val(supplierId);
        $("#Supplier").val(supplierName);
    });

    $(document).on('change', 'input[type=radio][name=stepmachineselect]', function () {
        // Select the currently checked radio button
        var chkdelm = $(this);
        // Get the closest row (`tr`) for the selected radio button
        var currentrow = chkdelm.closest('tr');

        // Make sure we have found the row before proceeding
        if (currentrow.length > 0) {
            // Extract values from specific columns within the row
            var machineId = chkdelm.val();  // Get the machine ID from the radio button's value
            var plantName = currentrow.find("td:eq(1)").text();  // Second column (Plant)
            var shopName = currentrow.find("td:eq(2)").text();   // Third column (Shop)
            var machinename = currentrow.find("td:eq(4)").text(); // Fifth column (M/c Name)

            // Update the target elements with the retrieved values
            $("#MachineId").val(machineId);
            $("#MPopupMcNameSpan").text(machinename);
            $("#MPopupMcPlantSpan").text(plantName);
            $("#MPopupMcShopSpan").text(shopName);
            //McIdUploadDocList(parseInt(machineId));
        } else {
        }
    });
    $("#SaveSubCon").on("click", function (event) {
        //alert("Add SubCons");
        var WorkDone = document.getElementById('WorkDone');
        if (!WorkDone.value) {
            // Add red border directly using inline style
            WorkDone.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            WorkDone.style.border = '';
        }
        var TransportTime = document.getElementById('TransportTime');
        if (!TransportTime.value) {
            // Add red border directly using inline style
            TransportTime.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            TransportTime.style.border = '';
        }
        var CostPerPart = document.getElementById('CostPerPart');
        if (!CostPerPart.value) {
            // Add red border directly using inline style
            CostPerPart.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            CostPerPart.style.border = '';
        }
        var Notes = document.getElementById('Notes');
        if (!Notes.value) {
            // Add red border directly using inline style
            Notes.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            Notes.style.border = '';
        }
        AddSubCon();
    });
    $("#SaveSubConWS").on("click", function (event) {
        //alert("Add SubConsWS");
        let wsid = $("#SubConWSSubConDetailsId").val();
        let stepid = $("#SubConWSRoutingStepId").val();
        if (wsid == "0" || stepid == "0") {
            alert("Please save subcon before entering the work step details.");
            return;
        }
        var WorkStepDesc = document.getElementById('WorkStepDesc');
        if (!WorkStepDesc.value) {
            // Add red border directly using inline style
            WorkStepDesc.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            WorkStepDesc.style.border = '';
        }
        var MachineType = document.getElementById('MachineType');
        if (!MachineType.value) {
            // Add red border directly using inline style
            MachineType.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            MachineType.style.border = '';
        }
        var FloorToFloorTime = document.getElementById('FloorToFloorTime');
        if (!FloorToFloorTime.value) {
            // Add red border directly using inline style
            FloorToFloorTime.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            FloorToFloorTime.style.border = '';
        }
        var SetupTime = document.getElementById('SetupTime');
        if (!SetupTime.value) {
            // Add red border directly using inline style
            SetupTime.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            SetupTime.style.border = '';
        }
        var NoOfPartsPerLoading = document.getElementById('NoOfPartsPerLoading');
        if (!NoOfPartsPerLoading.value) {
            // Add red border directly using inline style
            NoOfPartsPerLoading.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            NoOfPartsPerLoading.style.border = '';
        }
        AddSubConWS();
    });

    $("#SaveRouteSupplier").click(function (event) {
        var chkdelm = $('input[name=stepsupplierselect]:checked');
        var currentrow = chkdelm.closest('tr');
        var supplierId = $('input[name=stepsupplierselect]:checked').val();
        var supplierName = currentrow.find("td:eq(1)").html();
        $("#SupplierId").val(supplierId);
        $("#Supplier").val(supplierName);
        //SupplierRoutingStepId
        //Supplier
        //SupplierId
        //stepsupplierselect
        //alert("Save route supplier");
        //routings/addnewrouting
        

        var formData = AppUtil.GetFormData("FormRoutingSupplier");
        api.post("/routings/savestepsupplier", formData).then((data) => {
            //console.log(data);
            loadSetSuppliers();
            document.getElementById("Add-Supplier-Close").click();
        }).catch((error) => {
            AppUtil.HandleError("FormRoutingSupplier", error);
        });
        event.preventDefault();
    });



    $("#RoutingAvailable").click(function (event) {
        const params = new Proxy(new URLSearchParams(window.location.search), {
            get: (searchParams, prop) => searchParams.get(prop),
        });
        var encodedManufPartId = params.manufPartId;
        var urlpartype = params.partType;
        window.location.href = "/routings/routingdetails?manufPartId=" + encodedManufPartId + "&partType=" + urlpartype;
    });
    //$("#RoutingDetails").click(function (event) {
    //    EditRoute();
    //    //const params = new Proxy(new URLSearchParams(window.location.search), {
    //    //    get: (searchParams, prop) => searchParams.get(prop),
    //    //});
    //    //var encodedManufPartId = params.manufPartId;
    //    //window.location.href = "/routings/routingdetails?manufPartId=" + encodedManufPartId + "&partType=" + partType;
    //});
    $("#Add-SubCon-Close").click(function (event) {
        //const params = new Proxy(new URLSearchParams(window.location.search), {
        //    get: (searchParams, prop) => searchParams.get(prop),
        //});
        //var encodedManufPartId = params.manufPartId;
        //var parttypeurl = params.partType;
        //window.location.href = "/routings/routingdetails?manufPartId=" + encodedManufPartId + "&partType=" + parttypeurl;
    });
    $("#SaveRouteMachine").click(function (event) {
        //alert("Save route Machine");
        var chkdelm = $('input[name=stepmachineselect]:checked');
        var currentrow = chkdelm.closest('tr');
        var machineId = $('input[name=stepmachineselect]:checked').val();
        var machinename = currentrow.find("td:eq(4)").html();
        $("#MachineId").val(machineId);
        var formData = AppUtil.GetFormData("FormRoutingMachine");
        api.post("/routings/savestepmachine", formData).then((data) => {
            //console.log(data);
            var mcid = data["routingStepMachineId"];
            $("#RoutingStepMachineId").val(mcid);
            loadStepMachines();
            McIdUploadDocList(machineId);
            //document.getElementById("Add-Machine-Close").click();
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });
            //var encodedManufPartId = params.manufPartId;
            //var parttypeurl = params.partType;
            //window.location.href = "/routings/routingdetails?manufPartId=" + encodedManufPartId + "&partType=" + parttypeurl;
            //EditRoute();
        }).catch((error) => {
            AppUtil.HandleError("FormRoutingMachine", error);
        });
        event.preventDefault();
    });



    $("#BtnSaveStep").click(function (event) {
        //routings/addnewrouting

        var StepNumber = document.getElementById('StepNumber');
        if (!StepNumber.value) {
            // Add red border directly using inline style
            StepNumber.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            StepNumber.style.border = '';
        }
        var StepDescription = document.getElementById('StepDescription');
        if (!StepDescription.value) {
            // Add red border directly using inline style
            StepDescription.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            StepDescription.style.border = '';
        }
        var StepOperation = document.getElementById('StepOperation');
        if (!StepOperation.value) {
            // Add red border directly using inline style
            StepOperation.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            StepOperation.style.border = '';
        }
        var StepLocation = document.getElementById('StepLocation');
        if (!StepLocation.value) {
            // Add red border directly using inline style
            StepLocation.style.border = '1px solid red';
            return false;
        } else {
            // Remove the red border if the input is valid
            StepLocation.style.border = '';
        }
        var formData = AppUtil.GetFormData("FormRoutingStep");
        api.post("/routings/savestep", formData).then((data) => {
            stepModel = data;
            RoutingDetails["stepNumber"] = data["stepNumber"];
            RoutingDetails["stepId"] = data["stepId"];
            //console.log("====1=====");
            //console.log(data);
            //console.log("====1-End=====");
            $("#StepId").val(data.stepId);
            $("#BOMRoutingStepId").val(data.stepId);
            document.getElementById('addMachine').removeAttribute('disabled');
            document.getElementById('addSubCon').removeAttribute('disabled');
            var StepNumber = document.getElementById('StepNumber');
            StepNumber.style.border = '';
            var StepDescription = document.getElementById('StepDescription');
            StepDescription.style.border = '';
            var StepOperation = document.getElementById('StepOperation');
            StepOperation.style.border = '1px solid red';
            StepOperation.style.border = '';
            var StepLocation = document.getElementById('StepLocation');
            StepLocation.style.border = '';

        }).catch((error) => {
            AppUtil.HandleError("FormRoutingStep", error);
        });
        event.preventDefault();
    });

    $('#AssociateBOMParts').on('show.bs.modal', function (event) {
        var StepNumber = $("#StepNumber").val();

        $("#BomStepNo").text(StepNumber);
        $("#BomRouting").text(RoutingDetails.routingName);
        var QuantityAssembly = document.getElementById('QuantityAssembly');
        QuantityAssembly.style.border = '';
        // Get the part number
        var table = $('#headingPartsDetails');

        // Get the last two td elements
        var partNoTd = table.find('tr td:nth-last-child(2)');
        var partDescriptionTd = table.find('tr td:last-child');

        // Get the text content after the span element
        var partNoText = partNoTd.contents().filter(function () {
            return this.nodeType == 3; // Node type 3 is a text node
        }).text().trim();
        var partDescriptionText = partDescriptionTd.contents().filter(function () {
            return this.nodeType == 3; // Node type 3 is a text node
        }).text().trim();

        $("#BomPartNo").text(partNoText);
        $("#BomPartDesc").text(partDescriptionText);

        //const params = new Proxy(new URLSearchParams(window.location.search), {
        //    get: (searchParams, prop) => searchParams.get(prop),
        //});
        //let pvalue = params.manufPartId;
        //let manufId = pvalue;
        //api.get("/routings/GetMasterName?ManufId=" + manufId).then((data) => {
        //    $("#BomPartNo").text(data.partNo);
        //    $("#BomPartDesc").text(data.partDescription);
        //});
    });

    $("#BtnAddToStepList").on('click',function (event) {
        var qtyEntered = parseInt($("#QuantityAssembly").val());
        if ($("#QuantityAssembly").val() == "") {
            var QuantityAssembly = document.getElementById('QuantityAssembly');
            QuantityAssembly.style.border = '1px solid red';
            return false;
        }
        if (qtyEntered > qtyAvailable) {
            alert("Quantity not available.");
            return false;
        }
        if (qtyEntered <= 0) {
            alert("Quantity not available.");
            return false;
        }
        var formData = AppUtil.GetFormData("FormStepPart");
        api.post("/routings/addBomtoassembly", formData).then((data) => {
            //console.log(data);
            var QuantityAssembly = document.getElementById('QuantityAssembly');
            QuantityAssembly.style.border = '';
            LoadBOMList(data.routingStepId);
            var tablebody = $("#BomUsedGridDisplay tbody");
            tablebody.html("");
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });
            let pvalue = params.manufPartId;
            let manufId = pvalue;
            let stepId = $("#StepId").val();
            api.get("/routings/boms?manufId=" + manufId + "&stepId=" + data.routingStepId).then((rData) => {
                for (i = 0; i < rData.length; i++) {
                    if (rData[i].quantityUsed != "0") {
                        $(tablebody).append(AppUtil.ProcessTemplateData("BomGridLandingRow", rData[i]));

                    }
                }
            }).catch((error) => {
            });
            //LoadStepParts(data.manufacturedPartId,data.routingStepId);
            document.getElementById("FormStepPart").reset();
            SetAssemblyIds();
        }).catch((error) => {
            AppUtil.HandleError("FormStepPart", error);
        });
        event.preventDefault();
    });


    $("#BtnAddNextStep").click(function (event) {
        //routings/addnewrouting
        //alert("next step... todo...");
        document.getElementById("FormRoutingStep").reset();
        hideMachinesSuppliersTable();
        //getRoutingInfoFromTable();
        $('#StepRoutingId').val(RoutingDetails.routingId);
        $('a[href="#rsd"]').tab("show");
        var select = document.getElementById('StepLocation');
        select.style.pointerEvents = 'auto';
        var tablebody = $("#SubConsTable tbody");
        $(tablebody).html("");
        var Machinetablebody = $("#RouteMachinesTable tbody");
        $(Machinetablebody).html("");
        //"#rou-det"
        const params = new Proxy(new URLSearchParams(window.location.search), {
        get: (searchParams, prop) => searchParams.get(prop),
        });
        nspartType = params.partType;
        if (nspartType == "ManufacturedPart") {
            $("#tab-step-parts").hide();
            $("#Div_BomGrid").hide();
        } else {
            $("#tab-step-parts").show();
            $("#Div_BomGrid").show();
            var tablebody = $("#BomUsedGridDisplay tbody");
            tablebody.html("");
        }
    });

    $('#addWorkStep').on('hidden.bs.modal', function (event) {
        document.getElementById("FormSubConWS").reset();
    });
    $('#doc-item').on('hidden.bs.modal', function (event) {
        var InfoComments = document.getElementById('InfoComments');
        InfoComments.style.border = '';
        var newNamevalidate = document.getElementById('fileNameDisplay');
        newNamevalidate.style.border = '';
        $("#doclistidFile").val(0);
        $("#fileNameDisplay").val('');
        $("#InfoComments").val('');
        $("#DocTypeName").val('');
        $("#FileExtnName").val('');
        $("#docTypeIdFile").val(0);
        var fileInput = document.getElementById("fileUploadInput");
        fileInput.value = "";

    });
    $('#doc-item').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var filename = relatedTarget.data("filename");
        var doctypename = relatedTarget.data("doctypename");
        var comments = relatedTarget.data("comments");
        var doclistid = relatedTarget.data("doclistid");
        var documenttypeid = relatedTarget.data("documenttypeid");
        var upload = relatedTarget.data("upload");
        var fileextnname = relatedTarget.data("fileextnname");
        var deletiondate = relatedTarget.data("deletiondate");
        var addmachinedoc = relatedTarget.data("addmachinedoc");
        if (doclistid == 0 && upload == 1) {
            $("#doclistidFile").val(0);
            $("#fileNameDisplay").val('');
            $("#InfoComments").val(comments);
            $("#DocTypeName").val(doctypename);
            $("#FileExtnName").val(fileextnname);
            $("#docTypeIdFile").val(documenttypeid);
            var dateOnly = deletiondate.split('T')[0];

            if (addmachinedoc == 1) {
                var rmcid = parseInt($("#RoutingStepMachineId").val());
                if (rmcid == 0 || isNaN(rmcid)) {
                    alert("Please Save the Machine Details.");
                    $("#btnCloseDocItem").trigger("click");
                }
            }
            // Set the value of the input field with the formatted date
            $("#deletiondate").val(dateOnly);
        } else if (doclistid > 0 && upload == 2) {
            $("#doclistidFile").val(doclistid);
            $("#fileNameDisplay").val(filename);
            $("#InfoComments").val(comments);
            $("#DocTypeName").val(doctypename);
            $("#FileExtnName").val(fileextnname);
            $("#docTypeIdFile").val(documenttypeid);
            var dateOnly = deletiondate.split('T')[0];

            // Set the value of the input field with the formatted date
            $("#deletiondate").val(dateOnly);
        }
        else {
            $("#DocTypeName").val("Others");
            $("#docTypeIdFile").val(3);
            $("#doclistidFile").val(0);
            $("#FileExtnName").val("Any Extn");
            var today = new Date();
            today.setDate(today.getDate() + 10);
            var deletionDate = today.toISOString().split('T')[0];
            $("#deletiondate").val(deletionDate);
            if (addmachinedoc == 1) {
                var rmcid = parseInt($("RoutingStepMachineId").val());
                if (rmcid == 0 || isNaN(rmcid)) {
                    alert("Please Save the Machine Details.");
                    $("#btnCloseDocItem").trigger("click");
                }
            }

        }

    });

    document.getElementById("UploadFileSave").addEventListener("click", function (event) {
        event.preventDefault();  // Prevent the default form submission
        var partids = parseInt($("#StepId").val());
        if (partids <= 0) {
            alert("Please Save Step Information.");
            return false;
        }
        var com = document.getElementById("InfoComments").value;
        if (com.trim() === "" || com.length === 1) {
            var newNamevalidate = document.getElementById('InfoComments');
            newNamevalidate.style.border = '2px solid red'; // Set border to red for invalid input
            return false; // Prevent form submission or further processing
        } else {
            var newNamevalidate = document.getElementById('InfoComments');
            newNamevalidate.style.border = ''; // Clear the border for valid input
        }
        // Create a FormData object to hold file and form data
        var formData = new FormData();

        // Add the file to the FormData object
        var fileInput = document.getElementById("fileUploadInput");
        var file = fileInput.files[0];
        var allowedExtensions = $("#FileExtnName").val().split(',').map(function (ext) {
            return ext.trim().toLowerCase(); // Create an array of allowed extensions (e.g., ['.pdf'])
        });
        if (!isNaN(file) || file) {
            var fileName = file.name;
            var fileExtension = '.' + fileName.split('.').pop().toLowerCase(); // Get the file extension and add a dot (e.g., '.pdf')

            // Validate that the file's extension is in the allowedExtensions array
            if (allowedExtensions.includes(fileExtension) || allowedExtensions.includes("any extn")) {
                formData.append("uploadedFile", file);
            } else {
                var newNamevalidate = document.getElementById('fileNameDisplay');
                newNamevalidate.style.border = '2px solid red';
                $("#fileNameDisplay").val("Invalid file type. Please upload a valid file."); // Show error message for invalid file type
                return false;
            }
        } else {
            var newNamevalidate = document.getElementById('fileNameDisplay');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }

        // Add the other form inputs to the FormData object
        formData.append("DocumentTypeName", document.getElementById("DocTypeName").value);
        formData.append("FileExtnName", document.getElementById("FileExtnName").value);
        formData.append("FileName", document.getElementById("fileNameDisplay").value);
        formData.append("StorageLocation", "/Active");
        formData.append("Comments", document.getElementById("InfoComments").value);
        formData.append("DocListId", parseInt(document.getElementById("doclistidFile").value));
        formData.append("DocumentTypeId", parseInt(document.getElementById("docTypeIdFile").value));
        formData.append("RoutingId", parseInt(document.getElementById("StepRoutingId").value));
        formData.append("OprNo", parseInt(document.getElementById("StepId").value));
        formData.append("McTypeId", parseInt(mcTypeId));
        formData.append("McId", parseInt(Mcid));
        formData.append("UploadUiId", parseInt(1));
        //var today = new Date();
        //today.setDate(today.getDate() + 10);  // Add 10 days to the current date

        //// Format the date as YYYY-MM-DD (you can modify this to your required format)
        //var deletionDate = today.toISOString().split('T')[0];

        // Append the DeletionDate to the FormData
        var deletionDateValue = document.getElementById("deletiondate").value;
        var deletionDate = new Date(deletionDateValue);

        var formattedDate = deletionDate.toISOString().split('T')[0];

        formData.append("DeletionDate", formattedDate);
        // Post the form data to the server
        if (archive == 0) {
            $.ajax({
                type: "POST",
                url: "/masters/PostDocList",
                data: formData,
                contentType: false,  // Important: Let the browser set the Content-Type header automatically
                processData: false,  // Important: Don't process the form data, let it be as FormData
                success: function (response) {
                    // Handle the success response here
                    //alert("File uploaded and data saved successfully!");
                    $("#doc-item").modal("hide");
                    loadDocUploadList();
                    McTypeUploadDocList(mcTypeId);
                    McIdUploadDocList(Mcid);
                    archive = 0;
                },
                error: function (xhr, status, error) {
                    // Handle any errors here
                    console.error("An error occurred:", error);
                    alert("There was an error saving the file and data.");
                }
            });
        } else {
            $.ajax({
                type: "POST",
                url: "/masters/MoveFileToArchive",
                data: formData,
                contentType: false,  // Important: Let the browser set the Content-Type header automatically
                processData: false,  // Important: Don't process the form data, let it be as FormData
                success: function (response) {
                    // Handle the success response here
                    //alert("File uploaded and data saved successfully!");
                    $("#doc-item").modal("hide");
                    loadDocUploadList();
                    McTypeUploadDocList(mcTypeId);
                    McIdUploadDocList(Mcid);
                    archive = 0;
                },
                error: function (xhr, status, error) {
                    // Handle any errors here
                    console.error("An error occurred:", error);
                    alert("There was an error saving the file and data.");
                }
            });
        }
    });


    loadDocUploadList();
    $("#StepOperation").change(function () {
        loadDocUploadList();
    });
    $('#RefLogPopup').on('shown.bs.modal', function (event) {

        var relatedTarget = $(event.relatedTarget);
        var doclistid = relatedTarget.data("doclistid");
        var doctypename = relatedTarget.data("doctypename");
        var documenttypeid = relatedTarget.data("documenttypeid");
        var partnos = $("#PartNo").val();
        api.get("/DocumentManagement/GetAllDocumentType").then((data) => {
            const fdoc = data.find(item => item.documentTypeId === documenttypeid);
            if (fdoc.docuCategory == 2) {
                $("#SpanDocType").text(doctypename);
                $("#SpanDocPart").text(RoutingDetails.routingName);
                api.getbulk("/masters/GetRefDocLogOfDoclistId?doclistid=" + doclistid).then((data) => {
                    //data = data.filter(item => item.status == 1 || item.status==0);\
                    var tablebody = $("#RefLogGrid tbody");
                    $(tablebody).html("");//empty tbody
                    for (i = 0; i < data.length; i++) {
                        $(tablebody).append(AppUtil.ProcessTemplateDataNew("RefLogGridRow", data[i], i));
                    }
                }).catch((error) => {
                    console.log(error);
                });
            } else {
                $("#RefLogPopup").modal("hide");
            }
        }).catch((error) => {
        });
    });
    $('#RefDocReason').on('hidden.bs.modal', function (event) {
        $("#RefDoc").val("");
    });
    $('#RefDocReason').on('shown.bs.modal', function (event) {
        loadReasonDropDown();
    });

    $("#RefDocSave").on("click", function () {
        var PartIds = parseInt($("#PartId").val());
        var RefDocId = parseInt($("#RefDocId").val());
        var DocReasonName = parseInt($("#DocReasonName").val());
        var doclistidFile = parseInt($("#doclistidFile").val());
        var comments = $("#RefDoc").val();
        var today = new Date();
        //today.setDate(today.getDate() + 10);
        var deletionDate = today.toISOString().split('T')[0];
        if (isNaN(DocReasonName) || DocReasonName === 0) {
            var newNamevalidate = document.getElementById('DocReasonName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('DocReasonName');
            newNamevalidate.style.border = '';
        }

        var rowData = {
            refDocLogId: 0,
            partId: PartIds,
            docListId: doclistidFile,
            docReasonId: DocReasonName,
            comments: comments,
            uploadedOn: deletionDate,
            action: "Replacement"
        };
        $.ajax({
            type: "POST",
            url: '/Masters/PostRefDocLog',
            contentType: "application/json; charset=utf-8",
            headers: { 'Content-Type': 'application/json' },
            data: JSON.stringify(rowData),
            dataType: "json",
            success: function (result) {
                var DocReasonName = document.getElementById('DocReasonName');
                var RefDoc = document.getElementById('RefDoc');
                DocReasonName.style.border = '';
                RefDoc.style.border = '';
                $('#RefDocReason').modal('hide');
            }
        });

    });

});

function loadReasonDropDown() {
    var selElem = $('#DocReasonName');
    selElem.html('');
    api.getbulk("/DocumentManagement/GetAllRefReson").then((data) => {

        var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        selElem.append(rdiv_data);
        for (i = 0; i < data.length; i++) {
            rdiv_data = "<option value='" + data[i].refDocReasonListId + "'>" + data[i].docReason + "</option>";
            selElem.append(rdiv_data);
        }
    });

}

function displayFileName() {
    var docid = parseInt($("#doclistidFile").val());
    if (docid != 0) {
        var doctypeid = parseInt($("#docTypeIdFile").val());
        api.get("/DocumentManagement/GetAllDocumentType").then((data) => {
            const fdoc = data.find(item => item.documentTypeId === doctypeid);
            if (fdoc.docuCategory == 2) {
                var confrimval = confirm("Do You Want Move the Exsisting File To Archive.");
                if (confrimval) {
                    var fileInput = document.getElementById('fileUploadInput');
                    var fileName = fileInput.files[0].name; // Get the uploaded file name
                    document.getElementById('fileNameDisplay').value = fileName; // Display the file name in the text input
                    var docid = parseInt($("#doclistidFile").val());
                    archive = 1;
                    $("#Resonbtn").click();
                } else {
                    var fileInput = document.getElementById("fileUploadInput");
                    fileInput.value = "";
                }
            } else {
                var fileInput = document.getElementById('fileUploadInput');
                var fileName = fileInput.files[0].name; // Get the uploaded file name
                document.getElementById('fileNameDisplay').value = fileName;
            }
        }).catch((error) => {
        });
    } else {
        var fileInput = document.getElementById('fileUploadInput');
        var fileName = fileInput.files[0].name; // Get the uploaded file name
        document.getElementById('fileNameDisplay').value = fileName;
    }
}
function DeleteDocList(element) {
    var relatedTarget = $(element);
    var doclistid = relatedTarget.data("doclistid");
    if (doclistid != 0) {
        var confrimval = confirm("Do You Want This Document.");
        if (confrimval) {
            api.get("/masters/DeleteDocListAndFile?doclistid=" + doclistid).then((data) => {
                //console.log(data);
                loadDocUploadList();
                McTypeUploadDocList(mcTypeId);
                McIdUploadDocList(Mcid);
            }).catch((error) => {
                //console.log(error);
            });
        }
    } else {
        alert("This Document Type Do Not Have Document.");
    }
}
function viewFile(element) {
    var relatedTarget = $(element);
    var file = relatedTarget.data("filename");
    var doctypename = relatedTarget.data("doctypename");
    var customername = relatedTarget.data("customername");
    var uploadby = relatedTarget.data("uploadby");
    var uploadon = relatedTarget.data("uploadon");
    var partno = RoutingDetails["partNo"] ;
    var partdesc = RoutingDetails["partDescription"];
    var routingname = RoutingDetails["routingName"] ;
    var oprno = relatedTarget.data("oprno");
    var retdate = relatedTarget.data("retdate");
    if (file == null) {
        $('#viewDoc').modal('hide');
        return false;
    }
    if (doctypename == "Other") {
        $('#viewDoc').modal('hide');
        return false;
    } else {
        $('#viewDoc').modal('show');
        $("#DocTypenameText").text(doctypename);
        $("#PartNoText").text(partno);
        $("#PartDescText").text(partdesc);
        $("#CustomerText").text(customername);
        $("#RetentionDateText").text(retdate);
        $("#RoutingNameText").text(routingname);
        $("#OprNoText").text(oprno);
        $("#UploadByText").text(uploadby);
        $("#UpdatedDateText").text(uploadon);


        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/masters/ViewFile?fileName=' + file, true);
        xhr.responseType = 'arraybuffer';
        xhr.onload = function (e) {
            if (this.status == 200) {
                var blob = new Blob([this.response], { type: "application/pdf" });

                const objectElement = document.getElementById('fileViewer');
                const url = URL.createObjectURL(blob);
                objectElement.src = url;
                //objectElement.width = '1000px';
                //objectElement.height = '1000px';
                //objectElement.type = 'text/plain';
                //var link = document.createElement('a');
                //link.href = window.URL.createObjectURL(blob);
                //link.download = "Report_" + new Date() + ".pdf";
                //link.click();
            }
        };
        xhr.send();
    }
}

function downloadFile(element) {
    var relatedTarget = $(element);
    var file = relatedTarget.data("filename");

    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/masters/ViewFile?fileName=' + file, true);
    xhr.responseType = 'arraybuffer';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: "application/pdf" });

            //const objectElement = document.getElementById('fileViewer');
            //const url = URL.createObjectURL(blob);
            //objectElement.src = url;
            //objectElement.width = '1000px';
            //objectElement.height = '1000px';
            //objectElement.type = 'text/plain';
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = file;
            link.click();
        }
    };
    xhr.send();
}
function loadDocUploadList() {

    var content = parseInt($("#StepOperation").val());
    var StepRoutingId = $("#StepRoutingId").val();
    var partid = $("#StepId").val();
    
        api.getbulk("/Routings/GetOpertaionDocList?opId=" + content + "&routingId=" + StepRoutingId + "&stepId=" + partid).then((data) => {
            //data = data.filter(item => item.status == 1 || item.status == 0);
            var tablebody = $("#RoutingDocgrid tbody");
            $(tablebody).html("");//empty tbody
            //console.log(data);
            for (i = 0; i < data.length; i++) {
                var rowHtml = AppUtil.ProcessTemplateData("maufDocUploadRow", data[i]);

                // Check if mandatory is 'Y', if so, hide the Delete option
                if (data[i].docListId === 0) {
                    // Simplified regex to match the Upload link
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Edit *<\/a>/i, '');
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
                }
                if (data[i].docListId != 0) {
                    // Remove the Edit link from the generated row
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*> *Upload *<\/a>/i, '');
                }

                if (data[i].mandatory === 'Y') {
                    // Remove the Delete link from the generated row
                    rowHtml = rowHtml.replace(/<a href="javascript:void\(0\);" class="dropdown-item"[^>]*data-doclistid="[^"]*"[^>]*onclick="DeleteDocList\(this\)"[^>]*>Delete<\/a>/, '');
                }

                // Append the processed row to the table body
                $(tablebody).append(rowHtml);
            }
        }).catch((error) => {
            console.log(error);
        });

}