var ppLlist = new Array();
let editStatus = 0;
let masterPartNo = "";
let ppdOp = "";
let masterPartType = "";

$(function () {
    $("#PSupplierId").select2();
    loadSuppliers("PSupplierId");
    //ppLlist.
    var tablebody = $("#TablePurchaseDetails tbody");
    tablebody.html("");
    
    $("#PSupplierId").change(function () {
        //alert("setting val");
        $("#PSupplier").val($("#PSupplierId option:selected").text());
    });

    $("#EdPPDSupplierId").change(function () {
        //alert("setting val");
        $("#EdPPDSupplier").val($("#EdPPDSupplierId option:selected").text());
    });

    $("#AddSupplier").click(function (event) {
        ////debugger;
        AddPurchaseDetail(event);
    });
    $("#EditSupplier").click(function (event) {
        ////debugger;
        EditPurchaseDetail(event);
    });

    $("#DelSupplier").click(function (event) {
        ////debugger;
       DeletePurchaseDetail(event);
    });
    $("#CancelDelSupplier").click(function (event) {
        ////debugger;
        document.getElementById("btnDeletePDClose").click();
    });

    ppLlist = new Array();
    
    
    //dialog-
    $('#dialog-DeletePurchaseDetail').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var partPurchaseId = relatedTarget.data("partpurchaseid");
     //   alert("Show...delPurchaseDetail " + partPurchaseId);
        //GetPartPurchase
        let key = "";
        let dData = {};
        api.get("/masters/getpartpurchase?partPurchaseId=" + partPurchaseId).then((dData) => {
            /*for (var key in dData) {
                console.log(key);
                console.log(dData[key]);
            }
            console.log("++++++++++++++++");*/
            key = "pSupplierId";
            $("#DelPPDSupplierId").val(dData[key]);
            key = "pSupplier";
            $("#DelPSupplier").val(dData[key]);
            //console.log("Point 1");

            key = "partPurchaseId";
            $("#DelPartPurchaseId").val(dData[key]);
            /*key = "masterPartNo";
            $("#DelMasterPartNo").val(dData[key]);
            masterPartNo = dData[key];*/
            //console.log("Point 2");
            key = "pPartId";
            $("#DelMasterPartId").val(dData[key]);

            key = "shareOfBusiness";
            $("#DelShareOfBusiness").val(dData[key]);
            //console.log("Point 3");
            key = "price";
            $("#DelPrice").val(dData[key]);

            key = "minimumOrderQuantity";
            $("#DelMinimumOrderQuantity").val(dData[key]);
            key = "pAdditionalInfo";
            $("#DelAdditionalInfo").val(dData[key]);
            //console.log("Point 4");

            key = "leadTimeInDays";
            $("#DelLeadTimeInDays").val(dData[key]);
            key = "pSupplierPartNo";
            $("#DelSupplierPartNo").val(dData[key]);
            //console.log("Point 5");

            key = "rmId";
            $("#DelRMId").val(eData[key]);
            key = "bofId";
            $("#DelBOFId").val(eData[key]);


        }).catch((error) => {
        });
    });
    /**
     * pPartId:82
purchasedetails.js:222 pSupplierId:128
purchasedetails.js:222 pSupplierPartNo:sdf
purchasedetails.js:222 leadTimeInDays:2
purchasedetails.js:222 minimumOrderQuantity:2
purchasedetails.js:222 price:2
purchasedetails.js:222 shareOfBusiness:1
purchasedetails.js:222 pAdditionalInfo:null
purchasedetails.js:222 bofId:14
purchasedetails.js:222 rmId:-1
purchasedetails.js:222 partPurchaseId:18
purchasedetails.js:222 pMasterPartType:0
purchasedetails.js:222 pSupplier:null
purchasedetails.js:222 tenantId:1
<input type="text" hidden id="EdPPDSupplier" value="" asp-for="PSupplier" />
                                <input type="text" hidden id="EdPartId" value="0" asp-for="PPartId" />
                                <input type="text" hidden id="EdPMasterPartType" value="0" asp-for="PMasterPartType" />
                                <input type="text" hidden id="EdPartPurchaseId" value="0" asp-for="PartPurchaseId" />
                                <input type="text" hidden id="EdRMId" value="-1" name="RMId" />
                                <input type="text" hidden id="EdBOFId" value="-1" name="BOFId" />
     * 
     */
    $('#dialog-EditPurchaseDetail').on('show.bs.modal', function (event) {
        $("#EdPPDSupplierId").select2();
        loadSuppliersFromMem("EdPPDSupplierId");
        var relatedTarget = $(event.relatedTarget);
        var partPurchaseId = relatedTarget.data("partpurchaseid");
        //alert("Show...EditPurchaseDetail " + partPurchaseId);
        //GetPartPurchase
        let key = "";
        let eData = {};
        api.get("/masters/getpartpurchase?partPurchaseId=" + partPurchaseId).then((eData) => {
            /*for (var key in data) {
                console.log(key);
                console.log(data[key]);
            }
            console.log("++++++++++++++++");*/
            key = "pSupplierId";
            $("#EdPPDSupplierId").val(eData[key]);
            $("#EdPPDSupplierId").trigger("change");
            key = "pSupplier";
            $("#EdPPDSupplier").val(eData[key]);

            key = "partPurchaseId";
            $("#EdPartPurchaseId").val(eData[key]);
            /*key = "masterPartNo";
            $("#EdMasterPartNo").val(eData[key]);
            masterPartNo = eData[key];*/
            key = "pPartId";
            $("#EdPartId").val(eData[key]);

            key = "shareOfBusiness";
            $("#EdShareOfBusiness").val(eData[key]);
            key = "price";
            $("#EdPrice").val(eData[key]);

            key = "minimumOrderQuantity";
            $("#EdMinimumOrderQuantity").val(eData[key]);
            key = "pAdditionalInfo";
            $("#EdAdditionalInfo").val(eData[key]);

            key = "leadTimeInDays";
            $("#EdLeadTimeInDays").val(eData[key]);
            key = "pSupplierPartNo";
            $("#EdSupplierPartNo").val(eData[key]);

            key = "rmId";
            $("#EdRMId").val(eData[key]);
            key = "bofId";
            $("#EdBOFId").val(eData[key]);
            

        }).catch((error) => {
        });
    });

    $('#setPreferred').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var partpurchaseid = relatedTarget.data("partpurchaseid");
        var supplier = relatedTarget.data("supplier");
        var partNumber = $("#Span_PartNo").text();
        var lblPartDescription = $("#Span_PartDescription").text();
        $("#SetPreferedPDid").val(partpurchaseid);
        $("#supplierName").text(supplier);
        $("#ppartNameSpan").text(partNumber);
        $("#pPartDescNameSPan").text(lblPartDescription);

    });
    $('#setPreferred').on('hidden.bs.modal', function (event) {
        $("#SetPreferedPDid").val(0);
        $("#SetPreferedChkBox").prop("checked", false);
    });
    $("#btnSetPreferedSave").click(function (event) {
        var mkID = parseInt($("#SetPreferedPDid").val());
        var prefred = 0;
        if ($("#SetPreferedChkBox").prop("checked") == true) {
            prefred = 1;
        }
        var rowData = {
            PartPurchaseId: mkID,
            PreferredSupplier: prefred
        };
        api.post("/masters/PreferredSupplier", rowData).then((data) => {
            var partId = data.ppartId;
            reloadPPDs(partId);
            //reloadMakeFroms(partId);
            $("#setPreferred").modal("hide");
            event.preventDefault();
            //$("#TabHeadMakefrom").click();
            //document.getElementById("btnPreferredInputClose").click();
        }).catch((error) => {
            AppUtil.HandleError("FormEditMakeFrom", error);
        });
    });

});


function modifyPPDListForEdit(newData) {
    ppdOp = "Edit";
    modifyMakeFromList(newData);
}
function modifyPPDListForDelete(newData) {
    ppdOp = "Delete";
    modifyMakeFromList(newData);
}
function modifyPPDList(newData) {
    let foundObj = false;
    let i = 0;
    let newObj = {};
    let idxToDel = -1;
    //console.log("----")
    //console.log(newData);
    //console.log("----")
    for (i = 0; i < ppLlist.length; i++) {
        if (newData['partPurchaseId'] == ppLlist[i]['partPurchaseId']) {
            idxToDel = i;
            foundObj = true;
            break;
        }
    }
    if (foundObj) {
        if (idxToDel > -1) {
            ppLlist.splice(idxToDel, 1);
            //console.log("Spliced one element " + idxToDel)
        }
        if (modOpN == "Edit") {
            newObj = JSON.parse(JSON.stringify(newData));
            newObj['deleted'] = false;
            //console.log("Adding new Obj makefroms..");
            //console.log(newObj);
            ppLlist.push(newObj);
        }
    }
}

function reloadPPDs(partNo) {
    var tablebody = $("#TablePurchaseDetails tbody");
    tablebody.html("");
    let rData = {};
    let i = 0;
    let partId = $('#PartId').val();
    var templateElement = $("#PurchaseDetailTemplate").html();
    let uomtxt = $("#UOMId option:selected").text();
    api.get("/masters/partpurchasesfor?partId=" + partId).then((rData) => {
        var tablebody = $("#TablePurchaseDetails tbody");
        tablebody.html();
        for (i = 0; i < rData.length; i++) {
            rData[i].uom = uomtxt;
            let rowData = rData[i];
            rowData.checked = rowData.preferredSupplier === 1 ? 'checked' : '';
            $(tablebody).append(AppUtil.ProcessTemplateData("PurchaseDetailTemplate", rowData));
            //UpdatePurchaseDetailsTable(rData[i]);
        }
    }).catch((error) => {
    });
    /*for (i = 0; i < ppLlist.length; i++) {
        if (ppLlist[i]['deleted']) {
            continue;
        }
        console.log(ppLlist[i]);
        $(tablebody).append(AppUtil.ProcessTemplateData("PurchaseDetailTemplate", ppLlist[i]));
    }*/
}

function UpdatePurchaseDetailsTable(dataObj) {
    var tablebody = $("#TablePurchaseDetails tbody");
    var templateElement = $("#PurchaseDetailTemplate").html();
    for (var key in dataObj) {
        //console.log(key + ":" + dataObj[key]);
        templateElement = templateElement.replaceAll("{" + key + "}", dataObj[key]);
    }
    //console.log(templateElement);
    $(tablebody).append(templateElement);
}

function DeletePurchaseDetail(event) {
    var formName = "FormDelPurchaseDetails";
    let editData = {};
    if ($("#RawMaterialDetailId").length)
        $("#DelRMId").val($("#RawMaterialDetailId").val());
    if ($("#BoughtOutFinishDetailId").length)
        $("#DelBOFId").val($("#BoughtOutFinishDetailId").val());
    $("#DelMasterPartType").val(masterPartType);
    $("#DelPartId").val($("#PartId").val());

    if ($("#FormDelPurchaseDetails").valid()) {
        var formData = AppUtil.GetFormData(formName);
        api.post("/masters/rempartpurchase", formData).then((editData) => {
            editStatus = 1;
            //alert("edited..." + editStatus);
           // modifyPPDListForDelete(editData);
            reloadPPDs(masterPartNo);
            document.getElementById("btnDeletePDClose").click();
            //ppLlist.push(editData);
            // $('select#SupplierId').val(1).select2();
        }).catch((error) => {
            AppUtil.HandleError(formName, error);
        });
    } else {
        alert("Invalid form...")
    }
    event.preventDefault();
}

function EditPurchaseDetail(event) {
    var formName = "FormEditPurchaseDetails";
    let editData = {};
    if ($("#RawMaterialDetailId").length)
        $("#EdRMId").val($("#RawMaterialDetailId").val());
    if ($("#BoughtOutFinishDetailId").length)
        $("#EdBOFId").val($("#BoughtOutFinishDetailId").val());
    $("#EdMasterPartType").val(masterPartType);
    $("#EdPartId").val($("#PartId").val());

    if ($("#FormEditPurchaseDetails").valid()) {
        var formData = AppUtil.GetFormData(formName);
        api.post("/masters/partpurchase", formData).then((editData) => {
            editStatus = 1;
            //alert("edited..." + editStatus);
            //modifyPPDListForEdit(editData);
            reloadPPDs(masterPartNo);
            document.getElementById("btnEditPDClose").click();
            //ppLlist.push(editData);
            // $('select#SupplierId').val(1).select2();
        }).catch((error) => {
            AppUtil.HandleError(formName, error);
        });
    } else {
        alert("Invalid form...")
    }
    event.preventDefault();
}
function AddPurchaseDetail(event) {
    var formName = "FormPurchaseDetails";
    if ($("#RawMaterialDetailId").length)
        $("#RMId").val($("#RawMaterialDetailId").val());
    if ($("#BoughtOutFinishDetailId").length)
        $("#BOFId").val($("#BoughtOutFinishDetailId").val());
    $("#PMasterPartType").val(masterPartType);
    $("#PPartId").val($("#PartId").val());
    if ($("#PSupplierId").val() == 0) {
        var newNamevalidate = document.getElementById('PSupplierId');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('PSupplierId');
        newNamevalidate.style.border = '';
    }
    var type = parseInt($("#BoughtOutFinishMadeType").val());
    if (type == 3) {
    } else {
        if ($("#PSupplierPartNo").val().length == 0) {
            var newNamevalidate = document.getElementById('PSupplierPartNo');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('PSupplierPartNo');
            newNamevalidate.style.border = '';
        }
    }
    if ($("#MinimumOrderQuantity").val().length == 0) {
        var newNamevalidate = document.getElementById('MinimumOrderQuantity');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('MinimumOrderQuantity');
        newNamevalidate.style.border = '';
    } 
    if ($("#LeadTimeInDays").val().length == 0) {
        var newNamevalidate = document.getElementById('LeadTimeInDays');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('LeadTimeInDays');
        newNamevalidate.style.border = '';
    } 
    if ($("#Price").val().length == 0) {
        var newNamevalidate = document.getElementById('Price');
        newNamevalidate.style.border = '2px solid red';
        return false;
    } else {
        var newNamevalidate = document.getElementById('Price');
        newNamevalidate.style.border = '';
    }
    var pref = 0;
    if ($("#pPreferredSupplier").prop("checked")) {
        pref = 1;
        $("#pPreferredSupplier").val(pref);
    } else {
        pref = 0;
        $("#pPreferredSupplier").val(pref);
    }
    if ($("#FormPurchaseDetails").valid()) {
        var prefred = 0;
        if ($("#pPreferredSupplier").prop("checked") == true) {
            prefred = 1;
        }
        $("#fPreferredSupplier").val(prefred);
        var formData = AppUtil.GetFormData(formName);
        //formData.append("PreferredSupplier", parseInt(pref));
        let data = {};
        api.post("/masters/partpurchase", formData).then((data) => {
            UpdatePurchaseDetailsTable(data);
            ppLlist.push(data);
          //  AppUtil.ProcessTemplateDataNew();
            let MasterPartId = $("#PPartId").val();
            let MasterPartNo = $("#PPartNo").val();
            document.getElementById(formName).reset();
            $("#PPartId").val(MasterPartId);
            $("#PPartNo").val(MasterPartNo);
            $("#PSupplierId").val("").trigger('change');
        }).catch((error) => {
            AppUtil.HandleError(formName, error);
        });
    } else {
        alert("Invalid form...")
    }
    event.preventDefault();
}

